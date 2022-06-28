namespace Cusco;

public static class ActionExtensions
{
    public static bool TryInvokeSafely(this Action self, out Exception exc)
    {
        try
        {
            self.Invoke();
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1>(
        this Action<T1> self,
        T1 arg1,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2>(
        this Action<T1, T2> self,
        T1 arg1,
        T2 arg2,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3>(
        this Action<T1, T2, T3> self,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4>(
        this Action<T1, T2, T3, T4> self,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5>(
        this Action<T1, T2, T3, T4, T5> self,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6>(
        this Action<T1, T2, T3, T4, T5, T6> self,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7>(
        this Action<T1, T2, T3, T4, T5, T6, T7> self,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8> self,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> self,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self,
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
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self,
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
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self,
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
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self,
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
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self,
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
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self,
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
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
    
    public static bool TryInvokeSafely<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> self,
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
        out Exception exc)
    {
        try
        {
            self.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            exc = null;
            return true;
        }
        catch (Exception e)
        {
            exc = e;
            return false;
        }
    }
}