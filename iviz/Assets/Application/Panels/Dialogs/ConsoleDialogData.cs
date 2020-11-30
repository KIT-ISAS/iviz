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

        [NotNull] readonly ConsoleDialogContents dialog;
        public override IDialogPanelContents Panel => dialog;

        Listener<Log> listener;
        readonly Queue<Log> messageQueue = new Queue<Log>();
        readonly StringBuilder description = new StringBuilder();
        readonly HashSet<string> ids = new HashSet<string>();
        bool queueIsDirty;

        public ConsoleDialogData([NotNull] ModuleListPanel newPanel) : base(newPanel)
        {
            dialog = DialogPanelManager.GetPanelByType<ConsoleDialogContents>(DialogPanelType.Console);
            listener = new Listener<Log>("/rosout_agg", HandleMessage);
        }

        public override void SetupPanel()
        {
            dialog.Close.Clicked += Close;
            ProcessLog();
            dialog.FromField.Hints = ids;
        }

        public override void UpdatePanel()
        {
            ProcessLog();
            dialog.FromField.Hints = ExtraFields.Concat(ids);
        }

        void HandleMessage(Log log)
        {
            messageQueue.Enqueue(log);
            if (messageQueue.Count > MaxMessages)
            {
                messageQueue.Dequeue();
            }

            queueIsDirty = true;
        }

        static string ColorFromLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error:
                    return "brown";
                case LogLevel.Warn:
                    return "orange";
                case LogLevel.Fatal:
                    return "red";
                case LogLevel.Info:
                    return "navy";
                default:
                    return "black";
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
                if (message.Header.Stamp == default)
                {
                    description.Append("<b>[] ");
                }
                else
                {
                    description.AppendFormat("<b>[{0:HH:mm:ss}] ", message.Header.Stamp.ToDateTime());
                }

                string levelColor = ColorFromLevel((LogLevel) message.Level);

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