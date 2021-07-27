using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public sealed class ARFoundationController : ARController
    {
        const float AnchorPauseTimeInSec = 2;

        //static ARSessionInfo savedSessionInfo;

        [SerializeField] Camera arCamera = null;
        [SerializeField] ARSession arSession = null;
        [SerializeField] ARSessionOrigin arSessionOrigin = null;
        [SerializeField] Light arLight = null;
        [SerializeField] ARCameraFovDisplay fovDisplay = null;
        [SerializeField] AxisFrameResource setupModeFrame = null;
        [SerializeField] MeshFilter meshPrefab = null;

        Camera mainCamera;
        ARCameraManager cameraManager;
        ARSession session;
        [CanBeNull] ARPlaneManager planeManager;
        ARTrackedImageManager tracker;
        ARRaycastManager raycaster;
        ARAnchorManager anchorManager;
        AROcclusionManager occlusionManager;
        [CanBeNull] ARAnchorResource worldAnchor;
        readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        int defaultCullingMask;

        static AnchorToggleButton ArSet => ModuleListPanel.AnchorCanvas.ArSet;
        static GameObject ArInfoPanel => ModuleListPanel.AnchorCanvas.ArInfoPanel;

        [NotNull]
        public string Description
        {
            get
            {
                if (session == null || session.subsystem == null)
                {
                    return "<b>[No AR Subsystem]</b>";
                }

                string trackingState;
                switch (session.subsystem.trackingState)
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
                    planeManager == null
                        ? "Plane Detection Disabled"
                        : planeManager.trackables.count == 0
                            ? "Planes: None"
                            : "Planes: " + planeManager.trackables.count.ToString();

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
                        occlusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Disabled;
                        occlusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Disabled;
                        occlusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Disabled;
                        break;
                    case OcclusionQualityType.Fast:
                        occlusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Fastest;
                        occlusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Fastest;
                        occlusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Fastest;
                        break;
                    case OcclusionQualityType.Medium:
                        occlusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Medium;
                        occlusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Fastest;
                        occlusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Medium;
                        break;
                    case OcclusionQualityType.Best:
                        occlusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Best;
                        occlusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Best;
                        occlusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Best;
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
                //resource.Visible = value && UseMarker;
                //resource.Visible = value;
                mainCamera.gameObject.SetActive(!value);
                arCamera.enabled = value;
                arLight.gameObject.SetActive(value);
                ArSet.Visible = SetupModeEnabled;
                Settings.MainCamera = value ? arCamera : mainCamera;
                canvas.worldCamera = Settings.MainCamera;

                if (value)
                {
                    RenderSettings.ambientMode = AmbientMode.Flat;
                }
                else
                {
                    RenderSettings.ambientMode = AmbientMode.Trilight;
                    if (Settings.SettingsManager != null)
                    {
                        Settings.SettingsManager.BackgroundColor = Settings.SettingsManager.BackgroundColor;
                    }
                }
            }
        }

        bool setupModeEnabled = true;

        public bool SetupModeEnabled
        {
            get => setupModeEnabled;
            private set
            {
                setupModeEnabled = value;
                ArSet.Visible = value;
                ArSet.State = value;

                if (value)
                {
                    arCamera.cullingMask = (1 << LayerType.ARSetupMode) | (1 << LayerType.UI);
                    ArInfoPanel.SetActive(true);
                }
                else
                {
                    arCamera.cullingMask = defaultCullingMask;
                    var (sourcePosition, sourceRotation) = setupModeFrame.transform.AsPose();
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
            get => cameraManager.autoFocusRequested;
            set => cameraManager.autoFocusRequested = value;
        }

        public override bool EnableMeshing
        {
            get => base.EnableMeshing;
            set
            {
                base.EnableMeshing = value;
                if (value)
                {
                    var meshManager = arCamera.gameObject.EnsureComponent<ARMeshManager>();
                    //Debug.Log("Adding new mesh managgr");
                    meshManager.meshPrefab = meshPrefab;
                    meshManager.normals = false;
                }
                else
                {
                    var meshManager = arCamera.gameObject.GetComponent<ARMeshManager>();
                    if (meshManager != null)
                    {
                        //Debug.Log("Destroying mesh managgr");
                        meshManager.DestroyAllMeshes();
                        Destroy(meshManager);
                    }
                }
            }
        }

        public override bool EnablePlaneDetection
        {
            get => base.EnablePlaneDetection;
            set
            {
                if (base.EnablePlaneDetection == value)
                {
                    return;
                }

                base.EnablePlaneDetection = value;
                if (value)
                {
                    planeManager = arSessionOrigin.gameObject.EnsureComponent<ARPlaneManager>();
                }
                else
                {
                    Destroy(planeManager);
                    planeManager = null;
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

        protected override void Awake()
        {
            base.Awake();
            Instance = this;

            if (mainCamera == null)
            {
                mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            }

            if (arCamera == null)
            {
                arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
            }

            Settings.ARCamera = arCamera;

            if (arSessionOrigin == null)
            {
                arSessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
            }

            session = GetComponentInChildren<ARSession>();
            occlusionManager = GetComponentInChildren<AROcclusionManager>();
            planeManager = arSessionOrigin.GetComponent<ARPlaneManager>();
            tracker = arSessionOrigin.GetComponent<ARTrackedImageManager>();
            raycaster = arSessionOrigin.GetComponent<ARRaycastManager>();
            anchorManager = arSessionOrigin.GetComponent<ARAnchorManager>();
            cameraManager = arCamera.GetComponent<ARCameraManager>();
            lastAnchorMoved = Time.time;

            defaultCullingMask = arCamera.cullingMask;

            cameraManager.frameReceived += args => ProcessLights(args.lightEstimation);

            Config = new ARConfiguration();

            if (setupModeFrame == null)
            {
                setupModeFrame = ResourcePool.RentDisplay<AxisFrameResource>(arCamera.transform);
                setupModeFrame.Layer = LayerType.ARSetupMode;
            }

            SetupModeEnabled = true;
            setupModeFrame.AxisLength = 0.5f * TfListener.Instance.FrameSize;

            ArSet.Clicked += ArSetOnClicked;
            ArSet.Visible = false;
            ArInfoPanel.SetActive(true);

            WorldPoseChanged += OnWorldPoseChanged;

            Settings.ScreenCaptureManager =
                new ARFoundationScreenCaptureManager(cameraManager, arCamera.transform, occlusionManager, arSession);

            if (GuiInputModule.Instance != null)
            {
                GuiInputModule.Instance.ShortClick += TriggerPulse;
            }

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
            arSession.Reset();
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
                //Destroy(worldAnchor.Anchor);
                anchorManager.RemoveAnchor(worldAnchor.Anchor);
                worldAnchor = null;
            }

            lastAnchorMoved = recreateNow ? Time.time - AnchorPauseTimeInSec : Time.time;
            //Debug.Log("destroying anchor! " + recreateNow);
        }

        float? lastAnchorMoved;
        float lastScreenCapture = 0;

        public override void Update()
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

            Transform cameraTransform = arCamera.transform;
            setupModeFrame.Transform.rotation = Quaternion.Euler(0, 90 + cameraTransform.rotation.eulerAngles.y, 0);
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (TryGetPlaneHit(ray, out ARRaycastHit hit))
            {
                setupModeFrame.Transform.position = hit.pose.position;
                setupModeFrame.Tint = Color.white;
                ArSet.Visible = true;
                ArInfoPanel.SetActive(false);
            }
            else
            {
                setupModeFrame.Transform.localPosition = new Vector3(0, 0, 0.5f);
                setupModeFrame.Tint = Color.white.WithAlpha(0.3f);
                ArSet.Visible = false;
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

                Vector3 origin = WorldPosition + maxDistanceAbovePlane * Vector3.up;
                Ray ray = new Ray(origin, Vector3.down);
                if (TryGetPlaneHit(ray, out ARRaycastHit hit))
                {
                    Pose pose = WorldPose.WithPosition(hit.pose.position);
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

        public int CaptureFps { get; set; } = 15;

        void ProcessCapture()
        {
            if (CaptureFps <= 0 || GameThread.GameTime - lastScreenCapture < 1 / (float) CaptureFps)
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
                var anchor = anchorManager.AddAnchor(WorldPose).GetComponent<ARAnchorResource>();
                anchor.Moved += OnWorldAnchorMoved;
                worldAnchor = anchor;
            }
            catch (InvalidOperationException e)
            {
                Logger.Error("Failed to initialize AR world anchor:", e);
            }
        }

        void OnWorldAnchorMoved(Pose newPose)
        {
            SetWorldPose(newPose, RootMover.Anchor);
        }

        bool TryGetPlaneHit(in Ray ray, out ARRaycastHit hit)
        {
            if (arSessionOrigin == null || arSessionOrigin.trackablesParent == null)
            {
                // not initialized yet!
                hit = default;
                return false;
            }

            List<ARRaycastHit> results = new List<ARRaycastHit>();
            raycaster.Raycast(ray, results);
            results.RemoveAll(rayHit => (rayHit.hitType & TrackableType.PlaneWithinPolygon) == 0);

            switch (results.Count)
            {
                case 0:
                    hit = default;
                    return false;
                case 1:
                    hit = results[0];
                    return true;
                default:
                    Vector3 origin = ray.origin;
                    hit = Enumerable.Select(results, rayHit => ((rayHit.pose.position - origin).sqrMagnitude, rayHit))
                        .Min().rayHit;
                    return true;
            }
        }


        void ProcessLights(in ARLightEstimationData lightEstimation)
        {
            if (lightEstimation.averageColorTemperature.HasValue && lightEstimation.averageBrightness.HasValue)
            {
                RenderSettings.ambientMode = AmbientMode.Flat;
                RenderSettings.ambientLight =
                    Mathf.CorrelatedColorTemperatureToRGB(lightEstimation.averageColorTemperature.Value) *
                    lightEstimation.averageBrightness.Value;
            }

            if (lightEstimation.mainLightDirection.HasValue)
            {
                arLight.transform.rotation = Quaternion.LookRotation(lightEstimation.mainLightDirection.Value);
            }

            if (lightEstimation.mainLightColor.HasValue)
            {
                arLight.color = lightEstimation.mainLightColor.Value;
            }

            if (lightEstimation.ambientSphericalHarmonics.HasValue)
            {
                var sphericalHarmonics = lightEstimation.ambientSphericalHarmonics;
                RenderSettings.ambientMode = AmbientMode.Skybox;
                RenderSettings.ambientProbe = sphericalHarmonics.Value;
            }
        }

        void TriggerPulse(Vector2 cursorPosition)
        {
            if (!Visible)
            {
                return;
            }

            var ray = Settings.MainCamera.ScreenPointToRay(cursorPosition);
            if (!TryGetPlaneHit(ray, out ARRaycastHit hit))
            {
                return;
            }

            ARMeshLines.TriggerPulse(hit.pose.position);
        }

        uint colorSeq, depthSeq;

        async void CaptureScreenForPublish(CancellationToken token)
        {
            try
            {
                var captureManager = (ARFoundationScreenCaptureManager) Settings.ScreenCaptureManager;
                bool shouldPublishColor = PublishColor && ColorSender.NumSubscribers != 0;
                bool shouldPublishDepth = PublishDepth && DepthSender.NumSubscribers != 0;
                bool shouldPublishConfidence = PublishDepth && DepthConfidenceSender.NumSubscribers != 0;
                if (captureManager == null || token.IsCancellationRequested ||
                    (!shouldPublishColor && !shouldPublishDepth && !shouldPublishConfidence))
                {
                    return;
                }

                const int captureReuseTimeInMs = 30;

                var colorTask = shouldPublishColor
                    ? captureManager.CaptureColorAsync(captureReuseTimeInMs, token).AwaitNoThrow(this)
                    : (ValueTask<Screenshot>?) null;

                var depthTask = shouldPublishDepth
                    ? captureManager.CaptureDepthAsync(captureReuseTimeInMs, token).AwaitNoThrow(this)
                    : (ValueTask<Screenshot>?) null;

                var confidenceTask = shouldPublishConfidence
                    ? captureManager.CaptureDepthConfidenceAsync(captureReuseTimeInMs, token).AwaitNoThrow(this)
                    : (ValueTask<Screenshot>?) null;

                var color = colorTask != null ? await colorTask.Value : null;
                var depth = depthTask != null ? await depthTask.Value : null;
                var confidence = confidenceTask != null ? await confidenceTask.Value : null;

                var frame = TfListener.ResolveFrame(CameraFrameId);
                frame.ForceInvisible = true;
                string frameId = frame.Id;

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

                if (depth != null || confidence != null)
                {
                    depthInfoSender.Publish((depth ?? confidence).CreateCameraInfoMessage(frameId, depthSeq++));
                }

                var anyPose = (color ?? depth)?.CameraPose;
                if (anyPose == null)
                {
                    return;
                }

                var absoluteArCameraPose = RelativePoseToOrigin(anyPose.Value);
                var relativePose = TfListener.RelativePoseToFixedFrame(absoluteArCameraPose).Unity2RosTransform();
                //var rosPose = TfListener.RelativePoseToFixedFrame(anyPose.Value).Unity2RosPose().ToCameraFrame();
                TfListener.Publish(frameId, relativePose.ToCameraFrame());
            }
            catch (Exception e)
            {
                Logger.Error("CaptureScreenForPublish failed", e);
            }
        }

        public override void StopController()
        {
            base.StopController();
            tokenSource.Cancel();
            ArSet.Clicked -= ArSetOnClicked;
            WorldPoseChanged -= OnWorldPoseChanged;
            if (GuiInputModule.Instance != null)
            {
                GuiInputModule.Instance.ShortClick -= TriggerPulse;
            }

            Destroy(fovDisplay.gameObject);

            ArSet.Visible = false;
            ArInfoPanel.SetActive(false);

            Settings.ARCamera = null;
            Settings.ScreenCaptureManager = null;
        }
    }
}