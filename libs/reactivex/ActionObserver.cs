namespace Cusco.ReactiveX;

public sealed class ActionObserver<T> : IObserver<T>
{
  private readonly Action onComplete;
  private readonly Action<Exception> onError;
  private readonly Action<T> onNext;

  public ActionObserver(Action<T> onNext = null, Action<Exception> onError = null, Action onComplete = null)
  {
    if (null == onNext && null == onError && null == onComplete)
      throw new ArgumentNullException(nameof(onNext), $"At least one of {nameof(onNext)}, {nameof(onError)} or {nameof(onComplete)} must not be null");
    this.onNext = onNext;
    this.onError = onError;
    this.onComplete = onComplete;
  }

  public void OnCompleted() => onComplete?.Invoke();
  public void OnError(Exception error) => onError?.Invoke(error);
  public void OnNext(T value) => onNext?.Invoke(value);
}
