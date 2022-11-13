namespace Cusco.LowLevel;

public class MutexLock : IDisposable
{
  private readonly Mutex m;
  private readonly bool locked;
  private bool disposed;

  public MutexLock(Mutex m)
  {
    this.m = m;
    locked = m.WaitOne();
  }

  public void Dispose()
  {
    if (disposed) return;
    if (locked)
      m.ReleaseMutex();
    disposed = true;
  }
}
