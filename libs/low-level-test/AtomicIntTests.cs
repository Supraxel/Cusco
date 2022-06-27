namespace Cusco.LowLevel.Test;

public class AtomicIntTests
{
  [Test]
  public void Add()
  {
    var atomicInt = new AtomicInt(42);

    Assert.That(atomicInt.Add(1), Is.EqualTo(43));
    Assert.That((int)atomicInt, Is.EqualTo(43));

    Assert.That(atomicInt.Add(-12), Is.EqualTo(31));
    Assert.That((int)atomicInt, Is.EqualTo(31));

    Assert.That(atomicInt.Add(8569), Is.EqualTo(8600));
    Assert.That((int)atomicInt, Is.EqualTo(8600));
  }

  [Test]
  public void ConvertsToInt()
  {
    var atomicInt = new AtomicInt(42);
    Assert.That((int)atomicInt, Is.EqualTo(42));

    atomicInt = new AtomicInt(-1);
    Assert.That((int)atomicInt, Is.EqualTo(-1));
  }

  [Test]
  public void CompareExchange()
  {
    var atomicInt = new AtomicInt(456);

    Assert.That(atomicInt.CompareExchange(789, 123), Is.EqualTo(456));
    Assert.That((int)atomicInt, Is.EqualTo(456));

    Assert.That(atomicInt.CompareExchange(456, 123), Is.EqualTo(456));
    Assert.That((int)atomicInt, Is.EqualTo(123));
  }

  [Test]
  public void Decrement()
  {
    var atomicInt = new AtomicInt(42);

    Assert.That(atomicInt.Decrement(), Is.EqualTo(41));
    Assert.That((int)atomicInt, Is.EqualTo(41));

    Assert.That(atomicInt.Decrement(), Is.EqualTo(40));
    Assert.That((int)atomicInt, Is.EqualTo(40));

    Assert.That(atomicInt.Decrement(), Is.EqualTo(39));
    Assert.That((int)atomicInt, Is.EqualTo(39));

    Assert.That(atomicInt.Decrement(), Is.EqualTo(38));
    Assert.That((int)atomicInt, Is.EqualTo(38));
  }

  [Test]
  public void Exchange()
  {
    var atomicInt = new AtomicInt(456);

    Assert.That(atomicInt.Exchange(123), Is.EqualTo(456));
    Assert.That((int)atomicInt, Is.EqualTo(123));

    Assert.That(atomicInt.Exchange(123), Is.EqualTo(123));
    Assert.That((int)atomicInt, Is.EqualTo(123));

    Assert.That(atomicInt.Exchange(789), Is.EqualTo(123));
    Assert.That((int)atomicInt, Is.EqualTo(789));
  }

  [Test]
  public void Increment()
  {
    var atomicInt = new AtomicInt(42);

    Assert.That(atomicInt.Increment(), Is.EqualTo(43));
    Assert.That((int)atomicInt, Is.EqualTo(43));

    Assert.That(atomicInt.Increment(), Is.EqualTo(44));
    Assert.That((int)atomicInt, Is.EqualTo(44));

    Assert.That(atomicInt.Increment(), Is.EqualTo(45));
    Assert.That((int)atomicInt, Is.EqualTo(45));

    Assert.That(atomicInt.Increment(), Is.EqualTo(46));
    Assert.That((int)atomicInt, Is.EqualTo(46));
  }
}
