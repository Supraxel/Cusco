namespace Cusco.Pathfinding;

public interface IGraphView<TNode, TGas>
  where TGas : struct, IComparable<TGas>
{
  bool Contains(TNode node);
  TGas? GetCost(TNode start, TNode end, TGas gasSoFar);
  IEnumerable<TNode> GetNeighbours(TNode node);
}
