#nullable enable

using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Tools;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Controllers
{
    public sealed class ARScreenCaptureManager : IScreenCaptureManager
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

        public ARScreenCaptureManager(
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

        public async ValueTask<Screenshot?> CaptureDepthAsync(
            int reuseCaptureAgeInMs = 0,
            CancellationToken token = default)
        {
            if (reuseCaptureAgeInMs > 0
                && lastDepth != null
                && (GameThread.Now - lastDepth.Timestamp.ToDateTime()).TotalMilliseconds < reuseCaptureAgeInMs)
            {
                return lastDepth;
            }

            token.ThrowIfCancellationRequested();

            if (occlusionManager.requestedEnvironmentDepthMode == EnvironmentDepthMode.Disabled)
            {
                return null;
            }

            if (!occlusionManager.TryAcquireEnvironmentDepthCpuImage(out var image))
            {
                RosLogger.Info($"{this}: Depth capture failed.");
                return null;
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.RFloat,
                transformation = XRCpuImage.Transformation.MirrorY // additional mirror x doesn't seem to work here
            };

            int width = image.width;
            int height = image.height;
            var pose = arCameraTransform.AsPose();
            int colorWidth = cameraManager.subsystem.currentConfiguration?.width ?? width;

            XRCpuImage.AsyncConversion conversion;
            using (image)
            {
                conversion = image.ConvertAsync(conversionParams);
            }

            using (conversion)
            {
                await GameThread.WaitUntilAsync(() => IsFinished(conversion));

                if (conversion.status != XRCpuImage.AsyncConversionStatus.Ready)
                {
                    throw InvalidScreenshotException(conversion.status);
                }

                byte[] array = new byte[width * height * sizeof(float)];
                float scale = width / (float)colorWidth;
                var screenshot = new Screenshot(ScreenshotFormat.Float,
                    GameThread.TimeNow, width, height,
                    Intrinsic.Scale(scale), pose, array);

                var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                try
                {
                    await ConversionUtils.MirrorXf(width, height, conversion.GetData<float>(),
                        NativeArrayUtils.CreateWrapper(handle, array.Length));
                }
                finally
                {
                    handle.Free();
                }

                return screenshot;
            }
        }

        public async ValueTask<Screenshot?> CaptureDepthConfidenceAsync(
            int reuseCaptureAgeInMs = 0,
            CancellationToken token = default)
        {
            if (reuseCaptureAgeInMs > 0
                && lastConfidence != null
                && (GameThread.Now - lastConfidence.Timestamp.ToDateTime()).TotalMilliseconds < reuseCaptureAgeInMs)
            {
                return lastConfidence;
            }

            token.ThrowIfCancellationRequested();

            if (occlusionManager.requestedEnvironmentDepthMode == EnvironmentDepthMode.Disabled)
            {
                return null;
            }

            if (!occlusionManager.TryAcquireEnvironmentDepthConfidenceCpuImage(out XRCpuImage image))
            {
                RosLogger.Info($"{this}: Depth capture failed.");
                return null;
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.R8,
                transformation = XRCpuImage.Transformation.MirrorY // additional mirror x doesn't seem to work here
            };

            int width = image.width;
            int height = image.height;
            var pose = arCameraTransform.AsPose();
            int colorWidth = cameraManager.subsystem.currentConfiguration?.width ?? width;

            XRCpuImage.AsyncConversion conversion;
            using (image)
            {
                conversion = image.ConvertAsync(conversionParams);
            }

            using (conversion)
            {
                await GameThread.WaitUntilAsync(() => IsFinished(conversion));

                if (conversion.status != XRCpuImage.AsyncConversionStatus.Ready)
                {
                    throw InvalidScreenshotException(conversion.status);
                }

                byte[] array = new byte[width * height];
                float scale = width / (float)colorWidth;
                var screenshot = new Screenshot(ScreenshotFormat.Mono8,
                    GameThread.TimeNow, width, height,
                    Intrinsic.Scale(scale), pose, array);

                var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                try
                {
                    await ConversionUtils.MirrorXb(width, height, conversion.GetData<byte>(),
                        NativeArrayUtils.CreateWrapper(handle, array.Length));
                }
                finally
                {
                    handle.Free();
                }

                return screenshot;
            }
        }

        static bool IsFinished(XRCpuImage.AsyncConversion conversion) =>
            conversion.status is XRCpuImage.AsyncConversionStatus.Ready or XRCpuImage.AsyncConversionStatus.Failed;

        static Exception InvalidScreenshotException(XRCpuImage.AsyncConversionStatus status) =>
            new InvalidOperationException($"Conversion request failed with status {status}");
    }
}