namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> Take(ulong count)
  {
    if (0 == count)
      return Observable.Empty<T>(dispatchQueue);

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      ulong counter = 0;
      var cts = new CancellationTokenSource();

      return Subscribe(
        onNext: value =>
        {
          observer.OnNext(value);
          if (++counter >= count)
          {
            ((Action)observer.OnCompleted).TryInvokeSafely(out _);
            cts.Cancel();
          }
        },
        onError: observer.OnError,
        onComplete: observer.OnCompleted,
        cts.Token);
    });
  }
}
