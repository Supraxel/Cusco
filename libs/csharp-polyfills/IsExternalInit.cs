using System.ComponentModel;

#if UNITY || NETSTANDARD2_1

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IsExternalInit { }
}

#endif
