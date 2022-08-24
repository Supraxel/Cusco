using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public static partial class Observable
{
  public static Observable<T> Just<T>(DispatchQueue queue, T value) => new(queue, (observer) =>
  {
    observer.OnNext(value);
    observer.OnCompleted();
    return DummyDisposable.instance;
  });
}
