namespace Cusco.Pathfinding;

public interface IGraphView<TNode, TIGas, TGas>
  where TIGas : struct, IGas<TIGas, TGas>
{
  bool Contains(TNode node);
  TIGas? GetCost(TNode start, TNode end, TIGas gasSoFar);
  IEnumerable<TNode> GetNeighbours(TNode node);
}
