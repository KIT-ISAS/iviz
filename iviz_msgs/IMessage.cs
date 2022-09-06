using System;

namespace Iviz.Msgs;

public interface IMessageRos1
{
    /// <summary> MD5 hash of a compact representation of the message. </summary>
    public string RosMd5Sum { get; }

    /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
    public string RosDependenciesBase64 { get; }
}
    
public interface IMessageRos2
{
}

/// <summary>
/// Interface for all ROS messages.
/// All classes or structs representing ROS messages derive from this.
/// </summary>
public interface IMessage : IMessageRos1, IMessageRos2, ISerializable
{
    /// <summary>
    /// Full ROS name of the message.
    /// </summary>
    string RosMessageType { get; }
}
