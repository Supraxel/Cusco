namespace Cusco.DependencyInjection;

public sealed class ServiceNotFoundException : Exception
{
    public ServiceNotFoundException(Type serviceType, string name = null)
        : base(name == null ? $"Unable to resolve instance of service {serviceType.FullName}" : $"Unable to resolve instance of service {serviceType.FullName} with name '{name}'")
    {}
}
