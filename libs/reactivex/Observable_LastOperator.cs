using Cusco.Dispatch;
using Cusco.LowLevel;

namespace Cusco.ReactiveX;

public sealed partial class Observable<T>
{
  public Observable<T> Last()
  {
    return Observable.Create<T>(dispatchQueue, observer =>
    {
      Option<T> lastValue = Option<T>.None();

      return Subscribe(
        onNext: value => { lastValue = Option<T>.Some(value); },
        onError: observer.OnError,
        onComplete: () =>
        {
          if (lastValue.isSome)
            observer.OnNext(lastValue.Unwrap());
          observer.OnCompleted();
        });
    });
  }

  public Future<Option<T>> LastAsFuture() => Last().FirstAsFuture();

  public Observable<T> LastOrDefault(T orDefault = default)
  {
    return Observable.Create<T>(dispatchQueue, observer =>
    {
      var lastValue = orDefault;

      return Subscribe(
        onNext: value => { lastValue = value; },
        onError: observer.OnError,
        onComplete: () =>
        {
          observer.OnNext(lastValue);
          observer.OnCompleted();
        });
    });
  }

  public Future<T> LastOrDefaultAsFuture(T orDefault = default) => Last().FirstOrDefaultAsFuture(orDefault);
}
