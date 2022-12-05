namespace Cusco.DependencyInjection;

internal readonly record struct ServiceKey(Type serviceType, string name = null);
