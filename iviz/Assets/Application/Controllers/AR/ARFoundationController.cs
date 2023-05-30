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
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    public sealed class ARFoundationController : ARController
    {
        const float AnchorPauseTimeInSec = 2;

        static AnchorToggleButton ARStartSession => ModuleListPanel.Instance.AnchorCanvasPanel.ARStartSession;
        static GameObject ARMoveDevicePanel => ModuleListPanel.Instance.AnchorCanvasPanel.ARInfoPanel;

        readonly ARContents arContents;
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

        public static event Action<bool>? SetupModeChanged;
        
        public ARMeshManager? MeshManager { get; private set; }
        public ARAnchorManager AnchorManager => arContents.AnchorManager;

        public string Description
        {
            get
            {
                if (arContents.Session.subsystem == null)
                {
                    return "<b>[No AR Subsystem]</b>";
                }

                string trackingState = arContents.Session.subsystem.trackingState switch
                {
                    TrackingState.Limited => "Tracking: Limited",
                    TrackingState.None => "Tracking: None",
                    _ => "Tracking: OK"
                };

                string numPlanes =
                    arContents.PlaneManager.trackables.count == 0
                        ? "Planes: None"
                        : "Planes: " + arContents.PlaneManager.trackables.count;

                return $"<b>{trackingState}</b>\n{numPlanes}";
            }
        }

        public override OcclusionQualityType OcclusionQuality
        {
            get => base.OcclusionQuality;
            set
            {
                base.OcclusionQuality = value;
                var manager = arContents.OcclusionManager;
                switch (value)
                {
                    case OcclusionQualityType.Off:
                        manager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Disabled;
                        manager.requestedHumanDepthMode = HumanSegmentationDepthMode.Disabled;
                        manager.requestedHumanStencilMode = HumanSegmentationStencilMode.Disabled;
                        break;
                    case OcclusionQualityType.Fast:
                        manager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Fastest;
                        manager.requestedHumanDepthMode = HumanSegmentationDepthMode.Fastest;
                        manager.requestedHumanStencilMode = HumanSegmentationStencilMode.Fastest;
                        break;
                    case OcclusionQualityType.Medium:
                        manager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Medium;
                        manager.requestedHumanDepthMode = HumanSegmentationDepthMode.Fastest;
                        manager.requestedHumanStencilMode = HumanSegmentationStencilMode.Medium;
                        break;
                    case OcclusionQualityType.Best:
                        manager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Best;
                        manager.requestedHumanDepthMode = HumanSegmentationDepthMode.Best;
                        manager.requestedHumanStencilMode = HumanSegmentationStencilMode.Best;
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

                Settings.MainCamera = value ? arContents.Camera : virtualCamera;
                virtualCamera.gameObject.SetActive(!value);
                arContents.Camera.enabled = value;
                arContents.ARLight.gameObject.SetActive(value);
                ARStartSession.Visible = SetupModeEnabled;
                canvas.worldCamera = Settings.MainCamera;

                if (value)
                {
                    RenderSettings.ambientMode = AmbientMode.Flat;
                }
                else
                {
                    RenderSettings.ambientMode = AmbientMode.Trilight;
                    GuiInputModule.Instance.BackgroundColor = GuiInputModule.Instance.BackgroundColor;
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
                ARStartSession.Visible = value;
                ARStartSession.State = value;

                if (value)
                {
                    arContents.Camera.cullingMask = (1 << LayerType.ARSetupMode) | (1 << LayerType.UI);
                    ARMoveDevicePanel.SetActive(true);
                    markerDetector.DelayBetweenCapturesInMs = 1000;
                }
                else
                {
                    arContents.Camera.cullingMask = defaultCullingMask;
                    var (sourcePosition, sourceRotation) = setupModeFrame.transform.AsPose();

                    Pose pose;
                    pose.position = sourcePosition;
                    pose.rotation = Quaternion.Euler(0, sourceRotation.eulerAngles.y - 90, 0);
                    SetWorldPose(pose, RootMover.Setup);

                    markerDetector.DelayBetweenCapturesInMs = 3000;
                }

                SetupModeChanged.TryRaise(value, this);
            }
        }

        public bool EnableAutoFocus
        {
            get => arContents.CameraManager.autoFocusRequested;
            set => arContents.CameraManager.autoFocusRequested = value;
        }

        public override bool EnableMeshing
        {
            get => base.EnableMeshing;
            set
            {
                if (!EnableMeshingSubsystem) return;
                
                base.EnableMeshing = value;
                if (value)
                {
                    MeshManager = arContents.Camera.gameObject.TryAddComponent<ARMeshManager>();
                    MeshManager.meshPrefab = arContents.MeshPrefab;
                    MeshManager.normals = false;
                }
                else if (MeshManager != null)
                {
                    MeshManager.DestroyAllMeshes();
                    Object.Destroy(MeshManager);
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

            var arObject = Object.Instantiate(ResourcePool.AppAssetHolder.ARPrefab);
            if (!arObject.TryGetComponent(out arContents))
            {
                ThrowHelper.ThrowMissingAssetField("AR object does not have contents");
            }

            Settings.ARCamera = arContents.Camera;
            arContents.CameraManager.frameReceived += args => ProcessLights(args.lightEstimation);

            canvas = GameObject.Find("Canvas").AssertHasComponent<Canvas>(nameof(canvas));
            virtualCamera = Settings.FindMainCamera().AssertHasComponent<Camera>(nameof(virtualCamera));

            lastAnchorMoved = Time.time;

            defaultCullingMask = arContents.Camera.cullingMask;

            var subsystems = new List<ISubsystem>();
            SubsystemManager.GetInstances(subsystems);

            ProvidesMesh = EnableMeshingSubsystem && subsystems.Any(s => s is XRMeshSubsystem);
            ProvidesOcclusion = subsystems.Any(s => s is XROcclusionSubsystem);

            Config = config ?? new ARConfiguration();

            setupModeFrame = ResourcePool.RentDisplay<AxisFrameDisplay>(arContents.Camera.transform);
            setupModeFrame.Layer = LayerType.ARSetupMode;
            setupModeFrame.AxisLength = 0.5f * TfModule.Instance.FrameSize;
            SetupModeEnabled = true;

            ARStartSession.Clicked += ArStartSessionOnClicked;
            ARStartSession.Visible = false;

            ARMoveDevicePanel.SetActive(true);

            WorldPoseChanged += OnWorldPoseChanged;
            GuiInputModule.Instance.LongClick += TriggerPulse;

            Settings.ScreenCaptureManager = new ARScreenCaptureManager(
                arContents.CameraManager, arContents.Camera.transform, arContents.OcclusionManager);

            RaiseARStateChanged();

            PostInitialize();
        }

        void ArStartSessionOnClicked()
        {
            SetupModeEnabled = false;
        }

        public void ResetSetupMode()
        {
            SetupModeEnabled = true;
        }

        public async ValueTask ResetSessionAsync()
        {
            bool meshingEnabled = EnableMeshing;
            var occlusionQuality = OcclusionQuality;
            EnableMeshing = false;
            arContents.Session.Reset();
            ResetSetupMode();

            await Task.Delay(200);
            EnableMeshing = meshingEnabled;
            OcclusionQuality = occlusionQuality;
        }

        bool IsSamePose(in Pose b)
        {
            return (WorldPosition - b.position).sqrMagnitude < 0.001f * 0.001f &&
                   Mathf.Abs(WorldAngle - AngleFromPose(b)) < 0.1f;
        }

        void OnWorldPoseChanged(RootMover mover)
        {
            if (mover == RootMover.Anchor || worldAnchor == null || IsSamePose(worldAnchor.Pose))
            {
                return;
            }

            ResetAnchor(false);
        }

        void ResetAnchor(bool recreateNow)
        {
            if (worldAnchor != null)
            {
                arContents.AnchorManager.RemoveAnchor(worldAnchor.Anchor);
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

            var cameraTransform = arContents.Camera.transform;
            var worldRotation = Quaternion.Euler(0, 90 + cameraTransform.rotation.eulerAngles.y, 0);
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (TryGetRaycastHit(ray, out Pose hit))
            {
                var projectedPose = markerManager.TryGetMarkerNearby(hit.position, out var markerPose)
                    ? markerPose
                    : new Pose(hit.position, worldRotation);
                setupModeFrame.Transform.SetPose(projectedPose);
                setupModeFrame.Tint = Color.white;
                ARStartSession.Visible = true;
                ARMoveDevicePanel.SetActive(false);
            }
            else
            {
                setupModeFrame.Transform.localPosition = new Vector3(0, 0, 0.5f);
                setupModeFrame.Transform.rotation = worldRotation;
                setupModeFrame.Tint = Color.white.WithAlpha(0.3f);
                ARStartSession.Visible = false;
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
            if (PublicationFrequency is PublicationFrequency.Off)
            {
                return;
            }

            float captureDelay = PublicationFrequency switch
            {
                PublicationFrequency.Fps5 => 1f / 5,
                PublicationFrequency.Fps10 => 1f / 10,
                PublicationFrequency.Fps15 => 1f / 15,
                PublicationFrequency.Fps20 => 1f / 20,
                PublicationFrequency.Fps30 => 1f / 30,
                PublicationFrequency.FpsMax => 0,
                _ => float.MaxValue
            };

            if (GameThread.GameTime - lastScreenCapture < captureDelay)
            {
                return;
            }

            lastScreenCapture = GameThread.GameTime;

            try
            {
                _ = CaptureScreenForPublish(tokenSource.Token);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: {nameof(CaptureScreenForPublish)} failed", e);
            }
        }

        void InitializeWorldAnchor()
        {
            try
            {
                var anchor = arContents.AnchorManager.AddAnchor(WorldPose)
                    .AssertHasComponent<ARAnchorResource>(nameof(arContents.AnchorManager));
                anchor.Moved += OnWorldAnchorMoved;
                worldAnchor = anchor;
            }
            catch (InvalidOperationException e)
            {
                RosLogger.Error("Failed to initialize AR world anchor", e);
            }
        }

        void OnWorldAnchorMoved(Pose newPose)
        {
            SetWorldPose(newPose, RootMover.Anchor);
        }

        bool TryGetRaycastHit(in Ray ray, out Pose hit)
        {
            if (arContents.ARSessionOrigin.trackablesParent == null)
            {
                // not initialized yet!
                hit = default;
                return false;
            }

            var results = new List<ARRaycastHit>();
            arContents.Raycaster.Raycast(ray, results);
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
            if (arContents.ARSessionOrigin.trackablesParent == null)
            {
                // not initialized yet!
                hits = null;
                return false;
            }

            hits = new List<ARRaycastHit>();
            arContents.Raycaster.Raycast(ray, hits);
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

                arContents.ARLight.color = color;
                arContents.ARLight.intensity = averageBrightness;

                // ambient light is only to prevent complete blacks
                RenderSettings.ambientMode = AmbientMode.Flat;
                RenderSettings.ambientLight = color;
                RenderSettings.ambientIntensity = 0.1f;
            }

            // ARKit front camera (unused)
            if (lightEstimation.mainLightDirection is { } mainLightDirection)
            {
                arContents.ARLight.transform.rotation = Quaternion.LookRotation(mainLightDirection);
            }

            // ARKit front camera (unused)
            if (lightEstimation.mainLightColor is { } mainLightColor)
            {
                arContents.ARLight.color = mainLightColor;
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
                pulseManager.TriggerPulse(results[0].Position);
            }
        }

        async ValueTask CaptureScreenForPublish(CancellationToken token)
        {
            if (DepthSender == null
                || DepthConfidenceSender == null
                || depthInfoSender == null)
            {
                return;
            }

            var captureManager = (ARScreenCaptureManager?)Settings.ScreenCaptureManager;
            if (captureManager == null || token.IsCancellationRequested)
            {
                return;
            }

            bool shouldPublishColor = ColorSender.NumSubscribers > 0;
            bool shouldPublishDepth = DepthSender.NumSubscribers > 0;
            bool shouldPublishConfidence = DepthConfidenceSender.NumSubscribers > 0;
            if (!shouldPublishColor && !shouldPublishDepth && !shouldPublishConfidence)
            {
                return;
            }

            var colorTask = shouldPublishColor ? captureManager.CaptureColorAsync(token: token) : default;
            var depthTask = shouldPublishDepth ? captureManager.CaptureDepthAsync(token: token) : default;
            var confidenceTask = shouldPublishConfidence
                ? captureManager.CaptureDepthConfidenceAsync(token: token)
                : default;

            Screenshot? color, depth, confidence;
            try
            {
                color = await colorTask;
                depth = await depthTask;
                confidence = await confidenceTask;
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Capture failed", e);
                return;
            }

            string frameId = TfModule.ResolveFrameId(XRNames.CameraFrameId);

            if (color != null)
            {
                ColorSender.Publish(color.CreateImageMessage(frameId, colorSeq));
                colorInfoSender.Publish(color.CreateCameraInfoMessage(frameId, colorSeq++));
            }

            if (depth != null)
            {
                DepthSender.Publish(depth.CreateImageMessage(frameId, depthSeq));
            }

            if (confidence != null)
            {
                DepthConfidenceSender.Publish(confidence.CreateImageMessage(frameId, depthSeq));
            }

            if ((depth ?? confidence) is { } anyDepth)
            {
                depthInfoSender.Publish(anyDepth.CreateCameraInfoMessage(frameId, depthSeq++));
            }

            if ((color ?? depth)?.CameraPose is { } anyPose)
            {
                cameraFrame.LocalPose = TfModule.RelativeToFixedFrame(ARPoseToUnity(anyPose))
                    .Unity2RosPose()
                    .ToCameraFrame()
                    .Ros2Unity();
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            tokenSource.Cancel();
            ARStartSession.Clicked -= ArStartSessionOnClicked;
            WorldPoseChanged -= OnWorldPoseChanged;
            GuiInputModule.Instance.LongClick -= TriggerPulse;

            ARStartSession.Visible = false;
            ARMoveDevicePanel.SetActive(false);

            Settings.ARCamera = null;
            Settings.ScreenCaptureManager = null;

            if (arContents.FovDisplay != null)
            {
                Object.Destroy(arContents.FovDisplay.gameObject);
            }

            Object.Destroy(arContents.gameObject);
        }

        public override string ToString() => $"[{nameof(ARFoundationController)}]";
    }
}