using System.Runtime.InteropServices;

namespace Cusco.LowLevel;

public interface IAllocator
{
  UnsafeMutablePointer<T> Allocate<T>(int count = 1) where T : unmanaged;
  UnsafeMutableView<T> AllocateContiguous<T>(int count) where T : unmanaged;
  UnsafeMutableRawPointer<T> AllocateRaw<T>(int bytes);

  UnsafePointer<T> Deallocate<T>(UnsafePointer<T> ptr) where T : unmanaged;
  UnsafeMutablePointer<T> Deallocate<T>(UnsafeMutablePointer<T> ptr) where T : unmanaged;
  UnsafeRawPointer<T> Deallocate<T>(UnsafeRawPointer<T> ptr);
  UnsafeMutableRawPointer<T> Deallocate<T>(UnsafeMutableRawPointer<T> ptr);

  UnsafeMutablePointer<T> Reallocate<T>(UnsafeMutablePointer<T> pointer, int count = 1) where T : unmanaged;
  UnsafeMutableRawPointer<T> ReallocateRaw<T>(UnsafeMutableRawPointer<T> pointer, int bytes);
}

public static class Allocator
{
  public static readonly IAllocator system = new SystemAllocator();

  private sealed class SystemAllocator : IAllocator
  {
    public UnsafeMutablePointer<T> Allocate<T>(int count = 1) where T : unmanaged
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Memory allocation size must be greater than zero");
      return new UnsafeMutablePointer<T>(Marshal.AllocHGlobal(UnsafeMutablePointer<T>.stride * count));
    }

    public UnsafeMutableView<T> AllocateContiguous<T>(int count) where T : unmanaged
    {
      return new(Allocate<T>(count), count);
    }

    public UnsafeMutableRawPointer<T> AllocateRaw<T>(int bytes)
    {
      if (bytes <= 0) throw new ArgumentOutOfRangeException(nameof(bytes), "Memory allocation size must be greater than zero");
      return new UnsafeMutableRawPointer<T>(Marshal.AllocHGlobal(bytes));
    }

    public UnsafePointer<T> Deallocate<T>(UnsafePointer<T> ptr) where T : unmanaged
    {
      Marshal.FreeHGlobal(ptr.address);
      return default;
    }

    public UnsafeMutablePointer<T> Deallocate<T>(UnsafeMutablePointer<T> ptr) where T : unmanaged
    {
      Marshal.FreeHGlobal(ptr.address);
      return default;
    }

    public UnsafeRawPointer<T> Deallocate<T>(UnsafeRawPointer<T> ptr)
    {
      Marshal.FreeHGlobal(ptr.address);
      return default;
    }

    public UnsafeMutableRawPointer<T> Deallocate<T>(UnsafeMutableRawPointer<T> ptr)
    {
      Marshal.FreeHGlobal(ptr.address);
      return default;
    }

    public UnsafeMutablePointer<T> Reallocate<T>(UnsafeMutablePointer<T> pointer, int count = 1) where T : unmanaged
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Memory allocation size must be greater than zero");
      return new UnsafeMutablePointer<T>(Marshal.ReAllocHGlobal(pointer.address, (IntPtr)(UnsafeMutablePointer<T>.stride * count)));
    }

    public UnsafeMutableRawPointer<T> ReallocateRaw<T>(UnsafeMutableRawPointer<T> pointer, int bytes)
    {
      if (bytes <= 0) throw new ArgumentOutOfRangeException(nameof(bytes), "Memory allocation size must be greater than zero");
      return new UnsafeMutableRawPointer<T>(Marshal.ReAllocHGlobal(pointer.address, (IntPtr)bytes));
    }
  }
}

public static class AllocatorExtensions
{
  public static void Deallocate<T>(this IAllocator allocator, ref UnsafePointer<T> ptr) where T : unmanaged
  {
    ptr = allocator.Deallocate(ptr);
  }

  public static void Deallocate<T>(this IAllocator allocator, ref UnsafeMutablePointer<T> ptr) where T : unmanaged
  {
    ptr = allocator.Deallocate(ptr);
  }

  public static void Deallocate<T>(this IAllocator allocator, ref UnsafeRawPointer<T> ptr)
  {
    ptr = allocator.Deallocate(ptr);
  }

  public static void Deallocate<T>(this IAllocator allocator, ref UnsafeMutableRawPointer<T> ptr)
  {
    ptr = allocator.Deallocate(ptr);
  }
}
