using System;
using System.Collections;
using System.Threading.Tasks;
using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public async Task Observable_Max_LengthComparer_ShouldNextThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("abce");
        observer.OnNext("abc");
        observer.OnNext("abcd");
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Max(new LengthStringComparer());
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "abce")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Max_StringComparerOrdinal_ShouldNextThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<string>(DispatchQueue.main, observer =>
      {
        observer.OnNext("xyz");
        observer.OnNext("abc");
        observer.OnNext("bc");
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Max(StringComparer.Ordinal);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "xyz")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Max_ShouldNextThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(3);
        observer.OnNext(2);
        observer.OnNext(1);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Max();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 3)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Max_Comparable_ShouldNextThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(3);
        observer.OnNext(1);
        observer.OnNext(2);
        observer.OnNext(-5);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Max((x, y) => x - y);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 3)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Max_ShouldNextThenCompleteWithDuplicateValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(1);
        observer.OnNext(2);
        observer.OnNext(2);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Max();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Max_ShouldNextThenCompleteWithSameValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(1);
        observer.OnNext(1);
        observer.OnNext(1);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Max();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Max_ShouldNextThenCompleteWithOrderedValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(1);
        observer.OnNext(2);
        observer.OnNext(3);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Max();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 3)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Max_ShouldCompleteInstantly()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Max();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Max_ShouldNeverComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Never<int>(DispatchQueue.main)
      .Max();
    observable.Subscribe(observer);

    await Task.Delay(100);

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyNoOtherInvocation();
  }
}
