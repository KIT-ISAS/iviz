#nullable enable

using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.Services.Communication;
using UnityEngine;

namespace VNC
{
    internal readonly struct FrameRectangle : IDisposable
    {
        static readonly uint[] PaletteBuffer = new uint[256];

        readonly byte[] buffer;
        readonly int fullSize;
        readonly int width;
        readonly int height;

        Span<byte> BufferSpan() => buffer.AsSpan(0, fullSize);

        public FrameRectangle(Size size)
        {
            width = size.Width;
            height = size.Height;
            fullSize = width * height * 4;
            buffer = ArrayPool<byte>.Shared.Rent(fullSize);
        }

        public void Dispose()
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }

        public void SetPixels(ReadOnlySpan<byte> pixelData, in PixelFormat pixelFormat)
        {
            try
            {
                if (pixelFormat.IsBinaryCompatibleTo(TurboJpegDecoder.RgbaCompatiblePixelFormat))
                {
                    SetPixels4(pixelData);
                }
                else if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                {
                    SetPixels3(pixelData);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        void SetPixels4(ReadOnlySpan<byte> src)
        {
            src.BlockCopyTo(BufferSpan());
        }

        void SetPixels3(ReadOnlySpan<byte> src)
        {
            SetPixels3N(BufferSpan().Cast<uint>(), src.Cast<Rgb>());
        }

        static void SetPixels3N(Span<uint> dst4, ReadOnlySpan<Rgb> src3)
        {
            int sizeToWrite = src3.Length;

            var srcI4 = MemoryMarshal.Cast<Rgb, uint>(src3);
            dst4 = dst4[..sizeToWrite]; // assert dst4 size is same as src3

            ref uint dstIPtr = ref MemoryMarshal.GetReference(dst4[..sizeToWrite]);
            ref uint srcIPtr = ref MemoryMarshal.GetReference(srcI4);

            while (sizeToWrite > 8)
            {
                // stolen from https://stackoverflow.com/questions/2973708/fast-24-bit-array-32-bit-array-conversion

                uint sa = srcIPtr;
                dstIPtr = sa;

                uint sb = Unsafe.Add(ref srcIPtr, 1);
                Unsafe.Add(ref dstIPtr, 1) = (sa >> 24) | (sb << 8);

                uint sc = Unsafe.Add(ref srcIPtr, 2);
                Unsafe.Add(ref dstIPtr, 2) = (sb >> 16) | (sc << 16);
                Unsafe.Add(ref dstIPtr, 3) = sc >> 8;

                // but twice!

                uint sd = Unsafe.Add(ref srcIPtr, 3);
                Unsafe.Add(ref dstIPtr, 4) = sd;

                uint se = Unsafe.Add(ref srcIPtr, 4);
                Unsafe.Add(ref dstIPtr, 5) = (sd >> 24) | (se << 8);

                uint sf = Unsafe.Add(ref srcIPtr, 5);
                Unsafe.Add(ref dstIPtr, 6) = (se >> 16) | (sf << 16);
                Unsafe.Add(ref dstIPtr, 7) = sf >> 8;


                sizeToWrite -= 8;
                srcIPtr = ref Unsafe.Add(ref srcIPtr, 6);
                dstIPtr = ref Unsafe.Add(ref dstIPtr, 8);
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

        public void SetPixelsPalette(ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette, in PixelFormat pixelFormat)
        {
            try
            {
                if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                {
                    SetPixelsPalette3(indices, palette);
                }
                else if (pixelFormat.IsBinaryCompatibleTo(TurboJpegDecoder.RgbaCompatiblePixelFormat))
                {
                    SetPixelsPalette4(indices, palette);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        void SetPixelsPalette3(ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette)
        {
            var srcPalette3 = palette.Cast<Rgb>();
            SetPixels3N(PaletteBuffer, srcPalette3);
            SetPixelsPalette(indices, PaletteBuffer);
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

        void SetPixelsPalette(ReadOnlySpan<byte> indices, ReadOnlySpan<uint> palette)
        {
            SetPixelsPaletteN(BufferSpan(), indices, palette);
        }

        static void SetPixelsPaletteN(ReadOnlySpan<byte> dst, ReadOnlySpan<byte> indices, ReadOnlySpan<uint> palette)
        {
            var dst4 = dst.Cast<uint>();
            int sizeToWrite = dst4.Length;

            indices = indices[..sizeToWrite]; // assert indices size is same as dst4 

            ref uint palettePtr = ref MemoryMarshal.GetReference(palette); // palette is size 256
            ref byte indicesPtr = ref MemoryMarshal.GetReference(indices[..sizeToWrite]);
            ref uint dstPtr = ref MemoryMarshal.GetReference(dst4);

            while (sizeToWrite > 8)
            {
                dstPtr = Unsafe.Add(ref palettePtr, indicesPtr); // *dstPtr = *(palettePtr + *indicesPtr);
                Unsafe.Add(ref dstPtr, 1) = Unsafe.Add(ref palettePtr, Unsafe.Add(ref indicesPtr, 1));
                Unsafe.Add(ref dstPtr, 2) = Unsafe.Add(ref palettePtr, Unsafe.Add(ref indicesPtr, 2));
                Unsafe.Add(ref dstPtr, 3) = Unsafe.Add(ref palettePtr, Unsafe.Add(ref indicesPtr, 3));
                Unsafe.Add(ref dstPtr, 4) = Unsafe.Add(ref palettePtr, Unsafe.Add(ref indicesPtr, 4));
                Unsafe.Add(ref dstPtr, 5) = Unsafe.Add(ref palettePtr, Unsafe.Add(ref indicesPtr, 5));
                Unsafe.Add(ref dstPtr, 6) = Unsafe.Add(ref palettePtr, Unsafe.Add(ref indicesPtr, 6));
                Unsafe.Add(ref dstPtr, 7) = Unsafe.Add(ref palettePtr, Unsafe.Add(ref indicesPtr, 7));

                sizeToWrite -= 8;
                dstPtr = ref Unsafe.Add(ref dstPtr, 8);
                indicesPtr = ref Unsafe.Add(ref indicesPtr, 8);
            }

            for (int x = sizeToWrite; x > 0; x--)
            {
                dstPtr = Unsafe.Add(ref palettePtr, indicesPtr); // *dstPtr = *(palettePtr + *indicesPtr);
                dstPtr = ref Unsafe.Add(ref dstPtr, 1);
                indicesPtr = ref Unsafe.Add(ref indicesPtr, 1);
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
                for (int y = height - 1; y > 0; y--)
                {
                    src[..srcPitch].BlockCopyTo(dst);
                    dst = dst[dstPitch..];
                    src = src[srcPitch..];
                }

                src[..srcPitch].BlockCopyTo(dst);
            }
        }

        public static void FillRectangle(Span<byte> textureSpan, int textureWidth, in Rectangle rectangle, Rgba rgba)
        {
            uint color = Unsafe.As<Rgba, uint>(ref rgba);
            if (rgba.rgb.IsGray)
            {
                // use memset if possible
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