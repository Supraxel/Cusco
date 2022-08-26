namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> Filter(Func<T, bool> predicate)
  {
    if (null == predicate) throw new ArgumentNullException(nameof(predicate));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: value =>
        {
          try
          {
            if (predicate(value))
              observer.OnNext(value);
          }
          catch (Exception exc)
          {
            observer.OnError(exc);
          }
        },
        onError: observer.OnError,
        onComplete: observer.OnCompleted);
    });
  }
}
