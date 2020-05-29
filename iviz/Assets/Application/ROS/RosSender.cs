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
        [DataMember] public float JitterMin { get; }
        [DataMember] public float JitterMax { get; }
        [DataMember] public float JitterMean { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public int BytesPerSecond { get; }

        public RosSenderStats(int totalMessages, float jitterMin, float jitterMax, float jitterMean, int messagesPerSecond, int bytesPerSecond)
        {
            TotalMessages = totalMessages;
            JitterMin = jitterMin;
            JitterMax = jitterMax;
            JitterMean = jitterMean;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
        }

        public RosSenderStats()
        {
        }
    }

    public abstract class RosSender
    {
        public string Topic { get; }
        public string Type { get; }
        public abstract int Id { get; set; }
        public RosSenderStats Stats { get; protected set; } = new RosSenderStats();

        protected RosSender(string topic, string type)
        {
            Topic = topic ?? throw new System.ArgumentNullException(nameof(topic));
            Type = type ?? throw new System.ArgumentNullException(nameof(type));
            Debug.Log("RosListener: Requesting advertisement for topic " + Topic);
        }

        public abstract void Stop();
        public abstract void UpdateStats();
    }

    public class RosSender<T> : RosSender where T : IMessage
    {
        readonly List<float> timesOfArrival = new List<float>();

        public override int Id { get; set; }
        public int NumSubscribers => ConnectionManager.Connection.GetNumSubscribers(Topic);
        public int TotalMsgCounter { get; private set; }
        public int TotalMsgBytes { get; private set; }

        public RosSender(string topic) :
            base(topic, BuiltIns.GetMessageType(typeof(T)))
        {
            ConnectionManager.Advertise(this);
        }

        public void Publish(T msg)
        {
            TotalMsgCounter++;
            TotalMsgBytes += msg.RosMessageLength;
            timesOfArrival.Add(Time.time);

            ConnectionManager.Publish(this, msg);
        }

        public override void Stop()
        {
            ConnectionManager.Unadvertise(this);
        }

        public override void UpdateStats()
        {
            if (!timesOfArrival.Any())
            {
                Stats = new RosSenderStats();
                return;
            }
            else
            {
                float jitterMin = float.MaxValue;
                float jitterMax = float.MinValue;

                for (int i = 0; i < timesOfArrival.Count() - 1; i++)
                {
                    float jitter = timesOfArrival[i + 1] - timesOfArrival[i];
                    if (jitter < jitterMin) jitterMin = jitter;
                    if (jitter > jitterMax) jitterMax = jitter;
                }

                Stats = new RosSenderStats(
                    TotalMsgCounter,
                    jitterMin,
                    jitterMax,
                    timesOfArrival.Count == 0 ? 0 : (timesOfArrival.Last() - timesOfArrival.First()) / timesOfArrival.Count(),
                    timesOfArrival.Count,
                    TotalMsgBytes
                );
                TotalMsgBytes = 0;
                timesOfArrival.Clear();
            }
        }
    }
}


