using Cusco.LowLevel;

namespace Cusco.ReactiveX;

public sealed partial class Observable<T>
{
  public IDisposable Subscribe(IObserver<T> observer) => Subscribe(observer, default);

  public IDisposable Subscribe(IObserver<T> observer, CancellationToken cancellationToken)
  {
    if (null == observer) throw new ArgumentNullException(nameof(observer));
    return new Subscription(this, observer, cancellationToken);
  }

  public IDisposable Subscribe(Action<T> onNext = null, Action<Exception> onError = null, Action onComplete = null, CancellationToken cancellationToken = default)
  {
    if (null == onNext && null == onError && null == onComplete)
      throw new ArgumentNullException(nameof(onNext), $"At least one of {nameof(onNext)}, {nameof(onError)} or {nameof(onComplete)} must not be null");
    return new Subscription(this, new ActionObserver(onNext, onError, onComplete), cancellationToken);
  }

  private sealed class ActionObserver : IObserver<T>
  {
    private readonly Action onComplete;
    private readonly Action<Exception> onError;
    private readonly Action<T> onNext;

    public ActionObserver(Action<T> onNext, Action<Exception> onError, Action onComplete)
    {
      this.onNext = onNext;
      this.onError = onError;
      this.onComplete = onComplete;
    }

    public void OnCompleted() => onComplete?.Invoke();
    public void OnError(Exception error) => onError?.Invoke(error);
    public void OnNext(T value) => onNext?.Invoke(value);
  }

  private sealed class Subscription : IDisposable, IObserver<T>
  {
    private CancellationTokenSource cts;
    private OnceFlag haltOnce = new OnceFlag();
    private readonly IObserver<T> observer;
    private IDisposable subscription;

    public Subscription(Observable<T> observable, IObserver<T> observer, CancellationToken cancellationToken = default)
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
