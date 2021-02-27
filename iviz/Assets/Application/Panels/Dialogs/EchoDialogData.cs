using System;
using System.Collections.Generic;
using System.Text;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Ros;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class EchoDialogData : DialogData
    {
        const int MaxMessageLength = 1000;
        const int MaxMessages = 100;

        [NotNull] readonly EchoDialogContents dialog;
        public override IDialogPanelContents Panel => dialog;

        readonly Dictionary<string, Type> topicTypes = new Dictionary<string, Type>();

        readonly Queue<(string DateTime, IMessage Msg)> messageQueue = new Queue<(string, IMessage)>();

        readonly List<TopicEntry> entries = new List<TopicEntry>();
        readonly StringBuilder messageBuffer = new StringBuilder(65536);
        IListener listener;
        bool queueIsDirty;

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
                Action<IMessage> handler = Handler;
                Type listenerType = typeof(Listener<>).MakeGenericType(csType);
                listener = (IListener) Activator.CreateInstance(listenerType, topicName, handler);
            }
        }

        void CreateTopicList()
        {
            var newTopics = ConnectionManager.Connection.GetSystemTopicTypes();
            entries.Clear();

            entries.Add(TopicEntry.Empty);

            foreach (var entry in newTopics)
            {
                string topic = entry.Topic;
                string msgType = entry.Type;

                Type csType = TryGetType(msgType, out Type newCsType) ? newCsType : typeof(DynamicMessage);
                entries.Add(new TopicEntry(topic, msgType, csType));
            }

            entries.Sort();
        }

        void Handler(IMessage msg)
        {
            messageQueue.Enqueue((GameThread.NowFormatted, msg));
            if (messageQueue.Count > MaxMessages)
            {
                messageQueue.Dequeue();
            }

            queueIsDirty = true;
        }


        public override void SetupPanel()
        {
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

                messageQueue.Clear();
                queueIsDirty = false;
                messageBuffer.Length = 0;
                dialog.Text.text = "";
            };

            UpdateOptions();
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
                dialog.Publishers.text =
                    $"{listener.NumPublishers.Active.ToString()}/{listener.NumPublishers.Total.ToString()} publishers";
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

            dialog.Text.SetText(messageBuffer);
            queueIsDirty = false;
        }
    }
}