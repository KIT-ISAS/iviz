using System;

namespace Iviz.Msgs
{
    /// <summary>
    /// Attribute that tells the Unity engine not to strip these fields even if no code accesses them
    /// (the only requirement is to have the exact name 'Preserve').
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class PreserveAttribute : System.Attribute { }

    /// <summary>
    /// Class that contains information about an underlying buffer.
    /// </summary>
    public unsafe class Buffer
    {
        internal byte* ptr;
        internal readonly byte* end;

        internal Buffer(byte* ptr, byte* end)
        {
            this.ptr = ptr;
            this.end = end;
        }
    }

    public interface ISerializable<T> 
    {
        /// <summary>
        /// Fills this message with the information from the buffer.
        /// </summary>
        /// <param name="ptr">
        /// Pointer to the buffer position where the message starts.
        /// The position of the message end will be written to this pointer.
        /// </param>
        /// <param name="end">The maximum position that the function is allowed to read from.</param>
        T Deserialize(Buffer b);

        /// <summary>
        /// Fills the buffer with the information from this message.
        /// </summary>
        /// <param name="ptr">
        /// Pointer to the buffer position where the message will start.
        /// The position of the message end will be written to this pointer.
        /// </param>
        /// <param name="end">The maximum position that the function is allowed to write to.</param>
        void Serialize(Buffer b);

        /// <summary>
        /// Length of this message in bytes after serialization.
        /// </summary>
        int RosMessageLength { get; }

        /// <summary>
        /// Checks if this message is valid. If not, throws an exception.
        /// </summary>
        void Validate();
    }



}