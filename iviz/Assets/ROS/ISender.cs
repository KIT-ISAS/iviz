using System;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using JetBrains.Annotations;
using Logger = Iviz.Core.Logger;

namespace Iviz.Ros
{
    public interface ISender
    {
        [NotNull] string Topic { get; }
        [NotNull] string Type { get; }
        int Id { get; }
        RosSenderStats Stats { get; }
        int NumSubscribers { get; }
        void Stop();
        void Publish([NotNull] IMessage msg);
    }

    public sealed class Sender<T> : ISender where T : IMessage
    {
        [NotNull] static RoslibConnection Connection => ConnectionManager.Connection;

        int lastMsgBytes;
        int lastMsgCounter;
        int totalMsgCounter;

        public Sender([NotNull] string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Invalid topic!", nameof(topic));
            }

            Topic = topic;
            Type = BuiltIns.GetMessageType(typeof(T));

            Logger.Info($"Advertising <b>{topic}</b> <i>[{Type}]</i>.");
            GameThread.EverySecond += UpdateStats;
            Connection.Advertise(this);
        }

        public string Topic { get; }
        public string Type { get; }
        public int Id { get; internal set; }
        public RosSenderStats Stats { get; private set; }
        public int NumSubscribers { get; private set; }

        void ISender.Publish(IMessage msg)
        {
            Publish((T) msg);
        }

        public void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            Connection.Unadvertise(this);
        }

        public void Publish([NotNull] in T msg)
        {
            Connection.Publish(this, msg);

            totalMsgCounter++;
            lastMsgCounter++;
            lastMsgBytes += msg.RosMessageLength;

        }

        public void Reset()
        {
            Connection.Unadvertise(this);
            Connection.Advertise(this);
        }

        void UpdateStats()
        {
            if (lastMsgCounter == 0)
            {
                Stats = default;
                NumSubscribers = Connection.GetNumSubscribers(this);
                return;
            }

            Stats = new RosSenderStats(totalMsgCounter, lastMsgCounter, lastMsgBytes);

            ConnectionManager.ReportBandwidthUp(lastMsgBytes);

            lastMsgBytes = 0;
            lastMsgCounter = 0;

            NumSubscribers = Connection.GetNumSubscribers(this);
        }

        public bool TryGetResolvedTopicName([NotNull] out string topicName)
        {
            return ConnectionManager.Connection.TryGetResolvedTopicName(this, out topicName);
        }

        [NotNull]
        public override string ToString()
        {
            return $"[Sender {Topic} [{Type}]]";
        }
    }
}