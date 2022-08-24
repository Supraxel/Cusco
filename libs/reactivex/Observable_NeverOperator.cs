using Cusco.Dispatch;

namespace Cusco.ReactiveX;

public static partial class Observable
{
  public static Observable<T> Never<T>(DispatchQueue queue) => new(queue, (observer) =>
  {
    return DummyDisposable.instance;
  });
}
