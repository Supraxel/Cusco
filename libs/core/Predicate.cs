using System.Runtime.CompilerServices;

namespace Cusco;

public static class Predicate
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<bool> Not(Func<bool> predicate)
  {
    if (null == predicate) throw new ArgumentNullException(nameof(predicate));
    return () => !predicate();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, bool> Not<T1>(Func<T1, bool> predicate)
  {
    if (null == predicate) throw new ArgumentNullException(nameof(predicate));
    return (a1) => !predicate(a1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, bool> Not<T1, T2>(Func<T1, T2, bool> predicate)
  {
    if (null == predicate) throw new ArgumentNullException(nameof(predicate));
    return (a1, a2) => !predicate(a1, a2);
  }
}
