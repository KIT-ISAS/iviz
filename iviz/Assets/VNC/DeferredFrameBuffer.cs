#nullable enable

using System;
using System.Collections.Generic;
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

        bool useFrameSkip;
        bool frameSkip;

        public Size Size => new(width, height);
        public PixelFormat Format => TurboJpegDecoder.RgbaCompatiblePixelFormat;

        public DeferredFrameBuffer(VncScreen screen, Size size)
        {
            this.screen = screen;
            width = size.Width;
            height = size.Height;
            //GameThread.EveryFrame += Update;
        }

        public void SetPixels(in Rectangle rectangle, ReadOnlySpan<byte> pixelData, in PixelFormat pixelFormat)
        {
            var frame = new FrameRectangle(rectangle.Size);
            frame.SetPixels(pixelData, pixelFormat);
            Enqueue(new QueueEntry(frame, rectangle));
        }

        public void FillPixels(in Rectangle rectangle, ReadOnlySpan<byte> singlePixel, in PixelFormat pixelFormat)
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

        public void SetPixelsPalette(in Rectangle rectangle, ReadOnlySpan<byte> indices, ReadOnlySpan<byte> palette,
            in PixelFormat pixelFormat)
        {
            var frame = new FrameRectangle(rectangle.Size);
            frame.SetPixelsPalette(indices, palette, pixelFormat);
            Enqueue(new QueueEntry(frame, rectangle));
        }

        public void CopyFrom(in Rectangle rectangle, in Rectangle srcRectangle)
        {
            Enqueue(new QueueEntry(rectangle, srcRectangle));
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

                screen.UpdateFrame();
            });
        }

        void ProcessFrame(in QueueEntry queueEntry)
        {
            if (queueEntry.frame is { } frame)
            {
                using (frame)
                {
                    ProcessFrame(frame, queueEntry.rectangle.Position);
                }
            }
            else if (queueEntry.color is { } color)
            {
                ProcessFrame(queueEntry.rectangle, color);
            }
            else
            {
                CopyRectangle(queueEntry.rectangle, queueEntry.srcRectangle);
            }
        }

        void ProcessFrame(in FrameRectangle frame, in Position position)
        {
            int textureWidth = Size.Width;
            int srcPitch = frame.Width * 4;
            int dstPitch = textureWidth * 4;
            
            var src = frame.Address;
            int dstOffset = (position.Y * textureWidth + position.X) * 4;
            var dst = screen.GetTextureSpan(Size)[dstOffset..];

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

        void ProcessFrame(in Rectangle rectangle, Rgba color)
        {
            int dstPitch = Size.Width;
            int rowLength = rectangle.Size.Width;

            int dstOffset = rectangle.Position.Y * dstPitch + rectangle.Position.X;
            var dst = screen.GetTextureSpan(Size).Cast<Rgba>()[dstOffset..];

            if (rowLength == dstPitch)
            {
                dst[..(rectangle.Size.Height * rowLength)].Fill(color);
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
            var span = screen.GetTextureSpan(Size);
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