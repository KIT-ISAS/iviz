#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using Newtonsoft.Json;
using TMPro;

namespace Iviz.App
{
    public sealed class EchoDialogData : DialogData
    {
        const int MaxMessageLength = 1000;
        const int MaxMessages = 50;

        static readonly JsonSerializer JsonSerializer = JsonSerializer.CreateDefault(
            new JsonSerializerSettings
            {
                Converters = { new ClampJsonConverter(MaxMessageLength) }
            });

        readonly EchoDialogContents dialog;
        readonly Dictionary<string, Type> topicTypes = new();
        readonly ConcurrentQueue<(string DateTime, string? CallerId, IMessage Msg)> messageQueue = new();
        readonly List<TopicEntry> entries = new();

        IListener? listener;
        bool queueIsDirty;
        bool isPaused;

        public override IDialogPanelContents Panel => dialog;

        public EchoDialogData()
        {
            dialog = DialogPanelManager.GetPanelByType<EchoDialogContents>(DialogPanelType.Echo);
            dialog.Text.vertexBufferAutoSizeReduction = false;
        }

        bool TryGetType(string rosMsgType, [NotNullWhen(true)] out Type? type)
        {
            if (topicTypes.TryGetValue(rosMsgType, out var knownType))
            {
                type = knownType;
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

        void CreateListener(string topicName, string? rosMsgType, Type csType)
        {
            if (listener != null)
            {
                if (listener.Topic == topicName && listener.Type == rosMsgType)
                {
                    return;
                }

                listener.Stop();
            }

            listener = csType == typeof(DynamicMessage)
                ? new Listener<DynamicMessage>(topicName, Handler)
                : Listener.Create(topicName, Handler, csType);
        }

        void CreateTopicList()
        {
            var newTopics = ConnectionManager.Connection.GetSystemPublishedTopicTypes();

            entries.Clear();
            entries.Add(TopicEntry.Empty);

            foreach ((string topic, string msgType) in newTopics)
            {
                Type csType = TryGetType(msgType, out Type? newCsType) ? newCsType : typeof(DynamicMessage);
                entries.Add(new TopicEntry(topic, msgType, csType));
            }

            entries.Sort();
        }

        bool Handler(IMessage msg, IRosReceiver receiver)
        {
            messageQueue.Enqueue((GameThread.NowFormatted, receiver.RemoteId, msg));
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
            dialog.Topics.ValueChanged += i =>
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
                    messageQueue.TryDequeue(out _);
                }

                queueIsDirty = false;
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
            dialog.Topics.SetHints(entries.Select(entry => entry.Description));
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
                using (var description = BuilderPool.Rent())
                {
                    listener.WriteDescriptionTo(description);
                    dialog.Publishers.SetText(description);
                }

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

            using var description = BuilderPool.Rent();
            using var stringWriter = new StringWriter(description, CultureInfo.InvariantCulture);
            var jsonTextWriter = new BoldJsonWriter(stringWriter) { Formatting = Formatting.Indented };
            foreach (var (timeFormatted, callerId, msg) in messageQueue)
            {
                description.Append("<b>[").Append(timeFormatted).Append("]");
                if (callerId != null)
                {
                    description.Append(" ").Append(callerId);
                }

                description.Append(":</b>").AppendLine();

                JsonSerializer.Serialize(jsonTextWriter, msg, null);
                description.AppendLine();
            }

            if (description.Length > MaxMessages * MaxMessageLength)
            {
                SetText(dialog.Text, description,
                    description.Length - MaxMessages * MaxMessageLength,
                    MaxMessages * MaxMessageLength);
            }
            else
            {
                dialog.Text.SetText(description);
            }

            queueIsDirty = false;
        }


        sealed class BoldJsonWriter : JsonTextWriter
        {
            public BoldJsonWriter(TextWriter textWriter) : base(textWriter)
            {
            }

            public override void WritePropertyName(string name)
            {
                base.WritePropertyName($"<b>{name}</b>");
            }

            public override void WritePropertyName(string name, bool escape)
            {
                base.WritePropertyName($"<b>{name}</b>", escape);
            }
        }

        sealed class TopicEntry : IComparable<TopicEntry>, IEquatable<TopicEntry>
        {
            public static readonly TopicEntry Empty = new();
            public string Topic { get; }
            public string? RosMsgType { get; }
            public Type CsType { get; }
            public string Description { get; }

            TopicEntry()
            {
                Topic = "";
                RosMsgType = null;
                CsType = typeof(object);
                Description = "<color=grey>(None)</color>";
            }

            public TopicEntry(string topic, string rosMsgType, Type csType)
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

            public bool Equals(TopicEntry? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Topic == other.Topic;
            }
        }

        static Action<TMP_Text, StringBuilder, int, int>? setTextFn;

        /// <summary> Retrieves private TMP_Text.SetText(StringBuilder, int, int) as delegate </summary>
        static Action<TMP_Text, StringBuilder, int, int> SetText
        {
            get
            {
                if (setTextFn != null)
                {
                    return setTextFn;
                }

                var methodInfo = typeof(TMP_Text).GetMethod(nameof(TMP_Text.SetText),
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    null, new[] { typeof(StringBuilder), typeof(int), typeof(int) }, null);
                if (methodInfo == null)
                {
                    throw new NullReferenceException("Missing SetText in TMP_Text!"); // can't really happen
                }

                setTextFn = (Action<TMP_Text, StringBuilder, int, int>)methodInfo.CreateDelegate(
                    typeof(Action<TMP_Text, StringBuilder, int, int>));
                return setTextFn;
            }
        }
    }
}