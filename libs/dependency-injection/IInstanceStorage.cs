using System.Diagnostics.CodeAnalysis;

namespace Cusco.DependencyInjection;

internal interface IInstanceStorage
{
    object instance { get; set; }

    void GraphResolutionCompleted() { }

    object GetInstance([DisallowNull] GraphIdentifier graphId) => instance;
    void SetInstance(object instance, [DisallowNull] GraphIdentifier graphId) => this.instance = instance;
}

internal sealed class CompositeStorage : IInstanceStorage
{
    private readonly IInstanceStorage[] components;

    public object instance
    {
        get => components.Select(c => c.instance).First(i => i != null);
        set
        {
            foreach (var component in components)
            {
                component.instance = value;
            }
        }
    }

    public CompositeStorage(params IInstanceStorage[] components)
    {
        this.components = components;
    }

    public void GraphResolutionCompleted()
    {
        foreach (var component in components)
        {
            component.GraphResolutionCompleted();
        }
    }

    public object GetInstance(GraphIdentifier graphId)
    {
        foreach (var component in components)
        {
            var instance = component.GetInstance(graphId);
            if (instance != null)
                return instance;
        }

        return null;
    }

    public void SetInstance(object instance, GraphIdentifier graphId)
    {
        foreach (var component in components)
        {
            component.SetInstance(instance, graphId);
        }
    }
}

internal sealed class GraphStorage : IInstanceStorage
{
    public object instance { get; set; }
    private IDictionary<GraphIdentifier, WeakReference<object>> instances = new Dictionary<GraphIdentifier, WeakReference<object>>();

    public void GraphResolutionCompleted()
    {
        instance = null;
    }

    public object GetInstance(GraphIdentifier graphId)
    {
        if (false == instances.ContainsKey(graphId))
            return null;
        return instances[graphId].TryGetTarget(out var instance) ? instance : null;
    }

    public void SetInstance(object instance, GraphIdentifier graphId)
    {
        if (null == graphId) throw new ArgumentNullException(nameof(graphId));
        this.instance = instance;

        if (!instances.TryGetValue(graphId, out var weakInstance))
            instances[graphId] = new WeakReference<object>(instance);
        else
            weakInstance.SetTarget(instance);
    }
}

internal sealed class PermanentStorage : IInstanceStorage
{
    public object instance { get; set; }
}

internal sealed class TransientStorage : IInstanceStorage
{
    public object instance
    {
        get => null;
#pragma warning disable S108
        set { }
#pragma warning restore S108
    }
}

internal sealed class WeakStorage : IInstanceStorage
{
    private readonly WeakReference<object> weakInstance = new(null);

    public object instance
    {
        get => weakInstance.TryGetTarget(out var value) ? value : null;
        set => weakInstance.SetTarget(value);
    }
}
