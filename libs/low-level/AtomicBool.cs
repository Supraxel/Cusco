using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cusco.LowLevel;

[DebuggerDisplay("{value == 0 ? \"false\" : \"true\"}")]
public sealed class AtomicBool
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile int value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public AtomicBool(bool value = default)
  {
    this.value = value ? 1 : 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool CompareExchange(bool comparand, bool newValue)
  {
    return Interlocked.CompareExchange(ref value, newValue ? 1 : 0, comparand ? 1 : 0) != 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Exchange(bool newValue)
  {
    return Interlocked.Exchange(ref value, newValue ? 1 : 0) != 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator bool(AtomicBool atomic)
  {
    return atomic.value != 0;
  }
}
