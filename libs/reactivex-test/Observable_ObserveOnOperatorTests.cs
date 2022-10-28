using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public async Task Observable_ObserveOn_ShouldNextWithEachTransformedValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(1);
        observer.OnNext(2);
        observer.OnNext(3);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .ObserveOn(DispatchQueue.main);
    observable.Subscribe(observer);

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
