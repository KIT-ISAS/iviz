#nullable enable

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
        static RoslibConnection Connection => RosManager.Connection;

        long lastMsgBytes;
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
            Type = BuiltIns.GetMessageType<T>();

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
            Connection.Unadvertise(this);
        }

        public void Publish(in T msg)
        {
            Connection.Publish(this, msg);

            Interlocked.Increment(ref recentMsgs);
            Interlocked.Add(ref lastMsgBytes, msg.RosMessageLength);
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
                NumSubscribers = Connection.GetNumSubscribers(this);
                return;
            }

            Stats = new RosSenderStats(recentMsgs, lastMsgBytes);

            RosManager.ReportBandwidthUp(lastMsgBytes);

            Interlocked.Exchange(ref recentMsgs, 0);
            Interlocked.Exchange(ref lastMsgBytes, 0);

            NumSubscribers = Connection.GetNumSubscribers(this);
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