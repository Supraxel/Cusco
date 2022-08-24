namespace Cusco.Dispatch;

internal abstract class DispatchQueueImpl : IDispatchObject
{
  internal static void ExecuteWorkloadSafely(Action block)
  {
    if (false == block.TryInvokeSafely(out var blockException))
      CuscoRT.Panic("Uncatched exception in DispatchQueue or Future workload, which is forbidden", blockException);
  }

  private readonly PriorityQueue<DelayedWorkload, DateTimeOffset> delayedWorkloads;
  protected internal abstract bool inQueue { get; }
  public readonly string label;

  public DispatchQueueImpl(string label)
  {
    this.delayedWorkloads = new();
    this.label = label;
  }

  public abstract void Dispatch(Action block);

  public void DispatchDelayed(DateTimeOffset deadline, Action block, CancellationToken cancellationToken = default)
    => DispatchImmediately(() =>
    {
      delayedWorkloads.Enqueue(new DelayedWorkload(block, cancellationToken), deadline);
      Notify();
    });

  public void DispatchImmediately(Action block)
  {
    if (false == inQueue)
    {
      Dispatch(block);
      return;
    }

    ExecuteWorkloadSafely(block);
  }

  protected void ExecuteDelayedWorkloads()
  {
    while (delayedWorkloads.TryPeek(out var workload, out var deadline))
    {
      if (deadline > DateTimeOffset.Now) return;

      delayedWorkloads.Dequeue();
      workload.Execute();
    }
  }

  protected abstract void Notify();

  protected virtual void RunEpilogue()
  {
  }

  protected virtual void RunPrelude()
  {
  }
}
