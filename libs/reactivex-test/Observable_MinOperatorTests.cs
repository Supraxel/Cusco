using System;
using System.Collections;
using System.Threading.Tasks;
using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{

  class LengthStringComparer : IComparer<string>
  {
    public int Compare(string? x, string? y)
    {
      return x.Length - y.Length;
    }
  }

  [Test]
  public async Task Observable_Min_LengthComparer_ShouldNextThenComplete()
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
      .Min(new LengthStringComparer());
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "abc")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Min_StringComparerOrdinal_ShouldNextThenComplete()
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
      .Min(StringComparer.Ordinal);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "abc")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Min_ShouldNextThenComplete()
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
      .Min();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Min_Comparable_ShouldNextThenComplete()
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
      .Min((x, y) => x - y);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, -5)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Min_ShouldNextThenCompleteWithDuplicateValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(2);
        observer.OnNext(1);
        observer.OnNext(1);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .Min();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Min_ShouldNextThenCompleteWithSameValues()
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
      .Min();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Min_ShouldCompleteInstantly()
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
      .Min();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_Min_ShouldNeverComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Never<int>(DispatchQueue.main)
      .Min();
    observable.Subscribe(observer);

    await Task.Delay(100);

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyNoOtherInvocation();
  }
}
