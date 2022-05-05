using System;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;

namespace Iviz.Roslib;

/// <summary>
///     Full info about a ROS topic and its message type, including dependencies.
/// </summary>
internal sealed class TopicInfo<T> where T : IMessage
{
    readonly IDeserializable<T>? generator;

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

    public TopicInfo(string callerId, string topic, in T generator)
    {
        MessageDependencies = BuiltIns.DecompressDependencies(generator.RosDependenciesBase64);
        CallerId = callerId;
        Topic = topic;
        Md5Sum = generator.RosMd5Sum;
        Type = generator.RosMessageType;
        this.generator = generator is IDeserializable<T> deserializable
            ? deserializable
            : throw new InvalidOperationException("Type T needs to be IDeserializable");
    }

    public TopicInfo(string callerId, string topic, DynamicMessage generator)
        : this(callerId, topic,
            generator is T generatorT
                ? generatorT
                : throw new InvalidOperationException("Type T needs to be DynamicMessage"))
    {
    }
}