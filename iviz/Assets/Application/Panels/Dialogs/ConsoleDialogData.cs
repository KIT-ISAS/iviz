﻿#nullable enable

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Iviz.Core;
using Iviz.Ros;
using Iviz.Tools;

namespace Iviz.App
{
    public sealed class ConsoleDialogData : DialogData
    {
        enum FromIdCode
        {
            All,
            None,
            Me,
            OnlyId
        }

        const int MaxMessageLength = 300;

        const int MaxMessagesToPrint = 64; // must be stackalloc-able
        const int MaxMessages = 50000;

        const string FatalColor = "#ff0000";
        const string ErrorColor = "#a52a2a";
        const string WarnColor = "#7F5200";
        const string InfoColor = "#000080";
        const string DefaultColor = "#000000";

        const string AllString = "[All]";
        const string NoneString = "[None]";
        const string MeString = "[Me]";

        static readonly string[] ExtraFields = { AllString, NoneString, MeString };

        static readonly string[] LogLevelFields =
        {
            "Debug or Higher",
            "<color=#000080>Info or Higher</color>",
            "<color=#7F5200>Warn or Higher</color>",
            "<color=#a52a2a>Error or Higher</color>",
            "<color=#ff0000>Fatal Only</color>"
        };

        readonly ConsoleDialogPanel dialog;
        
        readonly ConcurrentQueue<LogMessage> messageQueue = new();
        int messageQueueCount;
            
        readonly Core.ConcurrentSet<string> ids = new();

        bool isPaused;
        bool queueIsDirty;
        LogLevel minLogLevel = LogLevel.Info;
        FromIdCode idCode = FromIdCode.All;
        string id = AllString;

        bool UseUnixTimestamps => false;

        public override IDialogPanel Panel => dialog;

        public ConsoleDialogData()
        {
            dialog = DialogPanelManager.GetPanelByType<ConsoleDialogPanel>(DialogPanelType.Console);
            dialog.Text.vertexBufferAutoSizeReduction = false;
            RosOutLogger.MessageArrived += HandleExternalMessage;
            RosLogger.LogExternal += HandleOwnMessage;
        }

        public override void Dispose()
        {
            RosOutLogger.MessageArrived -= HandleExternalMessage;
            RosLogger.LogExternal -= HandleOwnMessage;
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            dialog.Close.Clicked += Close;
            dialog.Reset.Clicked += Reset;
            ProcessLog();
            dialog.FromField.Value = id;
            dialog.FromField.Hints = ExtraFields.Concat(ids);
            dialog.LogLevel.Options = LogLevelFields;
            dialog.LogLevel.Index = IndexFromLevel(minLogLevel);
            dialog.LogLevel.ValueChanged += (f, _) =>
            {
                minLogLevel = LevelFromIndex(f);
                ProcessLog(true);
            };
            dialog.FromField.Submit += f =>
            {
                id = f;
                idCode = GetIdCode(f);
                ProcessLog(true);
            };

            isPaused = dialog.Pause.State;
            RosManager.Logger.Listener.SetSuspend(isPaused);

            dialog.Pause.Clicked += () =>
            {
                dialog.Pause.State = !dialog.Pause.State;
                isPaused = dialog.Pause.State;
                RosManager.Logger.Listener.SetSuspend(isPaused);
            };
        }

        public override void UpdatePanel()
        {
            ProcessLog();
            dialog.FromField.Hints = ExtraFields.Concat(ids);
            UpdateStats();
        }

        void UpdateStats()
        {
            var listener = RosManager.Logger.Listener;
            using var description = BuilderPool.Rent();
            listener.WriteDescriptionTo(description);
            description.Append(" | ")
                .Append(listener.Stats.MessagesPerSecond)
                .Append(" msg/s | ")
                .AppendBandwidth(listener.Stats.BytesPerSecond);

            dialog.BottomText.SetTextRent(description);
        }

        void HandleExternalMessage(in LogMessage log)
        {
            if (log.Level < minLogLevel
                || idCode is FromIdCode.None or FromIdCode.Me
                || log.SourceId == RosManager.MyId)
            {
                return;
            }

            HandleOwnMessage(log);
        }

        void HandleOwnMessage(in LogMessage log)
        {
            if (log.SourceId != null)
            {
                ids.Add(log.SourceId);
            }

            if (idCode == FromIdCode.None ||
                idCode == FromIdCode.Me && log.SourceId != null ||
                idCode == FromIdCode.OnlyId && log.SourceId != dialog.FromField.Value)
            {
                return;
            }

            if (log.SourceId != null && log.Level < minLogLevel)
            {
                return;
            }

            if (messageQueueCount >= MaxMessages)
            {
                messageQueue.TryDequeue(out _);
                Interlocked.Decrement(ref messageQueueCount);
            }

            messageQueue.Enqueue(log);
            Interlocked.Increment(ref messageQueueCount);

            queueIsDirty = true;
        }

