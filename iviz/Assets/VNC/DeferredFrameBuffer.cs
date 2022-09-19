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
    internal sealed class DeferredFrameBuffer : FramebufferReference
    {
        const int MaxFramesInQueue = 500;

        readonly VncScreen screen;
        readonly Queue<QueueEntry> frames = new();
        readonly int width;

        public DeferredFrameBuffer(VncScreen screen, Size size) : base(size, TurboJpegDecoder.RgbaCompatiblePixelFormat)
        {
            this.screen = screen;
            width = size.Width;
        }

        public override void SetPixels(in Rectangle rectangle, ReadOnlySpan<byte> pixelData, in PixelFormat pixelFormat)
        {
            if (pixelData.Length == 0)
            {
                return;
            }

            try
            {
                var frame = new FrameRectangle(rectangle.Size);
                if (frame.SetPixels(pixelData, pixelFormat))
                {
                    Enqueue(new QueueEntry(frame, rectangle));
                }
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(DeferredFrameBuffer)}: Error in {nameof(SetPixels)}", e);
            }
        }

        public override void FillPixels(in Rectangle rectangle, ReadOnlySpan<byte> singlePixel,
            in PixelFormat pixelFormat)
        {
            try
            {
                Rgba color;
                if (pixelFormat.IsBinaryCompatibleTo(SupportedFormats.RfbRgb888))
                {
                    color = default;
                    color.Rgb = singlePixel.Read<Rgb>();
                }
                else if (pixelFormat.IsBinaryCompatibleTo(TurboJpegDecoder.RgbaCompatiblePixelFormat))
                {
                    color = singlePixel.Read<Rgba>();
                }
                else if (pixelFormat.IsBinaryCompatibleTo(SupportedFormats.RfbRgb565))
                {
                    int rgb565 = singlePixel.Read<ushort>();
                    int rgba = ConversionUtils.Convert565To888(rgb565);
                    color = Unsafe.As<int, Rgba>(ref rgba);
                }
                else
                {
                    return;
                }

                Enqueue(new QueueEntry(rectangle, color));
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(DeferredFrameBuffer)}: Error in {nameof(FillPixels)}", e);
            }
        }

        public override void SetPixelsPalette(in Rectangle rectangle, ReadOnlySpan<byte> indices,
            ReadOnlySpan<byte> palette,
            in PixelFormat pixelFormat)
        {
            if (indices.Length == 0)
            {
                return;
            }

            try
            {
                var frame = new FrameRectangle(rectangle.Size);
                if (frame.SetPixelsPalette(indices, palette, pixelFormat))
                {
                    Enqueue(new QueueEntry(frame, rectangle));
                }
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(DeferredFrameBuffer)}: Error in {nameof(SetPixelsPalette)}", e);
            }
        }

        public override void CopyFrom(in Rectangle rectangle, in Rectangle srcRectangle)
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

        public override void Dispose()
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
            var textureSpan = GetTextureSpan();
            if (queueEntry.frame is { } frame)
            {
                using (frame)
                {
                    frame.CopyTo(textureSpan, width, queueEntry.rectangle.Position);
                }
            }
            else if (queueEntry.color is { } color)
            {
                FrameRectangle.FillRectangle(textureSpan, width, queueEntry.rectangle, color);
            }
            else
            {
                FrameRectangle.CopyRectangle(textureSpan, width, queueEntry.rectangle, queueEntry.srcRectangle);
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