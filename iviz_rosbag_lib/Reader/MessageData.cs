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
    [DataContract]
    public readonly struct MessageData
    {
        static readonly Dictionary<string, ISerializable> GeneratorsByMessageType = new();
        static readonly Dictionary<string, ISerializable> GeneratorsByClassType = new();

        readonly Stream reader;
        [DataMember] readonly long dataStart;
        [DataMember] readonly int dataSize;

        [DataMember] public time Time { get; }
        [DataMember] public Connection? Connection { get; }

        public string? Topic => Connection?.Topic;
        public string? Type => Connection?.MessageType;
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

            if (!GeneratorsByMessageType.TryGetValue(type, out ISerializable? generator))
            {
                var msgType = BuiltIns.TryGetTypeFromMessageName(type);
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

            var rent = Rent.Empty<byte>();
            Span<byte> span = dataSize < 256
                ? stackalloc byte[dataSize]
                : (rent = new Rent<byte>(dataSize)).AsSpan();

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