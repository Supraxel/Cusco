using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Cusco.LowLevel;

public static class StringExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToManagedASCIIString(this UnsafePointer<byte> self)
  {
    return Marshal.PtrToStringAnsi(self.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToManagedASCIIString(this UnsafeMutablePointer<byte> self)
  {
    return Marshal.PtrToStringAnsi(self.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToManagedUnicodeString(this UnsafePointer<byte> self)
  {
    return Marshal.PtrToStringUni(self.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToManagedUnicodeString(this UnsafeMutablePointer<byte> self)
  {
    return Marshal.PtrToStringUni(self.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutablePointer<byte> ToNativeASCIIString(this string self)
  {
    return new(Marshal.StringToHGlobalAnsi(self));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutablePointer<byte> ToNativeUnicodeString(this string self)
  {
    return new(Marshal.StringToHGlobalUni(self));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutablePointer<byte> ToNativeUTF8String(this string self)
  {
    return ToNativeString(self, Encoding.UTF8);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UnsafeMutablePointer<byte> ToNativeString(this string self, Encoding encoding)
  {
    if (Equals(encoding, Encoding.ASCII))
      return ToNativeASCIIString(self);

#if NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
            unsafe
            {
                char* pChars = (char*)UnsafeIL.AsPointerReadonly(in self.GetPinnableReference());
                int byteCount = encoding.GetByteCount(self);
                var ptr = Allocator.system.Allocate<byte>(byteCount);
                encoding.GetBytes(pChars, self.Length, (byte*) ptr.address, byteCount);
                return ptr;
            }
#else
    var bytes = encoding.GetBytes(self);
    var ptr = Allocator.system.Allocate<byte>(bytes.Length);
    Marshal.Copy(bytes, 0, ptr.addrIfNotNull, bytes.Length);
    return ptr;
#endif
  }
}
