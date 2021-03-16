using System;

namespace BigGustave
{
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    /// <summary>
    /// Used to construct PNG images. Call <see cref="Create"/> to make a new builder.
    /// </summary>
    /// <remarks>
    /// The created image is not compliant with all image viewers due to ZLib compatibility issues.
    /// </remarks>
    public class PngBuilder
    {
        static readonly byte[] FakeDeflateHeader = {120, 156};

        readonly byte[] rawData;
        readonly int width;
        readonly int height;
        readonly int bytesPerPixel;
        readonly bool hasAlphaChannel;
        readonly bool flipRb;

        public PngBuilder(byte[] rawData, bool hasAlphaChannel, int width, int height, int bytesPerPixel,
            bool flipRb = false)
        {
            this.rawData = rawData;
            this.hasAlphaChannel = hasAlphaChannel;
            this.width = width;
            this.height = height;
            this.bytesPerPixel = bytesPerPixel;
            this.flipRb = flipRb;
        }

        /// <summary>
        /// Set the pixel value for the given column (x) and row (y).
        /// </summary>
        public PngBuilder SetPixel(Pixel pixel, int x, int y)
        {
            var start = (y * ((width * bytesPerPixel) + 1)) + 1 + (x * bytesPerPixel);

            rawData[start++] = pixel.R;
            rawData[start++] = pixel.G;
            rawData[start++] = pixel.B;

            if (hasAlphaChannel)
            {
                rawData[start] = pixel.A;
            }

            return this;
        }

        /// <summary>
        /// Get the bytes of the PNG file for this builder.
        /// </summary>
        public byte[] Save()
        {
            using (var memoryStream = new MemoryStream())
            {
                Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Write the PNG file bytes to the provided stream.
        /// </summary>
        public void Save(Stream outputStream)
        {
            outputStream.Write(HeaderValidationResult.ExpectedHeader, 0, HeaderValidationResult.ExpectedHeader.Length);

            var stream = new PngStreamWriteHelper(outputStream);

            stream.WriteChunkLength(13);
            stream.WriteChunkHeader(ImageHeader.HeaderBytes);

            StreamHelper.WriteBigEndianInt32(stream, width);
            StreamHelper.WriteBigEndianInt32(stream, height);
            stream.WriteByte(8);

            var colorType = ColorType.ColorUsed;
            if (hasAlphaChannel)
            {
                colorType |= ColorType.AlphaChannelUsed;
            }

            stream.WriteByte((byte) colorType);
            stream.WriteByte((byte) CompressionMethod.DeflateWithSlidingWindow);
            stream.WriteByte((byte) FilterMethod.AdaptiveFiltering);
            stream.WriteByte((byte) InterlaceMethod.None);

            stream.WriteCrc();

            byte[] imageData = bytesPerPixel switch
            {
                4 when flipRb => CompressFlipRb4(rawData, height, width * 4),
                4 => Compress(rawData, height, width * 4),
                3 when flipRb => CompressFlipRb3(rawData, height, width * 3),
                3 => Compress(rawData, height, width * 3),
                _ => throw new ArgumentOutOfRangeException()
            };
            stream.WriteChunkLength(imageData.Length);
            stream.WriteChunkHeader(Encoding.ASCII.GetBytes("IDAT"));
            stream.Write(imageData, 0, imageData.Length);
            stream.WriteCrc();

            stream.WriteChunkLength(0);
            stream.WriteChunkHeader(Encoding.ASCII.GetBytes("IEND"));
            stream.WriteCrc();
        }

        static byte[] Compress(byte[] data)
        {
            using (var compressStream = new MemoryStream())
            using (var compressor = new DeflateStream(compressStream, CompressionLevel.Fastest))
            {
                compressStream.Write(FakeDeflateHeader, 0, FakeDeflateHeader.Length);
                compressor.Write(data, 0, data.Length);
                compressor.Close();
                return compressStream.ToArray();
            }
        }

        static byte[] Compress(byte[] data, int height, int stride)
        {
            using var compressStream = new MemoryStream();
            using var compressor = new DeflateStream(compressStream, CompressionLevel.Fastest);

            compressStream.Write(FakeDeflateHeader, 0, FakeDeflateHeader.Length);
            for (int v = 0, offset = 0; v < height; v++, offset += stride)
            {
                compressor.WriteByte(0);
                compressor.Write(data, offset, stride);
            }

            compressor.Close();
            return compressStream.ToArray();
        }

        static unsafe byte[] CompressFlipRb4(byte[] data, int height, int stride)
        {
            using var compressStream = new MemoryStream();
            using var compressor = new DeflateStream(compressStream, CompressionLevel.Fastest);

            fixed (byte* srcPtr = data)
            {
                byte* srcPtrOff = srcPtr;
                compressStream.Write(FakeDeflateHeader, 0, FakeDeflateHeader.Length);
                for (int v = 0; v < height; v++)
                {
                    compressor.WriteByte(0);
                    for (int u = 0; u < stride; u += 4, srcPtrOff += 4)
                    {
                        byte tmp = srcPtrOff[0];
                        srcPtrOff[0] = srcPtrOff[2];
                        srcPtrOff[2] = tmp;
                    }

                    compressor.Write(data, v * stride, stride);
                }
            }

            compressor.Close();
            return compressStream.ToArray();
        }
        
        static unsafe byte[] CompressFlipRb3(byte[] data, int height, int stride)
        {
            using var compressStream = new MemoryStream();
            using var compressor = new DeflateStream(compressStream, CompressionLevel.Fastest);

            fixed (byte* srcPtr = data)
            {
                byte* srcPtrOff = srcPtr;
                compressStream.Write(FakeDeflateHeader, 0, FakeDeflateHeader.Length);
                for (int v = 0; v < height; v++)
                {
                    compressor.WriteByte(0);
                    for (int u = 0; u < stride; u += 3, srcPtrOff += 3)
                    {
                        byte tmp = srcPtrOff[0];
                        srcPtrOff[0] = srcPtrOff[2];
                        srcPtrOff[2] = tmp;
                    }

                    compressor.Write(data, v * stride, stride);
                }
            }

            compressor.Close();
            return compressStream.ToArray();
        }
    }
}