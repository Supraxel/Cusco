using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cusco.LowLevel;

public readonly struct UnsafeView<T> : IEnumerable<T> where T : unmanaged
{
  public static readonly int stride = Marshal.SizeOf<T>();

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly UnsafePointer<T> ptr;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly int count;

  public T this[int index] => ptr[index < count ? index : throw new ArgumentOutOfRangeException(nameof(index), index, "Out of view range")];

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeView(UnsafePointer<T> ptr, int count)
  {
    this.ptr = ptr;
    this.count = count;
  }

  public IEnumerator<T> GetEnumerator()
  {
    for (var i = 0; i < count; ++i)
      yield return ptr[i];
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  public override string ToString()
  {
    return $"UnsafeView<{typeof(T).Name}>(0x{ptr}, count: {count})";
  }
}

public readonly struct UnsafeMutableView<T> : IEnumerable<T> where T : unmanaged
{
  public static readonly int stride = Marshal.SizeOf<T>();

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly UnsafeMutablePointer<T> ptr;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly int count;

  public ref T this[int index] => ref ptr[index < count ? index : throw new ArgumentOutOfRangeException(nameof(index), index, "Out of view range")];

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutableView(UnsafeMutablePointer<T> ptr, int count)
  {
    this.ptr = ptr;
    this.count = count;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Assign(T value)
  {
    ptr.Assign(value, count);
  }

  public IEnumerator<T> GetEnumerator()
  {
    for (var i = 0; i < count; ++i)
      yield return ptr[i];
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  public override string ToString()
  {
    return $"UnsafeMutableView<{typeof(T).Name}>(0x{ptr}, count: {count})";
  }
}
