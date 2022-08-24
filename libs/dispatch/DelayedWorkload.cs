namespace Cusco.Dispatch;

internal sealed class DelayedWorkload
{
  private readonly Action block;
  private readonly CancellationToken cancellationToken;

  internal DelayedWorkload(Action block, CancellationToken cancellationToken)
  {
    this.block = block ?? throw new ArgumentNullException(nameof(block));
    this.cancellationToken = cancellationToken;
  }

  internal void Execute()
  {
    if (cancellationToken.IsCancellationRequested) return;

    DispatchQueueImpl.ExecuteWorkloadSafely(block);
  }
}
