#nullable enable

using System.Runtime.Serialization;
using Iviz.Msgs;

namespace Iviz.Ros
{
    [DataContract]
    public readonly struct RosSenderStats
    {
        [DataMember] public int TotalMessages { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public int BytesPerSecond { get; }
        
        public RosSenderStats(int totalMessages, int messagesPerSecond, int bytesPerSecond)
        {
            TotalMessages = totalMessages;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
        }

        public override string ToString()
        {
            return BuiltIns.ToJsonString(this);
        }        
    }
}