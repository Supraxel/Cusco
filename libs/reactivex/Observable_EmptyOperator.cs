using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public static partial class Observable
{
  public static Observable<T> Empty<T>(DispatchQueue queue) => new ObservableImpl<T>(queue, (observer) =>
  {
    observer.OnCompleted();
    return DummyDisposable.instance;
  });
}
