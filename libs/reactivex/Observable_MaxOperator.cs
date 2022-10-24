namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> Max(IComparer<T> comparer)
  {
    if (null == comparer) throw new ArgumentNullException(nameof(comparer));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      Option<T> maxValue = Option<T>.None();

      return Subscribe(
        onNext: value =>
        {
          maxValue = maxValue.Map(max => comparer.Compare(value, max) > 0 ? value : max).Or(value);
        },
        onError: observer.OnError,
        onComplete: () =>
        {
          if (maxValue.isSome)
            observer.OnNext(maxValue.Unwrap());
          observer.OnCompleted();
        });
    });
  }

  public Observable<T> Max(Comparison<T> comparison)
  {
    if (null == comparison) throw new ArgumentNullException(nameof(comparison));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      Option<T> maxValue = Option<T>.None();

      return Subscribe(
        onNext: value =>
        {
          maxValue = maxValue.Map(max => comparison(value, max) > 0 ? value : max).Or(value);
        },
        onError: observer.OnError,
        onComplete: () =>
        {
          if (maxValue.isSome)
            observer.OnNext(maxValue.Unwrap());
          observer.OnCompleted();
        });
    });
  }
}

public static partial class ObservableExtensions
{
  public static Observable<T> Max<T>(this Observable<T> observable) where T: IComparable
  {
    return Observable.Create<T>(observable.dispatchQueue, observer =>
    {
      Option<T> maxValue = Option<T>.None();

      return observable.Subscribe(
        onNext: value =>
        {
          maxValue = maxValue.Map(max => value.CompareTo(max) > 0 ? value : max).Or(value);
        },
        onError: observer.OnError,
        onComplete: () =>
        {
          if (maxValue.isSome)
            observer.OnNext(maxValue.Unwrap());
          observer.OnCompleted();
        });
    });
  }
}
