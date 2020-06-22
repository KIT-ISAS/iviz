using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App
{
    [DataContract]
    public class RosSenderStats : JsonToString
    {
        [DataMember] public int TotalMessages { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public int BytesPerSecond { get; }

        public RosSenderStats()
        {
        }

        public RosSenderStats(int totalMessages, int messagesPerSecond, int bytesPerSecond)
        {
            TotalMessages = totalMessages;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
        }
    }

    public abstract class RosSender
    {
        public string Topic { get; }
        public string Type { get; }
        public int Id { get; protected set; }
        public RosSenderStats Stats { get; protected set; } = new RosSenderStats();

        public int NumSubscribers => ConnectionManager.Connection.GetNumSubscribers(Topic);
        public int TotalMsgCounter { get; protected set; }
        public int LastMsgBytes { get; protected set; }
        public int LastMsgCounter { get; protected set; }

        protected RosSender(string topic, string type)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new System.ArgumentException("Invalid topic!", nameof(topic));
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new System.ArgumentException("Invalid type!", nameof(type));
            }
            Logger.Internal($"Advertising <b>{topic}</b> <i>[{type}]</i>.");
            Topic = topic;
            Type = type;

            GameThread.EverySecond += UpdateStats;
        }

        public virtual void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
        }

        void UpdateStats()
        {
            if (LastMsgCounter == 0)
            {
                Stats = new RosSenderStats();
            }
            else
            {
                Stats = new RosSenderStats(
                    TotalMsgCounter,
                    LastMsgCounter,
                    LastMsgBytes
                );
                LastMsgBytes = 0;
                LastMsgCounter = 0;
            }
        }
    }

    public sealed class RosSender<T> : RosSender where T : IMessage
    {
        public RosSender(string topic) :
            base(topic, BuiltIns.GetMessageType(typeof(T)))
        {
            ConnectionManager.Advertise(this);
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void Publish(T msg)
        {
            TotalMsgCounter++;
            LastMsgCounter++;
            LastMsgBytes += msg.RosMessageLength;

            ConnectionManager.Publish(this, msg);
        }

        public override void Stop()
        {
            base.Stop();
            Logger.Internal($"Unadvertising {Topic}.");
            ConnectionManager.Unadvertise(this);
        }
    }
}


