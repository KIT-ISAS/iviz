#nullable enable

using System;
using System.Text;
using System.Threading;
using Iviz.Core;
using Iviz.Msgs;

namespace Iviz.Ros
{
    /// <inheritdoc cref="Sender"/>
    /// <typeparam name="T">The ROS message type</typeparam>
    public sealed class Sender<T> : Sender where T : IMessage, new()
    {
        readonly Serializer<T> serializer;
        int lastMsgBytes;
        int recentMsgs;

        Sender(string topic, T generator) : base(topic, generator.RosMessageType)
        {
            serializer = generator.CreateSerializer();
            GameThread.EverySecond += UpdateStats;
            Connection.Advertise(this);
        }

        public Sender(string topic) : this(topic, new T())
        {
        }

        public override void Publish(IMessage msg)
        {
            if (msg is not T msgAsT)
            {
                BuiltIns.ThrowArgument(nameof(msg), "Message type does not match");
                return; // unreachable
            }

            Publish(msgAsT);
        }

        public override void Dispose()
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
    }
}