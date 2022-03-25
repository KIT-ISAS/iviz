#nullable enable

using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.Services.Communication;
using UnityEngine;

namespace VNC
{
    public readonly struct FrameRectangle : IDisposable
    {
        const int BytesPerPixel = 4;
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
            fullSize = Width * Height * BytesPerPixel;
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
            var dst4 = BufferSpan().Cast<Rgba>();
            var src3 = src.Cast<Rgb>();

            ref var dstPtr = ref MemoryMarshal.GetReference(dst4);
            ref var srcPtr = ref MemoryMarshal.GetReference(src3);

            for (int x = dst4.Length; x > 0; x--)
            {
                dstPtr.rgb = srcPtr;

                // this is messy, but il2cpp can't optimize bounds checking away if it happens in a span
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
            if (palette.Length > 256 * 3)
            {
                // this shouldn't happen
                return;
            }

            var srcPalette3 = palette.Cast<Rgb>();
            Span<Rgba> dstPalette4 = stackalloc Rgba[256]; // size 1024

            for (int i = 0; i < srcPalette3.Length; i++)
            {
                dstPalette4[i].rgb = srcPalette3[i];
            }

            SetPixelsPalette(indices, dstPalette4);
        }

        void SetPixelsPalette4(ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette)
        {
            if (palette.Length > 256 * 4)
            {
                // this shouldn't happen
                return;
            }

            var srcPalette4 = palette.Cast<Rgba>();
            if (srcPalette4.Length == 256)
            {
                SetPixelsPalette(indices, srcPalette4);
            }
            else
            {
                Span<Rgba> dstPalette4 = stackalloc Rgba[256]; // size 1024
                srcPalette4.CopyTo(dstPalette4);
                SetPixelsPalette(indices, dstPalette4);
            }
        }

        void SetPixelsPalette(ReadOnlySpan<byte> indices, ReadOnlySpan<Rgba> palette)
        {
            var dst4 = BufferSpan().Cast<Rgba>();
            ref Rgba palettePtr = ref MemoryMarshal.GetReference(palette);
            ref byte indicesPtr = ref MemoryMarshal.GetReference(indices);
            ref var dstPtr = ref MemoryMarshal.GetReference(dst4);

            for (int x = dst4.Length; x > 0; x--)
            {
                // avoid bounds checking, palette is big enough that this will never fail
                dstPtr = Unsafe.Add(ref palettePtr, indicesPtr);
                
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