using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Cusco;


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
        if (UnityHelpers.IsUnity())
        {
            UnityHelpers.DebugLog(message);
        }
        else
        {
            Console.WriteLine(message);
        }
    }

    private static void DebugLogError(string message)
    {
        if (UnityHelpers.IsUnity())
        {
            UnityHelpers.DebugLogError(message);
        }
        else
        {
            Console.WriteLine(message);
        }
    }

    public static void Panic(string message, Exception exception = null)
    {
      var finalMessageBuilder = new StringBuilder();
      finalMessageBuilder.AppendFormat("Panic: {0}\n", message);
      if (null != exception)
        finalMessageBuilder.AppendFormat("EXCEPTION:\n----------\n{0}", exception);
      finalMessageBuilder.AppendFormat("\n\nSTACKTRACE:\n-----------\n{0}", new StackTrace());

      var finalMessage = finalMessageBuilder.ToString();
      DebugLogError(finalMessage);

      try
      {
        Directory.CreateDirectory(Environment.CurrentDirectory);
        File.WriteAllText(Path.Combine(Environment.CurrentDirectory, $"cusco_panic_{DateTime.UtcNow:yyyy-MM-dd_HH-mm-ss}.crashlog"), finalMessage);
      }
      catch
      {
          DebugLogError("Unable to write cusco panic file.");
      }

      Debugger.Break();

      if (UnityHelpers.IsUnityEditor())
      {
        UnityHelpers.ExitPlaymode();
      }
      else
      {
        Environment.Exit(1);
      }
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


    private static class UnityHelpers
    {
      private static Option<Assembly> _unityEngineAssembly;
      public static Assembly unityEngineAssembly
      {
        get
        {
          if (false == _unityEngineAssembly.isNone)
            return _unityEngineAssembly.Unwrap();

          _unityEngineAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.FullName.Contains("UnityEngine"));
          return _unityEngineAssembly.Unwrap();
        }
      }

      private static Option<Assembly> _unityEditorAssembly;
      public static Assembly unityEditorAssembly
      {
        get
        {
          if (false == _unityEditorAssembly.isNone)
            return _unityEditorAssembly.Unwrap();

          _unityEditorAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.FullName.Contains("UnityEditor"));
          return _unityEditorAssembly.Unwrap();
        }
      }

      private static MethodInfo debugLogMethod;
      public static void DebugLog(string message)
      {
          if (false == IsUnity()) return;
          if (null == debugLogMethod)
              debugLogMethod = unityEngineAssembly?
                  .GetType("UnityEngine.Debug")?
                  .GetMethod(
                      "Log",
                      BindingFlags.Static | BindingFlags.Public,
                      null,
                      CallingConventions.Any,
                      new[] { typeof(string) },
                      Array.Empty<ParameterModifier>()
                  );
          debugLogMethod?.Invoke(null, new object[] { message });
      }

      private static MethodInfo debugLogErrorMethod;
      public static void DebugLogError(string message)
      {
          if (false == IsUnity()) return;
          if (null == debugLogErrorMethod)
              debugLogErrorMethod = unityEngineAssembly?
                  .GetType("UnityEngine.Debug")?
                  .GetMethod(
                      "LogError",
                      BindingFlags.Static | BindingFlags.Public,
                      null,
                      CallingConventions.Any,
                      new[] { typeof(string) },
                      Array.Empty<ParameterModifier>()
                  );
          debugLogErrorMethod?.Invoke(null, new object[] { message });
      }

      private static MethodInfo exitPlaymodeMethod;
      public static void ExitPlaymode()
      {
        if (false == IsUnityEditor()) return;
        if (null == exitPlaymodeMethod)
          exitPlaymodeMethod = unityEditorAssembly?
            .GetType("UnityEditor.EditorApplication")?
            .GetMethod("ExitPlaymode", BindingFlags.Static | BindingFlags.Public);
        exitPlaymodeMethod?.Invoke(null, Array.Empty<object>());
      }

      public static bool IsUnity()
        => null != unityEngineAssembly || null != unityEditorAssembly;

      public static bool IsUnityEditor()
        => null != unityEditorAssembly;
    }
  }
}
