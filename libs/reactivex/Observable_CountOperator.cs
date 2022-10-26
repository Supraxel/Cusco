namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<ulong> Count()
  {
    return Observable.Create<ulong>(dispatchQueue, observer =>
    {
      ulong count = 0;
      return Subscribe(
        onNext: _ => ++count,
        onError: observer.OnError,
        onComplete: () =>
        {
          observer.OnNext(count);
          observer.OnCompleted();
        });
    });
  }

  public Observable<ulong> Count(Func<T, bool> predicate)
  {
    if (null == predicate)
      throw new ArgumentNullException(nameof(predicate));

    return Observable.Create<ulong>(dispatchQueue, observer =>
    {
      ulong count = 0;
      return Subscribe(
        onNext: value =>
        {
          if (predicate(value))
            count += 1;
        },
        onError: observer.OnError,
        onComplete: () =>
        {
          observer.OnNext(count);
          observer.OnCompleted();
        });
    });
  }
}
