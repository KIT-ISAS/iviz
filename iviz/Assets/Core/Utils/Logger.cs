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

        public LogMessage(LogLevel level, [NotNull] string message, [NotNull] string file, int line)
        {
            Level = level;
            Message = message;
            File = file;
            Line = line;
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
            UnityEngine.Debug.Log(t);

            if (Settings.IsHololens)
            {
                LogInternal?.Invoke( t.ToString());
            }
        }

        public static void Error(object t)
        {
            UnityEngine.Debug.LogWarning(t);

            if (Settings.IsHololens)
            {
                LogInternal?.Invoke(t.ToString());
            }
        }

        public static void Warn(object t)
        {
            UnityEngine.Debug.LogWarning(t);

            if (Settings.IsHololens)
            {
                LogInternal?.Invoke(t.ToString());
            }
        }

        public static void Debug(object t)
        {
            UnityEngine.Debug.Log(t);

            if (Settings.IsHololens)
            {
                LogInternal?.Invoke(t.ToString());
            }
        }

        public static void Internal([CanBeNull] string msg)
        {
            var msgTxt = $"<b>[{DateTime.Now:HH:mm:ss}]</b> {msg ?? NullMessage}";
            LogInternal?.Invoke(msgTxt);
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

            LogInternal?.Invoke(str.ToString());
            Warn(str);
        }

        public static void External([CanBeNull] string msg, [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            LogExternal?.Invoke(new LogMessage(LogLevel.Debug, msg ?? NullMessage, file, line));
            UnityEngine.Debug.Log(msg);
        }

        public static void External(LogLevel level, [CanBeNull] string msg, [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            LogExternal?.Invoke(new LogMessage(level, msg ?? NullMessage, file, line));
            UnityEngine.Debug.Log(msg);
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
                        str.AppendLine().Append(childException.GetType()).Append(" ").Append(childException.Message);
                    }

                    childException = childException.InnerException;
                }
            }

            LogExternal?.Invoke(new LogMessage(LogLevel.Error, str.ToString(), file, line));
            UnityEngine.Debug.Log(str.ToString());
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