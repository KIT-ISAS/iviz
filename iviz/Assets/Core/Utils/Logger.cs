using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Iviz.Msgs.RosgraphMsgs;
using JetBrains.Annotations;

namespace Iviz.Core
{
    [DataContract]
    public readonly struct LogMessage
    {
        [DataMember] public LogLevel Level { get; }
        [NotNull] [DataMember] public string Message { get; }
        [NotNull] [DataMember] public string File { get; }
        [DataMember] public int Line { get; }

        public LogMessage(LogLevel level, string message, string file, int line)
        {
            Level = level;
            Message = message;
            File = file;
            Line = line;
        }
    }

    public static class Logger
    {
        public delegate void ExternalLogDelegate(in LogMessage msg);

        public static event Action<string> LogInternal;
        public static event ExternalLogDelegate LogExternal;

        public static void Info<T>(in T t)
        {
            UnityEngine.Debug.Log(t);
        }

        public static void Error<T>(T t)
        {
            UnityEngine.Debug.LogWarning(t);
        }

        public static void Warn<T>(T t)
        {
            UnityEngine.Debug.LogWarning(t);
        }

        public static void Debug<T>(T t)
        {
            UnityEngine.Debug.Log(t);
        }

        public static void Internal([CanBeNull] string msg)
        {
            var msgTxt = $"<b>[{DateTime.Now:HH:mm:ss}]</b> {msg ?? "[null message]"}";
            LogInternal?.Invoke(msgTxt);
        }

        public static void Internal([CanBeNull] string msg, [CanBeNull] Exception e)
        {
            var str = new StringBuilder();
            str.Append("<b>[")
                .AppendFormat("{0:HH:mm:ss}", DateTime.Now)
                .Append("]</b> ")
                .Append(msg ?? "[null message]");

            if (e == null)
            {
                str.AppendLine().Append("<color=red>→ (null exception)</color>");
            }
            else
            {
                Exception childException = e;
                while (childException != null)
                {
                    if (!(childException is AggregateException))
                    {
                        str.AppendLine()
                            .Append("<color=red>→ ")
                            .Append(childException.GetType())
                            .Append("</color> ")
                            .Append(childException.Message);
                    }

                    childException = childException.InnerException;
                }
            }

            LogInternal?.Invoke(str.ToString());
            UnityEngine.Debug.LogWarning(str);
        }

        public static void External(LogLevel level, string msg, [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            LogExternal?.Invoke(new LogMessage(level, msg, file, line));
        }

        public static void External([CanBeNull] string msg, [CanBeNull] Exception e, [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            var str = new StringBuilder();
            str.Append(msg ?? "[null msg]");

            if (e == null)
            {
                str.AppendLine().Append("→ (null exception)");
            }
            else
            {
                Exception childException = e;
                while (childException != null)
                {
                    if (!(childException is AggregateException))
                    {
                        str.AppendLine().Append(childException.GetType()).Append(" ").Append(childException.Message);
                    }

                    childException = childException.InnerException;
                }
            }

            LogExternal?.Invoke(new LogMessage(LogLevel.Error, msg, file, line));
            UnityEngine.Debug.Log(str);
        }
    }

    [Flags]
    public enum LogLevel
    {
        Debug = Log.DEBUG,
        Info = Log.INFO,
        Warn = Log.WARN,
        Error = Log.ERROR,
        Fatal = Log.FATAL
    }
}