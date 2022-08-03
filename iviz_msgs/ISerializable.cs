namespace Iviz.Msgs;

public interface ISerializableRos1
{
    /// <summary>
    /// Serializes this message into the buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    void RosSerialize(ref WriteBuffer b);

    /// <summary>
    /// Length of this message in bytes after serialization.
    /// </summary>
    int RosMessageLength { get; }
}

public interface ISerializableRos2
{
    /// <summary>
    /// Serializes this message into the buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    void RosSerialize(ref WriteBuffer2 b);

    /// <summary>
    /// Length of this message in bytes after serialization.
    /// </summary>
    int Ros2MessageLength { get; }
    
    /// <summary>
    /// Adds the length of the ROS2 message to the offset argument, including padding for alignment depending
    /// on the current value of the offset. 
    /// </summary>
    void AddRos2MessageLength(ref int offset);
}

/// <summary>
/// Establishes that the class can be (de-)serialized as a ROS binary stream. 
/// </summary>
public interface ISerializable : ISerializableRos1, ISerializableRos2
{
    /// <summary>
    /// Checks if this message is valid (no null pointers, fixed arrays have the right size, and so on).
    /// If not, throws an exception.
    /// </summary>
    void RosValidate();
}

public interface IDeserializableRos1<out T>
{
    /// <summary>
    /// Creates a new message an deserializes into it the information read from the given buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    T RosDeserialize(ref ReadBuffer b);
}

public interface IDeserializableRos2<out T>
{
    /// <summary>
    /// Creates a new message an deserializes into it the information read from the given buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    //T RosDeserialize(ref ReadBuffer b) => throw new RosInvalidMessageForVersion();
    T RosDeserialize(ref ReadBuffer2 b);
}


public interface IDeserializable<out T> : IDeserializableRos1<T>, IDeserializableRos2<T>
    where T : ISerializableRos1, ISerializableRos2
{
}