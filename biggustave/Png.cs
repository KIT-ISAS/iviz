namespace BigGustave
{
    using System;
    using System.IO;

    /// <summary>
    /// A PNG image. Call <see cref="Open(byte[],BigGustave.IChunkVisitor)"/> to open from file or bytes.
    /// </summary>
    public class Png
    {
        private readonly RawPngData data;

        /// <summary>
        /// The header data from the PNG image.
        /// </summary>
        public ImageHeader Header { get; }

        /// <summary>
        /// The width of the image in pixels.
        /// </summary>
        public int Width => Header.Width;

        /// <summary>
        /// The height of the image in pixels.
        /// </summary>
        public int Height => Header.Height;

        public byte[] Data => data.Data;

        public int RowOffset => data.RowOffset;

        public int RowStep => data.RowStep;

        public int RowSize => data.RowSize;

        /// <summary>
        /// Whether the image has an alpha (transparency) layer.
        /// </summary>
        public bool HasAlphaChannel => (Header.ColorType & ColorType.AlphaChannelUsed) != 0;

        internal Png(ImageHeader header, RawPngData data)
        {
            Header = header;
            this.data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Get the pixel at the given column and row (x, y).
        /// </summary>
        /// <remarks>
        /// Pixel values are generated on demand from the underlying data to prevent holding many items in memory at once, so consumers
        /// should cache values if they're going to be looped over many time.
        /// </remarks>
        /// <param name="x">The x coordinate (column).</param>
        /// <param name="y">The y coordinate (row).</param>
        /// <returns>The pixel at the coordinate.</returns>
        public Pixel GetPixel(int x, int y) => data.GetPixel(x, y);

        /// <summary>
        /// Read the PNG image from the stream.
        /// </summary>
        /// <param name="stream">The stream containing PNG data to be read.</param>
        /// <param name="chunkVisitor">Optional: A visitor which is called whenever a chunk is read by the library.</param>
        /// <returns>The <see cref="Png"/> data from the stream.</returns>
        public static Png Open(Stream stream, IChunkVisitor chunkVisitor = null)
            => PngOpener.Open(stream, chunkVisitor);

        /// <summary>
        /// Read the PNG image from the bytes.
        /// </summary>
        /// <param name="bytes">The bytes of the PNG data to be read.</param>
        /// <param name="chunkVisitor">Optional: A visitor which is called whenever a chunk is read by the library.</param>
        /// <returns>The <see cref="Png"/> data from the bytes.</returns>
        public static Png Open(byte[] bytes, IChunkVisitor chunkVisitor = null)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return PngOpener.Open(memoryStream, chunkVisitor);
            }
        }
    }
}
