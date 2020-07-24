using System;

namespace Iviz.Msgs
{
    /// <summary>
    /// Attribute that tells the Unity Engine not to strip these fields even if no code accesses them.
    /// (Note: Their only requirement is that the attribute must have the exact name 'Preserve')
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class PreserveAttribute : Attribute { }

    public interface ISerializable
    {
        /// <summary>
        /// Fills this message with information read from the given buffer.
        /// </summary>
        /// <param name="b">
        /// Buffer object.
        /// </param>
        ISerializable RosDeserialize(Buffer b);

        /// <summary>
        /// Fills the buffer with the information from this message.
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