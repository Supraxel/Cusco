using System;
using System.Threading.Tasks;
using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public void Observable_CompactMap_ShouldThrowIfTransformIsNull()
  {
    var observable = Observable.Empty<string>(DispatchQueue.main);
    Assert.Throws<ArgumentNullException>(() => observable.CompactMap<string>(null));
  }

  [Test]
  public async Task Observable_CompactMap_ShouldCompleteInstantlyIfSourceCompletesInstantly()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Empty<int>(DispatchQueue.main)
      .CompactMap<string>(x => x.ToString());
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CompactMap_ShouldNextWithOnlyTransformedValue()
  {
    const int sourceValue = 21;

    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
    var observer = observerMock.Object;

    // act
    var observable = Observable.Just(DispatchQueue.main, sourceValue)
      .CompactMap(x => x.ToString());
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "21")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CompactMap_ShouldNextWithEachTransformedValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
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
      .CompactMap(x => x.ToString());
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "2")
      .VerifyInvocation(observer => observer.OnNext, "3")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }

  [Test]
  public async Task Observable_CompactMap_ShouldNextWithEachTransformedValuesExceptNullValues()
  {
    // arrange
    var observerMock = new Mock<IObserver<string>>().SetupAllProperties();
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
      .CompactMap(x => x % 2 == 0 ? null : x.ToString());
    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(observer => observer.OnNext, "1")
      .VerifyInvocation(observer => observer.OnNext, "3")
      .VerifyInvocation(observer => observer.OnCompleted)
      .VerifyNoOtherInvocation();
  }
}
