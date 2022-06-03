#nullable enable

using System;
using System.Runtime.InteropServices;

namespace Iviz.ImageWrappers
{
    public sealed class PngDecoder : IDisposable
    {
        readonly IntPtr version;
        readonly IntPtr pngPtr;
        readonly IntPtr infoPtr;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        readonly PngNative.png_rw readCallback;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        readonly PngNative.png_error errorCallback;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        readonly PngNative.png_error warningCallback;

        readonly PngReadCallback onReadCallback;

        uint currentOffset;
        bool disposed;

        /// <summary>
        /// Gets the version of libpng.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// The event which is raised when an error occurs.
        /// </summary>
        public event Action<string>? Error;

        /// <summary>
        /// The event which is raised when a warning occurs.
        /// </summary>
        public event Action<string>? Warning;

        public delegate void PngReadCallback(uint currentOffset, Span<byte> buffer);

        /// <summary>
        /// Initializes a new instance of the <see cref="PngDecoder"/> class.
        /// </summary>
        public unsafe PngDecoder(PngReadCallback onReadCallback)
        {
            Span<byte> header = stackalloc byte[8];
            onReadCallback(0, header);

            fixed (byte* headerPtr = header)
            {
                if (PngNative.png_sig_cmp((IntPtr)headerPtr, 0, 8) != 0)
                {
                    throw new ArgumentException("Stream is not a PNG file!");
                }
            }


            this.onReadCallback = onReadCallback;

            version = PngNative.png_get_libpng_ver(IntPtr.Zero);
            Version = Marshal.PtrToStringAnsi(version) ?? "";

            errorCallback = OnError;
            warningCallback = OnWarning;
            readCallback = Read;

            pngPtr = PngNative.png_create_read_struct(version, (IntPtr)1, errorCallback, warningCallback);
            ThrowOnZero(pngPtr);

            infoPtr = PngNative.png_create_info_struct(pngPtr);
            ThrowOnZero(infoPtr);

            PngNative.png_set_read_fn(pngPtr, IntPtr.Zero, readCallback);
            PngNative.png_set_sig_bytes(pngPtr, 8);
        }

        public PngInfo ReadInfo()
        {
            PngNative.png_read_info(pngPtr, infoPtr);

            // Transforms paletted images to RGB.
            PngNative.png_set_palette_to_rgb(pngPtr);
            // Transforms grayscale images of less than 8 to 8 bits
            PngNative.png_set_gray_1_2_4_to_8(pngPtr);
            // Changes the pixel byte order for 16-bit pixels from bit-endian to little-endian.
            PngNative.png_set_swap(pngPtr);

            PngNative.png_read_update_info(pngPtr, infoPtr);

            return new PngInfo(
                (int)PngNative.png_get_image_width(pngPtr, infoPtr),
                (int)PngNative.png_get_image_height(pngPtr, infoPtr),
                PngNative.png_get_bit_depth(pngPtr, infoPtr),
                PngNative.png_get_channels(pngPtr, infoPtr),
                (int)PngNative.png_get_rowbytes(pngPtr, infoPtr),
                PngNative.png_get_color_type(pngPtr, infoPtr));
        }

        /// <summary>
        /// Decodes the image into a decompressed buffer.
        /// </summary>
        public unsafe void Decode(in PngInfo pngInfo, Span<byte> destBuffer)
        {
            Span<IntPtr> rowPointers = stackalloc IntPtr[pngInfo.Height];

            fixed (byte* ptr = destBuffer)
            fixed (IntPtr* rowPointersPtr = rowPointers)
            {
                var currentRow = (IntPtr)ptr;

                for (int i = 0; i < pngInfo.Height; i++)
                {
                    rowPointers[i] = currentRow;
                    currentRow += pngInfo.BytesPerRow;
                }

                PngNative.png_read_image(pngPtr, (IntPtr)rowPointersPtr);
            }

            // Don't actually read the end_info data.
            PngNative.png_read_end(pngPtr, IntPtr.Zero);
        }

        unsafe void Read(IntPtr _, IntPtr outBytes, uint byteCountToRead)
        {
            var target = new Span<byte>(outBytes.ToPointer(), (int)byteCountToRead);
            onReadCallback.Invoke(currentOffset, target);
            currentOffset += byteCountToRead;
        }

        void OnError(IntPtr _, IntPtr strPtr)
        {
            string error = Marshal.PtrToStringAnsi(strPtr) ?? "";
            Error?.Invoke(error);
        }

        void OnWarning(IntPtr _, IntPtr strPtr)
        {
            string error = Marshal.PtrToStringAnsi(strPtr) ?? "";
            Warning?.Invoke(error);
        }

        static void ThrowOnZero(IntPtr value)
        {
            if (value == IntPtr.Zero)
            {
                throw new Exception();
            }
        }

        void ReleaseUnmanagedResources()
        {
            var tmpPngPtr = pngPtr;
            var tmpInfoPtr = infoPtr;
            var endInfoPtr = IntPtr.Zero;

            PngNative.png_destroy_read_struct(ref tmpPngPtr, ref tmpInfoPtr, ref endInfoPtr);
            PngNative.png_free(IntPtr.Zero, version);
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~PngDecoder()
        {
            ReleaseUnmanagedResources();
        }
    }

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