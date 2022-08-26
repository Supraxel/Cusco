using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public void Observable_Map_ShouldThrowIfTransformIsNull()
  {
    var observable = Observable.Empty<int>(DispatchQueue.main);
    Assert.Throws<ArgumentNullException>(() => observable.Map<double>(null));
  }

  [Test]
  public async Task Observable_Map_ShouldCompleteInstantlyIfSourceCompletesInstantly()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .Map(x => x * 2);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Map_ShouldNextWithOnlyTransformedValue()
  {
    const int sourceValue = 21;
    const int expectedValue = 42;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Just(DispatchQueue.main, sourceValue)
      .Map(x => x * 2);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Map_ShouldNextWithEachTransformedValues()
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
      .Map(x => x + 1);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnNext, 3)
      .VerifyInvocation(observer => observer.OnNext, 4)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public void Observable_Map_ShouldErrorWhenTransformThrows()
  {
    var expectedException = new Exception();

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
      .Map(x => x == 1 ? x + 1 : throw expectedException);
    observable.Subscribe(observer);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnError, expectedException)
      .VerifyNoOtherInvocation();
  }
}
