namespace Cusco.ReactiveX;

internal sealed class ActionObserver<T> : IObserver<T>
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
