namespace Iviz.Msgs;

/// <summary>
/// Establishes that the class can be (de-)serialized as a ROS binary stream. 
/// </summary>
public interface ISerializable
{
    /// <summary>
    /// Serializes this message into the buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    void RosSerialize(ref WriteBuffer b) => throw new RosInvalidMessageForVersion();

    void RosSerialize(ref WriteBuffer2 b) => throw new RosInvalidMessageForVersion();

    /// <summary>
    /// Length of this message in bytes after serialization.
    /// </summary>
    int RosMessageLength { get; }

    /// <summary>
    /// Checks if this message is valid (no null pointers, fixed arrays have the right size, and so on).
    /// If not, throws an exception.
    /// </summary>
    void RosValidate();

    /// <summary>
    /// Creates a new message and deserializes into it the information read from the given buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    ISerializable RosDeserializeBase(ref ReadBuffer b) => throw new RosInvalidMessageForVersion();

    ISerializable RosDeserializeBase(ref ReadBuffer2 b) => throw new RosInvalidMessageForVersion();
}

public interface IDeserializable<out T> where T : ISerializable
{
}

public interface IDeserializableRos1<out T> : IDeserializable<T> where T : ISerializable
{
    /// <summary>
    /// Creates a new message an deserializes into it the information read from the given buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    T RosDeserialize(ref ReadBuffer b) => throw new RosInvalidMessageForVersion();

    //T RosDeserialize(ref ReadBuffer2 b) => throw new RosInvalidMessageForVersion();
}

public interface IDeserializableRos2<out T> : IDeserializable<T> where T : ISerializable
{
    /// <summary>
    /// Creates a new message an deserializes into it the information read from the given buffer.
    /// </summary>
    /// <param name="b">
    /// Buffer object.
    /// </param>
    //T RosDeserialize(ref ReadBuffer b) => throw new RosInvalidMessageForVersion();

    T RosDeserialize(ref ReadBuffer2 b) => throw new RosInvalidMessageForVersion();
}