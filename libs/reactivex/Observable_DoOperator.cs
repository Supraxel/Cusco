namespace Cusco.ReactiveX;

public delegate void DoCallback<T>(Notification<T> notification);

public delegate void DoOnCompleteCallback();

public delegate void DoOnErrorCallback(Exception error);

public delegate void DoOnNextCallback<in T>(T next);

public sealed partial class Observable<T>
{
  public Observable<T> Do(DoCallback<T> onNotification)
  {
    if (null == onNotification) throw new ArgumentNullException(nameof(onNotification));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      Subscribe(
        onNext: value => onNotification.Invoke(Notification<T>.WithNextValue(value)),
        onError: error => onNotification.Invoke(Notification<T>.WithError(error)),
        onComplete: () => onNotification.Invoke(Notification<T>.Completed()));
      return Subscribe(observer);
    });
  }

  public Observable<T> DoOnComplete(DoOnCompleteCallback onCompleteNotification)
  {
    if (null == onCompleteNotification) throw new ArgumentNullException(nameof(onCompleteNotification));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: observer.OnNext,
        onError: observer.OnError,
        onComplete: () =>
        {
          try
          {
            onCompleteNotification.Invoke();
          }
          finally
          {
            observer.OnCompleted();
          }
        });
    });
  }

  public Observable<T> DoOnError(DoOnErrorCallback onErrorNotification)
  {
    if (null == onErrorNotification) throw new ArgumentNullException(nameof(onErrorNotification));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: observer.OnNext,
        onError: error =>
        {
          try
          {
            onErrorNotification.Invoke(error);
          }
          finally
          {
            observer.OnError(error);
          }
        },
        onComplete: observer.OnCompleted);
    });
  }

  public Observable<T> DoOnNext(DoOnNextCallback<T> onNextNotification)
  {
    if (null == onNextNotification) throw new ArgumentNullException(nameof(onNextNotification));

    return Observable.Create<T>(dispatchQueue, observer =>
    {
      return Subscribe(
        onNext: value =>
        {
          try
          {
            onNextNotification.Invoke(value);
          }
          finally
          {
            observer.OnNext(value);
          }
        },
        onError: observer.OnError,
        onComplete: observer.OnCompleted);
    });
  }
}
