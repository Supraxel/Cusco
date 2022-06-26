using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cusco.LowLevel;

public readonly struct UnsafeRawPointer<T> : IEquatable<UnsafeRawPointer<T>>, IEquatable<UnsafeMutableRawPointer<T>>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly IntPtr address;

  [DebuggerHidden] public IntPtr addrIfNotNull => false == isNull ? address : throw new NullReferenceException();
  public bool isNull => address == IntPtr.Zero;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public unsafe UnsafeRawPointer(void* address)
  {
    this.address = (IntPtr)address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeRawPointer(IntPtr address)
  {
    this.address = address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static unsafe implicit operator UnsafeRawPointer<T>(void* address)
  {
    return new(address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator UnsafeRawPointer<T>(UnsafeMutableRawPointer<T> ptr)
  {
    return new(ptr.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeRawPointer<T> AdvancedBy(int bytes)
  {
    return new(address + bytes);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeRawPointer<T> Predecessor()
  {
    return AdvancedBy(-1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeRawPointer<T> Successor()
  {
    return AdvancedBy(1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeRawPointer<U> UnsafeCast<U>() where U : unmanaged
  {
    return new(address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeRawPointer<T> operator +(UnsafeRawPointer<T> lhs, int rhs)
  {
    return lhs.AdvancedBy(rhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeRawPointer<T> operator +(int lhs, UnsafeRawPointer<T> rhs)
  {
    return rhs.AdvancedBy(lhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeRawPointer<T> operator -(UnsafeRawPointer<T> lhs, int rhs)
  {
    return lhs.AdvancedBy(-rhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(UnsafeRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address == rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(UnsafeRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address == rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(UnsafeRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address != rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(UnsafeRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address != rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(UnsafeRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() < rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(UnsafeRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() < rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(UnsafeRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() > rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(UnsafeRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() > rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(UnsafeRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() <= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(UnsafeRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() <= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(UnsafeRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() >= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(UnsafeRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() >= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(UnsafeRawPointer<T> other)
  {
    return address.Equals(other.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(UnsafeMutableRawPointer<T> other)
  {
    return address.Equals(other.address);
  }

  public override bool Equals(object obj)
  {
    return (obj is UnsafeMutableRawPointer<T> mutable && Equals(mutable)) || (obj is UnsafeRawPointer<T> immutable && Equals(immutable));
  }

  public override int GetHashCode()
  {
    return address.GetHashCode();
  }

  public override string ToString()
  {
    return $"UnsafeRawPointer<{typeof(T).Name}>(0x{address:x8})";
  }
}

public readonly struct UnsafeMutableRawPointer<T> : IEquatable<UnsafeRawPointer<T>>, IEquatable<UnsafeMutableRawPointer<T>>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly IntPtr address;

  [DebuggerHidden] public IntPtr addrIfNotNull => false == isNull ? address : throw new NullReferenceException();
  public bool isNull => address == IntPtr.Zero;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutableRawPointer<T> Mutating(UnsafeRawPointer<T> pointer)
  {
    return new(pointer.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public unsafe UnsafeMutableRawPointer(void* address)
  {
    this.address = (IntPtr)address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutableRawPointer(IntPtr address)
  {
    this.address = address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static unsafe implicit operator UnsafeMutableRawPointer<T>(void* address)
  {
    return new(address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutableRawPointer<T> AdvancedBy(int bytes)
  {
    return new(address + bytes);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutableRawPointer<T> Predecessor()
  {
    return AdvancedBy(-1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutableRawPointer<T> Successor()
  {
    return AdvancedBy(1);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafeMutableRawPointer<U> UnsafeCast<U>()
  {
    return new(address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeRawPointer<T> operator +(UnsafeMutableRawPointer<T> lhs, int rhs)
  {
    return lhs.AdvancedBy(rhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeRawPointer<T> operator +(int lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return rhs.AdvancedBy(lhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeRawPointer<T> operator -(UnsafeMutableRawPointer<T> lhs, int rhs)
  {
    return lhs.AdvancedBy(-rhs);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(UnsafeMutableRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address == rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(UnsafeMutableRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address == rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(UnsafeMutableRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address != rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(UnsafeMutableRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address != rhs.address;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(UnsafeMutableRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() < rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(UnsafeMutableRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() < rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(UnsafeMutableRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() > rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(UnsafeMutableRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() > rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(UnsafeMutableRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() <= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(UnsafeMutableRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() <= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(UnsafeMutableRawPointer<T> lhs, UnsafeMutableRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() >= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(UnsafeMutableRawPointer<T> lhs, UnsafeRawPointer<T> rhs)
  {
    return lhs.address.ToInt64() >= rhs.address.ToInt64();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(UnsafeMutableRawPointer<T> other)
  {
    return address.Equals(other.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(UnsafeRawPointer<T> other)
  {
    return address.Equals(other.address);
  }

  public override bool Equals(object obj)
  {
    return (obj is UnsafeMutableRawPointer<T> mutable && Equals(mutable)) || (obj is UnsafeRawPointer<T> immutable && Equals(immutable));
  }

  public override int GetHashCode()
  {
    return address.GetHashCode();
  }

  public override string ToString()
  {
    return $"UnsafeMutableRawPointer<{typeof(T).Name}>(0x{address:x8})";
  }
}