        static string ColorFromLevel(LogLevel level) =>
            level switch
            {
                >= LogLevel.Fatal => FatalColor,
                >= LogLevel.Error => ErrorColor,
                >= LogLevel.Warn => WarnColor,
                >= LogLevel.Info => InfoColor,
                _ => DefaultColor
            };

        static int IndexFromLevel(LogLevel level) =>
            level switch
            {
                LogLevel.Debug => 0,
                LogLevel.Info => 1,
                LogLevel.Warn => 2,
                LogLevel.Error => 3,
                LogLevel.Fatal => 4,
                _ => throw new ArgumentException("Invalid level", nameof(level))
            };

        static LogLevel LevelFromIndex(int index) =>
            index switch
            {
                0 => LogLevel.Debug,
                1 => LogLevel.Info,
                2 => LogLevel.Warn,
                3 => LogLevel.Error,
                4 => LogLevel.Fatal,
                _ => throw new ArgumentException("Invalid index", nameof(index))
            };

        static FromIdCode GetIdCode(string id) =>
            id switch
            {
                AllString => FromIdCode.All,
                NoneString => FromIdCode.None,
                MeString => FromIdCode.Me,
                _ => FromIdCode.OnlyId
            };

        void ProcessLog(bool forceReprocess = false)
        {
            if (!forceReprocess && (!queueIsDirty || isPaused))
            {
                return;
            }

            if (idCode == FromIdCode.None)
            {
                dialog.Text.text = "";
                queueIsDirty = false;
                return;
            }

            Span<int> indices = stackalloc int[MaxMessagesToPrint];
            using var description = BuilderPool.Rent();
            using (var messages = new RentAndClear<LogMessage>(messageQueueCount))
            {
                int indexStart = 0;
                int numIndices = 0;

                messageQueue.CopyTo(messages.Array, 0);
                for (int i = 0; i < messages.Length; i++)
                {
                    var message = messages[i];
                    var messageLevel = message.Level;
                    if (messageLevel < minLogLevel)
                    {
                        continue;
                    }

                    if (idCode is FromIdCode.Me && message.SourceId != null ||
                        idCode is FromIdCode.OnlyId && message.SourceId != id)
                    {
                        continue;
                    }

                    if (numIndices != MaxMessagesToPrint)
                    {
                        indices[numIndices] = i;
                        numIndices++;
                    }
                    else
                    {
                        indices[indexStart] = i;
                        indexStart = (indexStart + 1) % MaxMessagesToPrint;
                    }
                }

                for (int i = 0; i < numIndices; i++)
                {
                    int index = indices[(indexStart + i) % MaxMessagesToPrint];
                    var message = messages[index];
                    var messageLevel = message.Level;

                    if (message.Stamp == default)
                    {
                        description.Append("<b>[] ");
                    }
                    else if (!UseUnixTimestamps)
                    {
                        string dateAsStr = message.Stamp.ToString(
                            message.Stamp.Date == GameThread.Now.Date
                                ? "HH:mm:ss.fff"
                                : "yy-MM-dd HH:mm:ss.fff");
                        description.Append("<b>[").Append(dateAsStr).Append("] ");
                    }
                    else
                    {
                        description.Append("<b>[");
                        RosUtils.FormatTimestamp(message.Stamp, description);
                        description.Append("] ");
                    }

                    string levelColor = ColorFromLevel(messageLevel);

                    description.Append("<color=").Append(levelColor).Append(">");

                    description.Append(message.SourceId ?? "Me");

                    description.Append(messageLevel == LogLevel.Fatal ? " [F]: </color></b>" : ": </color></b>");

                    string innerMessage = message.Message;
                    if (message.SourceId == null || innerMessage.Length < MaxMessageLength)
                    {
                        description.Append(innerMessage).AppendLine();
                    }
                    else
                    {
                        description.Append(innerMessage, 0, MaxMessageLength)
                            .Append("<i>... +")
                            .Append(innerMessage.Length - MaxMessageLength)
                            .Append(" chars</i>")
                            .AppendLine();
                    }
                }
            }

            dialog.Text.SetTextRent(description);
            queueIsDirty = false;
        }

        void Reset()
        {
            messageQueue.Clear();
            Interlocked.Exchange(ref messageQueueCount, 0);
            
            dialog.Text.text = "";
            queueIsDirty = false;
        }
    }
}
