using JetBrains.Annotations;

namespace Cusco;

public sealed class Box<T> where T: struct
{
    private readonly T value;

    public ref readonly T asRef => ref value;

    public static Box<T> nil => null;

    public Box(T value) => this.value = value;
}