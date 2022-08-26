using Cusco.Dispatch;
using Cusco.LowLevel;

namespace Cusco.ReactiveX;

internal sealed class ObservableImpl<T> : Observable<T>
{
  private readonly Observable.SubscriptionHandler<T> subscriptionHandler;

  internal ObservableImpl(DispatchQueue dispatchQueue, Observable.SubscriptionHandler<T> subscriptionHandler) : base(dispatchQueue)
  {
    if (null == subscriptionHandler)
      throw new ArgumentNullException(nameof(subscriptionHandler));

    this.subscriptionHandler = subscriptionHandler;
  }

  public override IDisposable Subscribe(IObserver<T> observer, CancellationToken cancellationToken)
  {
    if (null == observer) throw new ArgumentNullException(nameof(observer));
    return new Subscription(this, observer, cancellationToken);
  }

  private sealed class Subscription : IDisposable, IObserver<T>
  {
    private CancellationTokenSource cts;
    private readonly OnceFlag haltOnce = new OnceFlag();
    private readonly IObserver<T> observer;
    private IDisposable subscription;

    public Subscription(ObservableImpl<T> observable, IObserver<T> observer, CancellationToken cancellationToken = default)
    {
      if (null == observable) throw new ArgumentNullException(nameof(observable));
      this.observer = observer ?? throw new ArgumentNullException(nameof(observer));

      // Register Dispose delegate in case the external cancellation token is cancelled
      cancellationToken.Register(Dispose);

      // Create a cancellationToken for the following async dispatch operation
      cts = new CancellationTokenSource();
      var ct = cts.Token;
      observable.dispatchQueue.DispatchImmediate(() =>
      {
        if (ct.IsCancellationRequested) return;

        try
        {
          subscription = observable.subscriptionHandler(this);
        }
        catch (Exception error)
        {
          OnError(error);
        }
      });
    }

    public void Dispose()
    {
      haltOnce.Do(() => observer.OnCompleted());
      cts?.Cancel();
      cts = null;
      subscription?.Dispose();
      subscription = null;
    }

    public void OnCompleted()
    {
      if (cts?.IsCancellationRequested ?? true) return;
      cts?.Cancel();
      haltOnce.Do(() => observer.OnCompleted());
    }

    public void OnError(Exception error)
    {
      if (cts?.IsCancellationRequested ?? true) return;
      cts?.Cancel();
      haltOnce.Do(() => observer.OnError(error.Enhance()));
    }

    public void OnNext(T value)
    {
      if (cts?.IsCancellationRequested ?? true) return;
      observer.OnNext(value);
    }
  }
}
