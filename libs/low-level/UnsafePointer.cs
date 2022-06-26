using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cusco.LowLevel;

public readonly struct UnsafePointer<T> : IEquatable<UnsafePointer<T>>, IEquatable<UnsafeMutablePointer<T>> where T : unmanaged
{
  public static readonly int stride = Marshal.SizeOf<T>();

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly IntPtr address;

  [DebuggerHidden] public IntPtr addrIfNotNull => false == isNull ? address : throw new NullReferenceException();
  public bool isNull => address == IntPtr.Zero;
  public unsafe ref readonly T pointee => ref *(T*)addrIfNotNull;

  // ReSharper disable once PossibleNullReferenceException
  public unsafe T this[int index] => ((T*)addrIfNotNull)[index];

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public unsafe UnsafePointer(T* address)
  {
    this.address = (IntPtr)address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafePointer(IntPtr address)
  {
    this.address = address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static unsafe implicit operator UnsafePointer<T>(T* address)
  {
    return new(address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator UnsafePointer<T>(UnsafeMutablePointer<T> ptr)
  {
    return new(ptr.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafePointer<T> AdvancedBy(int instances)
  {
    return new(address + instances * stride);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafePointer<T> Predecessor()
  {
    return AdvancedBy(-1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafePointer<T> Successor()
  {
    return AdvancedBy(1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafePointer<U> UnsafeCast<U>() where U : unmanaged
  {
    return new(address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafePointer<T> operator +(UnsafePointer<T> lhs, int rhs)
  {
    return lhs.AdvancedBy(rhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafePointer<T> operator +(int lhs, UnsafePointer<T> rhs)
  {
    return rhs.AdvancedBy(lhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafePointer<T> operator -(UnsafePointer<T> lhs, int rhs)
  {
    return lhs.AdvancedBy(-rhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(UnsafePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address == rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(UnsafePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address == rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(UnsafePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address != rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(UnsafePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address != rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(UnsafePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address.ToInt64() < rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(UnsafePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address.ToInt64() < rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(UnsafePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address.ToInt64() > rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(UnsafePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address.ToInt64() > rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(UnsafePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address.ToInt64() <= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(UnsafePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address.ToInt64() <= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(UnsafePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address.ToInt64() >= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(UnsafePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address.ToInt64() >= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(UnsafePointer<T> other)
  {
    return address.Equals(other.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(UnsafeMutablePointer<T> other)
  {
    return address.Equals(other.address);
  }

  public override bool Equals(object obj)
  {
    return (obj is UnsafeMutablePointer<T> mutable && Equals(mutable)) || (obj is UnsafePointer<T> immutable && Equals(immutable));
  }

  public override int GetHashCode()
  {
    return address.GetHashCode();
  }

  public override string ToString()
  {
    return $"UnsafePointer<{typeof(T).Name}>(0x{address:x8})";
  }
}

public struct UnsafeMutablePointer<T> : IEquatable<UnsafePointer<T>>, IEquatable<UnsafeMutablePointer<T>> where T : unmanaged
{
  public static readonly int stride = Marshal.SizeOf<T>();

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly IntPtr address;

  [DebuggerHidden] public IntPtr addrIfNotNull => false == isNull ? address : throw new NullReferenceException();
  public bool isNull => address == IntPtr.Zero;
  public unsafe ref T pointee => ref *(T*)addrIfNotNull;

  // ReSharper disable once PossibleNullReferenceException
  public unsafe ref T this[int index] => ref ((T*)addrIfNotNull)[index];

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutablePointer<T> Mutating(UnsafePointer<T> pointer)
  {
    return new(pointer.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public unsafe UnsafeMutablePointer(T* address)
  {
    this.address = (IntPtr)address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutablePointer(IntPtr address)
  {
    this.address = address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static unsafe implicit operator UnsafeMutablePointer<T>(T* address)
  {
    return new(address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutablePointer<T> AdvancedBy(int instances)
  {
    return new(address + instances * stride);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public readonly unsafe void Assign(T value, int count)
  {
    var ptr = (T*)(address != IntPtr.Zero ? address : throw new NullReferenceException());
    for (var i = 0; i < count; ++i)
      // Null reference should never happen, except if the pointer cycle from IntPtr.MaxValue to zero
      // ReSharper disable once PossibleNullReferenceException
      ptr[i] = value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Assign(UnsafePointer<T> value, int count)
  {
    Assign(value.pointee, count);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutablePointer<T> Predecessor()
  {
    return AdvancedBy(-1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutablePointer<T> Successor()
  {
    return AdvancedBy(1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Swap(UnsafeMutablePointer<T> other)
  {
    (pointee, other.pointee) = (other.pointee, pointee);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutablePointer<U> UnsafeCast<U>() where U : unmanaged
  {
    return new(address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutablePointer<T> operator +(UnsafeMutablePointer<T> lhs, int rhs)
  {
    return lhs.AdvancedBy(rhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutablePointer<T> operator +(int lhs, UnsafeMutablePointer<T> rhs)
  {
    return rhs.AdvancedBy(lhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutablePointer<T> operator -(UnsafeMutablePointer<T> lhs, int rhs)
  {
    return lhs.AdvancedBy(-rhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(UnsafeMutablePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address == rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(UnsafeMutablePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address == rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(UnsafeMutablePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address != rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(UnsafeMutablePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address != rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(UnsafeMutablePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address.ToInt64() < rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(UnsafeMutablePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address.ToInt64() < rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(UnsafeMutablePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address.ToInt64() > rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(UnsafeMutablePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address.ToInt64() > rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(UnsafeMutablePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address.ToInt64() <= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(UnsafeMutablePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address.ToInt64() <= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(UnsafeMutablePointer<T> lhs, UnsafeMutablePointer<T> rhs)
  {
    return lhs.address.ToInt64() >= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(UnsafeMutablePointer<T> lhs, UnsafePointer<T> rhs)
  {
    return lhs.address.ToInt64() >= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(UnsafePointer<T> other)
  {
    return address.Equals(other.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(UnsafeMutablePointer<T> other)
  {
    return address.Equals(other.address);
  }

  public override bool Equals(object obj)
  {
    return (obj is UnsafeMutablePointer<T> mutable && Equals(mutable)) || (obj is UnsafePointer<T> immutable && Equals(immutable));
  }

  public override int GetHashCode()
  {
    return address.GetHashCode();
  }

  public override string ToString()
  {
    return $"UnsafeMutablePointer<{typeof(T).Name}>(0x{address:x8})";
  }
}
