using System.Diagnostics;
using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public void Observable_Debounce_ShouldThrowIfDelayIsLessThanZero()
  {
    var observable = Observable.Empty<int>(DispatchQueue.main);

    Assert.Throws<ArgumentOutOfRangeException>(() => observable.Debounce(TimeSpan.FromSeconds(-1)));
  }

  [Test]
  public void Observable_Debounce_ShouldReturnTheOriginalObservableIfDelayIsExactlyZero()
  {
    var initialObservable = Observable.Empty<int>(DispatchQueue.main);
    var debouncedObservable = initialObservable.Debounce(TimeSpan.FromSeconds(0));

    Assert.That(debouncedObservable, Is.SameAs(initialObservable));
  }

  [Test]
  public async Task Observable_JustDebounce_ShouldNextValueInstantly()
  {
    const int expectedValue = 42;
    var expectedDelay = TimeSpan.FromSeconds(0.5);

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    var observable = Observable.Just(DispatchQueue.main, expectedValue)
      .Debounce(expectedDelay);

    // act
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Debounce_ShouldNextFirstValueDiscardSecondValue()
  {
    const int expectedValue = 42;
    var expectedDelay = TimeSpan.FromSeconds(0.5);

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue);
        observer.OnNext(expectedValue+1);
        return DummyDisposable.instance;
      })
      .Debounce(expectedDelay);

    // act
    observable.Subscribe(observer);

    await observable.FirstOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Debounce_ShouldNextFirstValueDiscardValuesDuringCooldownThenNextAnotherValue()
  {
    const int expectedValue = 42;
    const int expectedValue2 = 4242;
    var expectedDelay = TimeSpan.FromSeconds(0.5);

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue);
        observer.OnNext(expectedValue+1);
        observer.OnNext(expectedValue+2);
        observer.OnNext(expectedValue+3);
        Thread.Sleep(expectedDelay * 1.1);
        observer.OnNext(expectedValue2);
        return DummyDisposable.instance;
      })
      .Debounce(expectedDelay);

    // act
    observable.Subscribe(observer);
    await observable.FirstOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnNext, expectedValue2)
      .VerifyNoOtherInvocation();
  }
}
