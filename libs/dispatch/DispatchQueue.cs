using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cusco.Dispatch;

public sealed class DispatchQueue
{
  public static readonly DispatchQueue main = new DispatchQueue(MainDispatchQueueImpl.instance);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DispatchQueue MakeSerial(string label)
  {
    var impl = new SerialDispatchQueueImpl(label);
    new Thread(impl.Run).Start();
    return new DispatchQueue(impl);
  }

  private readonly DispatchQueueImpl impl;

  private DispatchQueue(DispatchQueueImpl impl)
    => this.impl = impl;

  [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AssertNotOnQueue() => CuscoRT.Assert(false == impl.inQueue, $"not on queue {impl.label}");

  [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AssertOnQueue() => CuscoRT.Assert(impl.inQueue, $"on queue {impl.label}");

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> AsyncSubmit<T>(Func<Future<T>> workload, CancellationToken cancellationToken = default)
  {
    var promise = MakePromise<T>(cancellationToken);

    impl.Dispatch(() =>
    {
      if (workload.TryInvokeSafely(out var value, out var err))
        value.Cascade(promise);
      else
        promise.CompleteWithErr(err);
    });

    return promise;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> AsyncSubmitDelayed<T>(DateTimeOffset deadline, Func<Future<T>> workload, CancellationToken cancellationToken = default)
  {
    var promise = MakePromise<T>(cancellationToken);

    impl.DispatchDelayed(deadline, () =>
    {
      if (workload.TryInvokeSafely(out var value, out var err))
        value.Cascade(promise);
      else
        promise.CompleteWithErr(err);
    }, cancellationToken);

    return promise;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> AsyncSubmitDelayed<T>(TimeSpan delay, Func<Future<T>> workload, CancellationToken cancellationToken = default)
    => AsyncSubmitDelayed(DateTimeOffset.Now + delay, workload, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Dispatch(Action workload)
    => impl.Dispatch(workload);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void DispatchDelayed(DateTimeOffset deadline, Action block, CancellationToken cancellationToken = default)
    => impl.DispatchDelayed(deadline, block, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void DispatchDelayed(TimeSpan delay, Action block, CancellationToken cancellationToken = default)
    => impl.DispatchDelayed(DateTimeOffset.Now + delay, block, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void DispatchImmediate(Action block)
    => impl.DispatchImmediately(block);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> MakeFailedFuture<T>(Exception exc)
  {
    var p = MakePromise<T>();
    p.CompleteWithErr(exc);
    return p;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> MakeFulfilledFuture<T>(Result<T> value)
  {
    var p = MakePromise<T>();
    p.Complete(value);
    return p;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Promise<T> MakePromise<T>(CancellationToken cancellationToken = default) => new(this, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> MakeSuccessfulFuture<T>(T value)
  {
    var p = MakePromise<T>();
    p.Complete(value);
    return p;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void PreconditionNotOnQueue() => CuscoRT.Precondition(false == impl.inQueue, $"not on queue {impl.label}");

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void PreconditionOnQueue() => CuscoRT.Precondition(impl.inQueue, $"on queue {impl.label}");

#if !UNITY
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void RunMain(CancellationToken cancellationToken = default)
    => ((MainDispatchQueueImpl)main.impl).Run(cancellationToken);
#endif

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<Empty> StartTimeout(DateTimeOffset deadline, CancellationToken cancellationToken = default)
  {
    var promise = MakePromise<Empty>(cancellationToken);

    impl.DispatchDelayed(deadline,
      () => promise.CompleteWithErr(new TimeoutException().Enhance()),
      cancellationToken
    );

    return promise;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<Empty> StartTimeout(TimeSpan delay, CancellationToken cancellationToken = default)
    => StartTimeout(DateTimeOffset.Now + delay, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<Empty> StartTimer(DateTimeOffset deadline, CancellationToken cancellationToken = default)
  {
    var promise = MakePromise<Empty>(cancellationToken);

    impl.DispatchDelayed(deadline,
      () => promise.Complete(Result<Empty>.Ok(default)),
      cancellationToken
    );

    return promise;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<Empty> StartTimer(TimeSpan delay, CancellationToken cancellationToken = default)
    => StartTimer(DateTimeOffset.Now + delay, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> Submit<T>(Func<T> workload, CancellationToken cancellationToken = default)
  {
    var promise = MakePromise<T>(cancellationToken);

    impl.Dispatch(() =>
    {
      if (workload.TryInvokeSafely(out var value, out var err))
        promise.Complete(value);
      else
        promise.CompleteWithErr(err);
    });

    return promise;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<Empty> Submit(Action workload, CancellationToken cancellationToken = default)
    => Submit(() =>
    {
      workload();
      return default(Empty);
    }, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> SubmitImmediately<T>(Func<T> workload, CancellationToken cancellationToken = default)
  {
    var promise = MakePromise<T>(cancellationToken);

    impl.DispatchImmediately(() =>
    {
      if (workload.TryInvokeSafely(out var value, out var err))
        promise.Complete(value);
      else
        promise.CompleteWithErr(err);
    });

    return promise;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<Empty> SubmitImmediately(Action workload, CancellationToken cancellationToken = default)
    => SubmitImmediately(() =>
    {
      workload();
      return default(Empty);
    }, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> SubmitDelayed<T>(DateTimeOffset deadline, Func<T> workload, CancellationToken cancellationToken = default)
  {
    var promise = MakePromise<T>(cancellationToken);

    impl.DispatchDelayed(deadline, () =>
    {
      if (workload.TryInvokeSafely(out var value, out var err))
        promise.Complete(value);
      else
        promise.CompleteWithErr(err);
    }, cancellationToken);

    return promise;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<Empty> SubmitDelayed(DateTimeOffset deadline, Action workload, CancellationToken cancellationToken = default)
    => SubmitDelayed(
      deadline,
      () =>
      {
        workload();
        return default(Empty);
      },
      cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<T> SubmitDelayed<T>(TimeSpan delay, Func<T> workload, CancellationToken cancellationToken = default)
    => SubmitDelayed(DateTimeOffset.Now + delay, workload, cancellationToken);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Future<Empty> SubmitDelayed(TimeSpan delay, Action workload, CancellationToken cancellationToken = default)
    => SubmitDelayed(
      DateTimeOffset.Now + delay,
      () =>
      {
        workload();
        return default(Empty);
      },
      cancellationToken);
}
