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
    public readonly struct FrameRectangle : IDisposable
    {
        static readonly uint[] PaletteBuffer = new uint[256];

        readonly byte[] buffer;
        readonly int fullSize;

        Span<byte> BufferSpan() => buffer.AsSpan(0, fullSize);

        public int Width { get; }
        public int Height { get; }
        public ReadOnlySpan<byte> Address => BufferSpan();

        public FrameRectangle(Size size)
        {
            Width = size.Width;
            Height = size.Height;
            fullSize = Width * Height * 4;
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