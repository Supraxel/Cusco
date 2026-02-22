namespace Cusco.Dispatch;

public static class FutureTaskExtensions
{
  public static Future<Empty> ToFuture(this Task task, DispatchQueue queue)
  {
    var promise = queue.MakePromise<Empty>();

    task.ContinueWith(_ => promise.Complete(task.Exception is null ? Result<Empty>.Ok(default) : Result<Empty>.Err(task.Exception)));

    return promise.future;
  }

  public static Future<T> ToFuture<T>(this Task<T> task, DispatchQueue queue)
  {
    var promise = queue.MakePromise<T>();

    task.ContinueWith(_ => promise.Complete(task.Exception is null ? Result<T>.Ok(task.Result) : Result<T>.Err(task.Exception)));

    return promise.future;
  }

  public static Task ToTask(this Future<Empty> future)
  {
    var tcs = new TaskCompletionSource<object>();

    future.Do(result =>
    {
      if (result.isOk)
      {
        tcs.SetResult(null);
      }
      else
      {
        tcs.SetException(result.UnwrapErr());
      }
    });

    return tcs.Task;
  }

  public static Task<T> ToTask<T>(this Future<T> future)
  {
    var tcs = new TaskCompletionSource<T>();

    future.Do(result =>
    {
      if (result.isOk)
      {
        tcs.SetResult(result.Unwrap());
      }
      else
      {
        tcs.SetException(result.UnwrapErr());
      }
    });

    return tcs.Task;
  }
}
