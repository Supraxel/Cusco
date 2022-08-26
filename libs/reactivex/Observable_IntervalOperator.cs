using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public static partial class Observable
{
  public static Observable<ulong> Interval(DispatchQueue queue, TimeSpan interval, bool drifting = true)
  {
    if (drifting)
    {
      return new ObservableImpl<ulong>(queue, (observer) =>
      {
        ulong count = 0;
        CancellationTokenSource cts = new();
        CancellationToken ct = cts.Token;

        void StartTimer()
        {
          queue.DispatchDelayed(interval, () =>
          {
            observer.OnNext(count++);
            StartTimer();
          }, ct);
        }

        StartTimer();
        return cts;
      });
    }

    return new ObservableImpl<ulong>(queue, (observer) =>
    {
      var timeOffset = DateTimeOffset.UtcNow;
      ulong count = 0;
      CancellationTokenSource cts = new();
      CancellationToken ct = cts.Token;

      void StartTimer()
      {
        timeOffset += interval;
        queue.DispatchDelayed(timeOffset, () =>
        {
          observer.OnNext(count++);
          StartTimer();
        }, ct);
      }

      StartTimer();
      return cts;
    });
  }
}
