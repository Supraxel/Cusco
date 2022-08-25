using Cusco.Dispatch;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public async Task Observable_Interval_ShouldNextThreeTimesWithDelay()
  {
    var nextInstanciations = new List<Tuple<DateTimeOffset, ulong>>();
    var expectedDelay = TimeSpan.FromMilliseconds(100);

    // arrange
    var observerMock = new Mock<IObserver<ulong>>().SetupAllProperties();
    observerMock.Setup(m => m.OnNext(It.IsAny<ulong>()))
      .Callback<ulong>(value => nextInstanciations.Add(new(DateTimeOffset.UtcNow, value)));
    var observer = observerMock.Object;

    // act
    var observable = Observable
      .Interval(DispatchQueue.main, expectedDelay)
      .Take(3);

    observable.Subscribe(observer);

    await observable.LastOrDefaultAsFuture();

    // assert
    CallSequence.ForMock(observerMock)
      .VerifyInvocation(_ => _.OnNext, 0ul)
      .VerifyInvocation(_ => _.OnNext, 1ul)
      .VerifyInvocation(_ => _.OnNext, 2ul)
      .VerifyInvocation(_ => _.OnCompleted)
      .VerifyNoOtherInvocation();

    Assert.That(nextInstanciations.Count, Is.EqualTo(3));
    Assert.Less(nextInstanciations[0].Item1 + expectedDelay, nextInstanciations[1].Item1);
    Assert.Less(nextInstanciations[1].Item1 + expectedDelay, nextInstanciations[2].Item1);
  }
}
