using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  #region Do

  [Test]
  public void Observable_Do_ThrowsIfCallbackIsNull()
  {
    var observable = Observable.Empty<int>(DispatchQueue.main);
    Assert.Throws<ArgumentNullException>(() => observable.Do(null));
  }

  [Test]
  public async Task Observable_Do_ShouldTriggerNextAndCompleteNotifications()
  {
    const int expectedValue = 42;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoCallback<int>>().SetupAllProperties();

    // act
    var observable = Observable.Just(DispatchQueue.main, expectedValue);

    observable
      .Do(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyInvocation(callbackMock => callbackMock.Invoke, Notification<int>.WithNextValue(42))
      .VerifyInvocation(callbackMock => callbackMock.Invoke, Notification<int>.Completed())
      .VerifyNoOtherInvocation();
  }

  [Test]
  public void Observable_ThrowDo_ShouldTriggerErrorNotification()
  {
    var expectedValue = new Exception();

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoCallback<int>>().SetupAllProperties();

    // act
    var observable = Observable.Throw<int>(DispatchQueue.main, expectedValue);

    observable
      .Do(callbackMock.Object)
      .Subscribe(observerMock.Object);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyInvocation(callbackMock => callbackMock.Invoke, Notification<int>.WithError(expectedValue))
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_EmptyDo_ShouldTriggerCompleteNotification()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoCallback<int>>().SetupAllProperties();

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main);

    observable
      .Do(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyInvocation(callbackMock => callbackMock.Invoke, Notification<int>.Completed())
      .VerifyNoOtherInvocation();
  }

  #endregion // Do

  #region DoOnComplete

  [Test]
  public void Observable_DoOnComplete_ThrowsIfCallbackIsNull()
  {
    var observable = Observable.Empty<int>(DispatchQueue.main);
    Assert.Throws<ArgumentNullException>(() => observable.DoOnComplete(null));
  }

  [Test]
  public async Task Observable_EmptyDoOnComplete_ShouldTriggerOnce()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnCompleteCallback>().SetupAllProperties();

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main);

    observable
      .DoOnComplete(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyInvocation(doCallback => doCallback.Invoke)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DoOnComplete_ShouldTriggerOnce()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnCompleteCallback>().SetupAllProperties();

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnCompleted();
      observer.OnCompleted();
      return DummyDisposable.instance;
    });

    observable
      .DoOnComplete(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyInvocation(doCallback => doCallback.Invoke)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public void Observable_ThrowDoOnComplete_ShouldNeverTrigger()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnCompleteCallback>().SetupAllProperties();

    // act
    var observable = Observable.Throw<int>(DispatchQueue.main, new Exception());

    observable
      .DoOnComplete(callbackMock.Object)
      .Subscribe(observerMock.Object);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyNoOtherInvocation();
  }

  #endregion // DoOnComplete

  #region DoOnError

  [Test]
  public void Observable_DoOnError_ThrowsIfCallbackIsNull()
  {
    var observable = Observable.Empty<int>(DispatchQueue.main);
    Assert.Throws<ArgumentNullException>(() => observable.DoOnError(null));
  }

  [Test]
  public async Task Observable_EmptyDoOnError_ShouldNeverTrigger()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnErrorCallback>().SetupAllProperties();

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main);

    observable
      .DoOnError(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_JustDoOnError_ShouldNeverTrigger()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnErrorCallback>().SetupAllProperties();

    // act
    var observable = Observable.Just(DispatchQueue.main, 42);

    observable
      .DoOnError(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public void Observable_ThrowDoOnError_ShouldTriggerOnce()
  {
    var expectedValue = new Exception();

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnErrorCallback>().SetupAllProperties();

    // act
    var observable = Observable.Throw<int>(DispatchQueue.main, expectedValue);

    observable
      .DoOnError(callbackMock.Object)
      .Subscribe(observerMock.Object);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyInvocation(callbackMock => callbackMock.Invoke, expectedValue)
      .VerifyNoOtherInvocation();
  }

  #endregion // DoOnError

  #region DoOnNext

  [Test]
  public void Observable_DoOnNext_ThrowsIfCallbackIsNull()
  {
    var observable = Observable.Empty<int>(DispatchQueue.main);
    Assert.Throws<ArgumentNullException>(() => observable.DoOnNext(null));
  }

  [Test]
  public async Task Observable_EmptyDoOnNext_ShouldNeverTrigger()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnNextCallback<int>>().SetupAllProperties();

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main);

    observable
      .DoOnNext(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_JustDoOnNext_ShouldTriggerOnce()
  {
    const int expectedValue = 42;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnNextCallback<int>>().SetupAllProperties();

    // act
    var observable = Observable.Just(DispatchQueue.main, expectedValue);

    observable
      .DoOnNext(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyInvocation(callbackMock => callbackMock.Invoke, expectedValue)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_JustDoOnNext_ShouldTriggerForEachNextValue()
  {
    const int expectedValue = 42;
    const int expectedValue2 = -564;
    const int expectedValue3 = 319745;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnNextCallback<int>>().SetupAllProperties();

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(expectedValue);
      observer.OnNext(expectedValue2);
      observer.OnNext(expectedValue3);
      observer.OnCompleted();
      return DummyDisposable.instance;
    });

    observable
      .DoOnNext(callbackMock.Object)
      .Subscribe(observerMock.Object);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyInvocation(callbackMock => callbackMock.Invoke, expectedValue)
      .VerifyInvocation(callbackMock => callbackMock.Invoke, expectedValue2)
      .VerifyInvocation(callbackMock => callbackMock.Invoke, expectedValue3)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public void Observable_ThrowDoOnNext_ShouldNeverTrigger()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var callbackMock = new Mock<DoOnNextCallback<int>>().SetupAllProperties();

    // act
    var observable = Observable.Throw<int>(DispatchQueue.main, new Exception());

    observable
      .DoOnNext(callbackMock.Object)
      .Subscribe(observerMock.Object);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(callbackMock)
      .VerifyNoOtherInvocation();
  }

  #endregion // DoOnNext

}
