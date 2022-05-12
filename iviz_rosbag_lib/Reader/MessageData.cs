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
        [DataMember] readonly int dataSize;

        /// <summary>
        /// Timestamp of the message.
        /// </summary>
        [DataMember]
        public time Time { get; }

        /// <summary>
        /// Connection from which the message originated.
        /// </summary>
        [DataMember]
        public Connection? Connection { get; }

        /// <summary>
        /// ROS topic from which the message originated.
        /// </summary>
        public string? Topic => Connection?.Topic;

        /// <summary>
        /// ROS message type.
        /// </summary>
        public string? Type => Connection?.MessageType;

        /// <summary>
        /// The MD5 checksum of the ROS message type.
        /// </summary>
        public string? Md5Sum => Connection?.Md5Sum;

        /// <summary>
        /// The text definition of the ROS message type.
        /// </summary>        
        public string? MessageDefinition => Connection?.MessageDefinition;

        internal MessageData(Stream reader, long dataStart, long dataEnd, time time, Connection? connection)
        {
            this.reader = reader;
            this.dataStart = dataStart;
            dataSize = (int)(dataEnd - dataStart);
            Time = time;
            Connection = connection;
        }

        /// <summary>
        /// Retrieves the enclosed message of type T. This is the preferred alternative, used automatically
        /// by <see cref="Utils.SelectMessage{T}"/>.
        /// </summary>
        /// <param name="generator">Any instance of T, such as new T(). If the generator is a <see cref="DynamicMessage"/>, it must be already defined.</param>
        /// <typeparam name="T">The message type.</typeparam>
        /// <returns>The enclosed message.</returns>
        public T GetMessage<T>(in T generator) where T : IMessage, IDeserializable<T>, new()
        {
            var rent = Rent.Empty<byte>();
            Span<byte> span = dataSize < 256
                ? stackalloc byte[dataSize]
                : (rent = new Rent<byte>(dataSize)).AsSpan();

            using (rent)
            {
                reader.Seek(dataStart, SeekOrigin.Begin);
                reader.Read(span);
                return ReadBuffer.Deserialize(generator, span);
            }
        }

        /// <summary>
        /// Retrieves the enclosed message of type T. Uses a cache of generators.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <returns>The enclosed message.</returns>
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

            var rent = Rent.Empty<byte>();
            Span<byte> span = dataSize < 256
                ? stackalloc byte[dataSize]
                : (rent = new Rent<byte>(dataSize)).AsSpan();

            using (rent)
            {
                reader.Seek(dataStart, SeekOrigin.Begin);
                reader.Read(span);
                return ReadBuffer.Deserialize(generator, span);
            }
        }

        /// <summary>
        /// Retrieves the enclosed message of type T. Use this if you do not know the message type.
        /// This will search for the ROS message type in the known messages, and if it does not exist,
        /// will create a new <see cref="DynamicMessage"/>. 
        /// </summary>
        /// <returns>The enclosed message.</returns>
        public IMessage GetMessage()
        {
            string? type = Type;
            if (type == null)
            {
                throw new InvalidOperationException("Connection record does not contain a type name");
            }

            ISerializable? generator;
            if (GeneratorsByMessageType.TryGetValue(type, out ISerializable? serializable))
            {
                generator = serializable;
            }
            else if (BuiltIns.TryGetGeneratorFromMessageName(type) is { } tryGenerator)
            {
                generator = tryGenerator;
                GeneratorsByMessageType[type] = generator;
            }
            else if (MessageDefinition != null)
            {
                generator = DynamicMessage.CreateFromDependencyString(type, MessageDefinition);
                GeneratorsByMessageType[type] = generator;
            }
            else
            {
                throw new InvalidOperationException($"Failed to create message of type '{type}'");
            }

            var rent = Rent.Empty<byte>();

            Span<byte> span = dataSize < 256
                ? stackalloc byte[dataSize]
                : (rent = new Rent<byte>(dataSize)).AsSpan();
            using (rent)
            {
                reader.Seek(dataStart, SeekOrigin.Begin);
                reader.Read(span);
                return (IMessage)ReadBuffer.Deserialize(generator, span);
            }
        }

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}