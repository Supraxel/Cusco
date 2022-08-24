using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public static partial class Observable
{
  public static Observable<T> Create<T>(DispatchQueue queue, Observable<T>.SubscriptionHandler handler) => new(queue, handler);
}
