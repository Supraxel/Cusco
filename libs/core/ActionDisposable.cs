using Cusco.LowLevel;

namespace Cusco;

public sealed class ActionDisposable : IDisposable
{
  private readonly Action disposeBlock;
  private readonly OnceFlag disposeOnce = new OnceFlag();

  public ActionDisposable(Action disposeBlock)
  {
    this.disposeBlock = disposeBlock ?? throw new ArgumentNullException(nameof(disposeBlock));
  }

  public void Dispose() => disposeOnce.Do(disposeBlock);
}
