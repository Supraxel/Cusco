namespace Cusco.DependencyInjection;

public sealed class Provider<T>
{
    private readonly Func<T> factory;

    internal Provider(Func<T> factory)
    {
        this.factory = factory;
    }

    public T Value => factory();
}
