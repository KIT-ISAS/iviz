using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Iviz.Msgs
{
    /// <summary>
    /// Generic information about a ROS data connection
    /// </summary>
    public interface IRosConnection
    {
        /// <summary>
        /// The connection topic
        /// </summary>
        string Topic { get; }

        /// <summary>
        /// The ROS message type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// List of TCPROS header entries in the form of strings a=b
        /// </summary>
        IReadOnlyCollection<string> RosHeader { get; }
    }

    /// <summary>
    /// Helper class to create a custom IRosConnection
    /// </summary>
    public static class RosConnection
    {
        public static IRosConnection Create<T>(string topic, string callerId, bool requestNoDelay = true)
            where T : IMessage
        {
            string type = BuiltIns.GetMessageType<T>();
            return new RosConnectionImpl(topic, type,
                new[]
                {
                    $"message_definition={BuiltIns.DecompressDependencies<T>()}",
                    $"callerid={callerId}",
                    $"topic={topic}",
                    $"md5sum={BuiltIns.GetMd5Sum<T>()}",
                    $"type={type}",
                    requestNoDelay ? "tcp_nodelay=1" : "tcp_nodelay=0"
                });
        }

        [DataContract]
        sealed class RosConnectionImpl : IRosConnection
        {
            [DataMember] public string Topic { get; }
            [DataMember] public string Type { get; }
            [DataMember] public IReadOnlyCollection<string> RosHeader { get; }

            public RosConnectionImpl(string topic, string type, IReadOnlyCollection<string> rosHeader) =>
                (Topic, Type, RosHeader) = (topic, type, rosHeader);
        }
    }
}