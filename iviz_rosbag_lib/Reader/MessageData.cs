using System;
using System.IO;
using Iviz.Msgs;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.MsgsGen.Dynamic;
using Iviz.Tools;

namespace Iviz.Rosbag.Reader;

/// <summary>
/// Record structure that contains a ROS message.
/// You should use <see cref="LinqUtils.SelectMessage{T}"/> to enumerate on the messages, instead of reading this directly.
/// </summary>
[DataContract]
public readonly struct MessageData
{
    static readonly Dictionary<string, Deserializer> GeneratorsByMessageType = new();

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
    public Connection Connection { get; }

    /// <summary>
    /// ROS topic from which the message originated.
    /// </summary>
    public string Topic => Connection.Topic;

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
    public string? MessageDefinition => Connection.MessageDefinition;

    internal MessageData(Stream reader, long dataStart, long dataEnd, time time, Connection connection)
    {
        this.reader = reader;
        this.dataStart = dataStart;
        dataSize = (int)(dataEnd - dataStart);
        Time = time;
        Connection = connection;
    }

    /// <summary>
    /// Retrieves the enclosed message of type T. Consider using <see cref="LinqUtils.SelectMessage{T}"/> instead of this.
    /// </summary>
    /// <returns>The enclosed message.</returns>
    public T GetMessage<T>(Deserializer<T> deserializer) where T : IMessage, new()
    {
        var rent = Rent.Empty();
        Span<byte> span = dataSize < 256
            ? stackalloc byte[dataSize]
            : (rent = new Rent(dataSize)).AsSpan();

        using (rent)
        {
            reader.Seek(dataStart, SeekOrigin.Begin);
            reader.ReadAll(span);
            return ReadBuffer.Deserialize(deserializer, span);
        }
    }

    /// <summary>
    /// Retrieves the enclosed dynamic message. Consider using <see cref="LinqUtils.SelectAnyMessage"/> instead of this.
    /// </summary>
    /// <returns>The enclosed message.</returns>
    public IMessage GetAnyMessage()
    {
        string? type = Type;
        if (type == null)
        {
            throw new InvalidOperationException("Connection record does not contain a type name");
        }

        Deserializer? deserializer;
        if (GeneratorsByMessageType.TryGetValue(type, out var existingDeserializer))
        {
            deserializer = existingDeserializer;
        }
        else if (MessageDefinition != null)
        {
            deserializer = DynamicMessage.CreateFromDependencyString(type, MessageDefinition).CreateDeserializer();
            GeneratorsByMessageType[type] = deserializer;
        }
        else
        {
            throw new InvalidOperationException($"Failed to create message of type '{type}'");
        }

        var rent = Rent.Empty();

        Span<byte> span = dataSize < 256
            ? stackalloc byte[dataSize]
            : (rent = new Rent(dataSize)).AsSpan();
        using (rent)
        {
            reader.Seek(dataStart, SeekOrigin.Begin);
            reader.ReadAll(span);
            return ReadBuffer.Deserialize(deserializer, span);
        }
    }

    public override string ToString() => BuiltIns.ToJsonString(this);
}