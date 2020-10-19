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

        /// <summary>
        /// Callback function when a log message of level 'default' is produced. 
        /// </summary>
        public static Action<object> Log { get; set; } = None;

        /// <summary>
        /// Callback function when a log message of level 'error' is produced. 
        /// </summary>
        public static Action<object> LogError { get; set; } = None;

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