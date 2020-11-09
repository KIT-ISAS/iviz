using System;

namespace Iviz.Msgs
{
    /// <summary>
    /// Class that processes logging information.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Shorthand for a logging function that does nothing.  
        /// </summary>
        public static readonly Action<object> None = _ => { };
        
        /// <summary>
        /// Callback function when a log message of level 'debug' is produced. 
        /// </summary>
        public static Action<object> LogDebug { get; set; } = None;

        public static void LogDebugFormat(string format, object? arg1)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, arg1));
            }
        }

        public static void LogDebugFormat(string format, object? arg1, object? arg2)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, arg1, arg2));
            }
        }
        
        public static void LogDebugFormat(string format, object? arg1, object? arg2, object? arg3)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, arg1, arg2, arg3));
            }
        }        

        public static void LogDebugFormat(string format, params object?[] objs)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, objs));
            }
        }

        /// <summary>
        /// Callback function when a log message of level 'default' is produced. 
        /// </summary>
        public static Action<object> Log { get; set; } = None;
        
        public static void LogFormat(string format, object? arg1)
        {
            if (Log != None)
            {
                Log(string.Format(format, arg1));
            }
        }

        public static void LogFormat(string format, object? arg1, object? arg2)
        {
            if (Log != None)
            {
                Log(string.Format(format, arg1, arg2));
            }
        }
        
        public static void LogFormat(string format, object? arg1, object? arg2, object? arg3)
        {
            if (Log != None)
            {
                Log(string.Format(format, arg1, arg2, arg3));
            }
        }                
        
        public static void LogFormat(string format, params object?[] objs)
        {
            if (Log != None)
            {
                Log(string.Format(format, objs));
            }
        }        

        /// <summary>
        /// Callback function when a log message of level 'error' is produced. 
        /// </summary>
        public static Action<object> LogError { get; set; } = None;

        public static void LogErrorFormat(string format, object? arg1)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, arg1));
            }
        }

        public static void LogErrorFormat(string format, object? arg1, object? arg2)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, arg1, arg2));
            }
        }
        
        public static void LogErrorFormat(string format, object? arg1, object? arg2, object? arg3)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, arg1, arg2, arg3));
            }
        }                
        
        public static void LogErrorFormat(string format, params object?[] objs)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, objs));
            }
        }        

        /// <summary>
        /// Suppresses all printing of log text. 
        /// </summary>
        public static void SuppressAll()
        {
            LogDebug = None;
            Log = None;
            LogError = None;
        }
    }
}