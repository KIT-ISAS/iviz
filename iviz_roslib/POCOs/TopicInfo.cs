using System;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;

namespace Iviz.Roslib
{
    /// <summary>
    ///     Full info about a ROS topic and its message type, including dependencies.
    /// </summary>
    internal sealed class TopicInfo<T> where T : IMessage
    {
        readonly IDeserializable<T>? generator;

        TopicInfo(string messageDependencies, string callerId, string topic, string md5Sum, string type,
            IDeserializable<T>? generator)
        {
            MessageDependencies = messageDependencies;
            CallerId = callerId;
            Topic = topic;
            Md5Sum = md5Sum;
            Type = type;
            this.generator = generator;
        }

        public TopicInfo(string callerId, string topic, IDeserializable<T>? generator = null)
            : this(
                BuiltIns.DecompressDependencies(typeof(T)),
                callerId, topic,
                BuiltIns.GetMd5Sum(typeof(T)),
                BuiltIns.GetMessageType(typeof(T)),
                generator
            )
        {
        }

        public TopicInfo(string callerId, string topic, DynamicMessage generator)
            : this(
                generator.RosInstanceDependencies ??
                throw new NullReferenceException("Dynamic message has not been initialized"),
                callerId, topic,
                generator.RosInstanceMd5Sum ??
                throw new NullReferenceException("Dynamic message has not been initialized"),
                generator.RosType,
                generator as IDeserializable<T> ??
                throw new InvalidOperationException("Type T needs to be DynamicMessage"))
        {
        }

        /// <summary>
        ///     Concatenated dependencies file.
        /// </summary>
        public string MessageDependencies { get; }

        /// <summary>
        ///     ROS name of this node.
        /// </summary>
        public string CallerId { get; }

        /// <summary>
        ///     Name of this topic.
        /// </summary>
        public string Topic { get; }

        /// <summary>
        ///     MD5 hash of the compact representation of the message.
        /// </summary>
        public string Md5Sum { get; }

        /// <summary>
        ///     Full ROS message type.
        /// </summary>
        public string Type { get; }

        /// <summary>
        ///     Instance of the message used to generate others of the same type.
        /// </summary>
        public IDeserializable<T> Generator =>
            generator ?? throw new InvalidOperationException("This type does not have a generator!");
    }
}