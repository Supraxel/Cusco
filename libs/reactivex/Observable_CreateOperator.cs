using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public static partial class Observable
{
  public delegate IDisposable SubscriptionHandler<out T>(IObserver<T> observer);

  public static Observable<T> Create<T>(DispatchQueue queue, SubscriptionHandler<T> handler) => new ObservableImpl<T>(queue, handler);
}
