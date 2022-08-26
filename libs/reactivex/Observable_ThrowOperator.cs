using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public static partial class Observable
{
  public static Observable<T> Throw<T>(DispatchQueue queue, Exception error) => new ObservableImpl<T>(queue, (observer) =>
  {
    observer.OnError(error);
    return DummyDisposable.instance;
  });
}
