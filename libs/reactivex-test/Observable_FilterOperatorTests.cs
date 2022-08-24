using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public void Observable_Filter_ShouldThrowIfPredicateIsNull()
  {
    var observable = Observable.Empty<int>(DispatchQueue.main);

    Assert.Throws<ArgumentNullException>(() => observable.Filter(null));
  }

  [Test]
  public async Task Observable_Filter_ShouldNextWithValuesPassingPredicateThenComplete()
  {
    const int expectedValue = 42;
    const int expectedValue2 = expectedValue * 3;
    const int expectedValue3 = expectedValue * 2;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(1);
      observer.OnNext(expectedValue);
      observer.OnNext(51);
      observer.OnNext(75);
      observer.OnNext(expectedValue2);
      observer.OnNext(795);
      observer.OnNext(expectedValue3);
      observer.OnCompleted();
      return DummyDisposable.instance;
    }).Filter(x => x % expectedValue == 0);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnNext, expectedValue2)
      .VerifyInvocation(observer => observer.OnNext, expectedValue3)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Filter_ShouldCompleteInstantlyBecauseOfRejectAllPredicate()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(1);
      observer.OnNext(42);
      observer.OnNext(795);
      observer.OnCompleted();
      return DummyDisposable.instance;
    }).Filter(x => false);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Filter_ShouldNextWithValuesPassingInversedPredicateThenComplete()
  {
    const int expectedValue = 42;
    const int expectedValue2 = expectedValue * 3;
    const int expectedValue3 = expectedValue * 2;

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(1);
      observer.OnNext(expectedValue);
      observer.OnNext(51);
      observer.OnNext(75);
      observer.OnNext(expectedValue2);
      observer.OnNext(795);
      observer.OnNext(expectedValue3);
      observer.OnCompleted();
      return DummyDisposable.instance;
    }).Filter(Predicate.Not<int>(x => x % expectedValue != 0));
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, expectedValue)
      .VerifyInvocation(observer => observer.OnNext, expectedValue2)
      .VerifyInvocation(observer => observer.OnNext, expectedValue3)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public void Observable_Filter_ShouldErrorIfPredicateThrows()
  {
    const int expectedValue = 1;
    var expectedException = new Exception();

    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(expectedValue);
      observer.OnNext(42);
      observer.OnNext(795);
      observer.OnCompleted();
      return DummyDisposable.instance;
    }).Filter(x => x == expectedValue ? true : throw expectedException);
    observable.Subscribe(observer);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnError, expectedException)
      .VerifyNoOtherInvocation();
  }
}
