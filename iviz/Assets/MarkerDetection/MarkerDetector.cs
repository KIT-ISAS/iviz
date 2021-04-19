using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.XmlRpc;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.MarkerDetection
{
    public sealed class MarkerDetector
    {
        bool enableQr;
        bool enableAruco;

        readonly Task task;
        readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        CancellationToken Token => tokenSource.Token;
        public event Action<Screenshot, IEnumerable<IMarkerCorners>> MarkerDetected;

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

                Logger.Info("Starting detector");

                while (!Token.IsCancellationRequested)
                {
                    //Logger.Info("Starting loop " + Enabled + " " + enableQr + " " + enableAruco);
                    await Task.Delay(2000, Token);

                    if (Settings.ScreenshotManager == null || !Enabled)
                    {
                        continue;
                    }


                    Logger.Info("Taking screenshot");

                    Screenshot screenshot = null;
                    GameThread.Post(async () =>
                    {
                        try
                        {
                            screenshot = await Settings.ScreenshotManager.TakeScreenshotColorAsync();
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Error during detector screenshot:", e);
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

                    Logger.Info("Back from screenshot");


                    if (cvContext != null &&
                        (cvContext.Width != screenshot.Width || cvContext.Height != screenshot.Height))
                    {
                        Logger.Info("Disposing old");
                        cvContext.Dispose();
                        cvContext = null;
                    }

                    if (cvContext == null)
                    {
                        Logger.Info("Creating new context " + screenshot.Width + " " + screenshot.Height);
                        cvContext = new CvContext(screenshot.Width, screenshot.Height);
                    }

                    if (Settings.IsHololens)
                    {
                        cvContext.SetImageDataFlipY(screenshot.Bytes.Array, screenshot.Bpp);
                    }
                    else
                    {
                        Logger.Info("Setting data");
                        cvContext.SetImageData(screenshot.Bytes.Array, screenshot.Bpp);
                    }


                    corners.Clear();
                    Logger.Info("Searching for markers: " + enableQr + " " + enableAruco);
                    if (EnableQr && cvContext.DetectQrMarkers() != 0)
                    {
                        Logger.Info("Found QR");
                        corners.AddRange(cvContext.GetDetectedQrCorners());
                    }

                    if (EnableAruco && cvContext.DetectArucoMarkers() != 0)
                    {
                        Logger.Info("Found Aruco");
                        corners.AddRange(cvContext.GetDetectedArucoCorners());
                    }

                    if (corners.Count == 0)
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
                    await Settings.ScreenshotManager.StartAsync(1920, 1080, false).AwaitNoThrow(this);
                }
            }
            else
            {
                if (Settings.IsHololens)
                {
                    await Settings.ScreenshotManager.StopAsync().AwaitNoThrow(this);
                }
            }
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            MarkerDetected = null;
            task.WaitNoThrow(2000, this);
        }

        public override string ToString()
        {
            return "[MarkerDetector]";
        }
    }
}