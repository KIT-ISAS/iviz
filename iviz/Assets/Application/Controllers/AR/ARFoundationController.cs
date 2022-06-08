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
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    public sealed class ARFoundationController : ARController
    {
        const float AnchorPauseTimeInSec = 2;

        static AnchorToggleButton ARSet => ModuleListPanel.Instance.AnchorCanvasPanel.ARSet;
        static GameObject ARMoveDevicePanel => ModuleListPanel.Instance.AnchorCanvasPanel.ARInfoPanel;

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
                var manager = ar.OcclusionManager;
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
                ARSet.Visible = value;
                ARSet.State = value;

                if (value)
                {
                    ar.Camera.cullingMask = (1 << LayerType.ARSetupMode) | (1 << LayerType.UI);
                    ARMoveDevicePanel.SetActive(true);
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
                    MeshManager = ar.Camera.gameObject.TryAddComponent<ARMeshManager>();
                    MeshManager.meshPrefab = ar.MeshPrefab;
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

        public bool ProvidesMesh { get; }
        public bool ProvidesOcclusion { get; }

        public ARFoundationController(ARConfiguration? config)
        {
            Instance = this;

            var arObject = Object.Instantiate(ResourcePool.AppAssetHolder.ARPrefab);
            if (!arObject.TryGetComponent(out ar))
            {
                ThrowHelper.ThrowMissingAssetField("AR object does not have contents");
            }

            Settings.ARCamera = ar.Camera;

            canvas = GameObject.Find("Canvas").AssertHasComponent<Canvas>(nameof(canvas));
            virtualCamera = Settings.FindMainCamera().AssertHasComponent<Camera>(nameof(virtualCamera));

            lastAnchorMoved = Time.time;

            defaultCullingMask = ar.Camera.cullingMask;

            ar.CameraManager.frameReceived += args => ProcessLights(args.lightEstimation);

            var subsystems = new List<ISubsystem>();
            SubsystemManager.GetInstances(subsystems);
            
            //ProvidesMesh = subsystems.Any(s => s is XRMeshSubsystem);
            ProvidesMesh = false; // disabled for now!
            ProvidesOcclusion = subsystems.Any(s => s is XROcclusionSubsystem);

            Config = config ?? new ARConfiguration();

            setupModeFrame = ResourcePool.RentDisplay<AxisFrameDisplay>(ar.Camera.transform);
            setupModeFrame.Layer = LayerType.ARSetupMode;
            setupModeFrame.AxisLength = 0.5f * TfModule.Instance.FrameSize;
            SetupModeEnabled = true;

            ARSet.Clicked += ArSetOnClicked;
            ARSet.Visible = false;
            ARMoveDevicePanel.SetActive(true);

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
            var worldRotation = Quaternion.Euler(0, 90 + cameraTransform.rotation.eulerAngles.y, 0);
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (TryGetRaycastHit(ray, out Pose hit))
            {
                setupModeFrame.Transform.SetPose(
                    MarkerManager.TryGetMarkerNearby(hit.position, out var pose)
                        ? pose
                        : new Pose(hit.position, worldRotation));
                setupModeFrame.Tint = Color.white;
                ARSet.Visible = true;
                ARMoveDevicePanel.SetActive(false);
            }
            else
            {
                setupModeFrame.Transform.localPosition = new Vector3(0, 0, 0.5f);
                setupModeFrame.Transform.rotation = worldRotation;
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
                CaptureScreenForPublish(tokenSource.Token);
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
                var anchor = ar.AnchorManager.AddAnchor(WorldPose)
                    .AssertHasComponent<ARAnchorResource>(nameof(ar.AnchorManager));
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
                pulseManager.TriggerPulse(results[0].Position);
            }
        }

        async void CaptureScreenForPublish(CancellationToken token)
        {
            if (ColorSender == null
                || DepthSender == null
                || DepthConfidenceSender == null
                || colorInfoSender == null
                || depthInfoSender == null)
            {
                return;
            }

            var captureManager = (ARFoundationScreenCaptureManager?)Settings.ScreenCaptureManager;

            bool shouldPublishColor = ColorSender.NumSubscribers != 0;
            bool shouldPublishDepth = DepthSender.NumSubscribers != 0;
            bool shouldPublishConfidence = DepthConfidenceSender.NumSubscribers != 0;
            if (captureManager == null || token.IsCancellationRequested ||
                (!shouldPublishColor && !shouldPublishDepth && !shouldPublishConfidence))
            {
                return;
            }

            var colorTask = shouldPublishColor
                ? captureManager.CaptureColorAsync(token: token).AwaitNoThrow(this)
                : null;

            var depthTask = shouldPublishDepth
                ? captureManager.CaptureDepthAsync(token: token).AwaitNoThrow(this)
                : null;

            var confidenceTask = shouldPublishConfidence
                ? captureManager.CaptureDepthConfidenceAsync(token: token).AwaitNoThrow(this)
                : null;

            var color = colorTask != null ? await colorTask : null;
            var depth = depthTask != null ? await depthTask : null;
            var confidence = confidenceTask != null ? await confidenceTask : null;

            string frameId = TfModule.ResolveFrameId(CameraFrameId);

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
            ARSet.Clicked -= ArSetOnClicked;
            WorldPoseChanged -= OnWorldPoseChanged;
            GuiInputModule.Instance.LongClick -= TriggerPulse;

            ARSet.Visible = false;
            ARMoveDevicePanel.SetActive(false);

            Settings.ARCamera = null;
            Settings.ScreenCaptureManager = null;

            if (ar.FovDisplay != null)
            {
                Object.Destroy(ar.FovDisplay.gameObject);
            }

            Object.Destroy(ar.gameObject);
        }
    }
}