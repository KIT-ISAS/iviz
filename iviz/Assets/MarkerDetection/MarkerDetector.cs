#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Tools;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.MarkerDetection
{
    public sealed class MarkerDetector
    {
        bool enableQr;
        bool enableAruco;

        readonly Task task;
        readonly CancellationTokenSource tokenSource = new();

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
            task = Task.Run(async () => await RunAsync().AwaitNoThrow(this));
        }

        async Task RunAsync()
        {
            CvContext? cvContext = null;

            try
            {
                var signal = new SemaphoreSlim(0);

                ARMarkerType? lastRoundFound = null;
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

                    await Task.Delay(lastRoundFound != null
                            ? DelayBetweenCapturesFastInMs
                            : DelayBetweenCapturesInMs,
                        Token);

                    Screenshot? screenshot = null;
                    GameThread.Post(async () =>
                    {
                        try
                        {
                            screenshot = await screenCaptureManager.CaptureColorAsync(250, Token).AwaitNoThrow(this);
                        }
                        finally
                        {
                            signal.Release();
                        }
                    });
                    await signal.WaitAsync(Token);

                    if (screenshot == null)
                    {
                        continue;
                    }

                    if (cvContext != null &&
                        (cvContext.Width != screenshot.Width || cvContext.Height != screenshot.Height))
                    {
                        cvContext.Dispose();
                        cvContext = null;
                    }

                    cvContext ??= new CvContext(screenshot.Width, screenshot.Height);

                    if (Settings.IsHololens)
                    {
                        cvContext.SetImageDataFlipY(screenshot.Bytes, screenshot.Bpp);
                    }
                    else
                    {
                        cvContext.SetImageData(screenshot.Bytes, screenshot.Bpp);
                    }

                    IEnumerable<IMarkerCorners>? markerCorners;

                    IEnumerable<IMarkerCorners>? ProcessQr()
                    {
                        if (cvContext.DetectQrMarkers() == 0)
                        {
                            return null;
                        }

                        lastRoundFound = ARMarkerType.QrCode;
                        return cvContext.GetDetectedQrCorners();
                    }

                    IEnumerable<IMarkerCorners>? ProcessAruco()
                    {
                        if (cvContext.DetectArucoMarkers() == 0)
                        {
                            return null;
                        }

                        lastRoundFound = ARMarkerType.Aruco;
                        return cvContext.GetDetectedArucoCorners();
                    }

                    //float start = GameThread.GameTime;
                    if (enableQr && !enableAruco)
                    {
                        markerCorners = ProcessQr();
                    }
                    else if (enableAruco && !enableQr)
                    {
                        markerCorners = ProcessAruco();
                    }
                    else if (lastRoundFound != null)
                    {
                        markerCorners = lastRoundFound == ARMarkerType.Aruco ? ProcessAruco() : ProcessQr();
                    }
                    else
                    {
                        var qrCorners = ProcessQr();
                        var arucoCorners = ProcessAruco();
                        markerCorners = (qrCorners != null && arucoCorners != null)
                            ? qrCorners.Concat(arucoCorners)
                            : (qrCorners ?? arucoCorners);
                    }

                    if (markerCorners == null)
                    {
                        lastRoundFound = null;
                        continue;
                    }

                    GameThread.Post(() =>
                    {
                        try
                        {
                            MarkerDetected?.Invoke(screenshot, markerCorners.ToArray());
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Error during detector event:", e);
                        }
                        finally
                        {
                            signal.Release();
                        }
                    });

                    await signal.WaitAsync(Token);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                cvContext?.Dispose();
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

        public static Pose SolvePnp(IReadOnlyList<Vector2f> imageCorners, in Intrinsic intrinsic, float sizeInM)
        {
            var objectCorners = new Vector3f[]
            {
                (-sizeInM / 2, sizeInM / 2, 0),
                (sizeInM / 2, sizeInM / 2, 0),
                (sizeInM / 2, -sizeInM / 2, 0),
                (-sizeInM / 2, -sizeInM / 2, 0),
            };

            return CvContext.SolvePnp(imageCorners, objectCorners, intrinsic);
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            MarkerDetected = null;
            task.WaitNoThrow(2000, this);
        }

        public override string ToString() => "[MarkerDetector]";
    }
}