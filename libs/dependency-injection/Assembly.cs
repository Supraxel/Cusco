using System.Diagnostics.CodeAnalysis;

namespace Cusco.DependencyInjection;

public interface Assembly
{
    void Assemble([NotNull] Container container);
    void Loaded([NotNull] IResolver resolver) { }
}
