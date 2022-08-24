using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public class Observable_ThrowOperatorTests
{
  [Test]
  public void Observable_Throw_ShouldError()
  {
    var expectedValue = new Exception();

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Throw<int>(DispatchQueue.main, expectedValue);
    observable.Subscribe(observer);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnError, expectedValue)
      .VerifyNoOtherInvocation();
  }
}
