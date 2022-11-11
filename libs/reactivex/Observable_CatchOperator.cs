namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> Catch(Func<Exception, Observable<T>> handler)
  {
    return Observable.Create<T>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: observer.OnNext,
        onError: error =>
        {
          Observable<T> continuationObservable;
          try
          {
            continuationObservable = handler(error);
          }
          catch (Exception exc)
          {
            observer.OnError(exc);
            return;
          }
          continuationObservable.Subscribe(observer);
        },
        onComplete: observer.OnCompleted);
    });
  }

  public Observable<T> CatchAndResult(Func<Exception, T> handler)
  {
    return Observable.Create<T>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: observer.OnNext,
        onError: error =>
        {
          T result;
          try
          {
            result = handler(error);
          }
          catch (Exception exc)
          {
            observer.OnError(exc);
            return;
          }
          observer.OnNext(result);
          observer.OnCompleted();
        },
        onComplete: observer.OnCompleted);
    });
  }

  public Observable<T> CatchAndReturn(T value)
  {
    return Observable.Create<T>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: observer.OnNext,
        onError: _ =>
        {
          observer.OnNext(value);
          observer.OnCompleted();
        },
        onComplete: observer.OnCompleted);
    });
  }
}
