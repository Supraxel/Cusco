namespace Cusco.ReactiveX;

public partial class Observable<T> : IObservableConvertible<T>
{
  public Observable<T> AsObservable() => this;
}
