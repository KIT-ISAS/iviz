using System;
using System.IO;
using Iviz.Msgs;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.MsgsGen.Dynamic;
using Iviz.Tools;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Rosbag.Reader
{
    /// <summary>
    /// Record structure that contains a ROS message.
    /// You should use <see cref="Utils.SelectMessage{T}"/> to enumerate on the messages, instead of reading this directly.
    /// </summary>
    [DataContract]
    public readonly struct MessageData
    {
        static readonly Dictionary<string, ISerializable> GeneratorsByMessageType = new();
        static readonly Dictionary<string, ISerializable> GeneratorsByClassType = new();

        readonly Stream reader;
        [DataMember] readonly long dataStart;
        [DataMember] readonly long dataEnd;

        /// <summary>
        /// Timestamp of the message.
        /// </summary>
        [DataMember]
        public time Time { get; }

        /// <summary>
        /// Connection from which the message originated.
        /// </summary>
        [DataMember]
        public Connection Connection { get; }

        /// <summary>
        /// ROS topic from which the message originated.
        /// </summary>
        public string? Topic => Connection.Topic;

        /// <summary>
        /// ROS message type.
        /// </summary>
        public string? Type => Connection.MessageType;

        /// <summary>
        /// The MD5 checksum of the ROS message type.
        /// </summary>
        public string? Md5Sum => Connection.Md5Sum;

        /// <summary>
        /// The text definition of the ROS message type.
        /// </summary>        
        public string? MessageDefinition => Connection?.MessageDefinition;

        internal MessageData(Stream reader, long dataStart, long dataEnd, time time, Connection connection)
        {
            this.reader = reader;
            this.dataStart = dataStart;
            this.dataEnd = dataEnd;
            Time = time;
            Connection = connection;
        }

        public T GetMessage<T>(in T generator) where T : IMessage, IDeserializable<T>, new()
        {
            int msgSize = (int)(dataEnd - dataStart);
            var rent = Rent.Empty<byte>();
            Span<byte> span = msgSize < 256
                ? stackalloc byte[msgSize]
                : (rent = new Rent<byte>(msgSize)).AsSpan();

            try
            {
                reader.Seek(dataStart, SeekOrigin.Begin);
                reader.Read(span);
                return ReadBuffer.Deserialize(generator, span);
            }
            finally
            {
                rent.Dispose();
            }
        }

        public T GetMessage<T>() where T : IMessage, IDeserializable<T>, new()
        {
            IDeserializable<T> generator;
            if (GeneratorsByClassType.TryGetValue(typeof(T).Name, out ISerializable? serializable))
            {
                generator = (IDeserializable<T>)serializable;
            }
            else
            {
                generator = new T();
                GeneratorsByClassType[typeof(T).Name] = (ISerializable)generator;
            }

            int msgSize = (int)(dataEnd - dataStart);
            var rent = Rent.Empty<byte>();
            Span<byte> span = msgSize < 256
                ? stackalloc byte[msgSize]
                : (rent = new Rent<byte>(msgSize)).AsSpan();

            try
            {
                reader.Seek(dataStart, SeekOrigin.Begin);
                reader.Read(span);
                return ReadBuffer.Deserialize(generator, span);
            }
            finally
            {
                rent.Dispose();
            }
        }

        public IMessage GetMessage()
        {
            string? type = Type;
            if (type == null)
            {
                throw new InvalidOperationException("Connection record does not contain a type name");
            }

            if (!GeneratorsByMessageType.TryGetValue(type, out ISerializable? generator))
            {
                Type? msgType = BuiltIns.TryGetTypeFromMessageName(type);
                if (msgType != null)
                {
                    generator = (IMessage?)Activator.CreateInstance(msgType) ??
                                throw new InvalidOperationException($"Failed to create message of type '{type}'");
                }
                else if (MessageDefinition != null)
                {
                    generator = DynamicMessage.CreateFromDependencyString(type, MessageDefinition);
                }
                else
                {
                    throw new InvalidOperationException($"Failed to create message of type '{type}'");
                }

                GeneratorsByMessageType[type] = generator;
            }

            int msgSize = (int)(dataEnd - dataStart);
            var rent = Rent.Empty<byte>();
            Span<byte> span = msgSize < 256
                ? stackalloc byte[msgSize]
                : (rent = new Rent<byte>(msgSize)).AsSpan();

            try
            {
                reader.Seek(dataStart, SeekOrigin.Begin);
                reader.Read(span);
                return (IMessage)ReadBuffer.Deserialize(generator, span);
            }
            finally
            {
                rent.Dispose();
            }
        }

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}