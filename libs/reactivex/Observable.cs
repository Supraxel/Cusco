using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public abstract partial class Observable<T> : IObservable<T>
{
  public readonly DispatchQueue dispatchQueue;

  internal Observable(DispatchQueue dispatchQueue)
  {
    this.dispatchQueue = dispatchQueue ?? throw new ArgumentNullException(nameof(dispatchQueue));
  }

  public IDisposable Subscribe(IObserver<T> observer) => Subscribe(observer, default);

  public IDisposable Subscribe(Action<T> onNext = null, Action<Exception> onError = null, Action onComplete = null, CancellationToken cancellationToken = default)
    => Subscribe(new ActionObserver<T>(onNext, onError, onComplete), cancellationToken);

  public abstract IDisposable Subscribe(IObserver<T> observer, CancellationToken cancellationToken);
}
