namespace Iviz.Msgs
{
    public interface IRequest
    {
        /// <summary>
        /// Create an empty response to this message
        /// </summary>
        /// <returns>New message</returns>
        IResponse CreateResponse();

        bool IsResponseType<T>();

        /// <summary>
        /// Fills this message with the information from the buffer.
        /// </summary>
        /// <param name="ptr">
        /// Pointer to the buffer position where the message starts.
        /// The position of the message end will be written to this pointer.
        /// </param>
        /// <param name="end">The maximum position that the function is allowed to read from.</param>
        unsafe void Deserialize(ref byte* ptr, byte* end);

        /// <summary>
        /// Fills the buffer with the information from this message.
        /// </summary>
        /// <param name="ptr">
        /// Pointer to the buffer position where the message will start.
        /// The position of the message end will be written to this pointer.
        /// </param>
        /// <param name="end">The maximum position that the function is allowed to write to.</param>
        unsafe void Serialize(ref byte* ptr, byte* end);

        /// <summary>
        /// Length of this message in bytes.
        /// </summary>
        int GetLength();
    }

}