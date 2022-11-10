namespace Cusco.Pathfinding;

public interface IGas<TSelf, T> : IComparable<TSelf> where TSelf : IGas<TSelf, T>
{
  public TSelf Add(TSelf rhs);
}
