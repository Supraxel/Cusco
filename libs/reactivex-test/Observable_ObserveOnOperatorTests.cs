using Cusco.Dispatch;
using Cusco.LowLevel;
using Moq;

namespace Cusco.ReactiveX.Test;

public sealed partial class ObservableTests
{
  [Test]
  public async Task Observable_ObserveOn_ShouldNextWithEachTransformedValues()
  {
    // arrange
    var disposeBag = new DisposeBag();

    var backgroundQueue = DispatchQueue.MakeSerial("test");

    var publish = new PublishSubject<int>(DispatchQueue.main);

    // act
    var observable = publish.ObserveOn(backgroundQueue);

    observable.Subscribe(value =>
    {
      Assert.Equals( value, 2);
      Assert.Equals( Thread.CurrentThread.Name, "test");
    }).DisposedBy(disposeBag);

    publish.On(Notification<int>.WithNextValue(1));

    await Task.Delay(100);
    disposeBag.Dispose();
  }
}
