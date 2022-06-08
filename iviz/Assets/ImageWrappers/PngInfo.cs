#nullable enable

namespace Iviz.ImageWrappers
{
    public readonly struct PngInfo
    {
        /// <summary>
        /// Gets the image width in pixels.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the image height in pixels.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the image color type.
        /// </summary>
        public PngColorType ColorType { get; }

        /// <summary>
        /// Gets the number of color channels in the image.
        /// </summary>
        public int Channels { get; }

        /// <summary>
        /// Gets the image bit depth.
        /// </summary>
        public int BitDepth { get; }

        /// <summary>
        /// Gets the number of bytes in a row.
        /// </summary>
        public int BytesPerRow { get; }

        /// <summary>
        /// Gets the size of the decompressed image.
        /// </summary>
        public int DecompressedSize => BytesPerRow * Height;

        public PngInfo(int width, int height, int bitDepth, int channels, int bytesPerRow, PngColorType colorType)
        {
            Width = width;
            Height = height;
            ColorType = colorType;
            Channels = channels;
            BitDepth = bitDepth;
            BytesPerRow = bytesPerRow;
        }
    }
}