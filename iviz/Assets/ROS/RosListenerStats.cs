#nullable enable

using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Roslib;
using JetBrains.Annotations;

namespace Iviz.Ros
{
    /// <summary>
    /// Struct that contains statistics about a ROS listener
    /// </summary>
    [DataContract]
    public readonly struct RosListenerStats
    {
        /// <summary>
        /// Total messages received by the listener.
        /// </summary>
        [DataMember] public int TotalMessages { get; }

        /// <summary>
        /// The number of messages received in the last second.
        /// </summary>
        [DataMember] public int MessagesPerSecond { get; }

        /// <summary>
        /// The number of bytes received in the last second.
        /// </summary>
        [DataMember] public long BytesPerSecond { get; }

        /// <summary>
        /// The number of messages currently in queue.
        /// </summary>
        [DataMember] public int MessagesInQueue { get; }

        /// <summary>
        /// The number of messages that were received but weren't processed by iviz.
        /// </summary>
        [DataMember] public int Dropped { get; }

        public RosListenerStats(int totalMessages, int messagesPerSecond, long bytesPerSecond, int messagesInQueue, int dropped)
        {
            TotalMessages = totalMessages;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
            MessagesInQueue = messagesInQueue;
            Dropped = dropped;
        }

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}