using System;

namespace Iviz.Msgs
{
    /// <summary>
    /// Attribute that tells the Unity Engine not to strip these fields even if no code accesses them.
    /// (Note: Their only requirement is that the attribute must have the exact name 'Preserve')
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class PreserveAttribute : Attribute { }

    /// <summary>
    /// Establishes that the class can be (de-)serialized as a ROS binary stream. 
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// Deserializes a new message from information read from the given buffer.
        /// </summary>
        /// <param name="b">
        /// Buffer object.
        /// </param>
        ISerializable RosDeserialize(Buffer b);

        /// <summary>
        /// Serializes this message into the buffer.
        /// </summary>
        /// <param name="b">
        /// Buffer object.
        /// </param>
        void RosSerialize(Buffer b);

        /// <summary>
        /// Length of this message in bytes after serialization.
        /// </summary>
        int RosMessageLength { get; }

        /// <summary>
        /// Checks if this message is valid. If not, throws an exception.
        /// </summary>
        void RosValidate();
    }



}