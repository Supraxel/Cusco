using Cusco.Dispatch;
using Cusco.LowLevel;

namespace Cusco.ReactiveX;

public sealed class BehaviourSubject<T> : IDisposable, IObservable<T>, IObserver<T>
{
  private readonly DispatchQueue dispatchQueue;
  private AtomicBool isDisposed = new();
  private HashSet<IObserver<T>> observers = new();
  private Option<Notification<T>> stopNotification = Option<Notification<T>>.None();
  private T _value;

  public T value
  {
    get
    {
      if (isDisposed)
        throw new ObjectDisposedException(nameof(BehaviourSubject<T>));

      if (stopNotification.isSome && stopNotification.Unwrap().isErr)
        stopNotification.Unwrap().error.Unwrap().Rethrow();

      return _value;
    }
    set => OnNext(value);
  }

  public BehaviourSubject(DispatchQueue dispatchQueue, T value)
  {
    this.dispatchQueue = dispatchQueue ?? throw new ArgumentNullException(nameof(dispatchQueue));
    this._value = value;
  }

  private void On(Notification<T> notification)
  {
    if (isDisposed)
      return;

    dispatchQueue.DispatchImmediate(() =>
    {
      if (stopNotification.isSome || isDisposed)
        return;

      switch (notification)
      {
        case {isNext: true}:
          this._value = notification.next.Unwrap();
          break;
        case {isErr: true} or {isComplete: true}:
          this.stopNotification = notification;
          break;
      }

      foreach (var observer in observers)
        observer.On(notification);
    });
  }

  public void OnCompleted() => On(Notification<T>.Completed());

  public void OnError(Exception error) => On(Notification<T>.WithError(error));

  public void OnNext(T value) => On(Notification<T>.WithNextValue(value));

  public IDisposable Subscribe(IObserver<T> observer)
    => Subscribe(observer, default);

  public IDisposable Subscribe(IObserver<T> observer, CancellationToken cancellationToken)
  {
    if (null == observer)
      throw new ArgumentNullException(nameof(observer));

    if (isDisposed)
    {
      observer.OnError(new ObjectDisposedException(nameof(BehaviourSubject<T>)));
      return DummyDisposable.instance;
    }

    if (stopNotification.isSome)
    {
      observer.On(stopNotification.Unwrap());
      return DummyDisposable.instance;
    }

    var innerCancellationTokenSource = new CancellationTokenSource();
    dispatchQueue.DispatchImmediate(() =>
    {
      if (innerCancellationTokenSource.IsCancellationRequested || cancellationToken.IsCancellationRequested)
        return;

      observers.Add(observer);
      observer.OnNext(_value);
    });

    return new ActionDisposable(() =>
    {
      innerCancellationTokenSource.Cancel();
      innerCancellationTokenSource.Dispose();

      if (isDisposed) return;
      dispatchQueue.DispatchImmediate(() => observers.Remove(observer));
    });
  }

  public IDisposable Subscribe(Action<T> onNext = null, Action<Exception> onError = null, Action onComplete = null, CancellationToken cancellationToken = default)
  {
    if (null == onNext && null == onError && null == onComplete)
      throw new ArgumentNullException(nameof(onNext), $"At least one of {nameof(onNext)}, {nameof(onError)} or {nameof(onComplete)} must not be null");
    return Subscribe(new Observable<T>.ActionObserver(onNext, onError, onComplete), cancellationToken);
  }

  public void Dispose()
  {
    if (isDisposed.CompareExchange(false, true))
      return;

    dispatchQueue.DispatchImmediate(() =>
    {
      observers.Clear();
      stopNotification = Option<Notification<T>>.None();
    });
  }
}
