using System;
using JetBrains.Annotations;

namespace Iviz.Tools;


/// <summary>
/// Class that processes logging information.
/// </summary>
public static class Logger
{
    public static Action<string>? LogDebugCallback { get; set; }

    public static void LogDebug(string arg1)
    {
        LogDebugCallback?.Invoke(arg1);
    }
    
    [StringFormatMethod("format")]
    public static void LogDebugFormat(string format, object? arg1)
    {
        LogDebugCallback?.Invoke(string.Format(format, arg1));
    }

    [StringFormatMethod("format")]
    public static void LogDebugFormat(string format, Exception? arg1)
    {
        LogDebugCallback?.Invoke(string.Format(format, ExceptionToString(arg1)));
    }

    [StringFormatMethod("format")]
    public static void LogDebugFormat<TU>(string format, object? arg1, TU? arg2)
    {
        LogDebugCallback?.Invoke(string.Format(format, arg1, arg2));
    }

    [StringFormatMethod("format")]
    public static void LogDebugFormat(string format, object? arg1, string? arg2)
    {
        LogDebugCallback?.Invoke(string.Format(format, arg1, arg2));
    }

    [StringFormatMethod("format")]
    public static void LogDebugFormat(string format, object? arg1, Exception? arg2)
    {
        LogDebugCallback?.Invoke(string.Format(format, arg1, ExceptionToString(arg2)));
    }

    [StringFormatMethod("format")]
    public static void LogDebugFormat<TU, TV>(string format, object? arg1, TU? arg2, TV? arg3)
    {
        LogDebugCallback?.Invoke(string.Format(format, arg1, arg2, arg3));
    }

    [StringFormatMethod("format")]
    public static void LogDebugFormat<TU>(string format, object? arg1, TU? arg2, Exception? arg3)
    {
        LogDebugCallback?.Invoke(string.Format(format, arg1, arg2, ExceptionToString(arg3)));
    }

    [StringFormatMethod("format")]
    public static void LogDebugFormat<TU, TV, TW>(string format, object? arg1, TU? arg2, TV? arg3, TW? arg4)
    {
        LogDebugCallback?.Invoke(string.Format(format, arg1, arg2, arg3, arg4));
    }

    [StringFormatMethod("format")]
    public static void LogDebugFormat(string format, params object?[] objs)
    {
        LogDebugCallback?.Invoke(string.Format(format, objs));
    }

    /// <summary>
    /// Callback function when a log message of level 'default' is produced. 
    /// </summary>
    public static Action<string>? LogCallback { get; set; }

    public static void Log(string arg1)
    {
        LogCallback?.Invoke(arg1);
    }
    
    [StringFormatMethod("format")]
    public static void LogFormat(string format, object? arg1)
    {
        LogCallback?.Invoke(string.Format(format, arg1));
    }

    [StringFormatMethod("format")]
    public static void LogFormat(string format, Exception? arg1)
    {
        LogCallback?.Invoke(string.Format(format, ExceptionToString(arg1)));
    }

    [StringFormatMethod("format")]
    public static void LogFormat<TU>(string format, object? arg1, TU? arg2)
    {
        LogCallback?.Invoke(string.Format(format, arg1, arg2));
    }

    [StringFormatMethod("format")]
    public static void LogFormat(string format, object? arg1, string? arg2)
    {
        LogCallback?.Invoke(string.Format(format, arg1, arg2));
    }
    
    [StringFormatMethod("format")]
    public static void LogFormat(string format, object? arg1, Exception? arg2)
    {
        LogCallback?.Invoke(string.Format(format, arg1, ExceptionToString(arg2)));
    }

    [StringFormatMethod("format")]
    public static void LogFormat<TU, TV>(string format, object? arg1, TU? arg2, TV? arg3)
    {
        LogCallback?.Invoke(string.Format(format, arg1, arg2, arg3));
    }

    [StringFormatMethod("format")]
    public static void LogFormat<TU>(string format, object? arg1, TU? arg2, Exception? arg3)
    {
        LogCallback?.Invoke(string.Format(format, arg1, arg2, ExceptionToString(arg3)));
    }

    [StringFormatMethod("format")]
    public static void LogFormat(string format, params object?[] objs)
    {
        LogCallback?.Invoke(string.Format(format, objs));
    }

    /// <summary>
    /// Callback function when a log message of level 'error' is produced. 
    /// </summary>
    public static Action<string>? LogErrorCallback { get; set; }

    public static void LogError(string arg1)
    {
        LogErrorCallback?.Invoke(arg1);
    }

    [StringFormatMethod("format")]
    public static void LogErrorFormat(string format, object? arg1)
    {
        LogErrorCallback?.Invoke(string.Format(format, arg1));
    }

    [StringFormatMethod("format")]
    public static void LogErrorFormat(string format, Exception? arg1)
    {
        LogErrorCallback?.Invoke(string.Format(format, ExceptionToString(arg1)));
    }

    [StringFormatMethod("format")]
    public static void LogErrorFormat(string format, object? arg1, Exception? arg2)
    {
        LogErrorCallback?.Invoke(string.Format(format, arg1, ExceptionToString(arg2)));
    }

    [StringFormatMethod("format")]
    public static void LogErrorFormat<TU>(string format, object? arg1, TU? arg2)
    {
        LogErrorCallback?.Invoke(string.Format(format, arg1, arg2));
    }
    
    [StringFormatMethod("format")]
    public static void LogErrorFormat(string format, object? arg1, string? arg2)
    {
        LogErrorCallback?.Invoke(string.Format(format, arg1, arg2));
    }

    [StringFormatMethod("format")]
    public static void LogErrorFormat<TU, TV>(string format, object? arg1, TU? arg2, TV? arg3)
    {
        LogErrorCallback?.Invoke(string.Format(format, arg1, arg2, arg3));
    }

    [StringFormatMethod("format")]
    public static void LogErrorFormat<TU>(string format, object? arg1, TU? arg2, Exception? arg3)
    {
        LogErrorCallback?.Invoke(string.Format(format, arg1, arg2, ExceptionToString(arg3)));
    }

    [StringFormatMethod("format")]
    public static void LogErrorFormat(string format, params object?[] objs)
    {
        LogErrorCallback?.Invoke(string.Format(format, objs));
    }

    /// <summary>
    /// Suppresses all printing of log text. 
    /// </summary>
    public static void SuppressAll()
    {
        LogDebugCallback = null;
        LogCallback = null;
        LogErrorCallback = null;
    }

    public static string ExceptionToString(Exception? e)
    {
        if (e == null)
        {
            return "[null exception]";
        }

        using var str = BuilderPool.Rent();
        Exception? subException = e;

        bool firstException = true;
        while (subException != null)
        {
            if (subException is not AggregateException)
            {
                str.Append(firstException ? "\n[" : "\n   [");
                str.Append(subException.GetType().Name).Append("] ").Append(subException.CheckMessage());
                firstException = false;
            }

            subException = subException.InnerException;
        }

        return str.ToString();
    }
}