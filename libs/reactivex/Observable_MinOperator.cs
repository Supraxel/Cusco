using System.Collections;

namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> Min(IComparer<T> comparer)
  {
    if (null == comparer) throw new ArgumentNullException(nameof(comparer));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      Option<T> minValue = Option<T>.None();

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

  public Observable<T> Min(Comparison<T> comparison)
  {
    if (null == comparison) throw new ArgumentNullException(nameof(comparison));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      Option<T> minValue = Option<T>.None();

      return Subscribe(
        onNext: value =>
        {
          minValue = minValue.Map(min => comparison(value, min) < 0 ? value : min).Or(value);
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
    return Observable.Create<T>(observable.dispatchQueue, observer =>
    {
      Option<T> minValue = Option<T>.None();

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
