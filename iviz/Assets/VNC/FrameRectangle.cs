#nullable enable

using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using JetBrains.Annotations;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.Services.Communication;
using Iviz.Tools;

namespace VNC
{
    internal readonly struct FrameRectangle : IDisposable
    {
        static uint[]? paletteBytes;
        static Span<uint> PaletteBuffer => (paletteBytes ??= new uint[256]);

        readonly byte[] buffer;
        readonly int width;
        readonly int height;

        Span<byte> BufferSpan() => buffer.AsSpan(0, width * height * 4);

        public FrameRectangle(Size size)
        {
            width = size.Width;
            height = size.Height;
            buffer = ArrayPool<byte>.Shared.Rent(width * height * 4);
        }

        public void Dispose()
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }

        public bool SetPixels(ReadOnlySpan<byte> pixelData, in PixelFormat pixelFormat)
        {
            try
            {
                if (pixelFormat.IsBinaryCompatibleTo(TurboJpegDecoder.RgbaCompatiblePixelFormat))
                {
                    SetPixels4(pixelData);
                    return true;
                }

                if (pixelFormat.IsBinaryCompatibleTo(SupportedFormats.RfbRgb888))
                {
                    SetPixels3(pixelData);
                    return true;
                }

                if (pixelFormat.IsBinaryCompatibleTo(SupportedFormats.RfbRgb565))
                {
                    SetPixels2(pixelData);
                    return true;
                }
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(FrameRectangle)}: Error in {nameof(SetPixels)}", e);
            }

