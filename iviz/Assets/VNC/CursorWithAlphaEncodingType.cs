#nullable enable

using System;
using System.Buffers;
using System.IO;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.EncodingTypes;
using MarcusW.VncClient.Protocol.Implementation.EncodingTypes.Pseudo;

namespace VNC.Extensions
{
    public sealed class CursorWithAlphaEncodingType : PseudoEncodingType
    {
        /// <inheritdoc />
        public override int Id => (int)WellKnownEncodingType.CursorWithAlpha;

        /// <inheritdoc />
        public override string Name => "Cursor With Alpha";

        /// <inheritdoc />
        public override bool GetsConfirmed => true;

        /// <inheritdoc />
        public override void ReadPseudoEncoding(Stream transportStream, Rectangle rectangle)
        {
            Span<byte> encoding = stackalloc byte[4];
            transportStream.Read(encoding);

            int dataSize = rectangle.Size.Width * rectangle.Size.Height * 4;
            
            byte[]? dataBuffer = null;
            Span<byte> data = dataSize < 512
                ? stackalloc byte[dataSize]
                : (dataBuffer = ArrayPool<byte>.Shared.Rent(dataSize)).Slice(..dataSize);

            try
            {
                transportStream.Read(data);
            }
            finally
            {
                if (dataBuffer != null)
                {
                    ArrayPool<byte>.Shared.Return(dataBuffer);
                }
            }
        }
    }
}