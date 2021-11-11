#nullable enable

using System.Runtime.Serialization;
using Iviz.Msgs;

namespace Iviz.Ros
{
    [DataContract]
    public readonly struct RosSenderStats
    {
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public long BytesPerSecond { get; }
        
        public RosSenderStats(int messagesPerSecond, long bytesPerSecond)
        {
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
        }

        public override string ToString()
        {
            return BuiltIns.ToJsonString(this);
        }        
    }
}