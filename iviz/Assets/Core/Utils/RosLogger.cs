#nullable enable

using System;
using System.Text;
using System.Threading;
using Iviz.Tools;

namespace Iviz.Core
{
    public static class RosLogger
    {
        const string NullMessage = "[null message]";
        const string NullException = "[null exception]";

        const int MaxPublishedPerSecond = 30;
        static volatile int publishedThisSec;

        public delegate void ExternalLogDelegate(in LogMessage msg);

        public static event Action<string>? LogInternal;
        public static event ExternalLogDelegate? LogExternal;

        public static void ResetCounter()
        {
            Interlocked.Exchange(ref publishedThisSec, 0);
        }

        public static void Info(object? t)
        {
            PublishExternal(t?.ToString(), LogLevel.Info);
        }

        public static void Error(object? t)
        {
            PublishExternal(t?.ToString(), LogLevel.Error);
        }

        public static void Error(object? t, Exception? e)
        {
            PublishExternal(t, LogLevel.Error, e);
        }

        [Obsolete]
        public static void Error(Exception _)
        {
        }

        public static void Warn(object? t)
        {
            PublishExternal(t?.ToString(), LogLevel.Warn);
        }

        public static void Debug(object? t)
        {
            PublishExternal(t?.ToString(), LogLevel.Debug);
        }

        public static void Debug(object? t, Exception e)
        {
            PublishExternal(t, LogLevel.Debug, e);
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
                        if (childException is AggregateException)
                        {
                            childException = childException.InnerException;
                            continue;
                        }

                        if (childException is OperationCanceledException)
                        {
                            description.AppendLine()
                                .Append("<color=red>" + nameof(OperationCanceledException) + "</color> " +
                                        "The task was canceled");
                            childException = childException.InnerException;
                            continue;
                        }

                        description.AppendLine()
                            .Append("<color=red>")
                            .Append(childException.GetType().Name)
                            .Append("</color> ")
                            .Append(childException.CheckMessage());
                        childException = childException.InnerException;
                    }
                }

                message = description.ToString();
            }

            LogInternal?.Invoke(message);
            UnityEngine.Debug.LogWarning(message);
        }


        static void PublishExternal(string? msg, LogLevel level)
        {
            if (LogExternal == null)
            {
                PublishLocal();
                return;
            }

            Interlocked.Increment(ref publishedThisSec);
            if (publishedThisSec == MaxPublishedPerSecond)
            {
                UnityEngine.Debug.LogWarning($"{nameof(RosLogger)}: Already published " +
                                             MaxPublishedPerSecond + " messages this second. " +
                                             "Suppressing the rest.");
                return;
            }

            if (publishedThisSec > MaxPublishedPerSecond)
            {
                return;
            }

            try
            {
                LogExternal(new LogMessage(level, msg ?? NullMessage));
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(
                    $"[{nameof(RosLogger)}]: {nameof(PublishExternal)} failed! {Logger.ExceptionToString(e)}");
            }

            PublishLocal();

            void PublishLocal()
            {
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
        }

        static void PublishExternal(object? msg, LogLevel level, Exception? e)
        {
            if (LogExternal == null)
            {
                PublishLocal();
                return;
            }

            Interlocked.Increment(ref publishedThisSec);
            if (publishedThisSec == MaxPublishedPerSecond)
            {
                UnityEngine.Debug.LogWarning($"{nameof(RosLogger)}: Already published " +
                                             $"{MaxPublishedPerSecond.ToString()} messages this second. " +
                                             "Suppressing the rest.");
                return;
            }

            if (publishedThisSec > MaxPublishedPerSecond)
            {
                return;
            }

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

            LogExternal(new LogMessage(level, message));
            PublishLocal();

            void PublishLocal()
            {
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
                        UnityEngine.Debug.Log(msg);
                    }
                    else
                    {
                        UnityEngine.Debug.LogWarning(msg);
                    }
                }
            }
        }
    }
}