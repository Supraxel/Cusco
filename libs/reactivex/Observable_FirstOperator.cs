using Cusco.Dispatch;
using Cusco.LowLevel;

namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> First()
  {
    return Observable.Create<T>(dispatchQueue, observer =>
    {
      var onceFlag = new OnceFlag();
      var cts = new CancellationTokenSource();

      return Subscribe(
        onNext: value =>
        {
          onceFlag.Do(() =>
          {
            observer.OnNext(value);
            observer.OnCompleted();
            cts.Cancel();
          });
        },
        onError: observer.OnError,
        onComplete: observer.OnCompleted,
        cts.Token);
    });
  }

  public Future<Option<T>> FirstAsFuture()
  {
    var promise = dispatchQueue.MakePromise<Option<T>>();
    var cts = new CancellationTokenSource();

    Subscribe(
      onNext: value =>
      {
        promise.Complete(Option<T>.Some(value));
        cts.Cancel();
      },
      onError: error => promise.CompleteWithErr(error),
      onComplete: () => promise.Complete(Option<T>.None()),
      cts.Token);

    return promise;
  }

  public Observable<T> FirstOrDefault(T orDefault = default)
  {
    return Observable.Create<T>(dispatchQueue, observer =>
    {
      var onceFlag = new OnceFlag();
      var cts = new CancellationTokenSource();

      return Subscribe(
        onNext: value =>
        {
          onceFlag.Do(() =>
          {
            observer.OnNext(value);
            observer.OnCompleted();
            cts.Cancel();
          });
        },
        onError: observer.OnError,
        onComplete: () =>
        {
          observer.OnNext(orDefault);
          observer.OnCompleted();
        },
        cts.Token);
    });
  }

  public Future<T> FirstOrDefaultAsFuture(T orDefault = default)
  {
    var promise = dispatchQueue.MakePromise<T>();
    var cts = new CancellationTokenSource();

    Subscribe(
      onNext: value =>
      {
        promise.Complete(value);
        cts.Cancel();
      },
      onError: error =>
      {
        cts.Cancel();
        promise.CompleteWithErr(error);
      },
      onComplete: () => promise.Complete(orDefault),
      cts.Token);

    return promise;
  }
}
