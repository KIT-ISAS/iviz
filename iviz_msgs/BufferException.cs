using System;

namespace Iviz.Msgs
{
    public class BufferException : Exception
    {
        public BufferException()
        {
        }

        public BufferException(string? message) : base(message)
        {
        }

        public BufferException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}