#nullable enable

using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Controllers
{
    public sealed class ARFoundationScreenCaptureManager : IScreenCaptureManager
    {
        readonly ARCameraManager cameraManager;
        readonly Transform arCameraTransform;
        readonly AROcclusionManager occlusionManager;

        Intrinsic? intrinsic;
        Screenshot? lastColor;
        Screenshot? lastDepth;
        Screenshot? lastConfidence;

        Intrinsic Intrinsic
        {
            get
            {
                if (intrinsic != null)
                {
                    return intrinsic.Value;
                }

                if (cameraManager.TryGetIntrinsics(out var intrinsics))
                {
                    intrinsic = new Intrinsic(intrinsics.focalLength.x, intrinsics.principalPoint.x,
                        intrinsics.focalLength.y, intrinsics.principalPoint.y);
                    return intrinsic.Value;
                }

                // shouldn't happen
                if (cameraManager.currentConfiguration is not { } configuration)
                {
                    throw new InvalidOperationException("AR subsystem is not set!");
                }

                const float defaultFovInRad = 60;
                intrinsic = new Intrinsic(defaultFovInRad, configuration.width, configuration.height);
                return intrinsic.Value;
            }
        }

        public bool Started => true;

        public ARFoundationScreenCaptureManager(
            ARCameraManager cameraManager,
            Transform arCameraTransform,
            AROcclusionManager occlusionManager)
        {
            ThrowHelper.ThrowIfNull(cameraManager, nameof(cameraManager));
            this.cameraManager = cameraManager;
            ThrowHelper.ThrowIfNull(arCameraTransform, nameof(arCameraTransform));
            this.arCameraTransform = arCameraTransform;
            ThrowHelper.ThrowIfNull(occlusionManager, nameof(occlusionManager));
            this.occlusionManager = occlusionManager;
        }

        public IEnumerable<(int width, int height)> GetResolutions()
        {
            return cameraManager.subsystem.currentConfiguration is { } configuration
                ? new[] { (configuration.width, configuration.height) }
                : Array.Empty<(int, int)>();
        }

        public ValueTask StartAsync(int width, int height, bool withHolograms) => default;

        public ValueTask StopAsync() => default;

        public ValueTask<Screenshot?> CaptureColorAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default)
        {
            if (reuseCaptureAgeInMs > 0
                && lastColor != null
                && (GameThread.Now - lastColor.Timestamp.ToDateTime()).TotalMilliseconds < reuseCaptureAgeInMs)
            {
                return lastColor.AsTaskResultMaybeNull();
            }

            token.ThrowIfCancellationRequested();

            if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                return default; // completed, null
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
                outputFormat = TextureFormat.RGB24,
            };

            int width = image.width / 2;
            int height = image.height / 2;
            var pose = arCameraTransform.AsPose();
            var ts = TaskUtils.CreateCompletionSource<Screenshot?>();

            using var _ = image;
            image.ConvertAsync(conversionParams, (status, _, array) =>
            {
                if (token.IsCancellationRequested)
                {
                    ts.TrySetCanceled(token);
                    return;
                }

                if (status != XRCpuImage.AsyncConversionStatus.Ready)
                {
                    ts.TrySetException(InvalidScreenshotException(status));
                    return;
                }

                var screenshot = new Screenshot(ScreenshotFormat.Rgb,
                    GameThread.TimeNow, width, height,
                    Intrinsic.Scale(0.5f), pose, array.ToArray());
                lastColor = screenshot;
                ts.TrySetResult(screenshot);
            });


            return ts.Task.AsValueTask();
        }

        public ValueTask<Screenshot?> CaptureDepthAsync(
            int reuseCaptureAgeInMs = 0,
            CancellationToken token = default)
        {
            if (reuseCaptureAgeInMs > 0
                && lastDepth != null
                && (GameThread.Now - lastDepth.Timestamp.ToDateTime()).TotalMilliseconds < reuseCaptureAgeInMs)
            {
                return lastDepth.AsTaskResultMaybeNull();
            }

            token.ThrowIfCancellationRequested();

            if (occlusionManager.requestedEnvironmentDepthMode == EnvironmentDepthMode.Disabled)
            {
                return default; // completed, null
            }

            if (!occlusionManager.TryAcquireEnvironmentDepthCpuImage(out XRCpuImage image))
            {
                RosLogger.Info($"{this}: Depth capture failed.");
                return default; // completed, null
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.RFloat,
                transformation = XRCpuImage.Transformation.MirrorY // additional mirror x doesn't seem to work here
            };

            var ts = TaskUtils.CreateCompletionSource<Screenshot?>();
            int width = image.width;
            int height = image.height;
            var pose = arCameraTransform.AsPose();
            int colorWidth = cameraManager.subsystem.currentConfiguration?.width ?? width;

            using var _ = image;
            image.ConvertAsync(conversionParams, (status, _, array) =>
            {
                if (token.IsCancellationRequested)
                {
                    ts.TrySetCanceled(token);
                    return;
                }

                if (status != XRCpuImage.AsyncConversionStatus.Ready)
                {
                    ts.TrySetException(InvalidScreenshotException(status));
                    return;
                }

                float scale = width / (float)colorWidth;
                var screenshot = new Screenshot(ScreenshotFormat.Float,
                    GameThread.TimeNow, width, height,
                    Intrinsic.Scale(scale), pose, array.ToArray());

                Task.Run(() =>
                {
                    try
                    {
                        MirrorXf(screenshot.Width, screenshot.Height, screenshot.Bytes);
                        lastDepth = screenshot;
                        ts.TrySetResult(screenshot);
                    }
                    catch (Exception e)
                    {
                        ts.TrySetException(e);
                    }
                }, token);
            });

            return ts.Task.AsValueTask();
        }

        public ValueTask<Screenshot?> CaptureDepthConfidenceAsync(
            int reuseCaptureAgeInMs = 0,
            CancellationToken token = default)
        {
            if (reuseCaptureAgeInMs > 0
                && lastConfidence != null
                && (GameThread.Now - lastConfidence.Timestamp.ToDateTime()).TotalMilliseconds < reuseCaptureAgeInMs)
            {
                return lastConfidence.AsTaskResultMaybeNull();
            }

            token.ThrowIfCancellationRequested();

            if (occlusionManager.requestedEnvironmentDepthMode == EnvironmentDepthMode.Disabled)
            {
                return default; // completed, null
            }

            if (!occlusionManager.TryAcquireEnvironmentDepthConfidenceCpuImage(out XRCpuImage image))
            {
                RosLogger.Info($"{this}: Depth capture failed.");
                return default; // completed, null
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.R8,
                transformation = XRCpuImage.Transformation.MirrorY // additional mirror x doesn't seem to work here
            };


            var ts = TaskUtils.CreateCompletionSource<Screenshot?>();
            int width = image.width;
            int height = image.height;
            var pose = arCameraTransform.AsPose();

            int colorWidth = cameraManager.subsystem.currentConfiguration?.width ?? width;

            using var _ = image;
            image.ConvertAsync(conversionParams, (status, _, array) =>
            {
                if (token.IsCancellationRequested)
                {
                    ts.TrySetCanceled();
                    return;
                }

                if (status != XRCpuImage.AsyncConversionStatus.Ready)
                {
                    ts.TrySetException(InvalidScreenshotException(status));
                    return;
                }

                float scale = width / (float)colorWidth;
                var screenshot = new Screenshot(ScreenshotFormat.Mono8,
                    GameThread.TimeNow, width, height,
                    Intrinsic.Scale(scale), pose, array.ToArray());

                Task.Run(() =>
                {
                    try
                    {
                        MirrorXb(screenshot.Width, screenshot.Height, screenshot.Bytes);
                        lastConfidence = screenshot;
                        ts.TrySetResult(screenshot);
                    }
                    catch (Exception e)
                    {
                        ts.TrySetException(e);
                    }
                }, token);
            });

            return ts.Task.AsValueTask();
        }

        static Exception InvalidScreenshotException(XRCpuImage.AsyncConversionStatus status) =>
            new InvalidOperationException(
                $"Conversion request of color image failed with status {status}");

        static unsafe void MirrorXf(int width, int height, byte[] bytes)
        {
            int pitch = width * sizeof(float);
            if (pitch * height > bytes.Length) ThrowIndexOutOfRange();
            fixed (byte* bytesPtr = bytes)
            {
                foreach (int v in ..height)
                {
                    float* l = (float*)(bytesPtr + v * pitch);
                    float* r = l + (width - 1);

                    for (int u = width / 2; u > 0; u--)
                    {
                        (*l, *r) = (*r, *l);
                        l++;
                        r--;
                    }
                }
            }
        }

        static unsafe void MirrorXb(int width, int height, byte[] bytes)
        {
            if (width * height > bytes.Length) ThrowIndexOutOfRange();
            fixed (byte* bytesPtr = bytes)
            {
                foreach (int v in ..height)
                {
                    ulong* l = (ulong*)(bytesPtr + v * width);
                    ulong* r = l + (width / 8 - 1);

                    for (int u = width / 16; u > 0; u--)
                    {
                        ulong lFlip = BinaryPrimitives.ReverseEndianness(*l);
                        ulong rFlip = BinaryPrimitives.ReverseEndianness(*r);
                        *l = rFlip;
                        *r = lFlip;

                        l++;
                        r--;
                    }
                }
            }
        }

        [DoesNotReturn]
        static void ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();
    }
}