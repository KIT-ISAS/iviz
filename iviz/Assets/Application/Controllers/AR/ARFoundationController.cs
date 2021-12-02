#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Controllers
{
    public sealed class ARFoundationController : ARController
    {
        const float AnchorPauseTimeInSec = 2;

        static AnchorToggleButton ARSet => ModuleListPanel.Instance.AnchorCanvasPanel.ARSet;
        static GameObject ARInfoPanel => ModuleListPanel.Instance.AnchorCanvasPanel.ARInfoPanel;

        [SerializeField] Camera? arCamera = null;
        [SerializeField] ARSession? arSession = null;
        [SerializeField] ARSessionOrigin? arSessionOrigin = null;
        [SerializeField] Light? arLight = null;
        [SerializeField] ARCameraFovDisplay? fovDisplay = null;
        [SerializeField] MeshFilter? meshPrefab = null;
        [SerializeField] ARCameraManager? cameraManager = null;
        [SerializeField] AROcclusionManager? occlusionManager = null;
        [SerializeField] ARPlaneManager? planeManager = null;
        [SerializeField] ARRaycastManager? raycaster = null;
        [SerializeField] ARAnchorManager? anchorManager = null;

        readonly CancellationTokenSource tokenSource = new();

        bool setupModeEnabled = true;
        int defaultCullingMask;
        uint colorSeq, depthSeq;
        float? lastAnchorMoved;
        float lastScreenCapture;

        AxisFrameResource? setupModeFrame;
        Camera? mainCamera;
        ARAnchorResource? worldAnchor;

        Camera ARCamera => arCamera.AssertNotNull(nameof(arCamera));
        Light ARLight => arLight.AssertNotNull(nameof(arLight));
        ARSessionOrigin ARSessionOrigin => arSessionOrigin.AssertNotNull(nameof(arSessionOrigin));
        ARSession Session => arSession.AssertNotNull(nameof(arSession));
        ARCameraManager CameraManager => cameraManager.AssertNotNull(nameof(cameraManager));
        AROcclusionManager OcclusionManager => occlusionManager.AssertNotNull(nameof(occlusionManager));
        ARPlaneManager PlaneManager => planeManager.AssertNotNull(nameof(planeManager));
        ARRaycastManager Raycaster => raycaster.AssertNotNull(nameof(raycaster));
        ARAnchorManager AnchorManager => anchorManager.AssertNotNull(nameof(anchorManager));

        AxisFrameResource SetupModeFrame => setupModeFrame != null
            ? setupModeFrame
            : (setupModeFrame = ResourcePool.RentDisplay<AxisFrameResource>(ARCamera.transform));

        Camera MainCamera => mainCamera != null
            ? mainCamera
            : (mainCamera = Settings.FindMainCamera().GetComponent<Camera>());

        public string Description
        {
            get
            {
                if (Session.subsystem == null)
                {
                    return "<b>[No AR Subsystem]</b>";
                }

                string trackingState;
                switch (Session.subsystem.trackingState)
                {
                    case TrackingState.Limited:
                        trackingState = "Tracking: Limited";
                        break;
                    case TrackingState.None:
                        trackingState = "Tracking: None";
                        break;
                    default:
                        trackingState = "Tracking: OK";
                        break;
                }

                string numPlanes =
                    PlaneManager.trackables.count == 0
                        ? "Planes: None"
                        : "Planes: " + PlaneManager.trackables.count;

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
                        OcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Disabled;
                        OcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Disabled;
                        OcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Disabled;
                        break;
                    case OcclusionQualityType.Fast:
                        OcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Fastest;
                        OcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Fastest;
                        OcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Fastest;
                        break;
                    case OcclusionQualityType.Medium:
                        OcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Medium;
                        OcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Fastest;
                        OcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Medium;
                        break;
                    case OcclusionQualityType.Best:
                        OcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Best;
                        OcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Best;
                        OcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Best;
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
                MainCamera.gameObject.SetActive(!value);
                ARCamera.enabled = value;
                ARLight.gameObject.SetActive(value);
                ARSet.Visible = SetupModeEnabled;
                Settings.MainCamera = value ? ARCamera : MainCamera;
                Canvas.worldCamera = Settings.MainCamera;

                if (value)
                {
                    RenderSettings.ambientMode = AmbientMode.Flat;
                }
                else
                {
                    RenderSettings.ambientMode = AmbientMode.Trilight;
                    Settings.SettingsManager.BackgroundColor = Settings.SettingsManager.BackgroundColor;
                }
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
                    ARCamera.cullingMask = (1 << LayerType.ARSetupMode) | (1 << LayerType.UI);
                    ARInfoPanel.SetActive(true);
                }
                else
                {
                    ARCamera.cullingMask = defaultCullingMask;
                    var (sourcePosition, sourceRotation) = SetupModeFrame.transform.AsPose();
                    Pose pose = new Pose
                    {
                        position = sourcePosition,
                        rotation = Quaternion.Euler(0, sourceRotation.eulerAngles.y - 90, 0)
                    };

                    //Debug.Log(sourcePose);
                    SetWorldPose(pose, RootMover.Setup);
                }
            }
        }

        public bool EnableAutoFocus
        {
            get => CameraManager.autoFocusRequested;
            set => CameraManager.autoFocusRequested = value;
        }

        public override bool EnableMeshing
        {
            get => base.EnableMeshing;
            set
            {
                base.EnableMeshing = value;
                if (value)
                {
                    MeshManager = ARCamera.gameObject.EnsureComponent<ARMeshManager>();
                    MeshManager.meshPrefab = meshPrefab;
                    MeshManager.normals = false;
                }
                else if (MeshManager != null)
                {
                    MeshManager.DestroyAllMeshes();
                    Destroy(MeshManager);
                    MeshManager = null;
                }
            }
        }

        public bool ProvidesMesh { get; private set; }
        public bool ProvidesOcclusion { get; private set; }

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

        protected override void Awake()
        {
            base.Awake();
            Instance = this;

            Settings.ARCamera = ARCamera;

            MeshManager = ARCamera.gameObject.EnsureComponent<ARMeshManager>();

            lastAnchorMoved = Time.time;

            defaultCullingMask = ARCamera.cullingMask;

            CameraManager.frameReceived += args => ProcessLights(args.lightEstimation);

            var subsystems = new List<ISubsystem>();
            SubsystemManager.GetInstances(subsystems);
            ProvidesMesh = subsystems.Any(s => s is XRMeshSubsystem);
            ProvidesOcclusion = subsystems.Any(s => s is XROcclusionSubsystem);

            Config = new ARConfiguration();

            //SetupModeFrame = ResourcePool.RentDisplay<AxisFrameResource>(ArCamera.transform);
            SetupModeFrame.Layer = LayerType.ARSetupMode;

            SetupModeEnabled = true;
            SetupModeFrame.AxisLength = 0.5f * TfListener.Instance.FrameSize;

            ARSet.Clicked += ArSetOnClicked;
            ARSet.Visible = false;
            ARInfoPanel.SetActive(true);

            WorldPoseChanged += OnWorldPoseChanged;

            Settings.ScreenCaptureManager =
                new ARFoundationScreenCaptureManager(CameraManager, ARCamera.transform, OcclusionManager);

            GuiInputModule.Instance.LongClick += TriggerPulse;

            RaiseARActiveChanged();
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
            Session.Reset();
            ResetSetupMode();

            await Task.Delay(200);
            EnableMeshing = meshingEnabled;
            OcclusionQuality = occlusionQuality;
        }

        bool IsSamePose(in Pose b)
        {
            return Vector3.Distance(WorldPosition, b.position) < 0.001f &&
                   Mathf.Abs(WorldAngle - AngleFromPose(b)) < 0.1f;
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
                AnchorManager.RemoveAnchor(worldAnchor.Anchor);
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

            var cameraTransform = ARCamera.transform;
            SetupModeFrame.Transform.rotation = Quaternion.Euler(0, 90 + cameraTransform.rotation.eulerAngles.y, 0);
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (TryGetRaycastHit(ray, out Pose hit))
            {
                SetupModeFrame.Transform.position = hit.position;
                SetupModeFrame.Tint = Color.white;
                ARSet.Visible = true;
                ARInfoPanel.SetActive(false);
            }
            else
            {
                SetupModeFrame.Transform.localPosition = new Vector3(0, 0, 0.5f);
                SetupModeFrame.Tint = Color.white.WithAlpha(0.3f);
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
                    Pose pose = WorldPose.WithPosition(hit.position);
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
                var anchor = AnchorManager.AddAnchor(WorldPose).GetComponent<ARAnchorResource>();
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
            if (ARSessionOrigin.trackablesParent == null)
            {
                // not initialized yet!
                hit = default;
                return false;
            }

            var results = new List<ARRaycastHit>();
            Raycaster.Raycast(ray, results);
            results.RemoveAll(rayHit => (rayHit.hitType & TrackableType.PlaneWithinPolygon) == 0);

            switch (results.Count)
            {
                case 0:
                    hit = default;
                    return false;
                case 1:
                    hit = results[0].pose;
                    return true;
                default:
                    Vector3 origin = ray.origin;
                    hit = results.Select(rayHit => ((rayHit.pose.position - origin).sqrMagnitude, rayHit)).Min().rayHit
                        .pose;
                    return true;
            }
        }


        public bool TryGetRaycastHits(in Ray ray, [NotNullWhen(true)] out List<ARRaycastHit>? hits)
        {
            if (ARSessionOrigin.trackablesParent == null)
            {
                // not initialized yet!
                hits = null;
                return false;
            }

            hits = new List<ARRaycastHit>();
            Raycaster.Raycast(ray, hits);
            hits.RemoveAll(rayHit => (rayHit.hitType & TrackableType.PlaneWithinPolygon) == 0);
            hits.Sort((a, b) => a.distance.CompareTo(b.distance));

            return hits.Count != 0;
        }


        void ProcessLights(in ARLightEstimationData lightEstimation)
        {
            // ARKit back camera only appears to have these two
            if (lightEstimation.averageColorTemperature.HasValue && lightEstimation.averageBrightness.HasValue)
            {
                var color = Mathf.CorrelatedColorTemperatureToRGB(lightEstimation.averageColorTemperature.Value);
                float intensity = lightEstimation.averageBrightness.Value;

                ARLight.color = color;
                ARLight.intensity = intensity;

                // ambient light is only to prevent complete blacks
                RenderSettings.ambientMode = AmbientMode.Flat;
                RenderSettings.ambientLight = color;
                RenderSettings.ambientIntensity = 0.1f;
            }

            /*
            if (lightEstimation.colorCorrection.HasValue)
            {
                arLight.color = lightEstimation.colorCorrection.Value;
                Debug.Log("Color correction: " + lightEstimation.colorCorrection);
            }
            */

            // ARKit front camera (unused)
            if (lightEstimation.mainLightDirection.HasValue)
            {
                ARLight.transform.rotation = Quaternion.LookRotation(lightEstimation.mainLightDirection.Value);
            }

            // ARKit front camera (unused)
            if (lightEstimation.mainLightColor.HasValue)
            {
                ARLight.color = lightEstimation.mainLightColor.Value;
            }

            // Android only I think
            if (lightEstimation.ambientSphericalHarmonics.HasValue)
            {
                var sphericalHarmonics = lightEstimation.ambientSphericalHarmonics;
                RenderSettings.ambientMode = AmbientMode.Skybox;
                RenderSettings.ambientProbe = sphericalHarmonics.Value;
            }
        }

        void TriggerPulse(ClickInfo clickInfo)
        {
            if (Visible && clickInfo.TryGetARRaycastResults(out var results))
            {
                ARMeshLines.TriggerPulse(results[0].Position);
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
                    : (ValueTask<Screenshot?>?)null;

                var depthTask = shouldPublishDepth
                    ? captureManager.CaptureDepthAsync(captureReuseTimeInMs, token).AwaitNoThrow(this)
                    : (ValueTask<Screenshot?>?)null;

                var confidenceTask = shouldPublishConfidence
                    ? captureManager.CaptureDepthConfidenceAsync(captureReuseTimeInMs, token).AwaitNoThrow(this)
                    : (ValueTask<Screenshot?>?)null;

                var color = colorTask != null ? await colorTask.Value : null;
                var depth = depthTask != null ? await depthTask.Value : null;
                var confidence = confidenceTask != null ? await confidenceTask.Value : null;

                var frame = TfListener.GetOrCreateFrame(CameraFrameId);
                frame.ForceInvisible = true;
                string frameId = frame.Id;

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
                    byte[] bytes = confidence.Bytes;
                    for (int v = 0; v < confidence.Height * confidence.Width; v++)
                    {
                        bytes[v] = (byte)(bytes[v] * 127);
                    }

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
                var relativePose = TfListener.RelativePoseToFixedFrame(absoluteArCameraPose).Unity2RosTransform();
                TfListener.Publish(frameId, relativePose.ToCameraFrame());
            }
            catch (Exception e)
            {
                RosLogger.Error("CaptureScreenForPublish failed", e);
            }
        }

        public override void StopController()
        {
            base.StopController();
            tokenSource.Cancel();
            ARSet.Clicked -= ArSetOnClicked;
            WorldPoseChanged -= OnWorldPoseChanged;
            GuiInputModule.Instance.LongClick -= TriggerPulse;

            if (fovDisplay != null)
            {
                Destroy(fovDisplay.gameObject);
            }

            ARSet.Visible = false;
            ARInfoPanel.SetActive(false);

            Settings.ARCamera = null;
            Settings.ScreenCaptureManager = null;
        }
    }
}