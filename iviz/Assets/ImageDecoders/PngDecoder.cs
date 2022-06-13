#nullable enable

using System;
using System.Runtime.InteropServices;
using AOT;
using Iviz.Core;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.ImageDecoders
{
    /// <summary>
    /// Png Decoder.
    /// This code was adapted from https://github.com/qmfrederik/libpng-sharp
    /// </summary>
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

        readonly byte[] readBuffer;
        int position;
        GCHandle self;

        bool disposed;

        public PngInfo PngInfo { get; }

        public unsafe PngDecoder(byte[] readBuffer)
        {
            this.readBuffer = readBuffer;

            Span<byte> header = stackalloc byte[8];
            readBuffer.AsSpan(0, 8).CopyTo(header);
            position = 8;

            fixed (byte* headerPtr = header)
            {
                if (PngNative.png_sig_cmp((IntPtr)headerPtr, 0, 8) != 0)
                {
                    throw new DecoderException("Data is not a PNG file!");
                }
            }

            version = PngNative.png_get_libpng_ver(IntPtr.Zero);

            errorCallback = OnError;
            warningCallback = OnWarning;
            readCallback = Read;

            self = GCHandle.Alloc(this);

            pngPtr = PngNative.png_create_read_struct(version, IntPtr.Zero, errorCallback, warningCallback);

            if (pngPtr == IntPtr.Zero)
            {
                ThrowHelper.ThrowInvalidOperation("Failed to initialize " + nameof(pngPtr));
            }

            infoPtr = PngNative.png_create_info_struct(pngPtr);
            if (infoPtr == IntPtr.Zero)
            {
                ThrowHelper.ThrowInvalidOperation("Failed to initialize " + nameof(infoPtr));
            }

            try
            {
                var ioPtr = (IntPtr)self;
                PngNative.png_set_read_fn(pngPtr, ioPtr, readCallback);
                PngNative.png_set_sig_bytes(pngPtr, 8);

                PngNative.png_read_info(pngPtr, infoPtr);

                PngNative.png_set_palette_to_rgb(pngPtr); // transforms palette images to RGB.
                PngNative.png_set_expand_gray_1_2_4_to_8(
                    pngPtr); // transforms grayscale images of less than 8 to 8 bits
                PngNative.png_set_swap(pngPtr); // changes the pixel byte order from big-endian to little-endian.
                PngNative.png_set_strip_alpha(pngPtr); // removes the alpha channel

                PngNative.png_read_update_info(pngPtr, infoPtr);

                PngInfo = new PngInfo(
                    (int)PngNative.png_get_image_width(pngPtr, infoPtr),
                    (int)PngNative.png_get_image_height(pngPtr, infoPtr),
                    PngNative.png_get_bit_depth(pngPtr, infoPtr),
                    PngNative.png_get_channels(pngPtr, infoPtr),
                    (int)PngNative.png_get_rowbytes(pngPtr, infoPtr),
                    PngNative.png_get_color_type(pngPtr, infoPtr));
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        /// <summary>
        /// Decodes the image into a decompressed buffer.
        /// </summary>
        public unsafe void Decode(Span<byte> destBuffer)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(JpegDecoder));
            }
            
            int requiredLength = PngInfo.DecompressedSize;
            if (requiredLength == 0)
            {
                return;
            }

            if (destBuffer.Length == 0)
            {
                ThrowHelper.ThrowArgument("Destination buffer is empty", nameof(destBuffer));
            }

            int height = PngInfo.Height;
            var ptrRent = Rent.Empty<IntPtr>();
            Span<IntPtr> rowPointers = height < 256
                ? stackalloc IntPtr[height]
                : (ptrRent = new Rent<IntPtr>(height));

            using (ptrRent)
            {
                fixed (byte* destBufferPtr = destBuffer)
                fixed (IntPtr* rowPointersPtr = rowPointers)
                {
                    IntPtr currentRowPtr = (IntPtr)destBufferPtr;

                    for (int i = 0; i < height; i++)
                    {
                        rowPointers[i] = currentRowPtr;
                        currentRowPtr += PngInfo.BytesPerRow;
                    }

                    PngNative.png_read_image(pngPtr, (IntPtr)rowPointersPtr);
                }
            }

            // Don't actually read the end_info data.
            PngNative.png_read_end(pngPtr, IntPtr.Zero);
        }

        [MonoPInvokeCallback(typeof(PngNative.png_rw))]
        static unsafe void Read(IntPtr pngPtr, IntPtr outBytes, uint byteCountToRead)
        {
            var ioPtr = PngNative.png_get_io_ptr(pngPtr);
            var handle = (GCHandle)ioPtr;
            var self = (PngDecoder)handle.Target;
            
            int countToRead = (int)byteCountToRead;
            var targetBuffer = new Span<byte>(outBytes.ToPointer(), countToRead);
            if (self.position + countToRead > self.readBuffer.Length)
            {
                RosLogger.Error(nameof(PngDecoder) + ": Buffer underflow!");
                targetBuffer.Fill(0);
                return;
            }

            self.readBuffer.AsSpan(self.position, countToRead).CopyTo(targetBuffer);
            self.position += countToRead;
        }

        [MonoPInvokeCallback(typeof(PngNative.png_error))]
        static void OnError(IntPtr _, IntPtr strPtr)
        {
            string error = Marshal.PtrToStringAnsi(strPtr) ?? "";

            // this is nasty! we are throwing across p/invoke and back into C#, probably causing leaks. 
            // but libpng has no other error handling mechanism, i.e, if this function returns without throwing,
            // it will crash the whole app!
            throw new DecoderException(error);
        }

        [MonoPInvokeCallback(typeof(PngNative.png_error))]
        static void OnWarning(IntPtr _, IntPtr strPtr)
        {
            string error = Marshal.PtrToStringAnsi(strPtr) ?? "";
            RosLogger.Debug($"{nameof(PngDecoder)}: {error}");
        }

        void ReleaseUnmanagedResources()
        {
            var tmpPngPtr = pngPtr;
            var tmpInfoPtr = infoPtr;
            var endInfoPtr = IntPtr.Zero;

            PngNative.png_destroy_read_struct(ref tmpPngPtr, ref tmpInfoPtr, ref endInfoPtr);
            PngNative.png_free(IntPtr.Zero, version);
            self.Free();
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
}