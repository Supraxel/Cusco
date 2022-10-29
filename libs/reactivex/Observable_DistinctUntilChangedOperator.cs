namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> DistinctUntilChanged(Func<T, T, bool> predicate)
  {
    if (null == predicate) throw new ArgumentNullException(nameof(predicate));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      var lastValue = Option<T>.None();

      return Subscribe(
        onNext: value =>
        {
          try
          {
            if (!lastValue.isNone && false != predicate(lastValue.Unwrap(), value)) return;
            lastValue = value;
            observer.OnNext(value);
          }
          catch (Exception exc)
          {
            observer.OnError(exc);
          }
        },
        onError: observer.OnError,
        onComplete: observer.OnCompleted
        );
    });
  }
}

public static partial class ObservableExtensions
{
  public static Observable<T> DistinctUntilChanged<T>(this Observable<T> observable) where T: IEquatable<T>
  {
    return Observable.Create<T>(observable.dispatchQueue, observer =>
    {
      var lastValue = Option<T>.None();

      return observable.Subscribe(
        onNext: value =>
        {
          try
          {
            if (!lastValue.isNone && false != lastValue.Unwrap().Equals(value)) return;
            lastValue = value;
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
