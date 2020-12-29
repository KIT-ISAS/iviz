using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.App
{
    public sealed class EchoDialogData : DialogData
    {
        const int MaxMessageLength = 1000;
        const int MaxMessages = 100;

        [NotNull] readonly EchoDialogContents dialog;
        public override IDialogPanelContents Panel => dialog;

        readonly Dictionary<string, Type> topicTypes = new Dictionary<string, Type>();
        readonly Queue<(string, IMessage)> messageQueue = new Queue<(string, IMessage)>();
        readonly List<TopicEntry> entries = new List<TopicEntry>();
        readonly StringBuilder messageBuffer = new StringBuilder();
        IListener listener;
        bool queueIsDirty;
        
        class TopicEntry
        {
            public string Topic { get; }
            public string RosMsgType { get; }
            public Type CsType { get; }
            public string Description { get; }

            public TopicEntry()
            {
                Topic = null;
                RosMsgType = null;
                CsType = null;
                Description = $"<color=grey>(None)</color>";
            }
            
            public TopicEntry([NotNull] string topic, [NotNull] string rosMsgType, [NotNull] Type csType)
            {
                Topic = topic;
                RosMsgType = rosMsgType;
                CsType = csType;

                int lastSlash = RosMsgType.LastIndexOf('/');
                string shortType = (lastSlash == -1) ? RosMsgType : RosMsgType.Substring(lastSlash + 1);
                Description = $"<color=grey>{shortType}</color> {topic}";
            }
        }

        public EchoDialogData()
        {
            dialog = DialogPanelManager.GetPanelByType<EchoDialogContents>(DialogPanelType.Echo);
        }

        bool TryGetType(string rosMsgType, out Type type)
        {
            if (topicTypes.TryGetValue(rosMsgType, out type))
            {
                return true;
            }

            type = BuiltIns.TryGetTypeFromMessageName(rosMsgType);
            if (type != null)
            {
                topicTypes.Add(rosMsgType, type);
                return true;
            }

            return false;
        }

        void CreateListener(string topicName, string rosMsgType, Type csType)
        {
            if (listener != null)
            {
                if (listener.Topic == topicName && listener.Type == rosMsgType)
                {
                    return;
                }

                listener.Stop();
            }

            Type listenerType = typeof(Listener<>).MakeGenericType(csType);
            Action<IMessage> handler = Handler;

            listener = (IListener) Activator.CreateInstance(listenerType, topicName, handler);
        }

        void CreateTopicList()
        {
            var newTopics = ConnectionManager.Connection.GetSystemTopicTypes();
            entries.Clear();
            
            entries.Add(new TopicEntry());
            
            foreach (var entry in newTopics)
            {
                string topic = entry.Topic;
                string msgType = entry.Type;

                if (!TryGetType(msgType, out Type csType))
                {
                    continue;
                } 
                
                entries.Add(new TopicEntry(topic, msgType, csType));
            }
        }

        void Handler(IMessage msg)
        {
            string time = $"<b>{DateTime.Now:HH:mm:ss}</b> ";
            messageQueue.Enqueue((time, msg));
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
            dialog.Topics.Options = Enumerable.Select(entries, entry => entry.Description);
        }

        public override void UpdatePanel()
        {
            ProcessMessages();
        }
        
        void ProcessMessages()
        {
            if (!queueIsDirty)
            {
                return;
            }

            messageBuffer.Length = 0;

            (string time, IMessage msg)[] messages = messageQueue.ToArray();
            foreach ((string time, IMessage msg) in messages)
            {
                string msgAsText = msg.ToJsonString();
                messageBuffer.Append(time);

                if (msgAsText.Length < MaxMessageLength)
                {
                    messageBuffer.Append(msgAsText).AppendLine();
                }
                else
                {
                    messageBuffer.Append(msgAsText, 0, MaxMessageLength).Append("<i>... +")
                        .Append(msgAsText.Length - MaxMessageLength).Append(" chars</i>").AppendLine();
                }
            }

            dialog.Text.text = messageBuffer.ToString();
            queueIsDirty = false;
        }        
    }
}