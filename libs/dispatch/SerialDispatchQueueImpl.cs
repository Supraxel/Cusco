using System.Collections.Concurrent;
using Cusco.LowLevel;

namespace Cusco.Dispatch;

internal class SerialDispatchQueueImpl : DispatchQueueImpl
{
  protected Thread controlThread;
  private readonly AutoResetEvent resetEvent;
  private readonly AtomicBool running;
  private readonly DoubleBuffer<ConcurrentQueue<Action>> workloadsDoubleBuffer;

  public SerialDispatchQueueImpl(string label) : base(label)
  {
    resetEvent = new(false);
    running = new();
    workloadsDoubleBuffer = new(new(), new());
  }

  protected internal override bool inQueue => Thread.CurrentThread == controlThread;

  public override void Dispatch(Action block)
  {
    workloadsDoubleBuffer.producing.Enqueue(block);
    Notify();
  }

  private void ExecuteWorkloads()
  {
    var workloads = workloadsDoubleBuffer.consuming;

    while (workloads.TryDequeue(out var block))
      ExecuteWorkloadSafely(block);
  }

  protected override void Notify()
    => resetEvent.Set();

  internal void Run() => Run(default);

  internal void Run(CancellationToken cancellationToken)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      bool gotSignal = resetEvent.WaitOne(TimeSpan.FromSeconds(0.01));
      resetEvent.Reset();
      RunOnce(gotSignal);
    }
  }

  protected override void RunEpilogue()
  {
    base.RunEpilogue();
    controlThread = null;
  }

  internal void RunOnce(bool gotSignal)
  {
    if (running.Exchange(true)) // if is already running
      throw new InvalidOperationException($"DispatchQueue {label} is already running");

    try
    {
      RunPrelude();

      if (gotSignal)
      {
        workloadsDoubleBuffer.Swap();
        ExecuteWorkloads();
      }

      ExecuteDelayedWorkloads();
    }
    finally
    {
      RunEpilogue();
      running.Exchange(false);
    }
  }

  protected override void RunPrelude()
  {
    base.RunPrelude();
    controlThread = Thread.CurrentThread;
  }
}
