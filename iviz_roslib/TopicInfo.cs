using System;
using Iviz.Msgs;

namespace Iviz.RoslibSharp
{
    public class TopicInfo
    {
        /// <summary>
        /// Concatenated dependencies file.
        /// <seealso cref="IMessage.DependenciesBase64"/>
        /// </summary>
        public readonly string MessageDefinition;

        /// <summary>
        /// ROS name of this node.
        /// </summary>
        public readonly string CallerId;

        /// <summary>
        /// Name of this topic.
        /// </summary>
        public readonly string Topic;

        /// <summary>
        /// MD5 hash of the compact representation of the message.
        /// <seealso cref="IMessage.Md5Sum"/>
        /// </summary>
        public readonly string Md5Sum;

        /// <summary>
        /// Full ROS message type.
        /// <seealso cref="IMessage.MessageType"/>
        /// </summary>
        public readonly string Type;

        /// <summary>
        /// Instance of the message used to generate others of the same type.
        /// <seealso cref="IMessage.Create"/>
        /// </summary>
        public readonly IMessage Generator;

        public TopicInfo(string messageDefinition, string callerId, string topic, string md5Sum, string type, IMessage generator)
        {
            MessageDefinition = messageDefinition;
            CallerId = callerId;
            Topic = topic;
            Md5Sum = md5Sum;
            Type = type;
            Generator = generator;
        }

        public TopicInfo(string callerId, string topic, Type type, IMessage generator = null)
        : this(
                BuiltIns.DecompressDependency(type),
                callerId, topic,
                BuiltIns.GetMd5Sum(type),
                BuiltIns.GetMessageType(type),
                generator
                )
        {
        }
    }
}
