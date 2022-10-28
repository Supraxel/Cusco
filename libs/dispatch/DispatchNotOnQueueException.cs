namespace Cusco.Dispatch;

public class DispatchNotOnQueueException : Exception
{
  public DispatchNotOnQueueException(string message) : base(message) { }
}
