namespace Cusco.Core.Test;

public class BoxTests
{
  private struct TestStruct
  {
    public string str;
    public int number;
  }

  [Test]
  public void ShouldBeNullIfSetToNil()
  {
    var box = Box<TestStruct>.nil;
    Assert.That(box, Is.Null);
  }

  [Test]
  public void ShouldBeNullIfSetToDefault()
  {
    var box = default(Box<TestStruct>);
    Assert.That(box, Is.Null);
  }

  [Test]
  public void AsRefShouldHaveTheInitialValue()
  {
    var box = new Box<TestStruct>(new TestStruct { number = 1, str = "foo" });
    Assert.That(box.asRef.number, Is.EqualTo(1));
    Assert.That(box.asRef.str, Is.EqualTo("foo"));
  }
}
