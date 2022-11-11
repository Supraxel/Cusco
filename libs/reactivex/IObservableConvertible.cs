namespace Cusco.ReactiveX;

public interface IObservableConvertible<T>
{
  public Observable<T> AsObservable();
}
