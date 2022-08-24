using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public class Observable_CreateOperatorTests
{
  #region ShouldPreventDoubleComplete

  [Test]
  public async Task Observable_Create_ShouldPreventDoubleComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnCompleted();
      observer.OnCompleted();
      return DummyDisposable.instance;
    });
    observable.Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(_ => _.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public void Observable_Create_ShouldPreventDoubleCompleteWithErrorPre()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var expectedExc = new Exception();

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnError(expectedExc);
      observer.OnCompleted();
      return DummyDisposable.instance;
    });
    observable.Subscribe(observerMock.Object);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(_ => _.OnError, expectedExc)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Create_ShouldPreventDoubleCompleteWithErrorPost()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnCompleted();
      observer.OnError(new Exception());
      return DummyDisposable.instance;
    });
    observable.Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(_ => _.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Create_ShouldPreventDoubleCompleteWithNextPre()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var expectedValue = 42;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(expectedValue);
      observer.OnCompleted();
      return DummyDisposable.instance;
    });
    observable.Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(_ => _.OnNext, expectedValue)
      .VerifyInvocation(_ => _.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Create_ShouldPreventDoubleCompleteWithNextPost()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnCompleted();
      observer.OnNext(42);
      return DummyDisposable.instance;
    });
    observable.Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(_ => _.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  #endregion // ShouldPreventDoubleComplete

  #region ShouldNotifyForEachNextValue

  [Test]
  public async Task Observable_Create_ShouldNotifyForEachNextValue()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var expectedValues = new int[] { 1, 12, -414, 42, Int32.MinValue };

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      foreach (var value in expectedValues)
        observer.OnNext(value);
      observer.OnCompleted();
      return DummyDisposable.instance;
    });
    observable.Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    var seq = CallSequence.ForMock(observerMock);
    foreach (var expectedValue in expectedValues)
    {
      seq.VerifyInvocation(_ => _.OnNext, expectedValue);
    }

    seq
      .VerifyInvocation(_ => _.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  #endregion
}
