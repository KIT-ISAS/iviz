using System;
using Iviz.Msgs;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Core
{
    public enum ScreenshotFormat
    {
        Rgb,
        Bgra
    }

    public sealed class Screenshot : IDisposable
    {
        public ScreenshotFormat Format { get; }
        public time Timestamp { get; }
        public int Width { get; }
        public int Height { get; }
        public int Bpp { get; }
        public Intrinsic Intrinsic { get; }
        public Pose CameraPose { get; }
        public UniqueRef<byte> Bytes { get; }

        public Screenshot(ScreenshotFormat format, int width, int height, int bpp, in Intrinsic intrinsic,
            in Pose cameraPose, UniqueRef<byte> bytes)
        {
            Format = format;
            Timestamp = time.Now();
            Width = width;
            Height = height;
            Bpp = bpp;
            Intrinsic = intrinsic;
            CameraPose = cameraPose;
            Bytes = bytes;
        }
        public void Dispose()
        {
            Bytes.Dispose();
        }

        [NotNull]
        public override string ToString()
        {
            return $"[Screenshot width={Width} height={Height} At={CameraPose}]";
        }
    }
}