#nullable enable

using System;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using UnityEngine;

namespace Iviz.Common
{
    public sealed class Screenshot
    {
        public ScreenshotFormat Format { get; }
        public time Timestamp { get; }
        public int Width { get; }
        public int Height { get; }
        public int Bpp { get; }
        public Intrinsic Intrinsic { get; }
        public Pose CameraPose { get; }
        public byte[] Bytes { get; }

        public Screenshot(ScreenshotFormat format, time timestamp, int width, int height, in Intrinsic intrinsic,
            in Pose cameraPose, byte[] bytes)
        {
            Format = format;
            Timestamp = timestamp;
            Width = width;
            Height = height;
            Bpp = BppFromFormat(format);
            Intrinsic = intrinsic;
            CameraPose = cameraPose;
            Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
        }

        public CameraInfo CreateCameraInfoMessage(string cameraFrameId, uint seqId) =>
            new()
            {
                Header = (seqId, Timestamp, cameraFrameId),
                Width = (uint)Width,
                Height = (uint)Height,
                K = Intrinsic.ToArray(),
            };

        public Image CreateImageMessage(string cameraFrameId, uint seqId) =>
            new(
                (seqId, Timestamp, cameraFrameId),
                (uint)Height,
                (uint)Width,
                EncodingFromFormat(Format),
                0,
                (uint)(Bpp * Width),
                Bytes
            );

        static string EncodingFromFormat(ScreenshotFormat format)
        {
            return format switch
            {
                ScreenshotFormat.Bgra => "bgra8",
                ScreenshotFormat.Rgb => "rgb8",
                ScreenshotFormat.Mono8 => "mono8",
                ScreenshotFormat.Mono16 => "mono16",
                ScreenshotFormat.Float => "32FC",
                _ => throw new ArgumentException()
            };
        }

        static int BppFromFormat(ScreenshotFormat format)
        {
            return format switch
            {
                ScreenshotFormat.Mono8 => 1,
                ScreenshotFormat.Mono16 => 2,
                ScreenshotFormat.Rgb => 3,
                ScreenshotFormat.Float => 4,
                ScreenshotFormat.Bgra => 4,
                _ => throw new ArgumentException()
            };
        }

        public override string ToString()
        {
            return $"[Screenshot width={Width.ToString()} height={Height.ToString()} At={CameraPose.ToString()}]";
        }
    }
}