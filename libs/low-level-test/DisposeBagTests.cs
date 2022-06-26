using Moq;

namespace Cusco.LowLevel.Test;

public class DisposeBagTests
{
  [Test]
  public void DisposeAllObjects()
  {
    Mock<IDisposable> m1 = new Mock<IDisposable>();
    Mock<IDisposable> m2 = new Mock<IDisposable>();

    var disposeCalls = 0;
    m1.Setup(d => d.Dispose())
      .Callback(() => ++disposeCalls);
    m2.Setup(d => d.Dispose())
      .Callback(() => ++disposeCalls);

    using (var bag = new DisposeBag())
    {
      m1.Object.DisposedBy(bag);
      m2.Object.DisposedBy(bag);
      Assert.That(disposeCalls, Is.EqualTo(0));
    }

    Assert.That(disposeCalls, Is.EqualTo(2));
  }

  [Test]
  public void DisposeOnlyOnce()
  {
    Mock<IDisposable> m = new Mock<IDisposable>();

    var disposeCalls = 0;
    m.Setup(d => d.Dispose())
      .Callback(() => ++disposeCalls);

    using (var bag = new DisposeBag())
    {
      m.Object.DisposedBy(bag);
      Assert.That(disposeCalls, Is.EqualTo(0));
      bag.Dispose();
      Assert.That(disposeCalls, Is.EqualTo(1));
      bag.Dispose();
      Assert.That(disposeCalls, Is.EqualTo(1));
    }

    Assert.That(disposeCalls, Is.EqualTo(1));
  }
}
