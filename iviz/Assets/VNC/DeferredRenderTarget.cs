#nullable enable

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.Services.Communication;
using MarcusW.VncClient.Rendering;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Screen = MarcusW.VncClient.Screen;

namespace VNC
{
    public interface IRenderTargetCallback
    {
        void ProcessFrame(IRenderFrame frame, in Rectangle rectangle, Size size);
        void CopyFrame(in Rectangle rectangle, in Rectangle srcRectangle);
    }

    public interface IRenderFrame
    {
        public ReadOnlySpan<byte> Address { get; }
        public Size Size { get; }
    }

    internal sealed class DeferredRenderTarget : IRenderTarget
    {
        readonly IRenderTargetCallback callback;
        DeferredFrameBufferReference? cachedFrameBufferReference;

        public DeferredRenderTarget(IRenderTargetCallback callback)
        {
            this.callback = callback;
        }

        public IFramebufferReference GrabFramebufferReference(Size size, IImmutableSet<Screen> layout)
        {
            if (cachedFrameBufferReference != null && cachedFrameBufferReference.Size == size)
            {
                return cachedFrameBufferReference;
            }

            cachedFrameBufferReference = new DeferredFrameBufferReference(callback, size);
            return cachedFrameBufferReference;
        }

        sealed class DeferredFrameBufferReference : IFramebufferReference
        {
            readonly IRenderTargetCallback callback;
            readonly List<FrameEntry> frames = new();
            readonly int width;
            readonly int height;
            readonly byte[] buffer;

            public Span<byte> Address => buffer;
            public Size Size => new(width, height);
            public PixelFormat Format => TurboJpegDecoder.RgbaCompatiblePixelFormat;

            public DeferredFrameBufferReference(IRenderTargetCallback callback, Size size)
            {
                this.callback = callback;
                width = size.Width;
                height = size.Height;
                int requested = width * height * 4;
                buffer = new byte[requested];
            }

            public void SetPixels(in Rectangle rectangle, ReadOnlySpan<byte> pixelData, in PixelFormat pixelFormat)
            {
                int powWidth = UnityUtils.ClosestPow2(rectangle.Size.Width);
                var frame = new FrameBufferReference(new Size(powWidth, rectangle.Size.Height));
                frame.SetPixels(rectangle.Size, pixelData, pixelFormat);
                frames.Add(new FrameEntry(frame, rectangle));
            }

            public void FillPixels(in Rectangle rectangle, ReadOnlySpan<byte> singlePixel, in PixelFormat pixelFormat)
            {
                int powWidth = UnityUtils.ClosestPow2(rectangle.Size.Width);
                var frame = new FrameBufferReference(new Size(powWidth, rectangle.Size.Height));
                frame.FillPixels(rectangle.Size, singlePixel, pixelFormat);
                frames.Add(new FrameEntry(frame, rectangle));
            }

            public void SetPixelsPalette(in Rectangle rectangle, ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette,
                in PixelFormat pixelFormat)
            {
                int powWidth = UnityUtils.ClosestPow2(rectangle.Size.Width);
                var frame = new FrameBufferReference(new Size(powWidth, rectangle.Size.Height));
                frame.SetPixelsPalette(rectangle.Size, indices, palette, pixelFormat);
                frames.Add(new FrameEntry(frame, rectangle));
            }

            public void CopyFrom(in Rectangle rectangle, in Rectangle srcRectangle)
            {
                frames.Add(new FrameEntry(rectangle, srcRectangle));
            }


            public void Dispose()
            {
                if (frames.Count == 0)
                {
                    return;
                }

                var framesArray = frames.ToArray();
                frames.Clear();

                GameThread.Post(() =>
                {
                    foreach (var frameEntry in framesArray)
                    {
                        if (frameEntry.frame is { } frame)
                        {
                            callback.ProcessFrame(frame, frameEntry.rectangle, Size);
                            frame.Dispose();
                        }
                        else
                        {
                            callback.CopyFrame(frameEntry.rectangle, frameEntry.srcRectangle);
                        }
                    }
                });
            }
        }

        readonly struct FrameEntry
        {
            public readonly FrameBufferReference? frame;
            public readonly Rectangle rectangle;
            public readonly Rectangle srcRectangle;

            public FrameEntry(FrameBufferReference frame, in Rectangle rectangle) =>
                (this.frame, this.rectangle, srcRectangle) = (frame, rectangle, default);

            public FrameEntry(in Rectangle rectangle, in Rectangle srcRectangle) =>
                (frame, this.rectangle, this.srcRectangle) = (null, rectangle, srcRectangle);
        }

        sealed class FrameBufferReference : IRenderFrame
        {
            const int BytesPerPixel = 4;
            readonly IntPtr buffer;
            readonly int width;
            readonly int height;
            readonly int fullSize;
            bool disposed;

            Span<byte> BufferSpan() => buffer.CreateSpan(fullSize);
            public ReadOnlySpan<byte> Address => BufferSpan();
            public Size Size => new(width, height);

            public FrameBufferReference(Size size)
            {
                width = size.Width;
                height = size.Height;
                fullSize = width * height * BytesPerPixel;
                buffer = Marshal.AllocHGlobal(fullSize);
            }

            public void Dispose()
            {
                if (disposed)
                {
                    return;
                }

                disposed = true;
                Marshal.FreeHGlobal(buffer);
            }

            public void SetPixels(in Size size, ReadOnlySpan<byte> pixelData, in PixelFormat pixelFormat)
            {
                try
                {
                    if (pixelFormat.IsBinaryCompatibleTo(TurboJpegDecoder.RgbaCompatiblePixelFormat))
                    {
                        SetPixels4(size, pixelData);
                    }
                    else if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                    {
                        SetPixels3(size, pixelData);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            void SetPixels4(in Size size, ReadOnlySpan<byte> src)
            {
                var dst = BufferSpan();
                int pitch = width * BytesPerPixel;
                int srcPitch = size.Width * BytesPerPixel;
                int srcHeight = size.Height;

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

            void SetPixels3(in Size size, ReadOnlySpan<byte> src)
            {
                var dst = BufferSpan();
                int dstWidth = width;
                int srcWidth = size.Width;
                int srcHeight = size.Height;

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

            public void FillPixels(in Size size, ReadOnlySpan<byte> singlePixel, in PixelFormat pixelFormat)
            {
                try
                {
                    if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                    {
                        Rgba rgba = default;
                        rgba.rgb = singlePixel.Read<Rgb>();
                        FillPixelsSolid(size, rgba);
                    }
                    else if (pixelFormat.IsBinaryCompatibleTo(TurboJpegDecoder.RgbaCompatiblePixelFormat))
                    {
                        var rgba = singlePixel.Read<Rgba>();
                        FillPixelsSolid(size, rgba);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            void FillPixelsSolid(in Size size, Rgba targetPixel)
            {
                var dst = BufferSpan();
                int dstWidth = width;
                int srcWidth = size.Width;
                int srcHeight = size.Height;

                var dst4 = dst.Cast<Rgba>();

                for (int y = srcHeight; y > 1; y--)
                {
                    dst4[..srcWidth].Fill(targetPixel);
                    dst4 = dst4[dstWidth..];
                }

                dst4[..srcWidth].Fill(targetPixel);
            }

            public void SetPixelsPalette(in Size size, ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette,
                in PixelFormat pixelFormat)
            {
                try
                {
                    if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                    {
                        SetPixelsPalette3(size, indices, palette);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            void SetPixelsPalette3(in Size size, ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette)
            {
                var dst = BufferSpan();
                int dstWidth = width;
                int srcWidth = size.Width;
                int srcHeight = size.Height;
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