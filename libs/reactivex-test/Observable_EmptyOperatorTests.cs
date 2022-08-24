using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public class Observable_EmptyOperatorTests
{
  [Test]
  public async Task Observable_Empty_ShouldCompleteInstantly()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(_ => _.OnCompleted)
      .VerifyNoOtherInvocation();
  }
}
