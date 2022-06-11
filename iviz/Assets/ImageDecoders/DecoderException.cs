#nullable enable
using System;

namespace Iviz.ImageDecoders
{
    public sealed class DecoderException : Exception
    {
        public DecoderException(string message) : base(message)
        {
        }
    }
}