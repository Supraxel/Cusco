using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public class Observable_FirstOperatorTests
{
  #region First

  [Test]
  public async Task Observable_First_ShouldNextThenComplete()
  {
    const int expectedValue = 42;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue);
        observer.OnNext(expectedValue + 1);
        observer.OnNext(expectedValue + 2);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .First();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_EmptyFirst_ShouldCompleteInstantly()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .First();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  #endregion // First

  #region FirstOrDefault

  [Test]
  public async Task Observable_FirstOrDefault_ShouldNextWithNonDefaultThenComplete()
  {
    const int expectedValue = 42;
    const int unexpectedValue = 21;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue);
        observer.OnNext(expectedValue + 1);
        observer.OnNext(expectedValue + 2);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .FirstOrDefault(unexpectedValue);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_EmptyFirstOrDefault_ShouldNextWithDefaultThenComplete()
  {
    const int expectedValue = 42;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .FirstOrDefault(expectedValue);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  #endregion // FirstOrDefault

  #region FirstAsFuture

  [Test]
  public async Task Observable_FirstAsFuture_ShouldReturnSomeValue()
  {
    const int expectedValue = 42;

    // act
    var value = await Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue);
        observer.OnNext(expectedValue + 1);
        observer.OnNext(expectedValue + 2);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .FirstAsFuture();

    // assert
    Assert.That(value.isSome, Is.True);
    Assert.That(value.isNone, Is.False);
    Assert.That(value.Unwrap(), Is.EqualTo(expectedValue));
  }

  [Test]
  public async Task Observable_EmptyFirstAsFuture_ShouldReturnNoneValue()
  {
    // act
    var value = await Observable.Empty<int>(DispatchQueue.main)
      .FirstAsFuture();

    // assert
    Assert.That(value.isNone, Is.True);
    Assert.That(value.isSome, Is.False);
  }

  #endregion // FirstAsFuture

  #region FirstOrDefaultAsFuture

  [Test]
  public async Task Observable_FirstOrDefaultAsFuture_ShouldReturnNonDefaultValue()
  {
    const int expectedValue = 42;
    const int unexpectedValue = 21;

    // act
    var value = await Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue);
        observer.OnNext(expectedValue + 1);
        observer.OnNext(expectedValue + 2);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .FirstOrDefaultAsFuture(unexpectedValue);

    // assert
    Assert.That(value, Is.EqualTo(expectedValue));
  }

  [Test]
  public async Task Observable_EmptyFirstOrDefaultAsFuture_ShouldReturnDefaultValue()
  {
    const int expectedValue = 42;

    // act
    var value = await Observable.Empty<int>(DispatchQueue.main)
      .FirstOrDefaultAsFuture(expectedValue);

    // assert
    Assert.That(value, Is.EqualTo(expectedValue));
  }

  #endregion // FirstOrDefaultAsFuture
}