            return false;
        }

        void SetPixels4(ReadOnlySpan<byte> src)
        {
            src.BlockCopyTo(BufferSpan());
        }

        void SetPixels3(ReadOnlySpan<byte> src)
        {
            SetPixels3N(BufferSpan().Cast<uint>(), src.Cast<Rgb>());
        }

        void SetPixels2(ReadOnlySpan<byte> src)
        {
            SetPixels2N(BufferSpan().Cast<uint>(), src.Cast<ushort>());
        }

        static void SetPixels3N(Span<uint> dst4, ReadOnlySpan<Rgb> src3)
        {
            int sizeToWrite = src3.Length;
            AssertSize(dst4, sizeToWrite);

            var srcI4 = MemoryMarshal.Cast<Rgb, uint>(src3);

            ref uint dstIPtr = ref dst4.GetReference();
            ref uint srcIPtr = ref srcI4.GetReference();

            while (sizeToWrite > 8)
            {
                // stolen from https://stackoverflow.com/questions/2973708/fast-24-bit-array-32-bit-array-conversion

                uint sa = srcIPtr;
                dstIPtr = sa;

                uint sb = srcIPtr.Plus(1);
                dstIPtr.Plus(1) = (sa >> 24) | (sb << 8);

                uint sc = srcIPtr.Plus(2);
                dstIPtr.Plus(2) = (sb >> 16) | (sc << 16);
                dstIPtr.Plus(3) = sc >> 8;

                // but twice!

                uint sd = srcIPtr.Plus(3);
                dstIPtr.Plus(4) = sd;

                uint se = srcIPtr.Plus(4);
                dstIPtr.Plus(5) = (sd >> 24) | (se << 8);

                uint sf = srcIPtr.Plus(5);
                dstIPtr.Plus(6) = (se >> 16) | (sf << 16);
                dstIPtr.Plus(7) = sf >> 8;


                sizeToWrite -= 8;
                srcIPtr = ref srcIPtr.Plus(6);
                dstIPtr = ref dstIPtr.Plus(8);
            }

            ref var srcPtr = ref Unsafe.As<uint, Rgb>(ref srcIPtr);
            ref var dstPtr = ref Unsafe.As<uint, Rgba>(ref dstIPtr);

            for (int x = sizeToWrite; x > 0; x--)
            {
                dstPtr.rgb = srcPtr; // dstPtr->rgb = *srcPtr;
                srcPtr = ref Unsafe.Add(ref srcPtr, 1); // srcPtr++;
                dstPtr = ref Unsafe.Add(ref dstPtr, 1); // dstPtr++;
            }
        }

        static void SetPixels2N(Span<uint> dst4, ReadOnlySpan<ushort> src2)
        {
            int sizeToWrite = src2.Length;
            AssertSize(dst4, sizeToWrite);

            ref int dstPtr = ref Unsafe.As<uint, int>(ref dst4.GetReference());
            ref ushort srcPtr = ref src2.GetReference();

            for (int x = sizeToWrite; x > 0; x--)
            {
                // stolen from https://stackoverflow.com/questions/2442576/how-does-one-convert-16-bit-rgb565-to-24-bit-rgb888

                int rgb565 = srcPtr;
                srcPtr = ref srcPtr.Plus(1); // srcPtr++;

                int rgba = InternalConvert565To888(rgb565);

                dstPtr = rgba;
                dstPtr = ref dstPtr.Plus(1); // dstPtr++;
            }
        }

        internal static int Convert565To888(int rgb565) => InternalConvert565To888(rgb565);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int InternalConvert565To888(int rgb565)
        {
            int r5 = (rgb565 & 0xf800) >> 11;
            int r8 = (r5 * 527 + 23) >> 6;
            int rgba = r8;

            int g6 = (rgb565 & 0x07e0) >> 5;
            int g8 = (g6 * 259 + 33) >> 6;
            rgba |= g8 << 8;

            int b5 = rgb565 & 0x001f;
            int b8 = (b5 * 527 + 23) >> 6;
            rgba |= b8 << 16;

            return rgba;
        }

        public bool SetPixelsPalette(ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette, in PixelFormat pixelFormat)
        {
            try
            {
                if (pixelFormat.IsBinaryCompatibleTo(SupportedFormats.RfbRgb888))
                {
                    SetPixelsPalette3(indices, palette);
                    return true;
                }

                if (pixelFormat.IsBinaryCompatibleTo(TurboJpegDecoder.RgbaCompatiblePixelFormat))
                {
                    SetPixelsPalette4(indices, palette);
                    return true;
                }

                if (pixelFormat.IsBinaryCompatibleTo(SupportedFormats.RfbRgb565))
                {
                    SetPixelsPalette2(indices, palette);
                    return true;
                }
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(FrameRectangle)}: Error in {nameof(SetPixelsPalette)}", e);
            }

            return false;
        }

        void SetPixelsPalette4(ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette)
        {
            var srcPalette4 = palette.Cast<uint>();

            if (srcPalette4.Length == 256)
            {
                SetPixelsPalette(indices, srcPalette4);
            }
            else
            {
                srcPalette4.CopyTo(PaletteBuffer);
                SetPixelsPalette(indices, PaletteBuffer);
            }
        }

        void SetPixelsPalette3(ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette)
        {
            var srcPalette3 = palette.Cast<Rgb>();
            SetPixels3N(PaletteBuffer, srcPalette3);
            SetPixelsPalette(indices, PaletteBuffer);
        }

        void SetPixelsPalette2(ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette)
        {
            var srcPalette2 = palette.Cast<ushort>();
            SetPixels2N(PaletteBuffer, srcPalette2);
            SetPixelsPalette(indices, PaletteBuffer);
        }

        void SetPixelsPalette(ReadOnlySpan<byte> indices, ReadOnlySpan<uint> palette)
        {
            SetPixelsPaletteN(BufferSpan(), indices, palette);
        }

        static void SetPixelsPaletteN(ReadOnlySpan<byte> dst, ReadOnlySpan<byte> indices, ReadOnlySpan<uint> palette)
        {
            var dst4 = dst.Cast<uint>();
            int sizeToWrite = dst4.Length;

            AssertSize(indices, sizeToWrite);

            ref uint palettePtr = ref palette.GetReference(); // palette is size 256
            ref byte indicesPtr = ref indices.GetReference();
            ref uint dstPtr = ref dst4.GetReference();

            while (sizeToWrite > 8)
            {
                dstPtr = palettePtr.Plus(indicesPtr); // *dstPtr = *(palettePtr + *indicesPtr);
                dstPtr.Plus(1) = palettePtr.Plus(indicesPtr.Plus(1));
                dstPtr.Plus(2) = palettePtr.Plus(indicesPtr.Plus(2));
                dstPtr.Plus(3) = palettePtr.Plus(indicesPtr.Plus(3));
                dstPtr.Plus(4) = palettePtr.Plus(indicesPtr.Plus(4));
                dstPtr.Plus(5) = palettePtr.Plus(indicesPtr.Plus(5));
                dstPtr.Plus(6) = palettePtr.Plus(indicesPtr.Plus(6));
                dstPtr.Plus(7) = palettePtr.Plus(indicesPtr.Plus(7));

                sizeToWrite -= 8;
                dstPtr = ref dstPtr.Plus(8);
                indicesPtr = ref indicesPtr.Plus(8);
            }

            for (int x = sizeToWrite; x > 0; x--)
            {
                dstPtr = palettePtr.Plus(indicesPtr); // *dstPtr = *(palettePtr + *indicesPtr);
                dstPtr = ref dstPtr.Plus(1);
                indicesPtr = ref indicesPtr.Plus(1);
            }
        }

        public void CopyTo(Span<byte> textureSpan, int textureWidth, in Position position)
        {
            int srcPitch = width * 4;
            int dstPitch = textureWidth * 4;

            var src = BufferSpan();
            int dstOffset = (position.Y * textureWidth + position.X) * 4;
            var dst = textureSpan[dstOffset..];

            if (width == textureWidth)
            {
                src.BlockCopyTo(dst);
            }
            else
            {
                for (int y = height - 1; y > 0; y--) // skip the last one
                {
                    src[..srcPitch].BlockCopyTo(dst);
                    dst = dst[dstPitch..];
                    src = src[srcPitch..];
                }

                src[..srcPitch].BlockCopyTo(dst); // and process it here, otherwise spans will throw
            }
        }

        public static void FillRectangle(Span<byte> textureSpan, int textureWidth, in Rectangle rectangle, Rgba rgba)
        {
            uint color = Unsafe.As<Rgba, uint>(ref rgba);
            if (rgba.rgb.IsGray)
            {
                // fill with byte uses memset
                FillRectangle1(textureSpan, textureWidth, rectangle, (byte)(color & 0xff));
            }
            else
            {
                FillRectangle4(textureSpan, textureWidth, rectangle, color);
            }
        }

        static void FillRectangle1(Span<byte> textureSpan, int textureWidth, in Rectangle rectangle, byte c)
        {
            int dstPitch = textureWidth * 4;
            int rowLength = rectangle.Size.Width * 4;
            int dstOffset = rectangle.Position.Y * dstPitch + rectangle.Position.X * 4;

            var dst = textureSpan.Cast<byte>()[dstOffset..];
            if (rowLength == dstPitch)
            {
                int sizeToFill = rectangle.Size.Height * rowLength;
                dst[..sizeToFill].Fill(c);
            }
            else
            {
                int offset = 0;
                for (int y = rectangle.Size.Height; y > 0; y--)
                {
                    dst.Slice(offset, rowLength).Fill(c);
                    offset += dstPitch;
                }
            }
        }

        static void FillRectangle4(Span<byte> textureSpan, int textureWidth, in Rectangle rectangle, uint color)
        {
            int dstPitch = textureWidth;
            int rowLength = rectangle.Size.Width;

            int dstOffset = rectangle.Position.Y * dstPitch + rectangle.Position.X;

            var dst = textureSpan.Cast<uint>()[dstOffset..];
            if (rowLength == dstPitch)
            {
                int sizeToFill = rectangle.Size.Height * rowLength;
                dst[..sizeToFill].Fill(color);
            }
            else
            {
                int offset = 0;
                for (int y = rectangle.Size.Height; y > 0; y--)
                {
                    dst.Slice(offset, rowLength).Fill(color);
                    offset += dstPitch;
                }
            }
        }

        public static void CopyRectangle(Span<byte> textureSpan, int textureWidth, in Rectangle dstRectangle,
            in Rectangle srcRectangle)
        {
            int pitch = textureWidth * 4;
            int rectHeight = srcRectangle.Size.Height;
            int rectWidth = srcRectangle.Size.Width;
            int rowLength = rectWidth * 4;
            var span = textureSpan;
            var srcSpan = span[(srcRectangle.Position.Y * pitch + srcRectangle.Position.X * 4)..];
            var dstSpan = span[(dstRectangle.Position.Y * pitch + dstRectangle.Position.X * 4)..];

            if (srcRectangle.Position.Y > dstRectangle.Position.Y)
            {
                int offset = 0;
                for (int y = rectHeight; y > 0; y--)
                {
                    srcSpan.Slice(offset, rowLength).BlockCopyTo(dstSpan.Slice(offset, rowLength));
                    offset += pitch;
                }
            }
            else if (srcRectangle.Position.Y < dstRectangle.Position.Y)
            {
                int offset = pitch * (rectHeight - 1);
                for (int y = rectHeight; y > 0; y--)
                {
                    srcSpan.Slice(offset, rowLength).BlockCopyTo(dstSpan.Slice(offset, rowLength));
                    offset -= pitch;
                }
            }
            else
            {
                // use CopyTo (memmove checks) instead of BlockCopyTo (memcpy assumptions)
                int offset = 0;
                for (int y = rectHeight; y > 0; y--)
                {
                    srcSpan.Slice(offset, rowLength).CopyTo(dstSpan.Slice(offset, rowLength));
                    offset += pitch;
                }
            }
        }

        [AssertionMethod]
        static void AssertSize(in Span<uint> span, int size)
        {
            if (span.Length < size) ThrowIndexOutOfRange();
        }

        [AssertionMethod]
        static void AssertSize(in ReadOnlySpan<byte> span, int size)
        {
            if (span.Length < size) ThrowIndexOutOfRange();
        }

        [DoesNotReturn]
        static void ThrowIndexOutOfRange() =>
            throw new IndexOutOfRangeException("Span array is too short for the given operation");
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rgba
    {
        public Rgb rgb;
        readonly byte a;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct Rgb
    {
        readonly byte r, g, b;
        public bool IsGray => r == g && g == b;
    }
}