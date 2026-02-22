namespace Cusco.Dispatch;

public static class FutureTaskExtensions
{
  extension(Future<Empty> future)
  {
    public Task ToTask()
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
  }

  extension<T>(Future<T> future)
  {
    public Task<T> ToTask()
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

  extension(Task task)
  {
    public Future<Empty> ToFuture(DispatchQueue queue)
    {
      var promise = queue.MakePromise<Empty>();

      task.ContinueWith(_ => promise.Complete(task.Exception is null ? Result<Empty>.Ok(default) : Result<Empty>.Err(task.Exception)));

      return promise.future;
    }
  }

  extension<T>(Task<T> task)
  {
    public Future<T> ToFuture(DispatchQueue queue)
    {
      var promise = queue.MakePromise<T>();

      task.ContinueWith(_ => promise.Complete(task.Exception is null ? Result<T>.Ok(task.Result) : Result<T>.Err(task.Exception)));

      return promise.future;
    }
  }
}
