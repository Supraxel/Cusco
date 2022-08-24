using System.Runtime.CompilerServices;

namespace Cusco.Dispatch;

public sealed class FutureAwaiter<T> : INotifyCompletion
{
  private readonly Future<T> future;

  public bool IsCompleted => future.isDone;

  internal FutureAwaiter(Future<T> future)
  {
    this.future = future;
  }

  public T GetResult()
  {
    if (false == future.isDone)
      throw new InvalidOperationException("Can't get the result of a future that hasn't yet completed");

    switch (future)
    {
      case {isDone: false}:
        throw new InvalidOperationException("Can't get the result of a future that hasn't yet completed");
      case {isOk: true}:
        return future.result.asRef.Unwrap();
      case {isErr: true}:
        throw future.result.asRef.UnwrapErr().Rethrow();
      default:
        return CuscoRT.Unreachable<T>();
    }
  }

  public void OnCompleted(Action continuation)
  {
    future.Do(_ => continuation());
  }
}
