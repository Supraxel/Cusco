namespace Cusco.Pathfinding;

public abstract class PathfindingException : Exception
{
  protected PathfindingException(string message, Exception innerException = null) : base(message, innerException)
  {
  }
}

public class UnreachableException : PathfindingException
{
  public UnreachableException() : base("Node is unreachable")
  {
  }
}
