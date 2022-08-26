using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Cusco;

public static class ExceptionExtensions
{
#pragma warning disable S3011
  private static readonly FieldInfo STACK_TRACE_STRING_FI = typeof(Exception).GetField("_stackTraceString", BindingFlags.NonPublic | BindingFlags.Instance);
  private static readonly Type TRACE_FORMAT_TI = Type.GetType("System.Diagnostics.StackTrace")!.GetNestedType("TraceFormat", BindingFlags.NonPublic);
  private static readonly MethodInfo TRACE_TO_STRING_MI =
    typeof(StackTrace).GetMethod("ToString", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { TRACE_FORMAT_TI }, null);
#pragma warning restore S3011

  public static Exception Enhance(this Exception self)
    => null != self.StackTrace ? self : self.SetStackTrace(new StackTrace(StackTrace.METHODS_TO_SKIP + 1));

  public static Exception Rethrow(this Exception self)
  {
    ExceptionDispatchInfo.Throw(self);
    return self;
  }

  private static Exception SetStackTrace(this Exception target, StackTrace stack)
  {
    var getStackTraceString = TRACE_TO_STRING_MI.Invoke(stack, new[] { Enum.GetValues(TRACE_FORMAT_TI).GetValue(0) });
    STACK_TRACE_STRING_FI.SetValue(target, getStackTraceString);
    return target;
  }
}
