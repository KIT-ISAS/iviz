#nullable enable
using System;

namespace Iviz.ImageWrappers
{
    public sealed class DecoderException : Exception
    {
        public DecoderException(string message) : base(message)
        {
        }
    }
}