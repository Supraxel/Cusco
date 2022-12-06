using Cusco.LowLevel;

namespace Cusco.DependencyInjection;

public sealed class Container : IResolver
{
    private const int maxResolutionDepth = 200;

    private GraphIdentifier currentGraphId;
    private readonly ObjectScope defaultScope;
    private readonly Mutex mutex;
    private readonly Container parentContainer;
    private int resolutionDepth;
    private readonly IDictionary<ServiceKey, IServiceEntry> services;
    private readonly bool synchronized;

    internal Container(Container parentContainer, ObjectScope defaultScope, bool synchronized)
    {
        this.parentContainer = parentContainer;
        this.mutex = parentContainer?.mutex ?? new();
        this.defaultScope = defaultScope;
        this.services = new Dictionary<ServiceKey, IServiceEntry>();
        this.synchronized = synchronized;
    }

    public Container(Container parentContainer = null, ObjectScope defaultScope = ObjectScope.Graph) : this(parentContainer, defaultScope, false) { }

    public void Clear() => services.Clear();

    public Container Register<T>(string name = null, ObjectScope? inScope = null) where T : class, new()
    {
        services[new ServiceKey(typeof(T), name)] = new ServiceEntry<T>(() => new T(), inScope ?? defaultScope);
        return this;
    }

    public Container Register<TService, TConcrete>(string name = null, ObjectScope? inScope = null)
        where TService : class
        where TConcrete : class, TService, new()
    {
        services[new ServiceKey(typeof(TService), name)] = new ServiceEntry<TService>(() => new TConcrete(), inScope ?? defaultScope);
        return this;
    }

    public Container Register<TService>(Func<TService> factory, string name = null, ObjectScope? inScope = null) where TService : class
    {
        services[new ServiceKey(typeof(TService), name)] = new ServiceEntry<TService>(factory, inScope ?? defaultScope);
        return this;
    }

    public Container Register<TService>(Func<IResolver, TService> factory, string name = null, ObjectScope? inScope = null) where TService : class
    {
        services[new ServiceKey(typeof(TService), name)] = new ServiceEntry<TService>(() => factory(this), inScope ?? defaultScope);
        return this;
    }

    public T Resolve<T>(string name = null) where T : class
    {
        T resolvedInstance = null;

        if (TryGetEntry<T>(name, out var entry))
            resolvedInstance = SynchronizeIfNeeded(ResolveImpl0, entry);

        return resolvedInstance;
    }

    public Lazy<T> ResolveLazy<T>(string name = null) where T : class
    {
        if (!TryGetEntry<T>(name, out var entry)) return null;

        var graphId = currentGraphId;
        var weakThis = new WeakReference<Container>(this);
        return new Lazy<T>(() => weakThis.TryGetTarget(out var strongThis) ? strongThis.SynchronizeIfNeeded(ResolveDeplayedImpl0, weakThis, entry, graphId) : null);
    }

    public Provider<T> ResolveProvider<T>(string name = null) where T : class
    {
        if (!TryGetEntry<T>(name, out var entry)) return null;

        var weakThis = new WeakReference<Container>(this);
        return new Provider<T>(() => weakThis.TryGetTarget(out var strongThis) ? strongThis.SynchronizeIfNeeded(ResolveDeplayedImpl0, weakThis, entry, (GraphIdentifier)null) : null);
    }

    private void DecrementResolutionDepth()
    {
        parentContainer?.DecrementResolutionDepth();
        CuscoRT.Assert(resolutionDepth > 0, "The depth cannot be negative.");

        --resolutionDepth;
        if (resolutionDepth == 0)
            GraphResolutionCompleted();
    }

    private void GraphResolutionCompleted()
    {
        foreach (var (_, service) in services)
        {
            service.storage.GraphResolutionCompleted();
        }
        currentGraphId = null;
    }

    private void IncrementResolutionDepth()
    {
        parentContainer?.IncrementResolutionDepth();
        if (0 == resolutionDepth && null == currentGraphId)
        {
            currentGraphId = new GraphIdentifier();
        }

        CuscoRT.Precondition(resolutionDepth < maxResolutionDepth);
        ++resolutionDepth;
    }

    private T PersistedInstance<T>(ServiceEntry<T> entry, GraphIdentifier graphId) where T : class => entry.storage.GetInstance(graphId) as T;

    private static T ResolveDeplayedImpl0<T>(WeakReference<Container> weakThis, ServiceEntry<T> entry, GraphIdentifier graphId) where T : class
    {
        if (false == weakThis.TryGetTarget(out var strongThis))
            return null;
        if (null != graphId) strongThis.currentGraphId = graphId;
        return strongThis.ResolveImpl0(entry);
    }

    // https://github.com/Swinject/Swinject/blob/d026c7d140cdd26cd24a0ac30606fad34d2fd1d1/Sources/Container.swift#L316
    private T ResolveImpl0<T>(ServiceEntry<T> entry) where T : class
    {
        try
        {
            IncrementResolutionDepth();

            if (null == currentGraphId)
                return CuscoRT.Panic<T>("If accessing container from multiple threads, make sure to use a synchronized resolver.");

            var persistedInstance = PersistedInstance(entry, currentGraphId);
            if (null != persistedInstance)
                return persistedInstance;

            var resolvedInstance = entry.factory();
            persistedInstance = PersistedInstance(entry, currentGraphId);
            if (null != persistedInstance)
                // An instance for the key might be added by the factory invocation.
                return persistedInstance;
            entry.storage.SetInstance(resolvedInstance, currentGraphId);

            return resolvedInstance;
        }
        finally
        {
            DecrementResolutionDepth();
        }
    }

    private TResult SynchronizeIfNeeded<TArg1, TResult>(Func<TArg1, TResult> method, TArg1 arg1)
    {
        if (synchronized)
        {
            using var l = new MutexLock(this.mutex);
            return method(arg1);
        }
        else
        {
            return method(arg1);
        }
    }

    private TResult SynchronizeIfNeeded<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, TResult> method, TArg1 arg1, TArg2 arg2, TArg3 arg3)
    {
        if (synchronized)
        {
            using var l = new MutexLock(mutex);
            return method(arg1, arg2, arg3);
        }
        else
        {
            return method(arg1, arg2, arg3);
        }
    }

    private bool TryGetEntry<T>(string name, out ServiceEntry<T> entry) where T : class
    {
        if (services.TryGetValue(new ServiceKey(typeof(T), name), out var typeErasedEntry))
        {
            entry = (ServiceEntry<T>)typeErasedEntry;
            return true;
        }

        if (null != parentContainer)
            return parentContainer.TryGetEntry(name, out entry);

        entry = null;
        return false;
    }
}

public static class ContainerExtensions
{
    public static Container RegisterInstance<T>(this Container self, T instance, string name = null)
        where T : class
        => self.Register(() => instance, name, ObjectScope.Transient);

    public static Container RegisterWeakInstance<T>(this Container self, T instance, string name = null)
        where T : class
    {
        var weakInstance = new WeakReference<T>(instance);
        return self.Register(() => weakInstance.TryGetTarget(out var strongInstance) ? strongInstance : null, name, ObjectScope.Transient);
    }
}
