using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public sealed class ARFoundationScreenCaptureManager : IScreenCaptureManager, IDisposable
    {
        [NotNull] readonly ARCameraManager cameraManager;
        [NotNull] readonly Transform arCameraTransform;
        [NotNull] readonly AROcclusionManager occlusionManager;
        
        Intrinsic? intrinsic;

        Intrinsic Intrinsic
        {
            get
            {
                if (intrinsic != null)
                {
                    return intrinsic.Value;
                }

                intrinsic = (cameraManager.TryGetIntrinsics(out var intrinsics)
                    ? new Intrinsic(intrinsics.focalLength.x, intrinsics.principalPoint.x,
                        intrinsics.focalLength.y, intrinsics.principalPoint.y)
                    : default);
                return intrinsic.Value;
            }
        }

        [CanBeNull] Screenshot lastColor;
        [CanBeNull] Screenshot lastDepth;
        [CanBeNull] Screenshot lastConfidence;

        public event Action<Screenshot> ScreenshotColor;
        public event Action<Screenshot> ScreenshotDepth;

        public ARFoundationScreenCaptureManager(
            [NotNull] ARCameraManager cameraManager,
            [NotNull] Transform arCameraTransform,
            [NotNull] AROcclusionManager occlusionManager)
        {
            this.cameraManager = cameraManager != null
                ? cameraManager
                : throw new ArgumentNullException(nameof(cameraManager));
            this.arCameraTransform = arCameraTransform != null
                ? arCameraTransform
                : throw new ArgumentNullException(nameof(arCameraTransform));
            this.occlusionManager = occlusionManager != null
                ? occlusionManager
                : throw new ArgumentNullException(nameof(occlusionManager));
        }

        public bool Started => true;

        public IEnumerable<(int width, int height)> GetResolutions()
        {
            var configuration = cameraManager.subsystem.currentConfiguration;

            return configuration == null
                ? Array.Empty<(int width, int height)>()
                : new[] {(configuration.Value.width, configuration.Value.height)};
        }

        [NotNull]
        public Task StartAsync(int width, int height, bool withHolograms) => Task.CompletedTask;

        [NotNull]
        public Task StopAsync() => Task.CompletedTask;

        public async ValueTask<Screenshot> CaptureColorAsync(
            int reuseCaptureAgeInMs = 0,
            CancellationToken token = default)
        {
            if (reuseCaptureAgeInMs > 0
                && lastColor != null
                && (GameThread.Now - lastColor.Timestamp.ToDateTime()).TotalMilliseconds < reuseCaptureAgeInMs)
            {
                return lastColor;
            }

            token.ThrowIfCancellationRequested();

            if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                return null;
            }

            TaskCompletionSource<Screenshot> task;
            using (image)
            {
                var conversionParams = new XRCpuImage.ConversionParams
                {
                    inputRect = new RectInt(0, 0, image.width, image.height),
                    outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
                    outputFormat = TextureFormat.RGB24,
                };

                task = new TaskCompletionSource<Screenshot>();
                int width = image.width / 2;
                int height = image.height / 2;
                var pose = arCameraTransform.AsPose();

                image.ConvertAsync(conversionParams, (status, _, array) =>
                {
                    if (token.IsCancellationRequested)
                    {
                        task.TrySetCanceled();
                        return;
                    }

                    if (status != XRCpuImage.AsyncConversionStatus.Ready)
                    {
                        Logger.Info($"{this}: Conversion request of color image failed with status {status}");
                        task.TrySetResult(null);
                        return;
                    }

                    byte[] bytes = new byte[array.Length];
                    NativeArray<byte>.Copy(array, bytes, array.Length);

                    var ss = new Screenshot(ScreenshotFormat.Rgb, width, height, Intrinsic.Scale(0.5f), pose, bytes);
                    lastColor = ss;
                    ScreenshotColor?.Invoke(ss);
                    task.TrySetResult(ss);
                });
            }

            return await task.Task;
        }

        public async ValueTask<Screenshot> CaptureDepthAsync(
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

            if (!occlusionManager.TryAcquireEnvironmentDepthCpuImage(out XRCpuImage image))
            {
                Logger.Info($"{this}: Depth capture failed.");
                return null;
            }

            TaskCompletionSource<Screenshot> task;
            using (image)
            {
                var conversionParams = new XRCpuImage.ConversionParams
                {
                    inputRect = new RectInt(0, 0, image.width, image.height),
                    outputDimensions = new Vector2Int(image.width, image.height),
                    outputFormat = TextureFormat.RFloat,
                    transformation = XRCpuImage.Transformation.MirrorY
                };


                task = new TaskCompletionSource<Screenshot>();
                int width = image.width;
                int height = image.height;
                var pose = arCameraTransform.AsPose();

                int? colorWidth = cameraManager.subsystem.currentConfiguration?.width;

                image.ConvertAsync(conversionParams, (status, _, array) =>
                {
                    if (token.IsCancellationRequested)
                    {
                        task.TrySetCanceled();
                        return;
                    }

                    if (status != XRCpuImage.AsyncConversionStatus.Ready)
                    {
                        Logger.Info($"{this}: Conversion request of depth image failed with status {status}");
                        task.TrySetResult(null);
                        return;
                    }

                    byte[] bytes = new byte[array.Length];
                    NativeArray<byte>.Copy(array, bytes, array.Length);

                    float scale = colorWidth == null ? 1 : width / (float) colorWidth.Value;
                    var ss = new Screenshot(ScreenshotFormat.Float, width, height, Intrinsic.Scale(scale), pose, bytes);
                    lastDepth = ss;
                    ScreenshotDepth?.Invoke(ss);
                    task.TrySetResult(ss);
                });
            }

            var screenshot = await task.Task;
            await Task.Run(() => MirrorX<float>(screenshot.Bytes, screenshot.Width, screenshot.Height), token);
            return screenshot;
        }

        public async ValueTask<Screenshot> CaptureDepthConfidenceAsync(
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
                Logger.Info($"{this}: Depth capture failed.");
                return null;
            }

            TaskCompletionSource<Screenshot> task;
            using (image)
            {
                var conversionParams = new XRCpuImage.ConversionParams
                {
                    inputRect = new RectInt(0, 0, image.width, image.height),
                    outputDimensions = new Vector2Int(image.width, image.height),
                    outputFormat = TextureFormat.R8,
                    transformation = XRCpuImage.Transformation.MirrorY
                };


                task = new TaskCompletionSource<Screenshot>();
                int width = image.width;
                int height = image.height;
                var pose = arCameraTransform.AsPose();

                int? colorWidth = cameraManager.subsystem.currentConfiguration?.width;

                image.ConvertAsync(conversionParams, (status, _, array) =>
                {
                    if (token.IsCancellationRequested)
                    {
                        task.TrySetCanceled();
                        return;
                    }

                    if (status != XRCpuImage.AsyncConversionStatus.Ready)
                    {
                        Logger.Info($"{this}: Conversion request of confidence image failed with status {status}");
                        task.TrySetResult(null);
                        return;
                    }

                    byte[] bytes = new byte[array.Length];
                    NativeArray<byte>.Copy(array, bytes, array.Length);

                    float scale = colorWidth == null ? 1 : width / (float) colorWidth.Value;
                    var ss = new Screenshot(ScreenshotFormat.Mono8, width, height, Intrinsic.Scale(scale), pose, bytes);
                    lastConfidence = ss;
                    task.TrySetResult(ss);
                });
            }

            var screenshot = await task.Task;
            await Task.Run(() => MirrorX<byte>(screenshot.Bytes, screenshot.Width, screenshot.Height), token);
            return screenshot;
        }

        static unsafe void MirrorX<T>([NotNull] byte[] bytes, int width, int height) where T : unmanaged
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (width * height * sizeof(T) > bytes.Length)
            {
                throw new ArgumentException("Sizes are not correct and might cause a buffer overflow!");
            }

            fixed (byte* bytesPtr = bytes)
            {
                T* ptr = (T*) bytesPtr;

                for (int v = 0; v < height; v++)
                {
                    T* start = ptr + v * width;
                    T* end = ptr + (v + 1) * width - 1;
                    for (int u = 0; u < width / 2; u++, start++, end--)
                    {
                        T tmp = *end;
                        *end = *start;
                        *start = tmp;
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}