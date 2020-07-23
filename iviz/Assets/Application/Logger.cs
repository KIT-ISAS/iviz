
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Iviz.Msgs.RosgraphMsgs;

namespace Iviz.Controllers
{
    [Flags]
    public enum LogLevel
    {
        Debug = Log.DEBUG,
        Info = Log.INFO,
        Warn = Log.WARN,
        Error = Log.ERROR,
        Fatal = Log.FATAL,
    }

    [DataContract]
    public readonly struct LogMessage
    {
        [DataMember] public LogLevel Level { get; }
        [DataMember] public object Message { get; }
        [DataMember] public string File { get; }
        [DataMember] public int Line { get; }

        public LogMessage(LogLevel level, object message, string file, int line)
        {
            Level = level;
            Message = message;
            File = file;
            Line = line;
        }
    }

    public static class Logger
    {
        public delegate void Delegate(in LogMessage msg);
        public static event Delegate Log;

        public static event Action<string> LogInternal;

        public static void Info<T>(in T t, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            UnityEngine.Debug.Log(t);
            Log?.Invoke(new LogMessage(LogLevel.Info, t, file, line));
        }

        public static void Error<T>(T t, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            UnityEngine.Debug.LogError(t);
            Log?.Invoke(new LogMessage(LogLevel.Error, t, file, line));
        }

        public static void Warn<T>(T t, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            UnityEngine.Debug.LogWarning(t);
            Log?.Invoke(new LogMessage(LogLevel.Warn, t, file, line));
        }

        public static void Debug<T>(T t, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            UnityEngine.Debug.Log(t);
            Log?.Invoke(new LogMessage(LogLevel.Debug, t, file, line));
        }

        public static void Internal(string msg)
        {
            string msgTxt = $"<b>[{DateTime.Now:HH:mm:ss}]</b> {msg}";
            LogInternal?.Invoke(msgTxt);
        }

        public static void Internal(string msg, Exception e)
        {
            StringBuilder str = new StringBuilder();
            str.Append($"<b>[{DateTime.Now:HH:mm:ss}]</b> {msg}");
            while (e != null)
            {
                if (!(e is AggregateException))
                {
                    str.Append($"\n<color=red>→ {e.GetType()}</color> {e.Message}");
                }
                e = e.InnerException;
            }
            LogInternal?.Invoke(str.ToString());
        }
    }
}
