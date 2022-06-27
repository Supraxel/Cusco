namespace Cusco.LowLevel;

public sealed class DisposeBag : IDisposable
{
  private List<IDisposable> disposables = new();

  public void Dispose()
  {
    if (null == disposables)
      return;

    var disposablesList = disposables;
    disposables = null;

    foreach (var disposable in disposablesList)
      disposable.Dispose();
  }

  public void Add(IDisposable disposable)
  {
    if (null == disposable) return;

    if (null == disposables)
      disposable.Dispose();
    else
      disposables.Add(disposable);
  }
}

public static class DisposeBagExtensions
{
  public static void DisposedBy(this IDisposable disposable, DisposeBag disposeBag)
  {
    (disposeBag ?? throw new ArgumentNullException(nameof(disposeBag))).Add(disposable);
  }
}
