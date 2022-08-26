using Cusco.LowLevel;
using Timer = System.Timers.Timer;

namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> Debounce(TimeSpan delay, Action<bool> gateStatusCallback = null)
  {
    if (delay < TimeSpan.Zero)
      throw new ArgumentOutOfRangeException(nameof(delay));

    if (TimeSpan.Zero == delay)
      return this;

    double delayMs = Math.Max(1, delay.TotalMilliseconds);

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      var timer = new Timer(delayMs) { AutoReset = false };
      var gateStatus = new AtomicBool(true);

      gateStatusCallback?.Invoke(gateStatus);
      timer.Elapsed += (_1, _2) =>
      {
        gateStatus.Exchange(true);
        gateStatusCallback?.TryInvokeSafely(gateStatus, out _);
      };

      return Subscribe(
        onNext: value =>
        {
          if (gateStatus.CompareExchange(true, false))
          {
            timer.Start();
            gateStatusCallback?.TryInvokeSafely(gateStatus, out _);
            observer.OnNext(value);
          }
        },
        onError: error =>
        {
          observer.OnError(error);
          timer.Close();
          timer.Dispose();
        },
        onComplete: () =>
        {
          observer.OnCompleted();
          timer.Close();
          timer.Dispose();
        });
    });
  }
}
