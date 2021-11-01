using System;
using System.Collections.Concurrent;
using System.Text;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.Core
{
    public static class Logger
    {
        const string NullMessage = "[null message]";
        const string NullException = "[null exception]";

        public delegate void ExternalLogDelegate(in LogMessage msg);

        public static event Action<string> LogInternal;
        public static event ExternalLogDelegate LogExternal;

        public static void Info([CanBeNull] object t)
        {
            ExternalImpl(t?.ToString(), LogLevel.Info);
        }

        public static void Error([CanBeNull] object t)
        {
            ExternalImpl((string)t, LogLevel.Error);
        }

        public static void Error([CanBeNull] object t, [CanBeNull] Exception e)
        {
            ExternalImpl(t, LogLevel.Error, e);
        }

        [Obsolete]
        public static void Error(Exception _)
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

        public static void Debug([CanBeNull] object t, Exception e)
        {
            ExternalImpl(t, LogLevel.Debug, e);
        }

        public static void Internal([CanBeNull] string msg)
        {
            string msgTxt = $"<font=Bold>[{GameThread.NowFormatted}]</font> {msg ?? NullMessage}";
            LogInternal?.Invoke(msgTxt);
            UnityEngine.Debug.Log(msgTxt);
        }

        public static void Internal([CanBeNull] string msg, [CanBeNull] Exception e)
        {
            var str = BuilderPool.Rent();
            try
            {
                InternalImpl(msg, e, str);
            }
            finally
            {
                BuilderPool.Return(str);
            }
        }

        static void InternalImpl([CanBeNull] string msg, [CanBeNull] Exception e, [NotNull] StringBuilder str)
        {
            str.Length = 0;
            str.Append("<font=Bold>[")
                .Append(GameThread.NowFormatted)
                .Append("]</font> ")
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
                            .Append(childException.CheckMessage());
                    }

                    childException = childException.InnerException;
                }
            }

            string message = str.ToString();
            LogInternal?.Invoke(message);
            UnityEngine.Debug.LogWarning(message);
        }


        static void ExternalImpl([CanBeNull] string msg, LogLevel level)
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

        static void ExternalImpl([CanBeNull] object msg, LogLevel level, [CanBeNull] Exception e)
        {
            var str = BuilderPool.Rent();
            try
            {
                ExternalImpl(msg, level, e, str);
            }
            finally
            {
                BuilderPool.Return(str);
            }
        }

        static void ExternalImpl([CanBeNull] object msg, LogLevel level, [CanBeNull] Exception e,
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
                            .Append(childException.CheckMessage());
                    }

                    childException = childException.InnerException;
                }
            }

            string message = str.ToString();
            LogExternal?.Invoke(new LogMessage(level, message));

            if (!Settings.IsMobile)
            {
                UnityEngine.Debug.LogWarning((string)msg + e);
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