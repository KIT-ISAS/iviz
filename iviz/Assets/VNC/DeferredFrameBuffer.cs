#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Iviz.Core;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.Services.Communication;
using MarcusW.VncClient.Rendering;
using UnityEngine;

namespace VNC
{
    internal sealed class DeferredFrameBuffer : IFramebufferReference
    {
        const int MaxFramesInQueue = 500;

        readonly VncScreen screen;
        readonly Queue<QueueEntry> frames = new();
        readonly int width;
        readonly int height;

        public Size Size => new(width, height);
        public PixelFormat Format => TurboJpegDecoder.RgbaCompatiblePixelFormat;

        public DeferredFrameBuffer(VncScreen screen, Size size)
        {
            this.screen = screen;
            width = size.Width;
            height = size.Height;
        }

        public void SetPixels(in Rectangle rectangle, ReadOnlySpan<byte> pixelData, in PixelFormat pixelFormat)
        {
            try
            {
                var frame = new FrameRectangle(rectangle.Size);
                frame.SetPixels(pixelData, pixelFormat);
                Enqueue(new QueueEntry(frame, rectangle));
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public void FillPixels(in Rectangle rectangle, ReadOnlySpan<byte> singlePixel, in PixelFormat pixelFormat)
        {
            try
            {
                Rgba color;
                if (pixelFormat.IsBinaryCompatibleTo(PixelFormat.RfbRgb888))
                {
                    color = default;
                    color.rgb = singlePixel.Read<Rgb>();
                }
                else if (pixelFormat.IsBinaryCompatibleTo(TurboJpegDecoder.RgbaCompatiblePixelFormat))
                {
                    color = singlePixel.Read<Rgba>();
                }
                else
                {
                    return;
                }

                Enqueue(new QueueEntry(rectangle, color));
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
                var frame = new FrameRectangle(rectangle.Size);
                frame.SetPixelsPalette(indices, palette, pixelFormat);
                Enqueue(new QueueEntry(frame, rectangle));
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public void CopyFrom(in Rectangle rectangle, in Rectangle srcRectangle)
        {
            try
            {
                Enqueue(new QueueEntry(rectangle, srcRectangle));
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        void Enqueue(in QueueEntry entry)
        {
            lock (frames)
            {
                frames.Enqueue(entry);
                if (frames.Count > MaxFramesInQueue)
                {
                    frames.Dequeue().Dispose();
                }
            }
        }

        public void Dispose()
        {
            int count = frames.Count;
            if (count == 0)
            {
                return;
            }

            var framesBuffer = new RentAndClear<QueueEntry>(count);
            lock (frames)
            {
                foreach (ref var frame in framesBuffer)
                {
                    frames.TryDequeue(out frame);
                }
            }

            GameThread.Post(() =>
            {
                using (framesBuffer)
                {
                    foreach (ref readonly var frame in framesBuffer)
                    {
                        ProcessFrame(frame);
                    }
                }

                screen.UpdateFrame(Size);
            });
        }

        Span<byte> GetTextureSpan() => screen.GetTextureSpan(Size);

        void ProcessFrame(in QueueEntry queueEntry)
        {
            if (queueEntry.frame is { } frame)
            {
                using (frame)
                {
                    CopyFrame(frame, queueEntry.rectangle.Position);
                }
            }
            else if (queueEntry.color is { } color)
            {
                FillRectangle(queueEntry.rectangle, color);
            }
            else
            {
                CopyRectangle(queueEntry.rectangle, queueEntry.srcRectangle);
            }
        }

        void CopyFrame(in FrameRectangle frame, in Position position)
        {
            int textureWidth = Size.Width;
            int srcPitch = frame.Width * 4;
            int dstPitch = textureWidth * 4;

            var src = frame.Address;
            int dstOffset = (position.Y * textureWidth + position.X) * 4;
            var dst = GetTextureSpan()[dstOffset..];

            if (frame.Width == textureWidth)
            {
                src.BlockCopyTo(dst);
            }
            else
            {
                for (int y = frame.Height - 1; y > 0; y--)
                {
                    src[..srcPitch].BlockCopyTo(dst);
                    dst = dst[dstPitch..];
                    src = src[srcPitch..];
                }

                src[..srcPitch].BlockCopyTo(dst);
            }
        }

        void FillRectangle(in Rectangle rectangle, Rgba rgba)
        {
            uint color = Unsafe.As<Rgba, uint>(ref rgba);
            if (rgba.rgb.IsGray)
            {
                // use memset if possible
                FillRectangle1(rectangle, (byte)(color & 0xff));
            }
            else
            {
                FillRectangle4(rectangle, color);
            }
        }

        void FillRectangle1(in Rectangle rectangle, byte c)
        {
            int dstPitch = Size.Width * 4;
            int rowLength = rectangle.Size.Width * 4;
            int dstOffset = rectangle.Position.Y * dstPitch + rectangle.Position.X * 4;

            var dst = GetTextureSpan().Cast<byte>()[dstOffset..];
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

        void FillRectangle4(in Rectangle rectangle, uint color)
        {
            int dstPitch = Size.Width;
            int rowLength = rectangle.Size.Width;

            int dstOffset = rectangle.Position.Y * dstPitch + rectangle.Position.X;

            var dst = GetTextureSpan().Cast<uint>()[dstOffset..];
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

        void CopyRectangle(in Rectangle dstRectangle, in Rectangle srcRectangle)
        {
            int textureWidth = Size.Width;

            int pitch = textureWidth * 4;
            int rectHeight = srcRectangle.Size.Height;
            int rectWidth = srcRectangle.Size.Width;
            int rowLength = rectWidth * 4;
            var span = GetTextureSpan();
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

        public void DisposeAllFrames()
        {
            lock (frames)
            {
                while (frames.TryDequeue(out var frame))
                {
                    frame.Dispose();
                }
            }
        }

        readonly struct QueueEntry : IDisposable
        {
            public readonly FrameRectangle? frame;
            public readonly Rectangle rectangle;
            public readonly Rectangle srcRectangle;
            public readonly Rgba? color;

            public QueueEntry(in FrameRectangle renderFrame, in Rectangle rectangle) : this() =>
                (frame, this.rectangle) = (renderFrame, rectangle);

            public QueueEntry(in Rectangle rectangle, in Rectangle srcRectangle) : this() =>
                (this.rectangle, this.srcRectangle) = (rectangle, srcRectangle);

            public QueueEntry(in Rectangle rectangle, Rgba color) : this() =>
                (this.rectangle, this.color) = (rectangle, color);

            public void Dispose() => frame?.Dispose();
        }
    }
}