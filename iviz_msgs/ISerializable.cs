namespace Iviz.Msgs;

/// <summary>
/// Establishes that the class can be (de-)serialized as a ROS binary stream. 
/// </summary>
public interface ISerializable
{
    /// <summary>
    /// Checks if this message is valid (no null pointers, fixed arrays have the right size, and so on).
    /// If not, throws an exception.
    /// </summary>
    void RosValidate();
}

public interface ISerializableRos1 : ISerializable
{
    /// <summary>
    /// Serializes this message into the buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    void RosSerialize(ref WriteBuffer b) => throw new RosInvalidMessageForVersion();

    /// <summary>
    /// Length of this message in bytes after serialization.
    /// </summary>
    int RosMessageLength { get; }

    /// <summary>
    /// Creates a new message and deserializes into it the information read from the given buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    ISerializableRos1 RosDeserializeBase(ref ReadBuffer b);
}

public interface ISerializableRos2 : ISerializable
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
}

public interface IDeserializable<out T>
{
}

public interface IDeserializableRos1<out T> : IDeserializable<T> where T : ISerializableRos1
{
    /// <summary>
    /// Creates a new message an deserializes into it the information read from the given buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    T RosDeserialize(ref ReadBuffer b);
}

public interface IDeserializableRos2<out T> : IDeserializable<T> where T : ISerializableRos2
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

public interface IDeserializableCommon<out T> : IDeserializableRos1<T>, IDeserializableRos2<T>
    where T : ISerializableRos1, ISerializableRos2
{
}