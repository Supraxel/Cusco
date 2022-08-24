using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public class Observable_JustOperatorTests
{
  [Test]
  public async Task Observable_Just_ShouldNextThenComplete()
  {
    const int expectedValue = 42;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Just(DispatchQueue.main, expectedValue);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }
}
