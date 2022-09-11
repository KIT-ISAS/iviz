using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;

namespace Iviz.Roslib;

/// <summary>
/// Full info about a ROS topic and its message type, including dependencies.
/// </summary>
internal sealed class TopicInfo
{
    /// <summary>
    /// Concatenated dependencies file.
    /// </summary>
    public string MessageDependencies =>
        Generator.RosDependenciesBase64 == DynamicMessage.RosAny
            ? DynamicMessage.RosAny
            : BuiltIns.DecompressDependencies(Generator.RosDependenciesBase64);

    /// <summary>
    /// ROS name of this node.
    /// </summary>
    public string CallerId { get; }

    /// <summary>
    /// Name of this topic.
    /// </summary>
    public string Topic { get; }

    /// <summary>
    /// MD5 hash of the compact representation of the message.
    /// </summary>
    public string Md5Sum => Generator.RosMd5Sum;

    /// <summary>
    /// Full ROS message type.
    /// </summary>
    public string Type => Generator.RosMessageType;

    /// <summary>
    /// Instance of the message used to generate others of the same type.
    /// </summary>
    public IMessage Generator { get; }
    
    public TopicInfo(string callerId, string topic, IMessage generator)
    {
        CallerId = callerId;
        Topic = topic;
        Generator = generator;
    }
}