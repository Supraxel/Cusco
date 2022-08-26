using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  #region Last

  [Test]
  public async Task Observable_Last_ShouldNextThenComplete()
  {
    const int expectedValue = 42;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue - 2);
        observer.OnNext(expectedValue - 1);
        observer.OnNext(expectedValue);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Last();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_EmptyLast_ShouldCompleteInstantly()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .Last();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  #endregion // Last

  #region LastOrDefault

  [Test]
  public async Task Observable_LastOrDefault_ShouldNextWithNonDefaultThenComplete()
  {
    const int expectedValue = 42;
    const int unexpectedValue = 21;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue - 2);
        observer.OnNext(expectedValue - 1);
        observer.OnNext(expectedValue);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .LastOrDefault(unexpectedValue);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_EmptyLastOrDefault_ShouldNextWithDefaultThenComplete()
  {
    const int expectedValue = 42;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .LastOrDefault(expectedValue);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  #endregion // LastOrDefault

  #region LastAsFuture

  [Test]
  public async Task Observable_LastAsFuture_ShouldReturnSomeValue()
  {
    const int expectedValue = 42;

    // act
    var value = await Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue - 2);
        observer.OnNext(expectedValue - 1);
        observer.OnNext(expectedValue);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .LastAsFuture();

    // assert
    Assert.That(value.isSome, Is.True);
    Assert.That(value.isNone, Is.False);
    Assert.That(value.Unwrap(), Is.EqualTo(expectedValue));
  }

  [Test]
  public async Task Observable_EmptyLastAsFuture_ShouldReturnNoneValue()
  {
    // act
    var value = await Observable.Empty<int>(DispatchQueue.main)
      .LastAsFuture();

    // assert
    Assert.That(value.isNone, Is.True);
    Assert.That(value.isSome, Is.False);
  }

  #endregion // LastAsFuture

  #region LastOrDefaultAsFuture

  [Test]
  public async Task Observable_LastOrDefaultAsFuture_ShouldReturnNonDefaultValue()
  {
    const int expectedValue = 42;
    const int unexpectedValue = 21;

    // act
    var value = await Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(expectedValue - 2);
        observer.OnNext(expectedValue - 1);
        observer.OnNext(expectedValue);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .LastOrDefaultAsFuture(unexpectedValue);

    // assert
    Assert.That(value, Is.EqualTo(expectedValue));
  }

  [Test]
  public async Task Observable_EmptyLastOrDefaultAsFuture_ShouldReturnDefaultValue()
  {
    const int expectedValue = 42;

    // act
    var value = await Observable.Empty<int>(DispatchQueue.main)
      .LastOrDefaultAsFuture(expectedValue);

    // assert
    Assert.That(value, Is.EqualTo(expectedValue));
  }

  #endregion // LastOrDefaultAsFuture
}
