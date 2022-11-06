using Cusco.Dispatch;
using Cusco.LowLevel;

namespace Cusco.ReactiveX;

public sealed class BehaviourSubject<T> : Observable<T>, IDisposable, IObserver<T>
{
  private readonly AtomicBool isDisposed = new();
  private readonly HashSet<IObserver<T>> observers = new();
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
  }

  public BehaviourSubject(DispatchQueue dispatchQueue, T value) : base(dispatchQueue)
    => _value = value;

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
        case { isNext: true }:
          _value = notification.next.Unwrap();
          break;
        case { isErr: true } or { isComplete: true }:
          stopNotification = notification;
          break;
      }

      foreach (var observer in observers.ToList())
        observer.On(notification);

      if (stopNotification.isSome)
        observers.Clear();
    });
  }

  public void OnCompleted() => On(Notification<T>.Completed());
  public void OnError(Exception error) => On(Notification<T>.WithError(error));
  public void OnNext(T value) => On(Notification<T>.WithNextValue(value));

  public override IDisposable Subscribe(IObserver<T> observer, CancellationToken cancellationToken)
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
}
