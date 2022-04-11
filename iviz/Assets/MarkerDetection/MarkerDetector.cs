#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Tools;
using UnityEngine;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.MarkerDetection
{
    public sealed class MarkerDetector
    {
        bool enableQr;
        bool enableAruco;

        readonly Task task;
        readonly CancellationTokenSource tokenSource = new();
        readonly SemaphoreSlim signal = new(0);
        CvContext? cvContext;

        CancellationToken Token => tokenSource.Token;
        public event Action<Screenshot, IMarkerCorners[]>? MarkerDetected;

        public int DelayBetweenCapturesInMs { get; set; } = 3000;
        public int DelayBetweenCapturesFastInMs { get; set; } = 500;

        public bool EnableQr
        {
            get => enableQr;
            set
            {
                enableQr = value;
                SetEnabled(enableQr || enableAruco);
            }
        }

        public bool EnableAruco
        {
            get => enableAruco;
            set
            {
                enableAruco = value;
                SetEnabled(enableQr || enableAruco);
            }
        }

        public MarkerDetector()
        {
            task = TaskUtils.Run(() => RunAsync().AwaitNoThrow(this));
        }

        async Task RunAsync()
        {
            try
            {
                await RunAsyncImpl();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Marker detection thread stopped.", e);
            }
            finally
            {
                cvContext?.Dispose();
                cvContext = null;
            }
        }

        async Task RunAsyncImpl()
        {
            var detectedMarkers = new List<IMarkerCorners>();
            ARMarkerType? typeFoundInLastRound = null;

            while (!Token.IsCancellationRequested)
            {
                var screenCaptureManager = Settings.ScreenCaptureManager;
                if (screenCaptureManager == null
                    || MarkerDetected == null
                    || (!enableAruco && !enableQr))
                {
                    await Task.Delay(DelayBetweenCapturesInMs, Token);
                    continue;
                }

                await Task.Delay(typeFoundInLastRound != null
                        ? DelayBetweenCapturesFastInMs
                        : DelayBetweenCapturesInMs,
                    Token);

                var screenshot = await CaptureScreenshotAsync();
                if (screenshot == null)
                {
                    continue;
                }

                var context = ValidateCvContextForScreenshot(screenshot);
                if (context == null)
                {
                    RosLogger.Info($"{this}: Creating CV context failed. Stopping marker detector.");
                    return;
                }

                if (Settings.IsHololens)
                {
                    context.SetImageDataFlipY(screenshot.Bytes, screenshot.Bpp);
                }
                else
                {
                    context.SetImageData(screenshot.Bytes, screenshot.Bpp);
                }

                ARMarkerType? ProcessQr()
                {
                    var corners = context.DetectQrMarkers();
                    detectedMarkers.AddRange(corners);
                    return corners.Length != 0 ? ARMarkerType.QrCode : null;
                }

                ARMarkerType? ProcessAruco()
                {
                    var corners = context.DetectArucoMarkers();
                    detectedMarkers.AddRange(corners);
                    return corners.Length != 0 ? ARMarkerType.Aruco : null;
                }

                detectedMarkers.Clear();
                if (enableQr && !enableAruco)
                {
                    typeFoundInLastRound = ProcessQr();
                }
                else if (enableAruco && !enableQr)
                {
                    typeFoundInLastRound = ProcessAruco();
                }
                else if (typeFoundInLastRound != null)
                {
                    // if both active, prefer the last type found
                    typeFoundInLastRound = typeFoundInLastRound == ARMarkerType.Aruco
                        ? ProcessAruco()
                        : ProcessQr();
                }
                else
                {
                    typeFoundInLastRound = ProcessQr() ?? ProcessAruco();
                }

                if (typeFoundInLastRound == null)
                {
                    continue;
                }

                var detectedMarkersAsArray = detectedMarkers.ToArray();
                RaiseOnMarkerDetected(screenshot, detectedMarkersAsArray);
            }
        }

        async void SetEnabled(bool value)
        {
            if (Settings.ScreenCaptureManager == null)
            {
                return;
            }

            if (value)
            {
                if (Settings.IsHololens)
                {
                    await Settings.ScreenCaptureManager.StartAsync(1920, 1080, false).AwaitNoThrow(this);
                }
            }
            else
            {
                if (Settings.IsHololens)
                {
                    await Settings.ScreenCaptureManager.StopAsync().AwaitNoThrow(this);
                }
            }
        }

        public static Pose SolvePnp(ReadOnlySpan<Vector2f> imageCorners, in Intrinsic intrinsic, float sizeInM)
        {
            ReadOnlySpan<Vector3f> objectCorners = stackalloc Vector3f[]
            {
                (-sizeInM / 2, sizeInM / 2, 0),
                (sizeInM / 2, sizeInM / 2, 0),
                (sizeInM / 2, -sizeInM / 2, 0),
                (-sizeInM / 2, -sizeInM / 2, 0),
            };

            return CvContext.SolvePnp(imageCorners, objectCorners, intrinsic);
        }

        CvContext? ValidateCvContextForScreenshot(Screenshot screenshot)
        {
            if (cvContext != null
                && cvContext.Width == screenshot.Width
                && cvContext.Height == screenshot.Height)
            {
                return cvContext;
            }

            cvContext?.Dispose();

            try
            {
                cvContext = new CvContext(screenshot.Width, screenshot.Height);
            }
            catch (CvNotAvailableException e)
            {
                RosLogger.Error($"{this}: No OpenCV library found", e);
                cvContext = null;
            }

            return cvContext;
        }

        Task<Screenshot?> CaptureScreenshotAsync()
        {
            return GameThread.PostAsync(() =>
                Settings.ScreenCaptureManager?.CaptureColorAsync(250, Token) ?? new ValueTask<Screenshot?>());
        }

        void RaiseOnMarkerDetected(Screenshot screenshot, IMarkerCorners[] detectedMarkers)
        {
            try
            {
                MarkerDetected?.Invoke(screenshot, detectedMarkers);
            }
            catch (Exception e)
            {
                RosLogger.Error("Error during detector event:", e);
            }
            finally
            {
                signal.Release();
            }
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            MarkerDetected = null;
            task.WaitNoThrow(2000, this);
            signal.Dispose();
        }

        public override string ToString() => "[MarkerDetector]";
    }
}