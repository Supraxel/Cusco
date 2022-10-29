using System;
using System.Collections;
using System.Threading.Tasks;
using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  private class Value: IEquatable<Value>
  {
    public readonly int index;

    public Value(int index)
    {
      this.index = index;
    }

    public bool Equals(Value other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return index == other.index;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Value)obj);
    }

    public override int GetHashCode()
    {
      return index;
    }
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_ShouldAllNextThenComplete()
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
      .DistinctUntilChanged();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 3)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_ShouldNextThenComplete()
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
      .DistinctUntilChanged();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_ShouldAllNextWithDuplicateValueThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<int>(DispatchQueue.main, observer =>
      {
        observer.OnNext(1);
        observer.OnNext(2);
        observer.OnNext(1);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .DistinctUntilChanged();
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_Equatable_ShouldAllNextThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<Value>(DispatchQueue.main, observer =>
      {
        observer.OnNext(new Value(3));
        observer.OnNext(new Value(2));
        observer.OnNext(new Value(1));
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .DistinctUntilChanged()
      .Map(x => x.index);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 3)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_Equatable_ShouldNextThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<Value>(DispatchQueue.main, observer =>
      {
        observer.OnNext(new Value(1));
        observer.OnNext(new Value(1));
        observer.OnNext(new Value(1));
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .DistinctUntilChanged()
      .Map(x => x.index);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_Equatable_ShouldAllNextWithDuplicateValueThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<Value>(DispatchQueue.main, observer =>
      {
        observer.OnNext(new Value(1));
        observer.OnNext(new Value(2));
        observer.OnNext(new Value(1));
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .DistinctUntilChanged()
      .Map(x => x.index);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_Predicate_ShouldNextAllWithNullValuesThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int?>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<Value>(DispatchQueue.main, observer =>
      {
        observer.OnNext(null);
        observer.OnNext(new Value(1));
        observer.OnNext(new Value(2));
        observer.OnNext(null);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .DistinctUntilChanged((x, y) => x?.index == y?.index)
      .Map(value => value?.index);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation<int?>(observer => observer.OnNext, null)
      .VerifyInvocation(observer => observer.OnNext, new int?(1))
      .VerifyInvocation(observer => observer.OnNext, new int?(2))
      .VerifyInvocation<int?>(observer => observer.OnNext, null)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_Predicate_ShouldNextThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<Value>(DispatchQueue.main, observer =>
      {
        observer.OnNext(new Value(1));
        observer.OnNext(new Value(1));
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .DistinctUntilChanged((x, y) => x.index == y.index)
      .Map(value => value.index);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_Predicate_ShouldNextAllWithDuplicateValuesThenComplete()
  {
    // arrange
    var observerMock = new Mock<IObserver<int>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<Value>(DispatchQueue.main, observer =>
      {
        observer.OnNext(new Value(1));
        observer.OnNext(new Value(2));
        observer.OnNext(new Value(1));
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .DistinctUntilChanged((x, y) => x.index == y.index)
      .Map(value => value.index);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnNext, 2)
      .VerifyInvocation(observer => observer.OnNext, 1)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_DistinctUntilChanged_Predicate_ShouldNextThenCompleteWithAllNullValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<int?>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Create<Value>(DispatchQueue.main, observer =>
      {
        observer.OnNext(null);
        observer.OnNext(null);
        observer.OnNext(null);
        observer.OnCompleted();
        return DummyDisposable.instance;
      })
      .DistinctUntilChanged((x, y) => x?.index == y?.index)
      .Map(value => value?.index);
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation<int?>(observer => observer.OnNext, null)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }
}
