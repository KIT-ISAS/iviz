using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    [DataContract]
    public readonly struct RosSenderStats
    {
        public RosSenderStats(int totalMessages, int messagesPerSecond, int bytesPerSecond)
        {
            TotalMessages = totalMessages;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
        }

        [DataMember] public int TotalMessages { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public int BytesPerSecond { get; }
        
        public override string ToString()
        {
            return BuiltIns.ToJsonString(this);
        }        
    }
}