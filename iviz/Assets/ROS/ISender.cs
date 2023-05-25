#nullable enable

using System;
using System.Text;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    /// <summary>
    /// A wrapper around a <see cref="RosPublisher{TMessage}"/> that persists even if the connection is interrupted.
    /// After a connection is reestablished, this sender is used to readvertise the topic transparently.
    /// There can be multiple Senders that refer to the same shared publisher.
    /// The original is stored in an <see cref="AdvertisedTopic{T}"/>.  
    /// </summary>
    public abstract class Sender
    {
        readonly Serializer serializer;
        int lastMsgBytes;
        int recentMsgs;

        internal int? Id { get; set; }
        public string Topic { get; }
        public string Type { get; }
        public RosSenderStats Stats { get; private set; }
        public int NumSubscribers { get; private set; }

        protected Sender(string topic, string type, Serializer serializer)
        {
            ThrowHelper.ThrowIfNullOrEmpty(topic, nameof(topic));
            ThrowHelper.ThrowIfNullOrEmpty(type, nameof(type));

            Topic = topic;
            Type = type;
            this.serializer = serializer;
            
            GameThread.EverySecond += UpdateStats;
        }

        public void WriteDescriptionTo(StringBuilder description)
        {
            if (Connection.GetNumSubscribers(Id) is not { } numSubscribers)
            {
                description.Append("Off");
                return;
            }

            description.Append(numSubscribers).Append(" sub");
        }
        
        public void Publish(IMessage msg)
        {
            Connection.Publish(Id, msg);

            recentMsgs++;
            lastMsgBytes += serializer.RosMessageLength(msg);
        }

        public override string ToString() => $"[{nameof(Sender)} {Topic} [{Type}]]";

        public void Dispose()
        {
            GameThread.EverySecond -= UpdateStats;
            
            try
            {
                Connection.Unadvertise(this);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Exception while disposing", e);
            }
        }

        void UpdateStats()
        {
            NumSubscribers = Connection.GetNumSubscribers(Id) ?? 0;

            if (recentMsgs == 0)
            {
                Stats = default;
                return;
            }

            Stats = new RosSenderStats(recentMsgs, lastMsgBytes);

            RosManager.ReportBandwidthUp(lastMsgBytes);

            recentMsgs = 0;
            lastMsgBytes = 0;
        }        
        
        private protected static RosConnection Connection => RosManager.RosConnection;
    }
}