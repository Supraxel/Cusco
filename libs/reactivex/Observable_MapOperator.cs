namespace Cusco.ReactiveX;

public sealed partial class Observable<T>
{
  public Observable<TTransformed> Map<TTransformed>(Func<T, TTransformed> transformation)
  {
    if (null == transformation) throw new ArgumentNullException(nameof(transformation));

    return Observable.Create<TTransformed>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: value =>
        {
          try
          {
            observer.OnNext(transformation(value));
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
