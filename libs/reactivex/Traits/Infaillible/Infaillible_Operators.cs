using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public readonly partial record struct Infaillible<T>
{
  public Infaillible<TCombined> CombineLatest<TOther, TCombined>(Infaillible<TOther> other, Func<T, TOther, TCombined> combiner)
    => new(observable.CombineLatest(other.observable, combiner));

  public Infaillible<TTransformed> CompactMap<TTransformed>(Func<T, TTransformed> transformation) where TTransformed: class
    => new(observable.CompactMap(transformation));

  public Infaillible<TTransformed> CompactMap<TTransformed>(Func<T, TTransformed?> transformation) where TTransformed : struct
    => new(observable.CompactMap(transformation));

  public Infaillible<ulong> Count()
    => new(observable.Count());

  public Infaillible<ulong> Count(Func<T, bool> predicate)
    => new(observable.Count(predicate));

  public Infaillible<T> Debounce(TimeSpan delay, Action<bool> gateStatusCallback = null)
    => new(observable.Debounce(delay, gateStatusCallback));

  public Infaillible<T> DistinctUntilChanged(Func<T, T, bool> predicate)
    => new(observable.DistinctUntilChanged(predicate));

  public Infaillible<T> Do(DoCallback<T> onNotification)
    => new(observable.Do(onNotification));

  public Infaillible<T> DoOnComplete(DoOnCompleteCallback onCompleteNotification)
    => new(observable.DoOnComplete(onCompleteNotification));

  public Infaillible<T> DoOnError(DoOnErrorCallback onErrorNotification)
    => new(observable.DoOnError(onErrorNotification));

  public Infaillible<T> DoOnNext(DoOnNextCallback<T> onNextNotification)
    => new(observable.DoOnNext(onNextNotification));

  public static Infaillible<T> Empty(DispatchQueue queue)
    => new(Observable.Empty<T>(queue));

  public Infaillible<T> Filter(Func<T, bool> predicate)
    => new(observable.Filter(predicate));

  public Infaillible<T> First()
    => new(observable.First());

  public Future<Option<T>> FirstAsFuture()
    => observable.FirstAsFuture();

  public Infaillible<T> FirstOrDefault(T orDefault = default)
    => new(observable.FirstOrDefault(orDefault));

  public Future<T> FirstOrDefaultAsFuture(T orDefault = default)
    => observable.FirstOrDefaultAsFuture(orDefault);

  public static Infaillible<ulong> Interval(DispatchQueue queue, TimeSpan interval, bool drifting = true)
    => new(Observable.Interval(queue, interval, drifting: drifting));

  public static Infaillible<T> Just(DispatchQueue queue, T element)
    => new(Observable.Just(queue, element));

  public Infaillible<T> Last()
    => new(observable.Last());

  public Future<Option<T>> LastAsFuture()
    => observable.LastAsFuture();

  public Infaillible<T> LastOrDefault(T orDefault = default)
    => new(observable.LastOrDefault(orDefault));

  public Future<T> LastOrDefaultAsFuture(T orDefault = default)
    => observable.LastOrDefaultAsFuture(orDefault);

  public Infaillible<TTransformed> Map<TTransformed>(Func<T, TTransformed> transformation)
    => new(observable.Map(transformation));

  public Infaillible<T> Max(IComparer<T> comparer)
    => new(observable.Max(comparer));

  public Infaillible<T> Max(Comparison<T> comparer)
    => new(observable.Max(comparer));

  public Infaillible<T> Min(IComparer<T> comparer)
    => new(observable.Min(comparer));

  public Infaillible<T> Min(Comparison<T> comparer)
    => new(observable.Min(comparer));

  public static Infaillible<T> Never(DispatchQueue queue)
    => new(Observable.Never<T>(queue));

  public Infaillible<T> ObserveOn(DispatchQueue observeOnQueue)
    => new(observable.ObserveOn(observeOnQueue));

  public Infaillible<T> Skip(ulong count)
    => new(observable.Skip(count));

  public Infaillible<T> StartWith(T value)
    => new(observable.StartWith(value));

  public Infaillible<T> Take(ulong count)
    => new(observable.Take(count));

  public Infaillible<T> Throttle(TimeSpan delay)
    => new(observable.Throttle(delay));

  public static Infaillible<T> Throw<T>(DispatchQueue queue, Exception error)
    => new(Observable.Throw<T>(queue, error));
}

public static class InfaillibleExtensions
{
  public static Infaillible<T> DistinctUntilChanged<T>(this Infaillible<T> self) where T: IEquatable<T>
    => new(self.AsObservable().DistinctUntilChanged());

  public static Infaillible<T> Max<T>(this Infaillible<T> self) where T : IComparable
    => new(self.AsObservable().Max());

  public static Infaillible<T> Min<T>(this Infaillible<T> self) where T : IComparable
    => new(self.AsObservable().Min());
}
