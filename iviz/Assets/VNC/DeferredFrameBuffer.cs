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
                    frame.CopyTo(GetTextureSpan(), width, queueEntry.rectangle.Position);
                }
            }
            else if (queueEntry.color is { } color)
            {
                FrameRectangle.FillRectangle(GetTextureSpan(), width, queueEntry.rectangle, color);
            }
            else
            {
                FrameRectangle.CopyRectangle(GetTextureSpan(), width, queueEntry.rectangle, queueEntry.srcRectangle);
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