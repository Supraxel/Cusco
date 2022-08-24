namespace Cusco;

public static class FuncExtensions
{
  public static bool TryInvokeSafely<TReturn>(
    this Func<TReturn> self,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke();
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, TReturn>(
    this Func<T1, TReturn> self,
    T1 arg1,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, TReturn>(
    this Func<T1, T2, TReturn> self,
    T1 arg1,
    T2 arg2,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, TReturn>(
    this Func<T1, T2, T3, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, TReturn>(
    this Func<T1, T2, T3, T4, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, TReturn>(
    this Func<T1, T2, T3, T4, T5, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9,
    T10 arg10,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9,
    T10 arg10,
    T11 arg11,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9,
    T10 arg10,
    T11 arg11,
    T12 arg12,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9,
    T10 arg10,
    T11 arg11,
    T12 arg12,
    T13 arg13,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9,
    T10 arg10,
    T11 arg11,
    T12 arg12,
    T13 arg13,
    T14 arg14,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9,
    T10 arg10,
    T11 arg11,
    T12 arg12,
    T13 arg13,
    T14 arg14,
    T15 arg15,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }

  public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> self,
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9,
    T10 arg10,
    T11 arg11,
    T12 arg12,
    T13 arg13,
    T14 arg14,
    T15 arg15,
    T16 arg16,
    out TReturn value,
    out Exception exc)
  {
    try
    {
      value = self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
      exc = null;
      return true;
    }
    catch (Exception e)
    {
      value = default;
      exc = e;
      return false;
    }
  }
}
