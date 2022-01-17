using System;
using System.IO;
using Iviz.Msgs;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using Iviz.MsgsGen.Dynamic;
using Iviz.Tools;
using Newtonsoft.Json;

namespace Iviz.Rosbag.Reader
{
    [DataContract]
    public sealed class MessageData
    {
        static readonly ConcurrentDictionary<string, IMessage> Generators = new();

        readonly Stream reader;
        [DataMember] readonly long dataStart;
        [DataMember] readonly long dataEnd;
        IMessage? message;

        [DataMember] public time Time { get; }
        [DataMember] public Connection? Connection { get; internal set; }
        public string? Topic => Connection?.Topic;
        public string? Type => Connection?.MessageType;
        public string? Md5Sum => Connection?.Md5Sum;
        public string? MessageDefinition => Connection?.MessageDefinition;

        internal MessageData(Stream reader, long dataStart, long dataEnd, int connectionId, time time)
        {
            this.reader = reader;
            this.dataStart = dataStart;
            this.dataEnd = dataEnd;
            Time = time;
        }

        public IMessage Message
        {
            get
            {
                if (message != null)
                {
                    return message;
                }

                string? type = Type;
                if (type == null)
                {
                    throw new InvalidOperationException("Connection record does not contain a type name");
                }

                if (!Generators.TryGetValue(type, out IMessage? generator))
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

                    Generators[type] = generator;
                }

                if (reader is MemoryStream memoryStream)
                {
                    var buffer = memoryStream.GetBuffer().AsSpan();
                    message = (IMessage)ReadBuffer.Deserialize(generator, buffer[(int)dataStart..(int)dataEnd]);
                    return message;
                }

                using var bytes = new Rent<byte>((int)(dataEnd - dataStart));

                reader.Seek(dataStart, SeekOrigin.Begin);
                reader.Read(bytes.Array, 0, bytes.Length);

                message = (IMessage)ReadBuffer.Deserialize(generator, bytes);
                return message;
            }
        }

        public T GetMessage<T>() where T : IMessage => (T)Message;

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}