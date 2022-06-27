using System.Collections.Concurrent;

namespace Cusco.LowLevel;

public static class PinnedInstance
{
  public static PinnedInstance<T> Pin<T>(T instance) where T : class
  {
    return PinnedInstance<T>.Pin(instance);
  }
}

public struct PinnedInstance<T> where T : class
{
  public static readonly int size = IntPtr.Size;

  private readonly IntPtr identifier;
  public bool isValid => identifier != IntPtr.Zero && PinnedInstancesStore.instances.ContainsKey(identifier);

  private PinnedInstance(IntPtr identifier)
  {
    this.identifier = identifier;
  }

  public T instance
  {
    get
    {
      if (PinnedInstancesStore.instances.TryGetValue(identifier, out var obj))
        return (T)obj;
      throw new ObjectDisposedException("Instance has been unpinned");
    }
  }

  public static PinnedInstance<T> Pin(T instance)
  {
    IntPtr identifier;
    do
    {
      identifier = PinnedInstancesStore.nextIdentifier;
    } while (false == PinnedInstancesStore.instances.TryAdd(identifier, instance));

    return new PinnedInstance<T>(identifier);
  }

  public static PinnedInstance<T> Null => new(IntPtr.Zero);

  public void Unpin()
  {
    PinnedInstancesStore.instances.TryRemove(identifier, out _);
  }
}

internal static class PinnedInstancesStore
{
  private static readonly AtomicInt identifierGenerator = new();
  internal static readonly ConcurrentDictionary<IntPtr, object> instances = new();
  internal static IntPtr nextIdentifier => new(identifierGenerator.Increment());
}
