namespace Cusco.Pathfinding;

public static class AStarPathfinder<TNode> where TNode : IEquatable<TNode>
{
  public static unsafe Result<IEnumerable<TNode>> FindPath<TIGas, TGas>(
    IGraphView<TNode, TIGas, TGas> graphView,
    delegate* managed<TNode, TNode, TIGas> heuristic,
    TNode startNode,
    TNode endNode
  ) where TIGas : struct, IGas<TIGas, TGas>
  {
    if (null == graphView)
      throw new ArgumentNullException(nameof(graphView));

    if (null == heuristic)
      throw new ArgumentNullException(nameof(heuristic));

    if (null == startNode)
      throw new ArgumentNullException(nameof(startNode));

    if (null == endNode)
      throw new ArgumentNullException(nameof(endNode));

    if (false == graphView.Contains(startNode))
      throw new ArgumentException("Start node is not in graph", nameof(startNode));

    if (false == graphView.Contains(endNode))
      throw new ArgumentException("End node is not in graph", nameof(endNode));

    var frontier = new PriorityQueue<TNode, TIGas>();
    Dictionary<TNode, TNode> precedents = new();
    Dictionary<TNode, TIGas> gasSoFar = new();

    Result<IEnumerable<TNode>> ConstructPath(TNode finishNode)
    {
      var path = new LinkedList<TNode>();

      for (TNode node = finishNode; false == node.Equals(startNode); node = precedents[node])
        path.AddFirst(node);

      return Result<IEnumerable<TNode>>.Ok(path);
    }

    frontier.Enqueue(startNode, default);
    precedents[startNode] = startNode;
    gasSoFar[startNode] = default;

    while (frontier.TryDequeue(out var currentNode, out _))
    {
      if (currentNode.Equals(endNode))
        return ConstructPath(endNode);

      foreach (var neighbour in graphView.GetNeighbours(currentNode))
      {
        var gasSoFarForNode = gasSoFar[currentNode];
        var costToNeighbour = graphView.GetCost(currentNode, neighbour, gasSoFarForNode);
        if (false == costToNeighbour.HasValue)
          continue;

        var newGasCost = gasSoFarForNode.Add(costToNeighbour.Value);

        if (gasSoFar.TryGetValue(neighbour, out var gasToNeighbour) && newGasCost.CompareTo(gasToNeighbour) >= 0)
          continue;

        gasSoFar[neighbour] = newGasCost;
        var priority = newGasCost.Add(heuristic(neighbour, endNode));
        frontier.Enqueue(neighbour, priority);
        precedents[neighbour] = currentNode;
      }
    }

    return Result<IEnumerable<TNode>>.Err(new UnreachableException());
  }
}
