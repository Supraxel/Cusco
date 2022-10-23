using System;
using Cusco;
namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<TTransformed> CompactMap<TTransformed>(Func<T, TTransformed> transformation) where TTransformed: class
  {
    if (null == transformation) throw new ArgumentNullException(nameof(transformation));

    return Observable.Create<TTransformed>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: value =>
        {
          try
          {
            var transformed = transformation(value);
            if (null != transformed)
              observer.OnNext(transformed);
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
