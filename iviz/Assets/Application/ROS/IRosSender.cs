using System;
using System.Runtime.Serialization;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Controllers
{
    [DataContract]
    public class RosSenderStats : JsonToString
    {
        public RosSenderStats()
        {
        }

        public RosSenderStats(int totalMessages, int messagesPerSecond, int bytesPerSecond)
        {
            TotalMessages = totalMessages;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
        }

        [DataMember] public int TotalMessages { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public int BytesPerSecond { get; }
    }

    public interface IRosSender
    {
        string Topic { get; }
        string Type { get; }
        int Id { get; }
        RosSenderStats Stats { get; }
        int NumSubscribers { get; }
        void Stop();
        void Publish(IMessage msg);
    }

    public sealed class RosSender<T> : IRosSender where T : IMessage
    {
        int lastMsgBytes;
        int lastMsgCounter;

        int totalMsgCounter;

        public RosSender(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Invalid topic!", nameof(topic));
            }

            Topic = topic;
            Type = BuiltIns.GetMessageType(typeof(T));

            Logger.Internal($"Advertising <b>{topic}</b> <i>[{Type}]</i>.");
            GameThread.EverySecond += UpdateStats;
            ConnectionManager.Advertise(this);
        }

        public string Topic { get; }
        public string Type { get; }
        public int Id { get; private set; }
        public RosSenderStats Stats { get; private set; } = new RosSenderStats();
        public int NumSubscribers => ConnectionManager.Connection.GetNumSubscribers(Topic);

        public void Publish(IMessage msg)
        {
            Publish((T) msg);
        }

        public void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            Logger.Internal($"Unadvertising {Topic}.");
            ConnectionManager.Unadvertise(this);
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void Publish(T msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            totalMsgCounter++;
            lastMsgCounter++;
            lastMsgBytes += msg.RosMessageLength;

            ConnectionManager.Publish(this, msg);
        }

        public void Reset()
        {
            ConnectionManager.Unadvertise(this);
            ConnectionManager.Advertise(this);
        }

        void UpdateStats()
        {
            if (lastMsgCounter == 0)
            {
                Stats = new RosSenderStats();
                return;
            }

            Stats = new RosSenderStats(totalMsgCounter, lastMsgCounter, lastMsgBytes);

            ConnectionManager.ReportBandwidthUp(lastMsgBytes);

            lastMsgBytes = 0;
            lastMsgCounter = 0;
        }
    }
}