using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
using JetBrains.Annotations;

namespace Iviz.Core
{
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
            ExternalImpl((string) t, LogLevel.Error);
        }

        public static void Error([CanBeNull] object t, [CanBeNull] Exception e, [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            ExternalImpl(t, LogLevel.Error, e, file, line);
        }

        [Obsolete]
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

        public static void Debug([CanBeNull] object t, Exception e, [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            ExternalImpl(t, LogLevel.Debug, e, file, line);
        }

        public static void Internal([CanBeNull] string msg)
        {
            string msgTxt = $"<b>[{GameThread.NowFormatted}]</b> {msg ?? NullMessage}";
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
            [CallerFilePath] string _ = "", [CallerLineNumber] int __ = 0)
        {
            LogExternal?.Invoke(new LogMessage(level, msg ?? NullMessage));
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

        static void ExternalImpl([CanBeNull] object msg, LogLevel level, [CanBeNull] Exception e, string file, int line)
        {
            StringBuilder str = BuilderPool.TryTake(out StringBuilder result) ? result : new StringBuilder(100);
            try
            {
                ExternalImpl(msg, level, e, file, line, str);
            }
            finally
            {
                BuilderPool.Add(str);
            }
        }

        static void ExternalImpl([CanBeNull] object msg, LogLevel level, [CanBeNull] Exception e, string _, int __,
            [NotNull] StringBuilder str)
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
                        str.Append("[").Append(childException.GetType().Name).Append("] ")
                            .Append(childException.Message);
                    }

                    childException = childException.InnerException;
                }
            }

            string message = str.ToString();
            LogExternal?.Invoke(new LogMessage(level, message));

            //if (!Settings.IsMobile)
            if (true)
            {
                UnityEngine.Debug.LogWarning((string) msg + e);
            }
            else
            {
                UnityEngine.Debug.LogWarning(message);
            }
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