using Cusco.LowLevel;

namespace Cusco.Dispatch;

internal sealed class MainDispatchQueueImpl : SerialDispatchQueueImpl
{
  internal static readonly MainDispatchQueueImpl instance = new();
  internal readonly OnceFlag threadRenameOnce = new();

  private MainDispatchQueueImpl() : base(SR.mainQueueLabel)
  {
  }

  protected override void RunPrelude()
  {
    base.RunPrelude();
    threadRenameOnce.Do(() =>
    {
      try
      {
        Thread.CurrentThread.Name = SR.mainQueueLabel;
      }
      catch (InvalidOperationException)
      {
        // Ignore exception, if we can't set the name of the thread, it's no big deal.
      }
    });
  }

  protected override void RunEpilogue()
  {
    base.RunEpilogue();
    controlThread = Thread.CurrentThread;
  }
}
