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

namespace Iviz.Controllers
{
    public sealed class ARFoundationScreenCaptureManager : IScreenCaptureManager
    {
        [NotNull] readonly ARCameraManager cameraManager;
        [NotNull] readonly Transform arCameraTransform;
        [NotNull] readonly AROcclusionManager occlusionManager;

        [CanBeNull] Screenshot lastColor;
        [CanBeNull] Screenshot lastDepth;

        public event Action<Screenshot> ScreenshotColor;
        public event Action<Screenshot> ScreenshotDepth;

        public ARFoundationScreenCaptureManager(
            [NotNull] ARCameraManager cameraManager,
            [NotNull] Transform arCameraTransform,
            [NotNull] AROcclusionManager occlusionManager)
        {
            this.cameraManager = cameraManager;
            this.arCameraTransform = arCameraTransform;
            this.occlusionManager = occlusionManager;
        }

        public bool Started => true;

        [NotNull]
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

        public ValueTask<Screenshot> CaptureColorAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default)
        {
            if (reuseCaptureAgeInMs > 0
                && lastColor != null
                && (GameThread.Now - lastColor.Timestamp.ToDateTime()).TotalMilliseconds < reuseCaptureAgeInMs)
            {
                return new ValueTask<Screenshot>(lastColor);
            }

            token.ThrowIfCancellationRequested();
            if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                return new ValueTask<Screenshot>((Screenshot) null);
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.RGB24,
            };

            var task = new TaskCompletionSource<Screenshot>();
            int width = image.width;
            int height = image.height;
            var pose = arCameraTransform.AsPose();

            try
            {
                image.ConvertAsync(conversionParams, (status, _, array) =>
                {
                    if (token.IsCancellationRequested)
                    {
                        task.TrySetCanceled();
                        return;
                    }

                    if (status != XRCpuImage.AsyncConversionStatus.Ready)
                    {
                        Debug.LogErrorFormat("Request failed with status {0}", status);
                        task.TrySetResult(null);
                        return;
                    }

                    byte[] bytes = new byte[array.Length];
                    NativeArray<byte>.Copy(array, bytes, array.Length);

                    var intrinsic = cameraManager.TryGetIntrinsics(out var intrinsics)
                        ? new Intrinsic(intrinsics.focalLength.x, intrinsics.principalPoint.x,
                            intrinsics.focalLength.y, intrinsics.principalPoint.y)
                        : default;

                    var s = new Screenshot(ScreenshotFormat.Rgb, width, height, 3, intrinsic, pose, bytes);
                    lastColor = s;
                    ScreenshotColor?.Invoke(s);
                    task.TrySetResult(s);
                });
            }
            catch (Exception e)
            {
                task.TrySetException(e);
            }
            finally
            {
                image.Dispose();
            }

            return new ValueTask<Screenshot>(task.Task);
        }
        
        public ValueTask<Screenshot> CaptureDepthAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default)
        {
            if (reuseCaptureAgeInMs > 0
                && lastDepth != null
                && (GameThread.Now - lastDepth.Timestamp.ToDateTime()).TotalMilliseconds < reuseCaptureAgeInMs)
            {
                return new ValueTask<Screenshot>(lastDepth);
            }

            token.ThrowIfCancellationRequested();
            if (!occlusionManager.TryAcquireEnvironmentDepthCpuImage(out XRCpuImage image))
            {
                return new ValueTask<Screenshot>((Screenshot) null);
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.R16,
            };

            var task = new TaskCompletionSource<Screenshot>();
            int width = image.width;
            int height = image.height;
            var pose = arCameraTransform.AsPose();

            try
            {
                image.ConvertAsync(conversionParams, (status, _, array) =>
                {
                    if (token.IsCancellationRequested)
                    {
                        task.TrySetCanceled();
                        return;
                    }

                    if (status != XRCpuImage.AsyncConversionStatus.Ready)
                    {
                        Debug.LogErrorFormat("Request failed with status {0}", status);
                        task.TrySetResult(null);
                        return;
                    }

                    byte[] bytes = new byte[array.Length];
                    NativeArray<byte>.Copy(array, bytes, array.Length);

                    var intrinsic = cameraManager.TryGetIntrinsics(out var intrinsics)
                        ? new Intrinsic(intrinsics.focalLength.x, intrinsics.principalPoint.x,
                            intrinsics.focalLength.y, intrinsics.principalPoint.y)
                        : default;

                    var s = new Screenshot(ScreenshotFormat.R16, width, height, 2, intrinsic, pose, bytes);
                    lastDepth = s;
                    ScreenshotDepth?.Invoke(s);
                    task.TrySetResult(s);
                });
            }
            catch (Exception e)
            {
                task.TrySetException(e);
            }
            finally
            {
                image.Dispose();
            }

            return new ValueTask<Screenshot>(task.Task);
        }
    }
}