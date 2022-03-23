#nullable enable

using System;
using System.Buffers;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Rendering;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Screen = MarcusW.VncClient.Screen;

namespace VNC
{
    internal sealed class RenderTarget : IRenderTarget
    {
        readonly VncClient parent;
        FrameBufferReference? cachedFrameBufferReference;

        public RenderTarget(VncClient parent)
        {
            this.parent = parent;
        }

        public IFramebufferReference GrabFramebufferReference(Size size, IImmutableSet<Screen> layout)
        {
            if (cachedFrameBufferReference != null && cachedFrameBufferReference.Size == size)
            {
                return cachedFrameBufferReference;
            }

            cachedFrameBufferReference = new FrameBufferReference(parent, size);
            return cachedFrameBufferReference;
        }

        sealed class FrameBufferReference : IFramebufferReference
        {
            readonly Action frameArrived;
            readonly byte[] buffer;
            readonly int width;
            readonly int height;
            const int BytesPerPixel = 4;

            public Span<byte> Address => buffer;
            public Size Size => new(width, height);
            public PixelFormat Format => PixelFormat.BgraCompatiblePixelFormat;

            public FrameBufferReference(VncClient parent, Size size)
            {
                width = size.Width;
                height = size.Height;
                int requested = width * height * 4;
                buffer = new byte[requested];
                frameArrived = () => parent.OnFrameArrived(this);
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

            public void FillPixels(in Rectangle rectangle, ReadOnlySpan<byte> singlePixel, in PixelFormat pixelFormat,
                int numPixels)
            {
                try
                {
                    if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                    {
                        var rgb = singlePixel.Read<Rgb>();
                        FillPixelsSolid(rectangle, rgb);
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
                int pitch = width * BytesPerPixel;
                int rectangleWidth = rectangle.Size.Width;
                int rectangleHeight = rectangle.Size.Height;
                var palette3 = palette.Cast<Rgb>();

                for (int y = rectangleHeight; y > 0; y--)
                {
                    var dst4 = dst.Cast<Rgba>();
                    for (int x = 0; x < rectangleWidth; x++)
                    {
                        var rgb = palette3[indices[x]];
                        ref var dstPtr = ref dst4[x];
                        dstPtr.r = rgb.b;
                        dstPtr.g = rgb.g;
                        dstPtr.b = rgb.r;
                    }

                    indices = indices[rectangleWidth..];
                    dst = dst[pitch..];
                }
            }


            Span<byte> StartSpan(in Rectangle rectangle) =>
                buffer.AsSpan((rectangle.Position.Y * width + rectangle.Position.X) * BytesPerPixel);

            void FillPixelsSolid<T>(in Rectangle rectangle, T targetPixel) where T : unmanaged
            {
                var dst = StartSpan(rectangle);
                int pitch = width * BytesPerPixel;
                int rectanglePitch = rectangle.Size.Width * BytesPerPixel;
                int rectangleHeight = rectangle.Size.Height;

                for (int y = rectangleHeight; y > 0; y--)
                {
                    dst[..rectanglePitch].Cast<T>().Fill(targetPixel);
                    dst = dst[pitch..];
                }
            }

            void SetPixels4(in Rectangle rectangle, ReadOnlySpan<byte> src)
            {
                var dst = StartSpan(rectangle);
                int pitch = width * BytesPerPixel;
                int rectanglePitch = rectangle.Size.Width * BytesPerPixel;
                int rectangleHeight = rectangle.Size.Height;

                if (rectanglePitch == pitch)
                {
                    src.CopyTo(dst);
                }
                else
                {
                    for (int y = rectangleHeight; y > 0; y--)
                    {
                        src[..rectanglePitch].CopyTo(dst);
                        src = src[rectanglePitch..];
                        dst = dst[pitch..];
                    }
                }
            }

            void SetPixels3(in Rectangle rectangle, ReadOnlySpan<byte> src)
            {
                var dst = StartSpan(rectangle);
                int pitch = width * BytesPerPixel;
                int rectangleWidth = rectangle.Size.Width;
                int rectangleHeight = rectangle.Size.Height;

                var src3 = src.Cast<Rgb>();

                for (int y = rectangleHeight; y > 0; y--)
                {
                    // paranoid slicing to ensure we don't overflow
                    var dstRow = dst.Cast<Rgba>()[..rectangleWidth]; 
                    var srcRow = src3[..rectangleWidth];
                    
                    ref var srcPtr = ref Unsafe.AsRef(in src3[0]); // it's ok, we don't write here
                    ref var dstPtr = ref dstRow[0];

                    for (int x = rectangleWidth; x > 0; x--)
                    {
                        dstPtr.r = srcPtr.b;
                        dstPtr.g = srcPtr.g;
                        dstPtr.b = srcPtr.r;

                        // this is messy, but il2cpp can't optimize bounds checking away if it happens in a span
                        srcPtr = Unsafe.Add(ref srcPtr, 1);
                        dstPtr = Unsafe.Add(ref dstPtr, 1);
                    }

                    src3 = src3[rectangleWidth..];
                    dst = dst[pitch..];
                }
            }

            public void Dispose()
            {
                GameThread.Post(frameArrived);
            }
            
            [StructLayout(LayoutKind.Sequential)]
            struct Rgba
            {
                public byte r, g, b, a;
            }

            [StructLayout(LayoutKind.Sequential)]        
            readonly struct Rgb
            {
                public readonly byte r, g, b;
            }
        }
    }
}