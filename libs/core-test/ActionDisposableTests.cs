#pragma warning disable S3966

namespace Cusco.Core.Test;

public class ActionDisposableTests
{
  [Test]
  public void ShouldDisposeOnce()
  {
    var calls = 0;

    var disposable = new ActionDisposable(() =>
    {
      calls++;
    });

    Assert.That(calls, Is.EqualTo(0));
    disposable.Dispose();
    Assert.That(calls, Is.EqualTo(1));
    disposable.Dispose();
    Assert.That(calls, Is.EqualTo(1));
  }
}
