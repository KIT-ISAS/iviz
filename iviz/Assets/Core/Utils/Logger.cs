using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
using JetBrains.Annotations;

namespace Iviz.Core
{
    [DataContract]
    public readonly struct LogMessage
    {
        [CanBeNull, DataMember] public string SourceId { get; }
        [DataMember] public DateTime Stamp { get; }
        [DataMember] public LogLevel Level { get; }
        [NotNull, DataMember] public string Message { get; }
        [NotNull, DataMember] public string File { get; }
        [DataMember] public int Line { get; }

        public LogMessage(LogLevel level, [NotNull] string message, [NotNull] string file, int line)
        {
            SourceId = null;
            Stamp = GameThread.Now;
            Level = level;
            Message = message;
            File = file;
            Line = line;
        }

        public LogMessage(in Log msg)
        {
            SourceId = msg.Name;
            Stamp = msg.Header.Stamp.ToDateTime();
            Level = (LogLevel) msg.Level;
            Message = msg.Msg ?? "";
            File = "";
            Line = 0;
        }
    }

    public static class Logger
    {
        const string NullMessage = "[null message]";
        const string NullException = "[null exception]";

        static readonly ConcurrentBag<StringBuilder> BuilderPool = new ConcurrentBag<StringBuilder>();
        
        public delegate void ExternalLogDelegate(in LogMessage msg);

        public static event Action<string> LogInternal;
        public static event ExternalLogDelegate LogExternal;

        public static void Info([CanBeNull] object t)
        {
            ExternalImpl(t?.ToString(), LogLevel.Info);
        }

        public static void Error([CanBeNull] object t)
        {
            ExternalImpl(t?.ToString(), LogLevel.Error);
        }

        public static void Error([CanBeNull] object t, Exception e, [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            ExternalImpl(t, e, file, line);
        }

        public static void Error(Exception e)
        {
        }

        public static void Warn([CanBeNull] object t)
        {
            ExternalImpl(t?.ToString(), LogLevel.Warn);
        }

        public static void Debug([CanBeNull] object t)
        {
            ExternalImpl(t?.ToString(), LogLevel.Debug);
        }

        public static void Internal([CanBeNull] string msg)
        {
            var msgTxt = $"<b>[{GameThread.NowFormatted}]</b> {msg ?? NullMessage}";
            LogInternal?.Invoke(msgTxt);
            UnityEngine.Debug.Log(msgTxt);
        }
        
        public static void Internal([CanBeNull] string msg, [CanBeNull] Exception e)
        {
            StringBuilder str = BuilderPool.TryTake(out StringBuilder result) ? result : new StringBuilder(100);
            try
            {
                InternalImpl(msg, e, str);
            }
            finally
            {
                BuilderPool.Add(str);
            }
        }
        

        static void InternalImpl([CanBeNull] string msg, [CanBeNull] Exception e, [NotNull] StringBuilder str)
        {
            str.Length = 0;
            str.Append("<b>[")
                .Append(GameThread.NowFormatted)
                .Append("]</b> ")
                .Append(msg ?? NullMessage);

            if (e == null)
            {
                str.AppendLine().Append("<color=red>").Append(NullException).Append("</color>");
            }
            else
            {
                Exception childException = e;
                while (childException != null)
                {
                    if (!(childException is AggregateException))
                    {
                        str.AppendLine()
                            .Append("<color=red>")
                            .Append(childException.GetType().Name)
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


        static void ExternalImpl([CanBeNull] string msg, LogLevel level,
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

        static void ExternalImpl([CanBeNull] object msg, [CanBeNull] Exception e, string file, int line)
        {
            StringBuilder str = BuilderPool.TryTake(out StringBuilder result) ? result : new StringBuilder(100);
            try
            {
                ExternalImpl(msg, e, file, line, str);
            }
            finally
            {
                BuilderPool.Add(str);
            }
        }

        static void ExternalImpl([CanBeNull] object msg, [CanBeNull] Exception e, string file, int line, [NotNull] StringBuilder str)
        {
            str.Length = 0;
            str.Append(msg ?? NullMessage);

            if (e == null)
            {
                str.AppendLine().Append(NullException);
            }
            else
            {
                Exception childException = e;
                while (childException != null)
                {
                    if (!(childException is AggregateException))
                    {
                        str.AppendLine();
                        str.Append("[").Append(childException.GetType().Name).Append("] ").Append(childException.Message);
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