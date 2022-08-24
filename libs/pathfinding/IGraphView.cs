namespace Cusco.Pathfinding;

public interface IGraphView<TNode, TGas>
{
  bool Contains(TNode node);
  TGas GetCost(TNode start, TNode end);
  IEnumerable<TNode> GetNeighbours(TNode node);
}
