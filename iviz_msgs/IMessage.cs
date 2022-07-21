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
    void AddRos2MessageLength(ref int c);
}

/// <summary>
/// Interface for all ROS messages.
/// All classes or structs representing ROS messages derive from this.
/// </summary>
public interface IMessage : IMessageRos1, IMessageRos2, ISerializable, IDisposable
{
    /// <summary>
    /// Full ROS name of the message.
    /// </summary>
    string RosMessageType { get; }
        
    void IDisposable.Dispose()
    {
    }
}



public interface IMessageCommon : IMessageRos1, IMessageRos2
{
}