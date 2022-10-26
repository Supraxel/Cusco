namespace Cusco;

public interface IDeepCopyable<out T>
{
  public T DeepCopy();
}

public interface IMutableDeepCopyable<T, TMutable> : IDeepCopyable<T>
{
  public TMutable MutableDeepCopy();
}
