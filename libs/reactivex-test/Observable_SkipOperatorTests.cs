using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public async Task Observable_EmptySkip_ShouldCompleteInstantlyForAnyCount([Values(1UL, 2UL, 10UL)] ulong count)
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .Skip(count);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Skip_ShouldCompleteInstantlyForCountHigherThanNotificationsCount([Values(3UL, 5UL, 10UL)] ulong count)
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(42);
        observer.OnNext(21);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Skip(count);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public void Observable_Skip_ShouldReturnSameObservableWhenCountIsZero()
  {
    // act
    Observable<int> initialObservable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(42);
      observer.OnNext(21);
      observer.OnCompleted();
      return DummyDisposable.instance;
    });

    var skippedObservable = initialObservable.Skip(0);

    Assert.That(skippedObservable, Is.SameAs(initialObservable));
  }

  [Test]
  public async Task Observable_Skip_ShouldNextThreeTimesInsteadOfFive()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    Observable<int> observable = Observable.Create<int>(DispatchQueue.main, observer =>
    {
      observer.OnNext(1);
      observer.OnNext(2);
      observer.OnNext(3);
      observer.OnNext(4);
      observer.OnNext(5);
      observer.OnCompleted();
      return DummyDisposable.instance;
    })
      .Skip(2);

    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 3)
      .VerifyInvocation(observer => observer.OnNext, 4)
      .VerifyInvocation(observer => observer.OnNext, 5)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }
}
