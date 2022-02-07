#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Controllers
{
    public sealed class ARFoundationController : ARController
    {
        const float AnchorPauseTimeInSec = 2;

        static AnchorToggleButton ARSet => ModuleListPanel.Instance.AnchorCanvasPanel.ARSet;
        static GameObject ARInfoPanel => ModuleListPanel.Instance.AnchorCanvasPanel.ARInfoPanel;

        readonly ARContents ar;
        readonly CancellationTokenSource tokenSource = new();
        readonly int defaultCullingMask;
        readonly AxisFrameDisplay setupModeFrame;
        readonly Camera virtualCamera;
        readonly Canvas canvas;

        bool setupModeEnabled = true;
        uint colorSeq, depthSeq;
        float? lastAnchorMoved;
        float lastScreenCapture;

        ARAnchorResource? worldAnchor;

        public ARMeshManager? MeshManager { get; private set; }
        public ARAnchorManager AnchorManager => ar.AnchorManager;

        public string Description
        {
            get
            {
                if (ar.Session.subsystem == null)
                {
                    return "<b>[No AR Subsystem]</b>";
                }

                string trackingState = ar.Session.subsystem.trackingState switch
                {
                    TrackingState.Limited => "Tracking: Limited",
                    TrackingState.None => "Tracking: None",
                    _ => "Tracking: OK"
                };

                string numPlanes =
                    ar.PlaneManager.trackables.count == 0
                        ? "Planes: None"
                        : "Planes: " + ar.PlaneManager.trackables.count;

                return $"<b>{trackingState}</b>\n{numPlanes}";
            }
        }

        public override OcclusionQualityType OcclusionQuality
        {
            get => base.OcclusionQuality;
            set
            {
                base.OcclusionQuality = value;
                switch (value)
                {
                    case OcclusionQualityType.Off:
                        ar.OcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Disabled;
                        ar.OcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Disabled;
                        ar.OcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Disabled;
                        break;
                    case OcclusionQualityType.Fast:
                        ar.OcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Fastest;
                        ar.OcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Fastest;
                        ar.OcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Fastest;
                        break;
                    case OcclusionQualityType.Medium:
                        ar.OcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Medium;
                        ar.OcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Fastest;
                        ar.OcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Medium;
                        break;
                    case OcclusionQualityType.Best:
                        ar.OcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Best;
                        ar.OcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Best;
                        ar.OcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Best;
                        break;
                }
            }
        }


        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;

                Settings.MainCamera = value ? ar.Camera : virtualCamera;
                virtualCamera.gameObject.SetActive(!value);
                ar.Camera.enabled = value;
                ar.ARLight.gameObject.SetActive(value);
                ARSet.Visible = SetupModeEnabled;
                canvas.worldCamera = Settings.MainCamera;

                if (value)
                {
                    RenderSettings.ambientMode = AmbientMode.Flat;
                }
                else
                {
                    RenderSettings.ambientMode = AmbientMode.Trilight;
                    Settings.SettingsManager.BackgroundColor = Settings.SettingsManager.BackgroundColor;
                }

                RaiseARCameraViewChanged(value);
            }
        }

        public bool SetupModeEnabled
        {
            get => setupModeEnabled;
            private set
            {
                setupModeEnabled = value;
                ARSet.Visible = value;
                ARSet.State = value;

                if (value)
                {
                    ar.Camera.cullingMask = (1 << LayerType.ARSetupMode) | (1 << LayerType.UI);
                    ARInfoPanel.SetActive(true);
                }
                else
                {
                    ar.Camera.cullingMask = defaultCullingMask;
                    var (sourcePosition, sourceRotation) = setupModeFrame.transform.AsPose();

                    Pose pose;
                    pose.position = sourcePosition;
                    pose.rotation = Quaternion.Euler(0, sourceRotation.eulerAngles.y - 90, 0);
                    SetWorldPose(pose, RootMover.Setup);
                }
            }
        }

        public bool EnableAutoFocus
        {
            get => ar.CameraManager.autoFocusRequested;
            set => ar.CameraManager.autoFocusRequested = value;
        }

        public override bool EnableMeshing
        {
            get => base.EnableMeshing;
            set
            {
                base.EnableMeshing = value;
                if (value)
                {
                    MeshManager = ar.Camera.gameObject.EnsureComponent<ARMeshManager>();
                    MeshManager.meshPrefab = ar.MeshPrefab;
                    MeshManager.normals = false;
                }
                else if (MeshManager != null)
                {
                    MeshManager.DestroyAllMeshes();
                    UnityEngine.Object.Destroy(MeshManager);
                    MeshManager = null;
                }
            }
        }

        public override bool PinRootMarker
        {
            get => base.PinRootMarker;
            set
            {
                if (value && !base.PinRootMarker)
                {
                    ResetAnchor(true);
                }

                base.PinRootMarker = value;
            }
        }

        public ARFoundationController(ARConfiguration? config)
        {
            Instance = this;

            ar = UnityEngine.Object.Instantiate(Resource.Extras.AppAssetHolder.ARPrefab).GetComponent<ARContents>();
            if (ar == null)
            {
                throw new MissingAssetFieldException("AR object does not have contents");
            }

            Settings.ARCamera = ar.Camera;

            canvas = GameObject.Find("Canvas").GetComponent<Canvas>().AssertNotNull(nameof(canvas));
            virtualCamera = Settings.FindMainCamera().GetComponent<Camera>();

            MeshManager = ar.Camera.gameObject.GetComponent<ARMeshManager>();

            lastAnchorMoved = Time.time;

            defaultCullingMask = ar.Camera.cullingMask;

            ar.CameraManager.frameReceived += args => ProcessLights(args.lightEstimation);

            /*
            var subsystems = new List<ISubsystem>();
            SubsystemManager.GetInstances(subsystems);
            ProvidesMesh = subsystems.Any(s => s is XRMeshSubsystem);
            ProvidesOcclusion = subsystems.Any(s => s is XROcclusionSubsystem);
            */

            Config = config ?? new ARConfiguration();

            setupModeFrame = ResourcePool.RentDisplay<AxisFrameDisplay>(ar.Camera.transform);
            setupModeFrame.Layer = LayerType.ARSetupMode;
            setupModeFrame.AxisLength = 0.5f * TfListener.Instance.FrameSize;
            SetupModeEnabled = true;

            ARSet.Clicked += ArSetOnClicked;
            ARSet.Visible = false;
            ARInfoPanel.SetActive(true);

            WorldPoseChanged += OnWorldPoseChanged;
            GuiInputModule.Instance.LongClick += TriggerPulse;

            Settings.ScreenCaptureManager =
                new ARFoundationScreenCaptureManager(ar.CameraManager, ar.Camera.transform, ar.OcclusionManager);

            RaiseARStateChanged();
        }

        void ArSetOnClicked()
        {
            SetupModeEnabled = false;
        }

        public void ResetSetupMode()
        {
            SetupModeEnabled = true;
        }

        public async void ResetSession()
        {
            bool meshingEnabled = EnableMeshing;
            var occlusionQuality = OcclusionQuality;
            EnableMeshing = false;
            ar.Session.Reset();
            ResetSetupMode();

            await Task.Delay(200);
            EnableMeshing = meshingEnabled;
            OcclusionQuality = occlusionQuality;
        }

        bool IsSamePose(in Pose b)
        {
            return Vector3.Distance(WorldPosition, b.position) < 0.001f &&
                   Math.Abs(WorldAngle - AngleFromPose(b)) < 0.1f;
        }

        void OnWorldPoseChanged(RootMover mover)
        {
            if (mover == RootMover.Anchor ||
                worldAnchor == null ||
                IsSamePose(worldAnchor.Pose))
            {
                return;
            }

            ResetAnchor(false);
        }

        void ResetAnchor(bool recreateNow)
        {
            if (worldAnchor != null)
            {
                ar.AnchorManager.RemoveAnchor(worldAnchor.Anchor);
                worldAnchor = null;
            }

            lastAnchorMoved = recreateNow ? Time.time - AnchorPauseTimeInSec : Time.time;
        }

        protected override void Update()
        {
            base.Update();

            ProcessSetupMode();
            ProcessAnchorMoved();
            ProcessCapture();
        }

        void ProcessSetupMode()
        {
            if (!SetupModeEnabled)
            {
                return;
            }

            var cameraTransform = ar.Camera.transform;
            setupModeFrame.Transform.rotation = Quaternion.Euler(0, 90 + cameraTransform.rotation.eulerAngles.y, 0);
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (TryGetRaycastHit(ray, out Pose hit))
            {
                setupModeFrame.Transform.position = hit.position;
                setupModeFrame.Tint = Color.white;
                ARSet.Visible = true;
                ARInfoPanel.SetActive(false);
            }
            else
            {
                setupModeFrame.Transform.localPosition = new Vector3(0, 0, 0.5f);
                setupModeFrame.Tint = Color.white.WithAlpha(0.3f);
                ARSet.Visible = false;
            }
        }

        void ProcessAnchorMoved()
        {
            if (lastAnchorMoved == null || Time.time - lastAnchorMoved.Value < AnchorPauseTimeInSec)
            {
                return;
            }

            lastAnchorMoved = null;

            if (PinRootMarker)
            {
                const float maxDistanceAbovePlane = 0.05f;

                var origin = WorldPosition + maxDistanceAbovePlane * Vector3.up;
                var ray = new Ray(origin, Vector3.down);
                if (TryGetRaycastHit(ray, out Pose hit))
                {
                    var pose = WorldPose.WithPosition(hit.position);
                    InitializeWorldAnchor();
                    SetWorldPose(pose, RootMover.Anchor);
                }
                else
                {
                    lastAnchorMoved = Time.time + AnchorPauseTimeInSec;
                }
            }
            else
            {
                InitializeWorldAnchor();
            }
        }

        void ProcessCapture()
        {
            float captureFps = PublicationFrequency switch
            {
                PublicationFrequency.Fps5 => 5,
                PublicationFrequency.Fps10 => 10,
                PublicationFrequency.Fps15 => 15,
                PublicationFrequency.Fps20 => 20,
                PublicationFrequency.Fps30 => 30,
                _ => 0
            };

            if (captureFps == 0 || GameThread.GameTime - lastScreenCapture < 1 / captureFps)
            {
                return;
            }

            lastScreenCapture = GameThread.GameTime;
            CaptureScreenForPublish(tokenSource.Token);
        }

        void InitializeWorldAnchor()
        {
            try
            {
                var anchor = ar.AnchorManager.AddAnchor(WorldPose).GetComponent<ARAnchorResource>();
                anchor.Moved += OnWorldAnchorMoved;
                worldAnchor = anchor;
            }
            catch (InvalidOperationException e)
            {
                RosLogger.Error("Failed to initialize AR world anchor:", e);
            }
        }

        void OnWorldAnchorMoved(Pose newPose)
        {
            SetWorldPose(newPose, RootMover.Anchor);
        }

        bool TryGetRaycastHit(in Ray ray, out Pose hit)
        {
            if (ar.ARSessionOrigin.trackablesParent == null)
            {
                // not initialized yet!
                hit = default;
                return false;
            }

            var results = new List<ARRaycastHit>();
            ar.Raycaster.Raycast(ray, results);
            results.RemoveAll(static rayHit => (rayHit.hitType & TrackableType.PlaneWithinPolygon) == 0);

            switch (results.Count)
            {
                case 0:
                    hit = default;
                    return false;
                case 1:
                    hit = results[0].pose;
                    return true;
                default:
                    Pose minPose = default;
                    float minDistance = float.MaxValue;
                    foreach (var rayHit in results)
                    {
                        float currentDistance = (rayHit.pose.position - ray.origin).sqrMagnitude;
                        if (currentDistance < minDistance)
                        {
                            (minDistance, minPose) = (currentDistance, rayHit.pose);
                        }
                    }

                    hit = minPose;
                    return true;
            }
        }


        public bool TryGetRaycastHits(in Ray ray, [NotNullWhen(true)] out List<ARRaycastHit>? hits)
        {
            if (ar.ARSessionOrigin.trackablesParent == null)
            {
                // not initialized yet!
                hits = null;
                return false;
            }

            hits = new List<ARRaycastHit>();
            ar.Raycaster.Raycast(ray, hits);
            hits.RemoveAll(rayHit => (rayHit.hitType & TrackableType.PlaneWithinPolygon) == 0);
            hits.Sort((a, b) => a.distance.CompareTo(b.distance));

            return hits.Count != 0;
        }


        void ProcessLights(in ARLightEstimationData lightEstimation)
        {
            // ARKit back camera only appears to have these two
            if (lightEstimation.averageColorTemperature is { } averageColorTemperature &&
                lightEstimation.averageBrightness is { } averageBrightness)
            {
                var color = Mathf.CorrelatedColorTemperatureToRGB(averageColorTemperature);

                ar.ARLight.color = color;
                ar.ARLight.intensity = averageBrightness;

                // ambient light is only to prevent complete blacks
                RenderSettings.ambientMode = AmbientMode.Flat;
                RenderSettings.ambientLight = color;
                RenderSettings.ambientIntensity = 0.1f;
            }

            // ARKit front camera (unused)
            if (lightEstimation.mainLightDirection is { } mainLightDirection)
            {
                ar.ARLight.transform.rotation = Quaternion.LookRotation(mainLightDirection);
            }

            // ARKit front camera (unused)
            if (lightEstimation.mainLightColor is { } mainLightColor)
            {
                ar.ARLight.color = mainLightColor;
            }

            // Android only I think
            if (lightEstimation.ambientSphericalHarmonics is { } ambientSphericalHarmonics)
            {
                RenderSettings.ambientMode = AmbientMode.Skybox;
                RenderSettings.ambientProbe = ambientSphericalHarmonics;
            }
        }

        void TriggerPulse(ClickHitInfo clickHitInfo)
        {
            if (Visible && clickHitInfo.TryGetARRaycastResults(out var results))
            {
                ARController.TriggerPulse(results[0].Position);
            }
        }

        async void CaptureScreenForPublish(CancellationToken token)
        {
            if (ColorSender == null
                || DepthSender == null
                || DepthConfidenceSender == null
                || ColorInfoSender == null
                || DepthInfoSender == null)
            {
                return;
            }

            try
            {
                var captureManager = (ARFoundationScreenCaptureManager?)Settings.ScreenCaptureManager;

                bool shouldPublishColor = ColorSender.NumSubscribers != 0;
                bool shouldPublishDepth = DepthSender.NumSubscribers != 0;
                bool shouldPublishConfidence = DepthConfidenceSender.NumSubscribers != 0;
                if (captureManager == null || token.IsCancellationRequested ||
                    (!shouldPublishColor && !shouldPublishDepth && !shouldPublishConfidence))
                {
                    return;
                }

                const int captureReuseTimeInMs = 0;

                var colorTask = shouldPublishColor
                    ? captureManager.CaptureColorAsync(captureReuseTimeInMs, token).AwaitNoThrow(this)
                    : null;

                var depthTask = shouldPublishDepth
                    ? captureManager.CaptureDepthAsync(captureReuseTimeInMs, token).AwaitNoThrow(this)
                    : null;

                var confidenceTask = shouldPublishConfidence
                    ? captureManager.CaptureDepthConfidenceAsync(captureReuseTimeInMs, token).AwaitNoThrow(this)
                    : null;

                var color = colorTask != null ? await colorTask : null;
                var depth = depthTask != null ? await depthTask : null;
                var confidence = confidenceTask != null ? await confidenceTask : null;


                string frameId = TfListener.ResolveFrameId(CameraFrameId);

                if (color != null)
                {
                    ColorSender.Publish(color.CreateImageMessage(frameId, colorSeq));
                    ColorInfoSender.Publish(color.CreateCameraInfoMessage(frameId, colorSeq++));
                }

                if (depth != null)
                {
                    DepthSender.Publish(depth.CreateImageMessage(frameId, depthSeq));
                }

                if (confidence != null)
                {
                    void Multiply(Span<byte> bytes)
                    {
                        foreach (ref byte b in bytes)
                        {
                            b =  (byte)(b * 127);
                        }
                    }
                    
                    Multiply(confidence.Bytes);
                    
                    /*
                    var bytes = confidence.Bytes;
                    for (int v = 0; v < confidence.Height * confidence.Width; v++)
                    {
                        bytes[v] = (byte)(bytes[v] * 127);
                    }
                    */

                    DepthConfidenceSender.Publish(confidence.CreateImageMessage(frameId, depthSeq));
                }

                Screenshot? anyDepth = depth ?? confidence;
                if (anyDepth != null)
                {
                    DepthInfoSender.Publish(anyDepth.CreateCameraInfoMessage(frameId, depthSeq++));
                }

                var anyPose = (color ?? depth)?.CameraPose;
                if (anyPose == null)
                {
                    return;
                }

                var absoluteArCameraPose = ARPoseToUnity(anyPose.Value);
                cameraFrame.LocalPose = TfListener.RelativeToFixedFrame(absoluteArCameraPose);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: CaptureScreenForPublish failed", e);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            tokenSource.Cancel();
            ARSet.Clicked -= ArSetOnClicked;
            WorldPoseChanged -= OnWorldPoseChanged;
            GuiInputModule.Instance.LongClick -= TriggerPulse;

            ARSet.Visible = false;
            ARInfoPanel.SetActive(false);

            Settings.ARCamera = null;
            Settings.ScreenCaptureManager = null;

            if (ar.FovDisplay != null)
            {
                UnityEngine.Object.Destroy(ar.FovDisplay.gameObject);
            }

            UnityEngine.Object.Destroy(ar.gameObject);
        }
    }
}