using System;
using System.Text;

namespace Iviz.Tools
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

        public static void LogDebugFormat<TT>(string format, TT? arg1)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, arg1));
            }
        }

        public static void LogDebugFormat(string format, Exception? arg1)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, ExceptionToString(arg1)));
            }
        }        
        
        public static void LogDebugFormat<TT, TU>(string format, TU? arg1, TU? arg2)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, arg1, arg2));
            }
        }

        public static void LogDebugFormat<TT>(string format, TT? arg1, Exception? arg2)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, arg1, ExceptionToString(arg2)));
            }
        }

        public static void LogDebugFormat<TT, TU, TV>(string format, TU? arg1, TV? arg2, TV? arg3)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, arg1, arg2, arg3));
            }
        }
        
        public static void LogDebugFormat<TT, TU>(string format, TU? arg1, TU? arg2, Exception? arg3)
        {
            if (LogDebug != None)
            {
                LogDebug(string.Format(format, arg1, arg2, ExceptionToString(arg3)));
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

        public static void LogFormat<TT>(string format, TT? arg1)
        {
            if (Log != None)
            {
                Log(string.Format(format, arg1));
            }
        }

        public static void LogFormat(string format, Exception? arg1)
        {
            if (Log != None)
            {
                Log(string.Format(format, ExceptionToString(arg1)));
            }
        }

        public static void LogFormat<TT, TU>(string format, TU? arg1, TU? arg2)
        {
            if (Log != None)
            {
                Log(string.Format(format, arg1, arg2));
            }
        }
        
        public static void LogFormat<TT>(string format, TT? arg1, Exception? arg2)
        {
            if (Log != None)
            {
                Log(string.Format(format, arg1, ExceptionToString(arg2)));
            }
        }        

        public static void LogFormat<TT, TU, TV>(string format, TU? arg1, TV? arg2, TV? arg3)
        {
            if (Log != None)
            {
                Log(string.Format(format, arg1, arg2, arg3));
            }
        }
        
        public static void LogFormat<TT, TU>(string format, TU? arg1, TU? arg2, Exception? arg3)
        {
            if (Log != None)
            {
                Log(string.Format(format, arg1, arg2, ExceptionToString(arg3)));
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

        public static void LogErrorFormat<TT>(string format, TT? arg1)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, arg1));
            }
        }

        public static void LogErrorFormat(string format, Exception? arg1)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, ExceptionToString(arg1)));
            }
        }

        public static void LogErrorFormat<TT, TU>(string format, TU? arg1, TU? arg2)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, arg1, arg2));
            }
        }
        
        public static void LogErrorFormat<TT>(string format, TT? arg1, Exception? arg2)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, arg1, ExceptionToString(arg2)));
            }
        }        

        public static void LogErrorFormat<TT, TU, TV>(string format, TU? arg1, TV? arg2, TV? arg3)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, arg1, arg2, arg3));
            }
        }
        
        public static void LogErrorFormat<TT, TU>(string format, TU? arg1, TU? arg2, Exception? arg3)
        {
            if (LogError != None)
            {
                LogError(string.Format(format, arg1, arg2, ExceptionToString(arg3)));
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

        public static string ExceptionToString(Exception? e)
        {
            if (e == null)
            {
                return "[null exception]";
            }

            var str = new StringBuilder(100);
            Exception? subException = e;

            bool firstException = true;
            while (subException != null)
            {
                if (!(subException is AggregateException))
                {
                    str.Append(firstException ? "\n[" : "\n   [");
                    str.Append(subException.GetType().Name).Append("] ").Append(subException.Message);
                    firstException = false;
                }

                subException = subException.InnerException;
            }

            return str.ToString();
        }
    }
}