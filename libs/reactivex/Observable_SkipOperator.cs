namespace Cusco.ReactiveX;

public sealed partial class Observable<T>
{
  public Observable<T> Skip(ulong count)
  {
    if (0 == count)
      return this;

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      ulong counter = 0;

      return Subscribe(
        onNext: value =>
        {
          try
          {
            if (++counter <= count)
              return;

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
