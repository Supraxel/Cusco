using Cusco.LowLevel;
using Timer = System.Timers.Timer;

namespace Cusco.ReactiveX;

public sealed partial class Observable<T>
{
  public Observable<T> Throttle(TimeSpan delay)
  {
    if (delay < TimeSpan.Zero)
      throw new ArgumentOutOfRangeException(nameof(delay));

    if (TimeSpan.Zero == delay)
      return this;

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      CancellationTokenSource cts = null;

      return Subscribe(
        onNext: value =>
        {
          cts?.Cancel();
          cts = new CancellationTokenSource();
          dispatchQueue.DispatchDelayed(delay, () => observer.OnNext(value), cts.Token);
        },
        onError: error =>
        {
          cts?.Cancel();
          cts = null;
          observer.OnError(error);
        },
        onComplete: () =>
        {
          cts?.Cancel();
          cts = null;
          observer.OnCompleted();
        });
    });
  }
}
