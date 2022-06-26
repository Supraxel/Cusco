using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cusco.LowLevel;

[DebuggerDisplay("{value}")]
public sealed class AtomicInt
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile int value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public AtomicInt(int value = default)
  {
    this.value = value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int Add(int x)
  {
    return Interlocked.Add(ref value, x);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int CompareExchange(int comparand, int newValue)
  {
    return Interlocked.CompareExchange(ref value, newValue, comparand);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int Decrement()
  {
    return Interlocked.Decrement(ref value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int Exchange(int newValue)
  {
    return Interlocked.Exchange(ref value, newValue);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int Increment()
  {
    return Interlocked.Increment(ref value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator int(AtomicInt atomic)
  {
    return atomic.value;
  }
}
