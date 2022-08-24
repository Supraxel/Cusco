using Cusco.Dispatch;

namespace Cusco.ReactiveX.Test;

[SetUpFixture]
public class GlobalSetUp
{
  private static CancellationTokenSource cts;
  private static Thread mainThread;

  [OneTimeSetUp]
  public void SetUp()
  {
    cts = new();
    mainThread = new Thread(() => DispatchQueue.RunMain(cts.Token));
    mainThread.Start();
  }

  [OneTimeTearDown]
  public void TearDown()
  {
    cts.Cancel();
    mainThread.Join();
  }
}
