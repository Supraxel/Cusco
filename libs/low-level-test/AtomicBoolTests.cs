namespace Cusco.LowLevel.Test;

public class AtomicBoolTests
{
  [Test]
  public void ShouldImplicitCastToFalseBoolean()
  {
    var atomicBool = new AtomicBool();
    Assert.IsFalse(atomicBool);
  }

  [Test]
  public void ShouldImplicitCastToTrueBoolean()
  {
    var atomicBool = new AtomicBool(true);
    Assert.IsTrue(atomicBool);
  }

  [Test]
  public void Exchange()
  {
    var atomicBool = new AtomicBool(true);
    Assert.IsTrue(atomicBool);

    Assert.IsTrue(atomicBool.Exchange(true));
    Assert.IsTrue(atomicBool);

    Assert.IsTrue(atomicBool.Exchange(false));
    Assert.IsFalse(atomicBool);

    Assert.IsFalse(atomicBool.Exchange(false));
    Assert.IsFalse(atomicBool);

    Assert.IsFalse(atomicBool.Exchange(true));
    Assert.IsTrue(atomicBool);
  }

  [Test]
  public void CompareExchangeWhenTrue()
  {
    var atomicBool = new AtomicBool(true);
    Assert.IsTrue(atomicBool);

    Assert.IsTrue(atomicBool.CompareExchange(false, false));
    Assert.IsTrue(atomicBool);

    Assert.IsTrue(atomicBool.CompareExchange(false, true));
    Assert.IsTrue(atomicBool);

    Assert.IsTrue(atomicBool.CompareExchange(true, true));
    Assert.IsTrue(atomicBool);

    Assert.IsTrue(atomicBool.CompareExchange(true, false));
    Assert.IsFalse(atomicBool);
  }

  [Test]
  public void CompareExchangeWhenFalse()
  {
    var atomicBool = new AtomicBool();
    Assert.IsFalse(atomicBool);

    Assert.IsFalse(atomicBool.CompareExchange(true, true));
    Assert.IsFalse(atomicBool);

    Assert.IsFalse(atomicBool.CompareExchange(true, false));
    Assert.IsFalse(atomicBool);

    Assert.IsFalse(atomicBool.CompareExchange(false, false));
    Assert.IsFalse(atomicBool);

    Assert.IsFalse(atomicBool.CompareExchange(false, true));
    Assert.IsTrue(atomicBool);
  }
}
