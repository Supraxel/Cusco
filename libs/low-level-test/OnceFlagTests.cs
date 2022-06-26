namespace Cusco.LowLevel.Test;

public class OnceFlagTests
{
  [Test]
  public void CallMethodOnce()
  {
    var calls = 0;

    void ToBeCalledOnce()
    {
      calls++;
    }

    var onceFlag = new OnceFlag();

    Assert.That(calls, Is.EqualTo(0));
    onceFlag.Do(ToBeCalledOnce);
    Assert.That(calls, Is.EqualTo(1));
    onceFlag.Do(ToBeCalledOnce);
    Assert.That(calls, Is.EqualTo(1));
  }

  [Test]
  public void CallOnceEvenWithDifferentMethods()
  {
    var calls = 0;

    void ToBeCalledOnce()
    {
      calls++;
    }

    void ShouldNotBeCalled()
    {
      calls = -1;
    }

    var onceFlag = new OnceFlag();

    Assert.That(calls, Is.EqualTo(0));
    onceFlag.Do(ToBeCalledOnce);
    Assert.That(calls, Is.EqualTo(1));
    onceFlag.Do(ShouldNotBeCalled);
    Assert.That(calls, Is.EqualTo(1));
  }
}
