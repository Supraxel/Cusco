using System.Diagnostics;

namespace Cusco.LowLevel;

[DebuggerDisplay("{value == 0 ? \"Not used yet\" : \"Used\"}")]
public sealed class OnceFlag
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile int flag;

  public OnceFlag()
  {
    flag = 0;
  }

  public bool flagged => 1 == flag;

  public void Do(Action block)
  {
    if (Interlocked.CompareExchange(ref flag, 1, 0) == 0)
      block();
  }

  public T DoOrDefault<T>(Func<T> block, T defaultValue = default)
  {
    return Interlocked.CompareExchange(ref flag, 1, 0) == 0 ? block() : defaultValue;
  }
}
