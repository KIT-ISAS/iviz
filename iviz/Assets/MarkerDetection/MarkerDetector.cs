using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.XmlRpc;
using JetBrains.Annotations;
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
        readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        CancellationToken Token => tokenSource.Token;
        public event Action<Screenshot, IReadOnlyList<IMarkerCorners>> MarkerDetected;

        public int DelayBetweenCapturesInMs { get; set; } = 2000;
        public int DelayBetweenCapturesFastInMs { get; set; } = 500;

        public bool Enabled { get; private set; }

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
            CvContext cvContext = null;
            try
            {
                SemaphoreSlim signal = new SemaphoreSlim(0);
                List<IMarkerCorners> corners = new List<IMarkerCorners>();

                bool lastRoundSuccess = false;
                while (!Token.IsCancellationRequested)
                {
                    var screenCaptureManager = Settings.ScreenCaptureManager;
                    if (screenCaptureManager == null || MarkerDetected == null || !Enabled)
                    {
                        await Task.Delay(DelayBetweenCapturesInMs, Token);
                        continue;
                    }

                    await Task.Delay(lastRoundSuccess ? DelayBetweenCapturesFastInMs : DelayBetweenCapturesInMs, Token);

                    Screenshot screenshot = null;
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

                    if (cvContext == null)
                    {
                        cvContext = new CvContext(screenshot.Width, screenshot.Height);
                    }

                    if (Settings.IsHololens)
                    {
                        cvContext.SetImageDataFlipY(screenshot.Bytes, screenshot.Bpp);
                    }
                    else
                    {
                        cvContext.SetImageData(screenshot.Bytes, screenshot.Bpp);
                    }


                    corners.Clear();
                    if (EnableQr && cvContext.DetectQrMarkers() != 0)
                    {
                        var markerCorners = cvContext.GetDetectedQrCorners();
                        //Logger.Debug($"{this}: Found QR with code '{markerCorners[0].Code}'");
                        corners.AddRange(markerCorners);
                    }

                    if (EnableAruco && cvContext.DetectArucoMarkers() != 0)
                    {
                        var markerCorners = cvContext.GetDetectedArucoCorners();
                        //Logger.Debug($"{this}: Found Aruco with code {markerCorners[0].Code}");
                        corners.AddRange(cvContext.GetDetectedArucoCorners());
                    }

                    lastRoundSuccess = corners.Count != 0;
                    if (!lastRoundSuccess)
                    {
                        continue;
                    }

                    GameThread.Post(() =>
                    {
                        try
                        {
                            MarkerDetected?.Invoke(screenshot, corners);
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
            if (Enabled == value)
            {
                return;
            }

            Enabled = value;
            if (Enabled)
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

        public static Pose SolvePnp([NotNull] IReadOnlyList<Vector2f> imageCorners, in Intrinsic intrinsic,
            float sizeInM)
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

        [NotNull]
        public override string ToString()
        {
            return "[MarkerDetector]";
        }
    }
}