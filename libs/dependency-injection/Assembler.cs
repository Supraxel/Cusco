namespace Cusco.DependencyInjection;

public sealed class Assembler
{
    private readonly Container container;

    public IResolver resolver => container;

    public Assembler(Container container)
        => this.container = container ?? throw new ArgumentNullException(nameof(container));

    public Assembler(Assembler parentAssembler = null, ObjectScope scope = ObjectScope.Graph)
        : this(new Container(parentAssembler?.container, scope)) { }

    public void Apply(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
            assembly.Assemble(container);

        foreach (var assembly in assemblies)
            assembly.Loaded(container);
    }
}
