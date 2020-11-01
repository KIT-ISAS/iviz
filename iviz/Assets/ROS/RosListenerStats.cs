using System.Runtime.Serialization;
using Iviz.Roslib;

namespace Iviz.Ros
{
    [DataContract]
    public readonly struct RosListenerStats
    {
        public RosListenerStats(int totalMessages, float jitterMin, float jitterMax,
            float jitterMean, int messagesPerSecond, int bytesPerSecond, int messagesInQueue, int dropped)
        {
            TotalMessages = totalMessages;
            JitterMin = jitterMin;
            JitterMax = jitterMax;
            JitterMean = jitterMean;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
            MessagesInQueue = messagesInQueue;
            Dropped = dropped;
        }

        [DataMember] public int TotalMessages { get; }
        [DataMember] public float JitterMin { get; }
        [DataMember] public float JitterMax { get; }
        [DataMember] public float JitterMean { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public int BytesPerSecond { get; }
        [DataMember] public int MessagesInQueue { get; }
        [DataMember] public int Dropped { get; }

        public override string ToString()
        {
            return Utils.ToJsonString(this);
        }        
    }
}