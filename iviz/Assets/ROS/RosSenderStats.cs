#nullable enable

using System.Runtime.Serialization;
using Iviz.Msgs;

namespace Iviz.Ros
{
    /// <summary>
    /// Struct that contains statistics about a ROS listener
    /// </summary>
    [DataContract]
    public readonly struct RosSenderStats
    {
        /// <summary>
        /// The number of messages sent in the last second.
        /// </summary>
        [DataMember]
        public int MessagesPerSecond { get; }

        /// <summary>
        /// The number of bytes received in the last second.
        /// </summary>
        [DataMember]
        public int BytesPerSecond { get; }

        public RosSenderStats(int messagesPerSecond, int bytesPerSecond)
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