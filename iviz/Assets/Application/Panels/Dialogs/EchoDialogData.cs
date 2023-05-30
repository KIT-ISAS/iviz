#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
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

        readonly EchoDialogPanel dialog;
        readonly Dictionary<string, Type> topicTypes = new();
        readonly ConcurrentQueue<(string DateTime, string? CallerId, IMessage Msg)> messageQueue = new();
        readonly List<TopicEntry> entries = new();

        Listener? listener;
        bool queueIsDirty;
        bool isPaused;

        public override IDialogPanel Panel => dialog;

        public EchoDialogData()
        {
            dialog = DialogPanelManager.GetPanelByType<EchoDialogPanel>(DialogPanelType.Echo);
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

                listener.Dispose();
            }

            listener = csType == typeof(DynamicMessage)
                ? new Listener<DynamicMessage>(topicName, Handle)
                : Listener.Create(topicName, Handle, csType);
        }

        void CreateTopicList()
        {
            var newTopics = RosManager.Connection.GetSystemPublishedTopicTypes();

            entries.Clear();
            entries.Add(TopicEntry.Empty);

            foreach ((string topic, string msgType) in newTopics)
            {
                var csType = TryGetType(msgType, out Type? newCsType) ? newCsType : typeof(DynamicMessage);
                entries.Add(new TopicEntry(topic, msgType, csType));
            }

            entries.Sort();
        }

        bool Handle(IMessage msg, IRosConnection receiver)
        {
            
            messageQueue.Enqueue((GameThread.NowFormatted, (receiver as IRos1Receiver)?.RemoteId, msg));
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
            dialog.Reset.Clicked += Reset;
            dialog.Topics.ValueChanged += i =>
            {
                if (i == 0)
                {
                    listener?.Dispose();
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
            dialog.Topics.Hints = entries.Select(entry => entry.Description);
        }

        public override void UpdatePanel()
        {
            UpdateOptions();
            ProcessMessages();
            if (listener == null)
            {
                dialog.Messages.text = "---";
            }
            else
            {
                using var description = BuilderPool.Rent();
                listener.WriteDescriptionTo(description);
                description.Append(" | ")
                    .Append(listener.Stats.MessagesPerSecond)
                    .Append(" msg/s | ")
                    .AppendBandwidth(listener.Stats.BytesPerSecond);
                dialog.Messages.SetTextRent(description);
            }
        }

        void ProcessMessages()
        {
            if (!queueIsDirty)
            {
                return;
            }

            if (messageQueue.IsEmpty)
            {
                dialog.Text.text = "";
                queueIsDirty = false;
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
                const int count = MaxMessages * MaxMessageLength;
                int start = description.Length - count;

                dialog.Text.SetTextRent(description, start, count);
            }
            else
            {
                dialog.Text.SetTextRent(description);
            }

            queueIsDirty = false;
        }

        void Reset()
        {
            messageQueue.Clear();
            dialog.Text.text = "";
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

        sealed class TopicEntry : IComparable<TopicEntry>
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
                string shortType = (lastSlash == -1) ? RosMsgType : RosMsgType[(lastSlash + 1)..];
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