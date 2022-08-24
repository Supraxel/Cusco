using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public sealed partial class Observable<T> : IObservable<T>
{
  public delegate IDisposable SubscriptionHandler(IObserver<T> observer);

  public readonly DispatchQueue dispatchQueue;
  private readonly SubscriptionHandler subscriptionHandler;

  internal Observable(DispatchQueue dispatchQueue, SubscriptionHandler subscriptionHandler)
  {
    if (null == subscriptionHandler)
      throw new ArgumentNullException(nameof(subscriptionHandler));

    this.dispatchQueue = dispatchQueue ?? throw new ArgumentNullException(nameof(dispatchQueue));
    this.subscriptionHandler = subscriptionHandler;
  }
}
