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
        [CanBeNull, DataMember] public string SourceId { get; }
        [DataMember] public DateTime Stamp { get; }
        [DataMember] public LogLevel Level { get; }
        [NotNull] [DataMember] public string Message { get; }
        [NotNull] [DataMember] public string File { get; }
        [DataMember] public int Line { get; }

        public LogMessage(LogLevel level, [NotNull] string message, [NotNull] string file, int line)
        {
            SourceId = null;
            Stamp = DateTime.Now;
            Level = level;
            Message = message;
            File = file;
            Line = line;
        }

        public LogMessage([NotNull] Log msg)
        {
            SourceId = msg.Name;
            Stamp = msg.Header.Stamp.ToDateTime();
            Level = (LogLevel) msg.Level;
            Message = msg.Msg;
            File = msg.File;
            Line = (int) msg.Line;
        }
    }

    public static class Logger
    {
        const string NullMessage = "[null message]";
        const string NullException = "[null exception]";

        public delegate void ExternalLogDelegate(in LogMessage msg);

        public static event Action<string> LogInternal;
        public static event ExternalLogDelegate LogExternal;

        public static void Info(object t)
        {
            External(t.ToString(), LogLevel.Info);
        }

        public static void Error(object t)
        {
            External(t.ToString(), LogLevel.Error);
        }

        public static void Warn(object t)
        {
            External(t.ToString(), LogLevel.Warn);
        }

        public static void Debug(object t)
        {
            External(t.ToString());
        }

        public static void Internal([CanBeNull] string msg)
        {
            var msgTxt = $"<b>[{DateTime.Now:HH:mm:ss}]</b> {msg ?? NullMessage}";
            LogInternal?.Invoke(msgTxt);
            UnityEngine.Debug.Log(msgTxt);
        }

        public static void Internal([CanBeNull] string msg, [CanBeNull] Exception e)
        {
            var str = new StringBuilder();
            str.Append("<b>[")
                .AppendFormat("{0:HH:mm:ss}", DateTime.Now)
                .Append("]</b> ")
                .Append(msg ?? NullMessage);

            if (e == null)
            {
                str.AppendLine().Append($"<color=red>→ ").Append(NullException).Append("</color>");
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

            string message = str.ToString();
            LogInternal?.Invoke(message);
            UnityEngine.Debug.LogWarning(message);
        }


        public static void External([CanBeNull] string msg, LogLevel level = LogLevel.Debug,
            [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            LogExternal?.Invoke(new LogMessage(level, msg ?? NullMessage, file, line));
            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    UnityEngine.Debug.Log(msg);
                    break;
                case LogLevel.Warn:
                case LogLevel.Error:
                    UnityEngine.Debug.LogWarning(msg);
                    break;
                case LogLevel.Fatal:
                    UnityEngine.Debug.LogError(msg);
                    break;
            }
        }

        public static void External([CanBeNull] string msg, [CanBeNull] Exception e, [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            var str = new StringBuilder();
            str.Append(msg ?? NullMessage);

            if (e == null)
            {
                str.AppendLine().Append("→ ").Append(NullException);
            }
            else
            {
                Exception childException = e;
                while (childException != null)
                {
                    if (!(childException is AggregateException))
                    {
                        str.AppendLine();
                        str.Append("[").Append(childException.GetType()).Append("] ").Append(childException.Message);
                    }

                    childException = childException.InnerException;
                }
            }

            string message = str.ToString();
            LogExternal?.Invoke(new LogMessage(LogLevel.Error, message, file, line));
            UnityEngine.Debug.LogWarning(message);
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