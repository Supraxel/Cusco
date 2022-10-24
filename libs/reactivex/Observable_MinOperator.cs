using System.Collections;

namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> Min(IComparer<T> comparer)
  {
    if (null == comparer) throw new ArgumentNullException(nameof(comparer));

    Option<T> minValue = Option<T>.None();

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: value =>
        {
          minValue = minValue.Map(min => comparer.Compare(value, min) < 0 ? value : min).Or(value);
        },
        onError: observer.OnError,
        onComplete: () =>
        {
          if (minValue.isSome)
            observer.OnNext(minValue.Unwrap());
          observer.OnCompleted();
        });
    });
  }
}

public static class ObservableExtensions
{
  public static Observable<T> Min<T>(this Observable<T> observable) where T: IComparable
  {
    Option<T> minValue = Option<T>.None();

    return Observable.Create<T>(observable.dispatchQueue, observer =>
    {
      return observable.Subscribe(
        onNext: value =>
        {
          minValue = minValue.Map(min => value.CompareTo(min) < 0 ? value : min).Or(value);
        },
        onError: observer.OnError,
        onComplete: () =>
        {
          if (minValue.isSome)
            observer.OnNext(minValue.Unwrap());
          observer.OnCompleted();
        });
    });
  }
}
