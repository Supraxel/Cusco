namespace Cusco.DependencyInjection;

internal interface IServiceEntry
{
    IInstanceStorage storage { get; }
}

internal sealed class ServiceEntry<T> : IServiceEntry
{
    internal readonly Func<T> factory;
    internal readonly ObjectScope scope;
    private readonly Lazy<IInstanceStorage> _storage;
    public IInstanceStorage storage => _storage.Value;

    internal ServiceEntry(Func<T> factory, ObjectScope scope)
    {
        this.factory = factory;
        this.scope = scope;
        this._storage = new(() => scope.MakeStorage());
    }
}
