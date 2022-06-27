namespace Cusco.LowLevel;

public sealed class DoubleBuffer<T>
{
  private readonly T[] buffers;
  private volatile int index;

  public DoubleBuffer(T consuming, T producing)
  {
    buffers = new[] { consuming, producing };
  }

  public T consuming
    => buffers[index & 1];

  public T producing
    => buffers[~index & 1];

  public void Swap()
  {
    Interlocked.Increment(ref index);
  }
}
