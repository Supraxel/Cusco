using System.Runtime.CompilerServices;

namespace Cusco.DependencyInjection;

public interface IResolver
{
    T Resolve<T>(string name = null) where T : class;
    Lazy<T> ResolveLazy<T>(string name = null) where T : class;
    Provider<T> ResolveProvider<T>(string name = null) where T : class;
}

public static class ResolverExtensions
{
    public static T ResolveOrThrow<T>(this IResolver self, string name = null) where T : class => self.Resolve<T>() ?? throw new ServiceNotFoundException(typeof(T), name);
    public static Lazy<T> ResolveLazyOrThrow<T>(this IResolver self, string name = null) where T : class => self.ResolveLazy<T>() ?? throw new ServiceNotFoundException(typeof(T), name);
    public static Provider<T> ResolveProviderOrThrow<T>(this IResolver self, string name = null) where T : class => self.ResolveProvider<T>() ?? throw new ServiceNotFoundException(typeof(T), name);

    public static IResolver Resolve<T>(this IResolver self, out T service, string name = null) where T : class
    {
        service = self.Resolve<T>(name);
        return self;
    }

    public static IResolver Resolve<T>(this IResolver self, out Lazy<T> service, string name = null) where T : class
    {
        service = self.ResolveLazy<T>(name);
        return self;
    }

    public static IResolver Resolve<T>(this IResolver self, out Provider<T> service, string name = null) where T : class
    {
        service = self.ResolveProvider<T>(name);
        return self;
    }

    public static IResolver ResolveOrThrow<T>(this IResolver self, out T service, string name = null) where T : class
    {
        service = self.ResolveOrThrow<T>(name);
        return self;
    }

    public static IResolver ResolveOrThrow<T>(this IResolver self, out Lazy<T> service, string name = null) where T : class
    {
        service = self.ResolveLazyOrThrow<T>(name);
        return self;
    }

    public static IResolver ResolveOrThrow<T>(this IResolver self, out Provider<T> service, string name = null) where T : class
    {
        service = self.ResolveProviderOrThrow<T>(name);
        return self;
    }
}
