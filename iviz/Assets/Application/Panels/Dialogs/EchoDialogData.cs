using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using Iviz.XmlRpc;
using JetBrains.Annotations;
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

        [NotNull] readonly EchoDialogContents dialog;
        public override IDialogPanelContents Panel => dialog;

        readonly Dictionary<string, Type> topicTypes = new Dictionary<string, Type>();

        readonly ConcurrentQueue<(string DateTime, IMessage Msg)> messageQueue =
            new ConcurrentQueue<(string, IMessage)>();

        readonly List<TopicEntry> entries = new List<TopicEntry>();
        IListener listener;
        bool queueIsDirty;
        bool isPaused;

        public EchoDialogData()
        {
            dialog = DialogPanelManager.GetPanelByType<EchoDialogContents>(DialogPanelType.Echo);
            dialog.Text.vertexBufferAutoSizeReduction = false;
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

        void CreateListener([NotNull] string topicName, [CanBeNull] string rosMsgType, [NotNull] Type csType)
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
                listener = (IListener)Activator.CreateInstance(listenerType,
                    topicName, handler, RosTransportHint.PreferTcp);
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
                var description = BuilderPool.Rent();
                try
                {
                    listener.WriteDescriptionTo(description);
                    dialog.Publishers.SetText(description);
                }
                finally
                {
                    BuilderPool.Return(description);
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

            var description = BuilderPool.Rent();
            var stringWriter = new StringWriter(description, CultureInfo.InvariantCulture);
            var jsonTextWriter = new BoldJsonWriter(stringWriter) { Formatting = Formatting.Indented };
            try
            {
                foreach (var (timeFormatted, msg) in messageQueue)
                {
                    description.Append("<font=Bold>").Append(timeFormatted).Append("</font> ");
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
            finally
            {
                stringWriter.Dispose();
                BuilderPool.Return(description);
            }
        }

        sealed class BoldJsonWriter : JsonTextWriter
        {
            public BoldJsonWriter([NotNull] TextWriter textWriter) : base(textWriter)
            {
            }

            public override void WritePropertyName(string name)
            {
                base.WritePropertyName($"<font=Bold>{name}</font>");
            }

            public override void WritePropertyName(string name, bool escape)
            {
                base.WritePropertyName($"<font=Bold>{name}</font>", escape);
            }
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

        static Action<TMP_Text, StringBuilder, int, int> setTextFn;

        /// <summary> Retrieves private TMP_Text.SetText(StringBuilder, int, int) as delegate </summary>
        [NotNull]
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