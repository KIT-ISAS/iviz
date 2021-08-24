using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Ros;
using Iviz.Tools;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class EchoDialogData : DialogData
    {
        const int MaxMessageLength = 1000;
        const int MaxMessages = 50;

        [NotNull] readonly EchoDialogContents dialog;
        public override IDialogPanelContents Panel => dialog;

        readonly Dictionary<string, Type> topicTypes = new Dictionary<string, Type>();

        readonly ConcurrentQueue<(string DateTime, IMessage Msg)> messageQueue =
            new ConcurrentQueue<(string, IMessage)>();

        readonly List<TopicEntry> entries = new List<TopicEntry>();
        readonly StringBuilder messageBuffer = new StringBuilder(65536);
        IListener listener;
        bool queueIsDirty;
        bool isPaused;

        public EchoDialogData()
        {
            dialog = DialogPanelManager.GetPanelByType<EchoDialogContents>(DialogPanelType.Echo);
        }

        [ContractAnnotation("=> false, type:null; => true, type:notnull")]
        bool TryGetType([NotNull] string rosMsgType, out Type type)
        {
            if (topicTypes.TryGetValue(rosMsgType, out type))
            {
                return true;
            }

            type = BuiltIns.TryGetTypeFromMessageName(rosMsgType);
            if (type == null)
            {
                return false;
            }

            topicTypes.Add(rosMsgType, type);
            return true;
        }

        void CreateListener(string topicName, string rosMsgType, [NotNull] Type csType)
        {
            if (listener != null)
            {
                if (listener.Topic == topicName && listener.Type == rosMsgType)
                {
                    return;
                }

                listener.Stop();
            }

            if (csType == typeof(DynamicMessage))
            {
                listener = new Listener<DynamicMessage>(topicName, Handler);
            }
            else
            {
                Func<IMessage, bool> handler = Handler;
                Type listenerType = typeof(Listener<>).MakeGenericType(csType);
                listener = (IListener) Activator.CreateInstance(listenerType, topicName, handler);
            }
        }

        void CreateTopicList()
        {
            var newTopics = ConnectionManager.Connection.GetSystemPublishedTopicTypes();
            
            entries.Clear();
            entries.Add(TopicEntry.Empty);

            foreach ((string topic, string msgType) in newTopics)
            {
                Type csType = TryGetType(msgType, out Type newCsType) ? newCsType : typeof(DynamicMessage);
                entries.Add(new TopicEntry(topic, msgType, csType));
            }

            entries.Sort();
        }

        bool Handler(IMessage msg)
        {
            messageQueue.Enqueue((GameThread.NowFormatted, msg));
            if (messageQueue.Count > MaxMessages)
            {
                messageQueue.TryDequeue(out _);
            }

            queueIsDirty = true;
            return true;
        }


        public override void SetupPanel()
        {
            ResetPanelPosition();

            dialog.Close.Clicked += Close;
            dialog.Topics.ValueChanged += (i, _) =>
            {
                if (i == 0)
                {
                    listener?.Stop();
                    listener = null;
                    return;
                }

                var entry = entries[i];
                CreateListener(entry.Topic, entry.RosMsgType, entry.CsType);

                while (!messageQueue.IsEmpty)
                {
                    messageQueue.TryDequeue(out var __);
                }

                queueIsDirty = false;
                messageBuffer.Length = 0;
                dialog.Text.text = "";
            };

            UpdateOptions();
            
            isPaused = dialog.Pause.State;
            listener?.SetSuspend(isPaused);
            
            dialog.Pause.Clicked += () =>
            {
                dialog.Pause.State = !dialog.Pause.State;
                isPaused = dialog.Pause.State;
                listener?.SetSuspend(isPaused);
            };            
        }

        void UpdateOptions()
        {
            CreateTopicList();
            dialog.Topics.Options = entries.Select(entry => entry.Description);
        }

        public override void UpdatePanel()
        {
            UpdateOptions();
            ProcessMessages();
            if (listener == null)
            {
                dialog.Publishers.text = "---";
                dialog.Messages.text = "---";
                dialog.KBytes.text = "---";
            }
            else
            {
                messageBuffer.Length = 0;
                listener.WriteDescriptionTo(messageBuffer);
                dialog.Publishers.SetText(messageBuffer);
                dialog.Messages.text = $"{listener.Stats.MessagesPerSecond.ToString()} msg/s";
                long kBytesPerSecond = listener.Stats.BytesPerSecond / 1000;
                dialog.KBytes.text = $"{kBytesPerSecond.ToString("N0")} kB/s";
            }
        }

        void ProcessMessages()
        {
            if (!queueIsDirty)
            {
                return;
            }

            messageBuffer.Length = 0;
            foreach (var (timeFormatted, msg) in messageQueue)
            {
                string msgAsText = JsonConvert.SerializeObject(msg, Formatting.Indented,
                    new ClampJsonConverter(MaxMessageLength));
                messageBuffer.Append("<b>").Append(timeFormatted).Append("</b> ");
                messageBuffer.Append(msgAsText).AppendLine();
            }

            if (messageBuffer.Length > MaxMessages * MaxMessageLength)
            {
                messageBuffer.Remove(0, MaxMessages * MaxMessageLength - messageBuffer.Length);
            }

            dialog.Text.SetText(messageBuffer);
            queueIsDirty = false;
        }
        
        sealed class TopicEntry : IComparable<TopicEntry>
        {
            public static readonly TopicEntry Empty = new TopicEntry();
            [NotNull] public string Topic { get; }
            [CanBeNull] public string RosMsgType { get; }
            [NotNull] public Type CsType { get; }
            [NotNull] public string Description { get; }

            TopicEntry()
            {
                Topic = "";
                RosMsgType = null;
                CsType = typeof(object);
                Description = "<color=grey>(None)</color>";
            }

            public TopicEntry([NotNull] string topic, [NotNull] string rosMsgType, [NotNull] Type csType)
            {
                Topic = topic;
                RosMsgType = rosMsgType;
                CsType = csType;

                int lastSlash = RosMsgType.LastIndexOf('/');
                string shortType = (lastSlash == -1) ? RosMsgType : RosMsgType.Substring(lastSlash + 1);
                Description = $"{topic} <color=grey>[{shortType}]</color>";
            }

            public int CompareTo(TopicEntry other)
            {
                return ReferenceEquals(this, other)
                    ? 0
                    : string.Compare(Topic, other.Topic, StringComparison.Ordinal);
            }
        }        
    }
}