#nullable enable

#if false

using System;
using System.Buffers;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.Services.Communication;
using MarcusW.VncClient.Rendering;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Screen = MarcusW.VncClient.Screen;

namespace VNC
{
    internal sealed class RenderTarget : IRenderTarget
    {
        readonly Action<IFramebufferReference> callback;
        FrameBufferReference? cachedFrameBufferReference;

        public RenderTarget(Action<IFramebufferReference> callback)
        {
            this.callback = callback;
        }

        public IFramebufferReference GrabFramebufferReference(Size size, IImmutableSet<Screen> layout)
        {
            if (cachedFrameBufferReference != null && cachedFrameBufferReference.Size == size)
            {
                return cachedFrameBufferReference;
            }

            cachedFrameBufferReference = new FrameBufferReference(callback, size);
            return cachedFrameBufferReference;
        }

        sealed class FrameBufferReference : IFramebufferReference
        {
            readonly Action onFrameArrived;
            readonly byte[] buffer;
            readonly int width;
            readonly int height;
            const int BytesPerPixel = 4;

            public void CopyFrom(in Rectangle rectangle, in Rectangle srcRectangle)
            {
            }

            public Span<byte> Address => buffer;
            public Size Size => new(width, height);
            public PixelFormat Format => TurboJpegDecoder.RgbaCompatiblePixelFormat;

            public FrameBufferReference(Action<IFramebufferReference> callback, Size size)
            {
                width = size.Width;
                height = size.Height;
                int requested = width * height * 4;
                buffer = new byte[requested];
                onFrameArrived = () => callback(this);
            }

            public void SetPixels(in Rectangle rectangle, ReadOnlySpan<byte> pixelData, in PixelFormat pixelFormat)
            {
                try
                {
                    if (pixelFormat.IsBinaryCompatibleTo(Format))
                    {
                        SetPixels4(rectangle, pixelData);
                    }
                    else if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                    {
                        SetPixels3(rectangle, pixelData);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            void SetPixels4(in Rectangle rectangle, ReadOnlySpan<byte> src)
            {
                var dst = StartSpan(rectangle);
                int pitch = width * BytesPerPixel;
                int srcPitch = rectangle.Size.Width * BytesPerPixel;
                int srcHeight = rectangle.Size.Height;

                if (srcPitch == pitch)
                {
                    src.CopyTo(dst);
                }
                else
                {
                    for (int y = srcHeight; y > 1; y--)
                    {
                        src[..srcPitch].CopyTo(dst);
                        src = src[srcPitch..];
                        dst = dst[pitch..];
                    }
                    
                    src[..srcPitch].CopyTo(dst);
                }
            }

            void SetPixels3(in Rectangle rectangle, ReadOnlySpan<byte> src)
            {
                var dst = StartSpan(rectangle);
                int dstWidth = width;
                int srcWidth = rectangle.Size.Width;
                int srcHeight = rectangle.Size.Height;

                var dst4 = dst.Cast<Rgba>();
                var src3 = src.Cast<Rgb>();

                for (int y = srcHeight; y > 0; y--)
                {
                    // paranoid slicing to ensure we don't overflow
                    var dstRow = dst4[..srcWidth];
                    ref var dstPtr = ref dstRow[0];

                    var srcRow = src3[..srcWidth];
                    ref var srcPtr = ref Unsafe.AsRef(in srcRow[0]); // it's ok, we don't write here

                    for (int x = srcWidth; x > 0; x--)
                    {
                        dstPtr.rgb = srcPtr;

                        // this is messy, but il2cpp can't optimize bounds checking away if it happens in a span
                        srcPtr = ref Unsafe.Add(ref srcPtr, 1);
                        dstPtr = ref Unsafe.Add(ref dstPtr, 1);
                    }

                    if (y > 1)
                    {
                        src3 = src3[srcWidth..];
                        dst4 = dst4[dstWidth..];
                    }
                }
            }

            public void FillPixels(in Rectangle rectangle, ReadOnlySpan<byte> singlePixel, in PixelFormat pixelFormat)
            {
                try
                {
                    if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                    {
                        Rgba rgba = default;
                        rgba.rgb = singlePixel.Read<Rgb>();
                        FillPixelsSolid(rectangle, rgba);
                    }
                    else if (pixelFormat.IsBinaryCompatibleTo(Format))
                    {
                        var rgba = singlePixel.Read<Rgba>();
                        FillPixelsSolid(rectangle, rgba);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            
            void FillPixelsSolid(in Rectangle rectangle, Rgba targetPixel)
            {
                var dst = StartSpan(rectangle);
                int dstWidth = width;
                int srcWidth = rectangle.Size.Width;
                int srcHeight = rectangle.Size.Height;

                var dst4 = dst.Cast<Rgba>();
                
                for (int y = srcHeight; y > 1; y--)
                {
                    dst4[..srcWidth].Fill(targetPixel);
                    dst4 = dst4[dstWidth..];
                }

                dst4[..srcWidth].Fill(targetPixel);
            }
            
            public void SetPixelsPalette(in Rectangle rectangle, ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette,
                in PixelFormat pixelFormat)
            {
                try
                {
                    if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                    {
                        SetPixelsPalette3(rectangle, indices, palette);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            void SetPixelsPalette3(in Rectangle rectangle, ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette)
            {
                var dst = StartSpan(rectangle);
                int dstWidth = width;
                int srcWidth = rectangle.Size.Width;
                int srcHeight = rectangle.Size.Height;
                var palette3 = palette.Cast<Rgb>();

                var dst4 = dst.Cast<Rgba>();

                for (int y = srcHeight; y > 0; y--)
                {
                    // paranoid slicing to ensure we don't overflow
                    var dstRow = dst4[..srcWidth];
                    ref var dstPtr = ref dstRow[0];
                    
                    var indicesRow = indices[..srcWidth];
                    ref byte indicesPtr = ref Unsafe.AsRef(in indicesRow[0]); // it's ok, we don't write here

                    for (int x = srcWidth; x > 0; x--)
                    {
                        var rgb = palette3[indicesPtr]; // do not use pointer for palette here, this may throw
                        dstPtr.rgb = rgb;

                        dstPtr = ref Unsafe.Add(ref dstPtr, 1);
                        indicesPtr = ref Unsafe.Add(ref indicesPtr, 1);
                    }

                    if (y > 1)
                    {
                        indices = indices[srcWidth..];
                        dst4 = dst4[dstWidth..];
                    }
                }
            }


            Span<byte> StartSpan(in Rectangle rectangle) =>
                buffer.AsSpan((rectangle.Position.Y * width + rectangle.Position.X) * BytesPerPixel);
            
            public void Dispose()
            {
                GameThread.Post(onFrameArrived);
            }

            [StructLayout(LayoutKind.Sequential)]
            struct Rgba
            {
                public Rgb rgb;
                readonly byte a;
            }

            [StructLayout(LayoutKind.Sequential)]
            readonly struct Rgb
            {
                readonly byte r, g, b;
            }
        }
    }
}

#endif