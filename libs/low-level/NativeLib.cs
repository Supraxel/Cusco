using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cusco.LowLevel;

public sealed class NativeLib : IDisposable
{
  private readonly OnceFlag disposeOnce;
  private readonly Func<UnsafePointer<Handle>, string, UnsafePointer<Symbol>> getSymbolImpl;
  private readonly UnsafePointer<Handle> handle;

  public NativeLib(string path)
  {
    disposeOnce = new OnceFlag();

    switch (Platform.operatingSystem)
    {
      case Platform.OperatingSystem.Windows:
        {
          handle = null == path ? GetModuleHandleW(null) : LoadLibraryW(path);
          if (null != path && handle.isNull)
            throw new FileLoadException("Unable to load native library", path);
          getSymbolImpl = GetProcAddress;
          break;
        }
      default:
        throw new PlatformNotSupportedException();
    }
  }

  public void Dispose()
  {
    disposeOnce.Do(() => FreeLibrary(handle));
  }

  [DllImport("Kernel32.dll")]
  private static extern bool FreeLibrary(UnsafePointer<Handle> hLibModule);

  [DllImport("Kernel32.dll")]
  private static extern UnsafePointer<Handle> GetModuleHandleW([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);

  [DllImport("Kernel32.dll")]
  private static extern UnsafePointer<Symbol> GetProcAddress(UnsafePointer<Handle> hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

  [DllImport("Kernel32.dll")]
  private static extern UnsafePointer<Handle> LoadLibraryW([MarshalAs(UnmanagedType.LPWStr)] string lpLibFileName);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public UnsafePointer<T> GetGlobal<T>(string name) where T : unmanaged
  {
    var symbol = getSymbolImpl(handle, name);
    if (symbol.isNull) throw new ArgumentException($"No symbol found for the given name: {name}", nameof(name));
    return symbol.UnsafeCast<T>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T GetFunc<T>(string name) where T : Delegate
  {
    var symbol = getSymbolImpl(handle, name);
    if (symbol.isNull) throw new ArgumentException($"No symbol found for the given name: {name}", nameof(name));
    return Marshal.GetDelegateForFunctionPointer<T>(symbol.address);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public unsafe void* GetSymbolAddr(string name)
  {
    var symbol = getSymbolImpl(handle, name);
    if (symbol.isNull) throw new ArgumentException($"No symbol found for the given name: {name}", nameof(name));
    return symbol.address.ToPointer();
  }

  private struct Handle
  {
  }

  private struct Symbol
  {
  }
}
