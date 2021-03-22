using System;
using System.IO;
using Iviz.Msgs;
using System.Collections.Concurrent;
using Iviz.MsgsGen;
using Iviz.MsgsGen.Dynamic;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Rosbag
{
    public sealed class MessageData
    {
        static readonly ConcurrentDictionary<string, IMessage> Generators = new();

        readonly Stream reader;
        readonly long dataStart;
        readonly long dataEnd;
        IMessage? message;

        internal int ConnectionId { get; }

        public time Time { get; }
        public Connection? Connection { get; internal set; }
        public string? Topic => Connection?.Topic;
        public string? Type => Connection?.MessageType;

        public string? Md5Sum => Connection?.Md5Sum;

        public string? MessageDefinition => Connection?.MessageDefinition;

        internal MessageData(Stream reader, long dataStart, long dataEnd, int connectionId, time time)
        {
            this.reader = reader;
            this.dataStart = dataStart;
            this.dataEnd = dataEnd;
            ConnectionId = connectionId;
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
                        generator = (IMessage?) Activator.CreateInstance(msgType) ??
                                    throw new InvalidOperationException($"Failed to create message of type '{type}'");
                    }
                    else if (MessageDefinition != null)
                    {
                        generator = new DynamicMessage(new ClassInfo(null, type, MessageDefinition));
                    }
                    else
                    {
                        throw new InvalidOperationException($"Failed to create message of type '{type}'");
                    }

                    Generators[type] = generator;
                }

                if (reader is MemoryStream memoryStream)
                {
                    message = Buffer.Deserialize(generator, memoryStream.GetBuffer(),
                        (int) (dataEnd - dataStart),
                        (int) dataStart
                    );
                    return message;
                }

                using var bytes = new Rent<byte>((int) (dataEnd - dataStart));

                reader.Seek(dataStart, SeekOrigin.Begin);
                reader.Read(bytes.Array, 0, bytes.Length);

                message = Buffer.Deserialize(generator, bytes.Array, bytes.Length);
                return message;
            }
        }

        public T GetMessage<T>() where T : IMessage => (T) Message;
    }
}