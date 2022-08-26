namespace Cusco.ReactiveX;

public static class IObserverExtensions
{
  public static void On<T>(this IObserver<T> observer, Notification<T> notification)
  {
    switch (notification)
    {
      case { isErr: true }:
        observer.OnError(notification.error.Unwrap());
        break;
      case { isNext: true }:
        observer.OnNext(notification.next.Unwrap());
        break;
      case { isComplete: true }:
        observer.OnCompleted();
        break;
    }
  }
}
