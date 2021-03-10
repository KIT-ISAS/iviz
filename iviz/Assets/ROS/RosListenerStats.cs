using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Roslib;
using JetBrains.Annotations;

namespace Iviz.Ros
{
    [DataContract]
    public readonly struct RosListenerStats
    {
        public static readonly RosListenerStats Empty = default;
        
        public RosListenerStats(int totalMessages, int messagesPerSecond, long bytesPerSecond, int messagesInQueue, int dropped)
        {
            TotalMessages = totalMessages;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
            MessagesInQueue = messagesInQueue;
            Dropped = dropped;
        }

        [DataMember] public int TotalMessages { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public long BytesPerSecond { get; }
        [DataMember] public int MessagesInQueue { get; }
        [DataMember] public int Dropped { get; }

        [NotNull]
        public override string ToString()
        {
            return BuiltIns.ToJsonString(this);
        }        
    }
}