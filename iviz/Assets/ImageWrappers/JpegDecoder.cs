#nullable enable

using System;
using System.Runtime.InteropServices;
using Iviz.Core;
using MarcusW.VncClient.Protocol.Implementation.Native;

namespace Iviz.ImageWrappers
{
    /// <summary>
    /// Wrapper for the turbojpeg decoder functions.
    /// This code was adapted from the <see cref="TurboJpeg"/> class in the <see cref="MarcusW.VncClient"/> namespace.
    /// Original from https://github.com/MarcusWichelmann/MarcusW.VncClient
    /// </summary>
    public sealed class JpegDecoder : IDisposable
    {
        readonly IntPtr jpegPtr;
        readonly byte[] readBuffer;
        bool disposed;

        public JpegInfo JpegInfo { get; }

        public unsafe JpegDecoder(byte[] readBuffer)
        {
            this.readBuffer = readBuffer;

            jpegPtr = TurboJpegNative.InitDecompressorInstance();
            if (jpegPtr == IntPtr.Zero)
            {
                ThrowHelper.ThrowInvalidOperation(
                    $"Initializing TurboJPEG decompressor instance failed: {GetLastError()}");
            }

            fixed (byte* jpegBufferPtr = readBuffer)
            {
                if (TurboJpegNative.DecompressHeader(jpegPtr, (IntPtr)jpegBufferPtr, (ulong)readBuffer.Length,
                        out int width, out int height, out int subsampling, out int colorspace) == -1)
                {
                    throw new DecoderException(GetLastError());
                }

                var jpegColorspace = (TurboJpegColorspace)colorspace;
                var jpegFormat = jpegColorspace switch
                {
                    TurboJpegColorspace.TJCS_GRAY => TurboJpegPixelFormat.Gray,
                    _ when Settings.SupportsRGB24 => TurboJpegPixelFormat.RGB,
                    _ => TurboJpegPixelFormat.RGBA
                };
                
                JpegInfo = new JpegInfo(width, height, subsampling, jpegColorspace, jpegFormat);
            }
        }

        public unsafe void Decode(Span<byte> destBuffer)
        {
            int bpp = JpegInfo.Format switch
            {
                TurboJpegPixelFormat.Gray => 1,
                TurboJpegPixelFormat.RGB => 3,
                TurboJpegPixelFormat.RGBA => 4,
                _ => throw new IndexOutOfRangeException() // shouldn't happen
            };
            int stride = JpegInfo.Width * bpp;
            int requiredLength = JpegInfo.Height * stride;

            if (requiredLength == 0)
            {
                return;
            }

            if (destBuffer.Length < requiredLength)
            {
                ThrowHelper.ThrowArgumentOutOfRange(
                    $"Cannot decode JPEG image ({JpegInfo.Width.ToString()}x{JpegInfo.Height.ToString()}) because its size of " +
                    $"{requiredLength.ToString()} bytes would exceed the pixels buffer size of {destBuffer.Length.ToString()} " +
                    $"when decompressing. ");
            }

            fixed (byte* jpegBufferPtr = readBuffer)
            fixed (byte* pixelsBufferPtr = destBuffer)
            {
                if (TurboJpegNative.Decompress(jpegPtr, (IntPtr)jpegBufferPtr, (ulong)readBuffer.Length,
                        (IntPtr)pixelsBufferPtr, JpegInfo.Width, stride, JpegInfo.Height, (int)JpegInfo.Format,
                        (int)(TurboJpegFlags.FastUpsample | TurboJpegFlags.FastDct | TurboJpegFlags.NoRealloc)) == -1)
                    throw new DecoderException(GetLastError());
            }
        }

        void ReleaseUnmanagedResources()
        {
            if (jpegPtr != IntPtr.Zero)
            {
                TurboJpegNative.DestroyInstance(jpegPtr);
            }
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~JpegDecoder()
        {
            ReleaseUnmanagedResources();
        }

        static string GetLastError()
        {
            return Marshal.PtrToStringAnsi(TurboJpegNative.GetLastError()) ?? "";
        }
    }
}