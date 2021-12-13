#nullable enable

using System;
using System.Text;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Tools;

namespace Iviz.Core
{
    public static class RosLogger
    {
        const string NullMessage = "[null message]";
        const string NullException = "[null exception]";

        public delegate void ExternalLogDelegate(in LogMessage msg);

        public static event Action<string>? LogInternal;
        public static event ExternalLogDelegate? LogExternal;

        public static void Info(object? t)
        {
            ExternalImpl(t?.ToString(), LogLevel.Info);
        }

        public static void Error(object? t)
        {
            ExternalImpl((string?)t, LogLevel.Error);
        }

        public static void Error(object? t, Exception? e)
        {
            ExternalImpl(t, LogLevel.Error, e);
        }

        [Obsolete]
        public static void Error(Exception _)
        {
        }

        public static void Warn(object? t)
        {
            ExternalImpl(t?.ToString(), LogLevel.Warn);
        }

        public static void Debug(object? t)
        {
            ExternalImpl(t?.ToString(), LogLevel.Debug);
        }

        public static void Debug(object? t, Exception e)
        {
            ExternalImpl(t, LogLevel.Debug, e);
        }

        public static void Internal(string? msg)
        {
            string msgTxt = $"<b>[{GameThread.NowFormatted}]</b> {msg ?? NullMessage}";
            LogInternal?.Invoke(msgTxt);
            UnityEngine.Debug.Log(msgTxt);
        }

        public static void Internal(string? msg, Exception? e)
        {
            string message;
            using (var description = BuilderPool.Rent())
            {
                description.Append("<b>[")
                    .Append(GameThread.NowFormatted)
                    .Append("]</b> ")
                    .Append(msg ?? NullMessage);

                if (e == null)
                {
                    description.AppendLine().Append("<color=red>").Append(NullException).Append("</color>");
                }
                else
                {
                    Exception? childException = e;
                    while (childException != null)
                    {
                        if (childException is not AggregateException)
                        {
                            description.AppendLine()
                                .Append("<color=red>")
                                .Append(childException.GetType().Name)
                                .Append("</color> ")
                                .Append(childException.CheckMessage());
                        }

                        childException = childException.InnerException;
                    }
                }

                message = description.ToString();
            }

            LogInternal?.Invoke(message);
            UnityEngine.Debug.LogWarning(message);
        }


        static void ExternalImpl(string? msg, LogLevel level)
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

        static void ExternalImpl(object? msg, LogLevel level, Exception? e)
        {
            string message;
            using (var description = BuilderPool.Rent())
            {
                description.Append(msg ?? NullMessage);

                if (e == null)
                {
                    description.AppendLine().Append(NullException);
                }
                else
                {
                    Exception? childException = e;
                    while (childException != null)
                    {
                        if (childException is not AggregateException)
                        {
                            description.AppendLine();
                            description.Append("[").Append(childException.GetType().Name).Append("] ")
                                .Append(childException.CheckMessage());
                        }

                        childException = childException.InnerException;
                    }
                }
                
                message = description.ToString();
            }

            LogExternal?.Invoke(new LogMessage(level, message));

            if (!Settings.IsStandalone)
            {
                if (level == LogLevel.Debug)
                {
                    UnityEngine.Debug.Log((string?)msg + "\n" + e);
                }
                else
                {
                    UnityEngine.Debug.LogWarning((string?)msg + "\n" + e);
                }
            }
            else
            {
                if (level == LogLevel.Debug)
                {
                    UnityEngine.Debug.Log(message);
                }
                else
                {
                    UnityEngine.Debug.LogWarning(message);
                }
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