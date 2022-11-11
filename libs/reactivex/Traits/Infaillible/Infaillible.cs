namespace Cusco.ReactiveX;

public readonly partial record struct Infaillible<T> : IObservable<T>
{
  private readonly Observable<T> observable;

  public Infaillible(Observable<T> observable)
  {
    this.observable = observable;
  }

  public IDisposable Subscribe(IObserver<T> observer) => Subscribe(observer, default);
  public IDisposable Subscribe(IObserver<T> observer, CancellationToken token) => observable.Subscribe(observer, token);
}
