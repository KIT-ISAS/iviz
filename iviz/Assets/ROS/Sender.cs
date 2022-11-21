#nullable enable

using System;
using System.Text;
using System.Threading;
using Iviz.Core;
using Iviz.Msgs;

namespace Iviz.Ros
{
    /// <inheritdoc cref="ISender"/>
    /// <typeparam name="T">The ROS message type</typeparam>
    public sealed class Sender<T> : ISender where T : IMessage, new()
    {
        static RosConnection Connection => RosManager.RosConnection;

        readonly Serializer<T> serializer;
        int lastMsgBytes;
        int recentMsgs;

        public string Topic { get; }
        public string Type { get; }
        int? ISender.Id => Id;
        internal int? Id { get; set; }
        public RosSenderStats Stats { get; private set; }
        public int NumSubscribers { get; private set; }

        public Sender(string topic)
        {
            ThrowHelper.ThrowIfNullOrEmpty(topic, nameof(topic));

            Topic = topic;

            var generator = new T();
            Type = generator.RosMessageType;
            serializer = generator.CreateSerializer();

            GameThread.EverySecond += UpdateStats;
            Connection.Advertise(this);
        }

        void ISender.Publish(IMessage msg)
        {
            Publish((T)msg);
        }

        public void Dispose()
        {
            GameThread.EverySecond -= UpdateStats;

            try
            {
                Connection.Unadvertise(this);
            }
            catch (ObjectDisposedException)
            {
                // already shutdown
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Exception while disposing", e);
            }
        }

        public void Publish(T msg)
        {
            Connection.Publish(Id, msg);

            recentMsgs++;
            lastMsgBytes += serializer.RosMessageLength(msg);
        }

        public void Reset()
        {
            Connection.Unadvertise(this);
            Connection.Advertise(this);
        }

        void UpdateStats()
        {
            if (recentMsgs == 0)
            {
                Stats = default;
                NumSubscribers = Connection.GetNumSubscribers(Id);
                return;
            }

            Stats = new RosSenderStats(recentMsgs, lastMsgBytes);

            RosManager.ReportBandwidthUp(lastMsgBytes);

            recentMsgs = 0;
            lastMsgBytes = 0;

            NumSubscribers = Connection.GetNumSubscribers(Id);
        }

        public void WriteDescriptionTo(StringBuilder description)
        {
            int numSubscribers = NumSubscribers;
            if (numSubscribers == -1)
            {
                description.Append("Off");
                return;
            }

            description.Append(numSubscribers).Append(" sub");
        }

        public override string ToString() => $"[{nameof(Sender<T>)} {Topic} [{Type}]]";
    }
}