namespace Cusco;

public interface IDeepCopyable<T>
{
  void DeepCopy(out T copy);
}

public interface IMutableDeepCopyable<T, TMutable> : IDeepCopyable<T>
  where TMutable : T
{
  void MutableDeepCopy(out TMutable copy);
}
