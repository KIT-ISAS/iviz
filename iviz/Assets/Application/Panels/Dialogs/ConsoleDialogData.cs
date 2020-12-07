using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Ros;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class ConsoleDialogData : DialogData
    {
        const int MaxMessages = 100;

        static readonly string[] ExtraFields = {"All", "None"};

        static readonly string[] LogLevelFields =
        {
            "Debug or Higher",
            "<color=navy>Info or Higher</color>",
            "<color=#7F5200>Warn or Higher</color>",
            "<color=brown>Error or Higher</color>",
            "<color=red>Fatal Only</color>"
        };

        [NotNull] readonly ConsoleDialogContents dialog;
        public override IDialogPanelContents Panel => dialog;

        readonly Queue<Log> messageQueue = new Queue<Log>();
        readonly StringBuilder description = new StringBuilder();
        readonly HashSet<string> ids = new HashSet<string>();
        bool queueIsDirty;
        LogLevel minLogLevel = LogLevel.Warn;

        public ConsoleDialogData()
        {
            dialog = DialogPanelManager.GetPanelByType<ConsoleDialogContents>(DialogPanelType.Console);
            ConnectionManager.LogMessageArrived += HandleMessage;
        }

        public override void SetupPanel()
        {
            dialog.Close.Clicked += Close;
            ProcessLog();
            dialog.FromField.Hints = ids;
            dialog.LogLevel.Options = LogLevelFields;
            dialog.LogLevel.Index = IndexFromLevel(minLogLevel);
            dialog.LogLevel.ValueChanged += (f, _) =>
            {
                minLogLevel = LevelFromIndex(f);
                ProcessLog();
            };
        }

        public override void UpdatePanel()
        {
            ProcessLog();
            dialog.FromField.Hints = ExtraFields.Concat(ids);
        }

        void HandleMessage(Log log)
        {
            var messageLevel = (LogLevel) log.Level;
            if (messageLevel < minLogLevel)
            {
                return;
            }

            messageQueue.Enqueue(log);
            if (messageQueue.Count > MaxMessages)
            {
                messageQueue.Dequeue();
            }

            queueIsDirty = true;
        }

        [NotNull]
        static string ColorFromLevel(LogLevel level)
        {
            if (level >= LogLevel.Fatal)
            {
                return "red";
            }

            if (level >= LogLevel.Error)
            {
                return "red";
            }

            if (level >= LogLevel.Warn)
            {
                return "#7F5200";
            }

            if (level >= LogLevel.Info)
            {
                return "navy";
            }

            return "black";
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

        void ProcessLog()
        {
            if (!queueIsDirty)
            {
                return;
            }

            description.Length = 0;
            ids.Clear();

            Log[] messages = messageQueue.ToArray();
            foreach (var message in messages)
            {
                var messageLevel = (LogLevel) message.Level;
                if (messageLevel < minLogLevel)
                {
                    continue;
                }

                if (message.Header.Stamp == default)
                {
                    description.Append("<b>[] ");
                }
                else
                {
                    DateTime t = message.Header.Stamp.ToDateTime().ToLocalTime();
                    description.AppendFormat("<b>[{0:HH:mm:ss}] ", t);
                }

                string levelColor = ColorFromLevel(messageLevel);

                description
                    .Append("<color=").Append(levelColor).Append(">")
                    .Append(message.Name).Append(": </color></b>");
                description.Append(message.Msg).AppendLine();

                ids.Add(message.Name);
            }

            dialog.Text.text = description.ToString();
            queueIsDirty = false;
        }
    }
}