using System;
using Iviz.Msgs;
using UnityEngine;

namespace Iviz.Core
{
    public enum ScreenshotFormat
    {
        Rgb,
        Bgra
    }

    public class Screenshot : IDisposable
    {
        public ScreenshotFormat Format { get; }
        public time Timestamp { get; }
        public int Width { get; }
        public int Height { get; }
        public int Bpp { get; }

        public float Fx { get; }
        public float Cx { get; }
        public float Fy { get; }
        public float Cy { get; }
        public Pose CameraPose { get; }

        public UniqueRef<byte> Bytes { get; }

        public Screenshot(ScreenshotFormat format, int width, int height, int bpp, float fx, float cx, float fy, float cy,
            in Pose cameraPose, UniqueRef<byte> bytes)
        {
            Format = format;
            Timestamp = time.Now();
            Width = width;
            Height = height;
            Bpp = bpp;
            Fx = fx;
            Cx = cx;
            Fy = fy;
            Cy = cy;
            CameraPose = cameraPose;
            Bytes = bytes;
        }
        public void Dispose()
        {
            Bytes.Dispose();
        }

        public override string ToString()
        {
            return $"[Screenshot width={Width} height={Height} At={CameraPose}]";
        }
    }
}