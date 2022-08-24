using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public class Observable_SubscribeOperatorTests
{
  [Test]
  public async Task Observable_Subscribe_ShouldInterruptNotificationsAccordingToCancellationToken()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;
    var cts = new CancellationTokenSource();

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(1);
      observer.OnNext(2);
      observer.OnNext(3);
      cts.Cancel(); // simulate an external Cancel or Dispose
      observer.OnNext(4);
      observer.OnNext(5);
      observer.OnNext(6);
      observer.OnCompleted();
      return DummyDisposable.instance;
    });
    observable.Subscribe(observer, cts.Token);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnNext, 3)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }
}
