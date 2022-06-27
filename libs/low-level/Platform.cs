namespace Cusco.LowLevel;

public static class Platform
{
  public enum InstructionSet
  {
    IntelX86,
    AMD64,
    ARMv7,
    AArch64,
    PPC,
    PPC64,
    RISCV
  }

  public enum OperatingSystem
  {
    Windows,
    macOS,
    iOS,
    watchOS,
    tvOS,
    Linux,
    Android,

    /// <summary>
    ///   Sony Playstation 4
    /// </summary>
    Orbis,

    /// <summary>
    ///   Sony Playstation 5
    /// </summary>
    Prospero,

    /// <summary>
    ///   Microsoft Xbox One
    /// </summary>
    Durango,

    /// <summary>
    ///   Microsoft Xbox Series (X|S)
    /// </summary>
    Scorpio,

    /// <summary>
    ///   Nintendo Switch
    /// </summary>
    NX
  }

  [Flags]
  public enum Runtime
  {
    CoreRT = 0,
    Mono = 1 << 0,
    IL2CPP = 1 << 1,

    Unity = 1 << 31
  }

  public enum WordSize
  {
    Four = 4,
    Eight = 8
  }

  public static readonly string executableExtension;
  public static readonly InstructionSet instructionSet;
  public static readonly OperatingSystem operatingSystem;
  public static readonly Runtime runtime;
  public static readonly WordSize wordSize;

  static Platform()
  {
#if UNITY
#  if ENABLE_IL2CPP
        runtime = Runtime.Unity | Runtime.IL2CPP;
#  else
        runtime = Runtime.Unity | Runtime.Mono;
#  endif
#else
    if (null != Type.GetType("Mono.Runtime"))
      runtime = Runtime.Mono;
#endif

    instructionSet = wordSize == WordSize.Eight ? InstructionSet.AMD64 : InstructionSet.IntelX86; // TODO(abidon): multi platform detection
    wordSize = (WordSize)IntPtr.Size;

    operatingSystem = OperatingSystem.Windows; // TODO(abidon): multi platform detection
    executableExtension = operatingSystem switch
    {
      OperatingSystem.Windows => ".exe",
      OperatingSystem.macOS => string.Empty,
      OperatingSystem.iOS => string.Empty,
      OperatingSystem.watchOS => string.Empty,
      OperatingSystem.tvOS => string.Empty,
      OperatingSystem.Linux => string.Empty,
      OperatingSystem.Android => string.Empty,
      _ => string.Empty
    };
  }
}
