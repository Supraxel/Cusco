namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<T> StartWith(T value)
  {
    return Observable.Create<T>(dispatchQueue, observer =>
    {
      observer.OnNext(value);
      return Subscribe(observer);
    });
  }
}
