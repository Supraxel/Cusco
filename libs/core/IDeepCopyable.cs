namespace Cusco;

public interface IDeepCopyable<out T>
{
  T DeepCopy();
}

public interface IMutableDeepCopyable<T, TMutable> : IDeepCopyable<T>
{
  TMutable MutableDeepCopy();
}
