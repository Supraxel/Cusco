namespace Cusco.DependencyInjection;

public enum ObjectScope
{
    Transient,
    Graph,
    Container,
    Weak,
}

internal static class ObjectScopeExtensions
{
    public static IInstanceStorage MakeStorage(this ObjectScope scope) => scope switch
    {
        ObjectScope.Container => new PermanentStorage(),
        ObjectScope.Graph => new GraphStorage(),
        ObjectScope.Transient => new TransientStorage(),
        ObjectScope.Weak => new WeakStorage(),
        _ => CuscoRT.Panic<IInstanceStorage>("Unable to make storage for given ObjectScope"),
    };
}
