namespace Cusco;

public sealed class DummyDisposable : IDisposable
{
  public static readonly DummyDisposable instance = new();

  private DummyDisposable()
  {
  }

  public void Dispose()
  {
  }
}
