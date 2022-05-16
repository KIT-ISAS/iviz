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
        const int DelayBetweenCapturesInMs = 3000;
        const int DelayBetweenCapturesFastInMs = 500;

        bool enableQr;
        bool enableAruco;

        readonly Task task;
        readonly CancellationTokenSource tokenSource = new();

        public event Action<Screenshot, IReadOnlyList<IDetectedMarker>>? MarkerDetected;

        public bool EnableQr
        {
            get => enableQr;
            set
            {
                enableQr = value;
                if (Settings.IsHololens)
                {
                    SetHololensEnabled(enableQr || enableAruco);
                }
            }
        }

        public bool EnableAruco
        {
            get => enableAruco;
            set
            {
                enableAruco = value;
                if (Settings.IsHololens)
                {
                    SetHololensEnabled(enableQr || enableAruco);
                }
            }
        }

        public MarkerDetector()
        {
            task = TaskUtils.Run(RunAsync);
        }

        async Task RunAsync()
        {
            var token = tokenSource.Token;
            ARMarkerType? lastTypeFound = null;
            CvContext? cvContext = null;

            try
            {
                while (!token.IsCancellationRequested)
                {
                    var screenCaptureManager = Settings.ScreenCaptureManager;
                    if (screenCaptureManager == null
                        || MarkerDetected == null
                        || (!enableAruco && !enableQr))
                    {
                        await Task.Delay(DelayBetweenCapturesInMs, token);
                        continue;
                    }

                    await Task.Delay(lastTypeFound != null
                            ? DelayBetweenCapturesFastInMs
                            : DelayBetweenCapturesInMs,
                        token);

                    var screenshot = await CaptureScreenshotAsync(token);
                    if (screenshot == null)
                    {
                        continue;
                    }

                    cvContext = EnsureCvContext(cvContext, screenshot.Width, screenshot.Height);
                    if (cvContext == null)
                    {
                        RosLogger.Info($"{this}: Creating CV context failed. Stopping marker detector.");
                        return;
                    }

                    lastTypeFound = TryDetectMarkers(cvContext, screenshot, lastTypeFound);
                }
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
            }
        }

        ARMarkerType? TryDetectMarkers(CvContext context, Screenshot screenshot, ARMarkerType? lastTypeFound)
        {
            if (Settings.IsHololens)
            {
                context.SetImageDataFlipY(screenshot.Bytes, screenshot.Bpp);
            }
            else
            {
                context.SetImageData(screenshot.Bytes, screenshot.Bpp);
            }

            if (enableQr && (!enableAruco || lastTypeFound == ARMarkerType.QrCode))
            {
                if (context.DetectQrMarkers() is not { Length: > 0 } qrMarkers)
                {
                    return null;
                }

                RaiseOnMarkerDetected(screenshot, qrMarkers);
                return ARMarkerType.QrCode;
            }

            if (enableAruco && (!enableQr || lastTypeFound == ARMarkerType.Aruco))
            {
                if (context.DetectArucoMarkers() is not { Length: > 0 } arucoMarkers)
                {
                    return null;
                }

                RaiseOnMarkerDetected(screenshot, arucoMarkers);
                return ARMarkerType.Aruco;
            }

            var newArucoMarkers = context.DetectArucoMarkers();
            var newQrMarkers = context.DetectQrMarkers();
            switch (newArucoMarkers.Length != 0, newQrMarkers.Length != 0)
            {
                case (true, false):
                    RaiseOnMarkerDetected(screenshot, newArucoMarkers);
                    return ARMarkerType.Aruco;
                case (false, true):
                    RaiseOnMarkerDetected(screenshot, newQrMarkers);
                    return ARMarkerType.QrCode;
                case (true, true):
                    RaiseOnMarkerDetected(screenshot, newArucoMarkers.Concat<IDetectedMarker>(newQrMarkers).ToList());
                    return ARMarkerType.Aruco;
                case (false, false):
                    return null;
            }
        }

        async void SetHololensEnabled(bool value)
        {
            if (Settings.ScreenCaptureManager == null)
            {
                return;
            }

            if (value)
            {
                await Settings.ScreenCaptureManager.StartAsync(1920, 1080, false).AwaitNoThrow(this);
            }
            else
            {
                await Settings.ScreenCaptureManager.StopAsync().AwaitNoThrow(this);
            }
        }

        public static Pose SolvePnp(ReadOnlySpan<Vector2f> imageCorners, in Intrinsic intrinsic, float sizeInM)
        {
            ReadOnlySpan<Vector3f> objectCorners = stackalloc[]
            {
                new Vector3f(-sizeInM / 2, sizeInM / 2, 0),
                new Vector3f(sizeInM / 2, sizeInM / 2, 0),
                new Vector3f(sizeInM / 2, -sizeInM / 2, 0),
                new Vector3f(-sizeInM / 2, -sizeInM / 2, 0),
            };

            return CvContext.SolvePnp(imageCorners, objectCorners, intrinsic);
        }

        static CvContext? EnsureCvContext(CvContext? cvContext, int width, int height)
        {
            if (cvContext != null)
            {
                if (cvContext.MatchesSize(width, height))
                {
                    return cvContext;
                }

                cvContext.Dispose();
                // fall through
            }

            try
            {
                return new CvContext(width, height);
            }
            catch (CvNotAvailableException e)
            {
                RosLogger.Error($"[{nameof(MarkerDetector)}]: No OpenCV library found", e);
                return null;
            }
        }

        static Task<Screenshot?> CaptureScreenshotAsync(CancellationToken token)
        {
            return GameThread.PostAsync(() =>
                Settings.ScreenCaptureManager?.CaptureColorAsync(250, token) ?? default);
        }

        void RaiseOnMarkerDetected(Screenshot screenshot, IReadOnlyList<IDetectedMarker> detectedMarkers)
        {
            GameThread.Post(() =>
            {
                try
                {
                    MarkerDetected?.Invoke(screenshot, detectedMarkers);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"Error during {nameof(MarkerDetected)} event:", e);
                }
            });
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            MarkerDetected = null;
            task.WaitNoThrow(2000, this);
        }

        public override string ToString() => $"[{nameof(MarkerDetector)}]";
    }
}