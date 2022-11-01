using System;
using System.Threading.Tasks;
using Cusco.Dispatch;
using Cusco.LowLevel;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public void Observable_CombineLatest_ShouldThrowIfOtherIsNull()
  {
    Observable<string> sourceA = Observable.Empty<string>(DispatchQueue.main);
    Observable<string> sourceB = null;
    Func<string, string, string> combiner = (lhs, rhs) => $"{lhs} {rhs}";
    Assert.Throws<ArgumentNullException>(() => sourceA.CombineLatest(sourceB, combiner));
  }

  [Test]
  public void Observable_CombineLatest_ShouldThrowIfCombinerIsNull()
  {
    Observable<string> sourceA = Observable.Empty<string>(DispatchQueue.main);
    Observable<string> sourceB = Observable.Empty<string>(DispatchQueue.main);
    Func<string, string, string> combiner = null;
    Assert.Throws<ArgumentNullException>(() => sourceA.CombineLatest(sourceB, combiner));
  }

  [Test]
  public async Task Observable_CombineLatest_ShouldCombineValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    List<IObserver<string>> sourceObserversA = new();
    List<IObserver<string>> sourceObserversB = new();
    var sourceA = Observable.Create<string>(DispatchQueue.main, (observer) =>
    {
      sourceObserversA.Add(observer);
      return DummyDisposable.instance;
    });
    var sourceB = Observable.Create<string>(DispatchQueue.main, (observer) =>
    {
      sourceObserversB.Add(observer);
      return DummyDisposable.instance;
    });
    var observable = sourceA
      .CombineLatest(sourceB, (lhs, rhs) => $"{lhs}{rhs}");
    observable.Subscribe(observer);

    var actEndBarrier = observable.LastOrDefaultAsFuture();

    while (sourceObserversA.Count != 2 || sourceObserversB.Count != 2)
    {
      Thread.Yield();
    }
    sourceObserversA.ForEach(observer => observer.OnNext("A"));
    sourceObserversA.ForEach(observer => observer.OnNext("B"));
    sourceObserversB.ForEach(observer => observer.OnNext("1"));
    sourceObserversB.ForEach(observer => observer.OnNext("2"));
    sourceObserversA.ForEach(observer => observer.OnNext("C"));
    sourceObserversA.ForEach(observer => observer.OnNext("D"));
    sourceObserversA.ForEach(observer => observer.OnCompleted());
    sourceObserversB.ForEach(observer => observer.OnNext("3"));
    sourceObserversB.ForEach(observer => observer.OnNext("4"));
    sourceObserversB.ForEach(observer => observer.OnCompleted());

    await actEndBarrier;

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "B1")
      .VerifyInvocation(observer => observer.OnNext, "B2")
      .VerifyInvocation(observer => observer.OnNext, "C2")
      .VerifyInvocation(observer => observer.OnNext, "D2")
      .VerifyInvocation(observer => observer.OnNext, "D3")
      .VerifyInvocation(observer => observer.OnNext, "D4")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CombineLatest_ShouldErrorWhenObservableErrors()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    var expectedException = new ArithmeticException();

    // act
    List<IObserver<string>> sourceObserversA = new();
    List<IObserver<string>> sourceObserversB = new();
    var sourceA = Observable.Create<string>(DispatchQueue.main, (observer) =>
    {
      sourceObserversA.Add(observer);
      return DummyDisposable.instance;
    });
    var sourceB = Observable.Create<string>(DispatchQueue.main, (observer) =>
    {
      sourceObserversB.Add(observer);
      return DummyDisposable.instance;
    });
    var observable = sourceA
      .CombineLatest(sourceB, (lhs, rhs) => $"{lhs}{rhs}");
    observable.Subscribe(observer);

    var actEndBarrier = observable.LastOrDefaultAsFuture();

    while (sourceObserversA.Count != 2 || sourceObserversB.Count != 2)
    {
      Thread.Yield();
    }

    sourceObserversA.ForEach(observer => observer.OnNext("A"));
    sourceObserversA.ForEach(observer => observer.OnNext("B"));
    sourceObserversB.ForEach(observer => observer.OnNext("1"));
    sourceObserversA.ForEach(observer => observer.OnError(expectedException));

    Assert.ThrowsAsync<ArithmeticException>(async () => await actEndBarrier);

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "B1")
      .VerifyInvocation(observer => observer.OnError, expectedException)
      .VerifyNoOtherInvocation();
  }
}
