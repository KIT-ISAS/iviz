using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Core;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;
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

        const int MaxMessageLength = 250;
        const int MaxMessages = 50;

        const string AllString = "[All]";
        const string NoneString = "[None]";
        const string MeString = "[Me]";
        static readonly string[] ExtraFields = {AllString, NoneString, MeString};

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
        readonly StringBuilder description = new StringBuilder();
        readonly HashSet<string> ids = new HashSet<string>();
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

        public override void SetupPanel()
        {
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
        }

        public override void UpdatePanel()
        {
            ProcessLog();
            dialog.FromField.Hints = ExtraFields.Concat(ids);
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

        void HandleMessage(Log log)
        {
            if (log.Name == ConnectionManager.MyId)
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
                return "#ff0000";
            }

            if (level >= LogLevel.Error)
            {
                return "#a52a2a";
            }

            if (level >= LogLevel.Warn)
            {
                return "#7F5200";
            }

            if (level >= LogLevel.Info)
            {
                return "#000080";
            }

            return "#000000";
        }

        static int IndexFromLevel(LogLevel level)
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

        static LogLevel LevelFromIndex(int index)
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
            if (!forceReprocess && !queueIsDirty)
            {
                return;
            }

            description.Length = 0;

            if (idCode == FromIdCode.None)
            {
                dialog.Text.text = "";
                queueIsDirty = false;
                return;
            }

            LogMessage[] messages = messageQueue.ToArray();

            foreach (var message in messages)
            {
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

                if (message.Stamp == default)
                {
                    description.Append("<b>[] ");
                }
                else
                {
                    description.AppendFormat("<b>[{0:HH:mm:ss}] ", message.Stamp);
                }

                string levelColor = ColorFromLevel(messageLevel);

                description
                    .Append("<color=").Append(levelColor).Append(">")
                    .Append(message.SourceId ?? "[Me]").Append(": </color></b>");


                if (message.Message.Length < MaxMessageLength)
                {
                    description.Append(message.Message).AppendLine();
                }
                else
                {
                    description.Append(message.Message, 0, MaxMessageLength).Append("<i>... +")
                        .Append(message.Message.Length - MaxMessageLength).Append(" chars</i>").AppendLine();
                }
            }

            dialog.Text.text = description.ToString();
            queueIsDirty = false;
        }
    }
}