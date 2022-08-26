namespace Cusco.ReactiveX.Test;

public sealed class NotificationTests
{
  [Test]
  public void Notification_Completed_HasProperFlags()
  {
    // act
    var notification = Notification<int>.Completed();

    // assert
    Assert.That(notification.isComplete, Is.True);
    Assert.That(notification.isErr, Is.False);
    Assert.That(notification.isNext, Is.False);
    Assert.That(notification.error.isNone, Is.True);
    Assert.That(notification.next.isNone, Is.True);
  }

  [Test]
  public void Notification_WithError_HasProperFlags()
  {
    // act
    var notification = Notification<int>.WithError(new Exception());

    // assert
    Assert.That(notification.isComplete, Is.False);
    Assert.That(notification.isErr, Is.True);
    Assert.That(notification.isNext, Is.False);
    Assert.That(notification.error.isSome, Is.True);
    Assert.That(notification.next.isNone, Is.True);
  }

  [Test]
  public void Notification_WithNextValue_HasProperFlags()
  {
    // act
    var notification = Notification<int>.WithNextValue(42);

    // assert
    Assert.That(notification.isComplete, Is.False);
    Assert.That(notification.isErr, Is.False);
    Assert.That(notification.isNext, Is.True);
    Assert.That(notification.error.isNone, Is.True);
    Assert.That(notification.next.isSome, Is.True);
  }
}
