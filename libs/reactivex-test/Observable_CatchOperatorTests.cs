using System;
using System.Threading.Tasks;
using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{

  [Test]
  public async Task Observable_Catch_ShouldCatchError()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("1");
        observer.OnNext("2");
        observer.OnNext("3");
        observer.OnError(new InvalidDataException());
        return DummyDisposable.instance;
      })
      .Catch(exception => Observable.Just(DispatchQueue.main, "4"));

    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnNext, "3")
      .VerifyInvocation(observer => observer.OnNext, "4")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Catch_ShouldCatchErrorAndIgnoreNext()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("1");
        observer.OnNext("2");
        observer.OnError(new InvalidDataException());
        observer.OnNext("3");
        return DummyDisposable.instance;
      })
      .Catch(exception => Observable.Just(DispatchQueue.main, "4"));

    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnNext, "4")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Catch_ShouldCatchErrorAndThrowInCatch()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    var expectedException = new Exception();
    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("1");
        observer.OnNext("2");
        observer.OnError(new InvalidDataException());
        observer.OnNext("3");
        return DummyDisposable.instance;
      })
      .Catch(exception => throw expectedException);

    observable.Subscribe(observer);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnError, expectedException)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CatchAndResult_ShouldCatchError()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("1");
        observer.OnNext("2");
        observer.OnNext("3");
        observer.OnError(new InvalidDataException());
        return DummyDisposable.instance;
      })
      .CatchAndResult(exception => "4");

    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnNext, "3")
      .VerifyInvocation(observer => observer.OnNext, "4")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CatchAndResult_ShouldCatchErrorAndIgnoreNext()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("1");
        observer.OnNext("2");
        observer.OnError(new InvalidDataException());
        observer.OnNext("3");
        return DummyDisposable.instance;
      })
      .CatchAndResult(exception => "4");

    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnNext, "4")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CatchAndResult_ShouldCatchErrorAndThrowInCatch()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    var expectedException = new Exception();
    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("1");
        observer.OnNext("2");
        observer.OnError(new InvalidDataException());
        observer.OnNext("3");
        return DummyDisposable.instance;
      })
      .CatchAndResult(exception => throw expectedException);

    observable.Subscribe(observer);

    Assert.ThrowsAsync<Exception>(async () => await observable.LastOrDefaultAsFuture());

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnError, expectedException)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CatchAndReturn_ShouldCatchError()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("1");
        observer.OnNext("2");
        observer.OnNext("3");
        observer.OnError(new InvalidDataException());
        return DummyDisposable.instance;
      })
      .CatchAndReturn("4");

    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnNext, "3")
      .VerifyInvocation(observer => observer.OnNext, "4")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CatchAndReturn_ShouldCatchErrorAndIgnoreNext()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("1");
        observer.OnNext("2");
        observer.OnError(new InvalidDataException());
        observer.OnNext("3");
        return DummyDisposable.instance;
      })
      .CatchAndReturn("4");

    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnNext, "4")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }
}
