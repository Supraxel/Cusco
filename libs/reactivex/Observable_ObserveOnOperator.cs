using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> ObserveOn(DispatchQueue observeOnQueue)
  {
    if (null == observeOnQueue) throw new ArgumentNullException(nameof(observeOnQueue));

    return Observable.Create<T>(observeOnQueue, observer =>
    {
      return Subscribe(
        onNext: value =>
        {
          observeOnQueue.DispatchImmediate(() =>
          {
            observer.OnNext(value);
          });
        },
        onError: error =>
        {
          observeOnQueue.DispatchImmediate(() =>
          {
            observer.OnError(error);
          });
        },
        onComplete: () =>
        {
          observeOnQueue.DispatchImmediate(observer.OnCompleted);
        });
    });
  }
}
