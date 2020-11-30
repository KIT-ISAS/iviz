using System.Runtime.Serialization;
using Iviz.Roslib;

namespace Iviz.Ros
{
    [DataContract]
    public readonly struct RosListenerStats
    {
        public RosListenerStats(int totalMessages, int messagesPerSecond, int bytesPerSecond, int messagesInQueue, int dropped)
        {
            TotalMessages = totalMessages;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
            MessagesInQueue = messagesInQueue;
            Dropped = dropped;
        }

        [DataMember] public int TotalMessages { get; }
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