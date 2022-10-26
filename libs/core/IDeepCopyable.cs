namespace Cusco;

public interface IDeepCopyable<out T>
{
  public T DeepCopy();
}
