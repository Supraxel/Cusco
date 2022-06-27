namespace Cusco.LowLevel.Test;

public class PinnedInstanceTests
{
  [Test]
  public void Pin()
  {
    var instance = new List<int> { 42 };
    var pin = PinnedInstance.Pin(instance);
    Assert.That(pin.isValid, Is.True);

    Assert.That(pin.instance, Is.EqualTo(instance));
    Assert.That(pin.instance, Is.Not.SameAs(new List<int> { instance[0] }));

    pin.Unpin();
    Assert.That(pin.isValid, Is.False);
  }
}
