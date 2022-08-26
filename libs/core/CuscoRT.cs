using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

#if UNITY
using Debug = UnityEngine.Debug;
#endif

namespace Cusco
{
  public static class CuscoRT
  {
    [Conditional("DEBUG")]
    public static void Assert(bool expr, string message = null, [CallerFilePath] string file = null, [CallerLineNumber] int line = 0)
    {
      if (false == expr)
        Panic($"Assertion '{message}' failed at '{file}:{line}'");
    }

    public static void DebugLog(string message)
    {
#if UNITY
            Debug.Log(message);
#else
      Console.WriteLine(message);
#endif
    }

    public static void Panic(string message, Exception exception = null)
    {
      var finalMessageBuilder = new StringBuilder();
      finalMessageBuilder.AppendFormat("Panic: {0}\n", message);
      if (null != exception)
        finalMessageBuilder.AppendFormat("EXCEPTION:\n----------\n{0}", exception);
      finalMessageBuilder.AppendFormat("\n\nSTACKTRACE:\n-----------\n{0}", new StackTrace());

      var finalMessage = finalMessageBuilder.ToString();
      DebugLog(finalMessage);

      try
      {
        Directory.CreateDirectory(Environment.CurrentDirectory);
        File.WriteAllText(Path.Combine(Environment.CurrentDirectory, $"cusco_panic_{DateTime.UtcNow:yyyy-MM-dd_HH-mm-ss}.crashlog"), finalMessage);
      }
      catch
      {
      }

      Debugger.Break();

#if !UNITY_EDITOR
      Environment.Exit(1);
#else
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }

    public static T Panic<T>(string message, Exception exception = null)
    {
      Panic(message, exception);
      return default;
    }

    public static void Precondition(bool expr, string message = null, [CallerFilePath] string file = null, [CallerLineNumber] int line = 0)
    {
      if (false == expr)
        Panic($"Precondition '{message}' failed at '{file}:{line}'");
    }

    public static void Unreachable() => CuscoRT.Panic("Code supposed to be unreachable is being executed");

    public static T Unreachable<T>() => CuscoRT.Panic<T>("Code supposed to be unreachable is being executed");
  }
}
