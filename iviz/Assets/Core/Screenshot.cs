using System;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Core
{
    public enum ScreenshotFormat
    {
        Mono8,
        Rgb,
        Bgra,
        Mono16,
        Float,
    }

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

        public Screenshot(ScreenshotFormat format, int width, int height, in Intrinsic intrinsic, in Pose cameraPose,
            [NotNull] byte[] bytes)
        {
            Format = format;
            Timestamp = time.Now();
            Width = width;
            Height = height;
            Bpp = BppFromFormat(format);
            Intrinsic = intrinsic;
            CameraPose = cameraPose;
            Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
        }

        [NotNull]
        public CameraInfo CreateCameraInfoMessage(string cameraFrameId, uint seqId) =>
            new CameraInfo
            {
                Header = (seqId, Timestamp, cameraFrameId),
                Width = (uint)Width,
                Height = (uint)Height,
                K = Intrinsic.ToArray(),
            };

        [NotNull]
        public Image CreateImageMessage(string cameraFrameId, uint seqId) =>
            new Image(
                (seqId, Timestamp, cameraFrameId),
                (uint)Height,
                (uint)Width,
                EncodingFromFormat(Format),
                0,
                (uint)(Bpp * Width),
                Bytes
            );

        [NotNull]
        static string EncodingFromFormat(ScreenshotFormat format)
        {
            switch (format)
            {
                case ScreenshotFormat.Bgra:
                    return "bgra8";
                case ScreenshotFormat.Rgb:
                    return "rgb8";
                case ScreenshotFormat.Mono8:
                    return "mono8";
                case ScreenshotFormat.Mono16:
                    return "mono16";
                case ScreenshotFormat.Float:
                    return "32FC";
                default:
                    throw new ArgumentException();
            }
        }

        static int BppFromFormat(ScreenshotFormat format)
        {
            switch (format)
            {
                case ScreenshotFormat.Mono8:
                    return 1;
                case ScreenshotFormat.Mono16:
                    return 2;
                case ScreenshotFormat.Rgb:
                    return 3;
                case ScreenshotFormat.Float:
                case ScreenshotFormat.Bgra:
                    return 4;
                default:
                    throw new ArgumentException();
            }
        }

        [NotNull]
        public override string ToString()
        {
            return $"[Screenshot width={Width} height={Height} At={CameraPose}]";
        }
    }
}