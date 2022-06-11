#nullable enable

using Iviz.ImageDecoders;
using MarcusW.VncClient.Protocol.Implementation.Native;

namespace Iviz.ImageDecoders
{
    public readonly struct JpegInfo
    {
        public int Width { get; }
        public int Height { get; }
        public int Subsampling { get; }
        public TurboJpegColorspace Colorspace { get; }
        public TurboJpegPixelFormat Format { get; }

        public int DecompressedSize => Width * Height * 4;

        public JpegInfo(int width, int height, int subsampling, TurboJpegColorspace colorspace, TurboJpegPixelFormat format)
        {
            Width = width;
            Height = height;
            Subsampling = subsampling;
            Colorspace = colorspace;
            Format = format;
        }
    }
}