﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Ros;
using Iviz.Roslib.Utils;
using Iviz.Tools;
using JetBrains.Annotations;
using Logger = Iviz.Core.Logger;

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

        const int MaxMessagesToPrint = 64;
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

        [NotNull] readonly ConsoleDialogContents dialog;
        public override IDialogPanelContents Panel => dialog;

        readonly ConcurrentQueue<LogMessage> messageQueue = new ConcurrentQueue<LogMessage>();

        readonly ConcurrentSet<string> ids = new ConcurrentSet<string>();

        bool isPaused;

        bool queueIsDirty;
        LogLevel minLogLevel = LogLevel.Info;

        string id = AllString;
        FromIdCode idCode = FromIdCode.All;

        public ConsoleDialogData()
        {
            dialog = DialogPanelManager.GetPanelByType<ConsoleDialogContents>(DialogPanelType.Console);
            ConnectionManager.LogMessageArrived += HandleMessage;
            Logger.LogExternal += HandleMessage;
        }

        public override void FinalizePanel()
        {
            ConnectionManager.LogMessageArrived -= HandleMessage;
            Logger.LogExternal -= HandleMessage;
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            dialog.Close.Clicked += Close;
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
            dialog.FromField.EndEdit += f =>
            {
                id = f;
                idCode = GetIdCode(f);
                ProcessLog(true);
            };

            isPaused = dialog.Pause.State;
            ConnectionManager.LogListener?.SetSuspend(isPaused);

            dialog.Pause.Clicked += () =>
            {
                dialog.Pause.State = !dialog.Pause.State;
                isPaused = dialog.Pause.State;
                ConnectionManager.LogListener?.SetSuspend(isPaused);
            };
        }

        public override void UpdatePanel()
        {
            ProcessLog();
            dialog.FromField.Hints = ExtraFields.Concat(ids);
            dialog.BottomText.text = UpdateStats();
        }

        [NotNull]
        string UpdateStats()
        {
            var listener = ConnectionManager.LogListener;
            if (listener == null)
            {
                return "Error: No Log Listener";
            }

            var description = BuilderPool.Rent();
            try
            {
                listener.WriteDescriptionTo(description);
                string kbPerSecond = (listener.Stats.BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);
                description.Append(" | ").Append(listener.Stats.MessagesPerSecond).Append(" Hz | ")
                    .Append(kbPerSecond).Append(" kB/s");

                return description.ToString();
            }
            finally
            {
                BuilderPool.Return(description);
            }
        }


        void HandleMessage(in LogMessage log)
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

            if (messageQueue.Count >= MaxMessages)
            {
                messageQueue.TryDequeue(out _);
            }

            messageQueue.Enqueue(log);

            queueIsDirty = true;
        }

        void HandleMessage(in Log log)
        {
            if (log.Level < (byte)minLogLevel
                || idCode == FromIdCode.None
                || idCode == FromIdCode.Me
                || log.Name == ConnectionManager.MyId)
            {
                return;
            }

            HandleMessage(new LogMessage(log));
        }

        [NotNull]
        static string ColorFromLevel(LogLevel level)
        {
            if (level >= LogLevel.Fatal)
            {
                return FatalColor;
            }

            if (level >= LogLevel.Error)
            {
                return ErrorColor;
            }

            if (level >= LogLevel.Warn)
            {
                return WarnColor;
            }

            if (level >= LogLevel.Info)
            {
                return InfoColor;
            }

            return DefaultColor;
        }

        public static int IndexFromLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug: return 0;
                case LogLevel.Info: return 1;
                case LogLevel.Warn: return 2;
                case LogLevel.Error: return 3;
                case LogLevel.Fatal: return 4;
                default: throw new ArgumentException("Invalid level", nameof(level));
            }
        }

        public static LogLevel LevelFromIndex(int index)
        {
            switch (index)
            {
                case 0: return LogLevel.Debug;
                case 1: return LogLevel.Info;
                case 2: return LogLevel.Warn;
                case 3: return LogLevel.Error;
                case 4: return LogLevel.Fatal;
                default: throw new ArgumentException("Invalid index", nameof(index));
            }
        }

        static FromIdCode GetIdCode(string id)
        {
            switch (id)
            {
                case AllString:
                    return FromIdCode.All;
                case NoneString:
                    return FromIdCode.None;
                case MeString:
                    return FromIdCode.Me;
                default:
                    return FromIdCode.OnlyId;
            }
        }

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

            var description = BuilderPool.Rent();
            try
            {
                using (var messages = new RentAndClear<LogMessage>(messageQueue.Count))
                using (var indices = new Rent<int>(MaxMessagesToPrint))
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

                        if (idCode == FromIdCode.Me && message.SourceId != null ||
                            idCode == FromIdCode.OnlyId && message.SourceId != id)
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
                            description.Append("<font=Bold>[] ");
                        }
                        else
                        {
                            string dateAsStr = message.SourceId == null
                                ? GameThread.NowFormatted
                                : message.Stamp.ToString(message.Stamp.Date == GameThread.Now.Date
                                    ? "HH:mm:ss.fff"
                                    : "yy-MM-dd HH:mm:ss.fff");

                            description.Append("<font=Bold>[").Append(dateAsStr).Append("] ");
                        }

                        string levelColor = ColorFromLevel(messageLevel);

                        description
                            .Append("<color=").Append(levelColor).Append(">")
                            .Append(message.SourceId ?? "[Me]").Append(": </color></font>");


                        if (message.SourceId == null || message.Message.Length < MaxMessageLength)
                        {
                            description.Append(message.Message).AppendLine();
                        }
                        else
                        {
                            description.Append(message.Message, 0, MaxMessageLength).Append("<i>... +")
                                .Append(message.Message.Length - MaxMessageLength).Append(" chars</i>").AppendLine();
                        }
                    }
                }

                dialog.Text.SetText(description);
                queueIsDirty = false;
            }
            finally
            {
                BuilderPool.Return(description);
            }
        }
    }
}