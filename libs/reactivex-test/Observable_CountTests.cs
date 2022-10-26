using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  #region Count operator

  [Test]
  public async Task Observable_Count_ShouldReturnErrorForErroringSequence()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;
    var expectedException = new Exception();

    // act
    var observable = Observable.Throw<int>(DispatchQueue.main, expectedException)
      .Count();
    observable.Subscribe(observer);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnError, expectedException)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Count_ShouldReturnZeroForEmptySequence()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .Count();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 0ul)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Count_ShouldReturnOneForJustSequence()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Just(DispatchQueue.main, 42)
      .Count();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1ul)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Count_ShouldReturnThreeForASequenceOfThreeElements()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(-1);
        observer.OnNext(0);
        observer.OnNext(int.MaxValue);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Count();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 3ul)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  #endregion

  #region Count operator with predicate

  [Test]
  public async Task Observable_CountWithPredicate_ShouldReturnErrorForErroringSequence()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;
    var expectedException = new Exception();

    // act
    var observable = Observable.Throw<int>(DispatchQueue.main, expectedException)
      .Count(value => value % 2 == 0);
    observable.Subscribe(observer);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnError, expectedException)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CountWithPredicate_ShouldReturnZeroForEmptySequence()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .Count(value => value % 2 == 0);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 0ul)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CountWithPredicate_ShouldReturnOneForJustSequenceWithMatchingPredicate()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Just(DispatchQueue.main, 42)
      .Count(value => value % 2 == 0);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1ul)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CountWithPredicate_ShouldReturnZeroForJustSequenceWithNonMatchingPredicate()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Just(DispatchQueue.main, 43)
      .Count(value => value % 2 == 0);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 0ul)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CountWithPredicate_ShouldReturnThreeForASequenceOfThreeElements()
  {
    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(56);
        observer.OnNext(23);
        observer.OnNext(0);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Count(value => value % 2 == 0);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 2ul)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  #endregion
}
