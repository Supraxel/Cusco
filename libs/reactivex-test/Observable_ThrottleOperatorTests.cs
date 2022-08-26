using System.Diagnostics;
using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public void Observable_Throttle_ShouldThrowIfDelayIsLessThanZero()
  {
    var observable = Observable.Empty<int>(DispatchQueue.main);

    Assert.Throws<ArgumentOutOfRangeException>(() => observable.Throttle(TimeSpan.FromSeconds(-1)));
  }

  [Test]
  public void Observable_Throttle_ShouldReturnTheOriginalObservableIfDelayIsExactlyZero()
  {
    var initialObservable = Observable.Empty<int>(DispatchQueue.main);
    var throttledObservable = initialObservable.Throttle(TimeSpan.FromSeconds(0));

    Assert.That(throttledObservable, Is.SameAs(initialObservable));
  }

  [Test]
  public async Task Observable_Throttle_ShouldDelaySingleNextValue()
  {
    const int expectedValue = 42;
    var expectedDelay = TimeSpan.FromSeconds(0.5);

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue);
        return DummyDisposable.instance;
      })
      .Throttle(expectedDelay);

    // act
    var stopwatch = Stopwatch.StartNew();
    observable.Subscribe(observer);

    await observable.FirstOrDefaultAsFuture();
    var elapsedDelay = stopwatch.Elapsed;

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyNoOtherInvocation();

    Assert.That(expectedDelay, Is.LessThanOrEqualTo(elapsedDelay));
  }

  [Test]
  public async Task Observable_Throttle_ShouldDiscardFirstValueAndDelaySecondValue()
  {
    const int expectedValue = 42;
    var expectedDelay = TimeSpan.FromSeconds(0.5);

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue - 1);
        observer.OnNext(expectedValue);
        return DummyDisposable.instance;
      })
      .Throttle(expectedDelay);

    // act
    var stopwatch = Stopwatch.StartNew();
    observable.Subscribe(observer);

    await observable.FirstOrDefaultAsFuture();
    var elapsedDelay = stopwatch.Elapsed;

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyNoOtherInvocation();

    Assert.That(expectedDelay, Is.LessThanOrEqualTo(elapsedDelay));
  }

  [Test]
  public async Task Observable_Throttle_ShouldDiscardFirstValueAndDelaySecondValue2()
  {
    const int expectedValue = 42;
    var expectedDelay = TimeSpan.FromSeconds(0.5);

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue - 1);
        Thread.Sleep(expectedDelay / 3);
        observer.OnNext(expectedValue);
        return DummyDisposable.instance;
      })
      .Throttle(expectedDelay);

    // act
    var stopwatch = Stopwatch.StartNew();
    observable.Subscribe(observer);

    await observable.FirstOrDefaultAsFuture();
    var elapsedDelay = stopwatch.Elapsed;

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyNoOtherInvocation();

    Assert.That(expectedDelay, Is.LessThanOrEqualTo(elapsedDelay));
  }
}
