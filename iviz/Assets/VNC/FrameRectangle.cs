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
        static readonly Rgba[] PaletteBuffer = new Rgba[256];

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
            SetPixels3N(BufferSpan().Cast<Rgba>(), src.Cast<Rgb>());
        }

        static void SetPixels3N(Span<Rgba> dst4, ReadOnlySpan<Rgb> src3)
        {
            int sizeToWrite = src3.Length;
            
            dst4 = dst4[..sizeToWrite]; // ensure dst is big enough
            
            ref var dstPtr = ref MemoryMarshal.GetReference(dst4);
            ref var srcPtr = ref MemoryMarshal.GetReference(src3);

            while (sizeToWrite > 8)
            {
                dstPtr.rgb = srcPtr;
                Unsafe.Add(ref dstPtr, 1).rgb = Unsafe.Add(ref srcPtr, 1);
                Unsafe.Add(ref dstPtr, 2).rgb = Unsafe.Add(ref srcPtr, 2);
                Unsafe.Add(ref dstPtr, 3).rgb = Unsafe.Add(ref srcPtr, 3);
                Unsafe.Add(ref dstPtr, 4).rgb = Unsafe.Add(ref srcPtr, 4);
                Unsafe.Add(ref dstPtr, 5).rgb = Unsafe.Add(ref srcPtr, 5);
                Unsafe.Add(ref dstPtr, 6).rgb = Unsafe.Add(ref srcPtr, 6);
                Unsafe.Add(ref dstPtr, 7).rgb = Unsafe.Add(ref srcPtr, 7);

                sizeToWrite -= 8;
                srcPtr = ref Unsafe.Add(ref srcPtr, 8);
                dstPtr = ref Unsafe.Add(ref dstPtr, 8);
            }

            for (int x = sizeToWrite; x > 0; x--)
            {
                dstPtr.rgb = srcPtr;
                srcPtr = ref Unsafe.Add(ref srcPtr, 1);
                dstPtr = ref Unsafe.Add(ref dstPtr, 1);
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
            var dstPalette4 = PaletteBuffer.AsSpan();

            SetPixels3N(dstPalette4, srcPalette3);

            SetPixelsPalette(indices, dstPalette4);
        }

        void SetPixelsPalette4(ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette)
        {
            var srcPalette4 = palette.Cast<Rgba>();

            if (srcPalette4.Length == 256)
            {
                SetPixelsPalette(indices, srcPalette4);
            }
            else
            {
                var dstPalette4 = PaletteBuffer.AsSpan();
                srcPalette4.CopyTo(dstPalette4);
                SetPixelsPalette(indices, dstPalette4);
            }
        }

        void SetPixelsPalette(ReadOnlySpan<byte> indices, ReadOnlySpan<Rgba> palette)
        {
            SetPixelsPaletteN(BufferSpan(), indices, palette);
        }

        static void SetPixelsPaletteN(ReadOnlySpan<byte> dst, ReadOnlySpan<byte> indices, ReadOnlySpan<Rgba> palette)
        {
            var dst4 = dst.Cast<Rgba>();
            int sizeToWrite = dst4.Length;

            ref Rgba palettePtr = ref MemoryMarshal.GetReference(palette); // palette is size 256
            ref byte indicesPtr = ref MemoryMarshal.GetReference(indices[..sizeToWrite]); // ensure it's equal to dst4 
            ref var dstPtr = ref MemoryMarshal.GetReference(dst4);

            while (sizeToWrite > 8)
            {
                dstPtr = Unsafe.Add(ref palettePtr, indicesPtr);
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
    }
}