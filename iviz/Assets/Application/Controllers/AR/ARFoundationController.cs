using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public sealed class ARFoundationController : ARController, IScreenshotManager
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

        bool SetupModeEnabled
        {
            get => setupModeEnabled;
            set
            {
                setupModeEnabled = value;
                ArSet.Visible = value;
                ArSet.State = value;

                if (value)
                {
                    arCamera.cullingMask = 1 << LayerType.ARSetupMode;
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
                if (base.EnableMeshing == value)
                {
                    return;
                }

                base.EnableMeshing = value;
                if (value)
                {
                    var meshManager = arCamera.gameObject.EnsureComponent<ARMeshManager>();
                    meshManager.meshPrefab = meshPrefab;
                    meshManager.normals = false;
                }
                else
                {
                    var meshManager = arCamera.gameObject.GetComponent<ARMeshManager>();
                    meshManager.DestroyAllMeshes();
                    Destroy(meshManager);
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

            cameraManager.frameReceived += args => UpdateLights(args.lightEstimation);

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

            OcclusionQuality = OcclusionQualityType.Off;

            Settings.ScreenshotManager = this;

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

        public void ResetSession()
        {
            arSession.Reset();
            ResetSetupMode();
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

        public override void Update()
        {
            base.Update();

            if (SetupModeEnabled)
            {
                Transform cameraTransform = arCamera.transform;
                setupModeFrame.Transform.rotation = Quaternion.Euler(0, 90 + cameraTransform.rotation.eulerAngles.y, 0);
                Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
                if (TryGetPlaneHit(ray, out ARRaycastHit hit))
                {
                    setupModeFrame.Transform.position = hit.pose.position;
                    setupModeFrame.Tint = Color.white;
                    //hasSetupModePose = true;
                    ArSet.Visible = true;
                    ArInfoPanel.SetActive(false);
                }
                else
                {
                    setupModeFrame.Transform.localPosition = new Vector3(0, 0, 0.5f);
                    setupModeFrame.Tint = Color.white.WithAlpha(0.3f);
                    //hasSetupModePose = false;
                    ArSet.Visible = false;
                }
            }

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
                Logger.Error("Failed to initialize AR world anchor", e);
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
                    hit = results.Select(rayHit => ((rayHit.pose.position - origin).sqrMagnitude, rayHit)).Min().rayHit;
                    return true;
            }
        }


        void UpdateLights(in ARLightEstimationData lightEstimation)
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

        bool IScreenshotManager.Started => true;

        [NotNull]
        IEnumerable<(int width, int height)> IScreenshotManager.GetResolutions()
        {
            var configuration = cameraManager.subsystem.currentConfiguration;
            return configuration == null
                ? Array.Empty<(int width, int height)>()
                : new[] {(configuration.Value.width, configuration.Value.height)};
        }

        [NotNull]
        Task IScreenshotManager.StartAsync(int width, int height, bool withHolograms) => Task.CompletedTask;

        [NotNull]
        Task IScreenshotManager.StopAsync() => Task.CompletedTask;

        public Task<Screenshot> TakeScreenshotColorAsync()
        {
            if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                return Task.FromResult<Screenshot>(null);
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.RGB24,
            };

            var task = new TaskCompletionSource<Screenshot>();

            int width = image.width;
            int height = image.height;
            var pose = arCamera.transform.AsPose();

            using (image)
            {
                try
                {
                    image.ConvertAsync(conversionParams, (status, _, array) =>
                    {
                        if (status != XRCpuImage.AsyncConversionStatus.Ready)
                        {
                            Debug.LogErrorFormat("Request failed with status {0}", status);
                            task.TrySetResult(null);
                            return;
                        }

                        var bytes = new UniqueRef<byte>(array.Length);
                        NativeArray<byte>.Copy(array, bytes.Array, array.Length);

                        var intrinsic = cameraManager.TryGetIntrinsics(out var intrinsics)
                            ? new Intrinsic(intrinsics.focalLength.x, intrinsics.principalPoint.x,
                                intrinsics.focalLength.y, intrinsics.principalPoint.y)
                            : default;

                        var s = new Screenshot(ScreenshotFormat.Rgb, width, height, 3, intrinsic, pose, bytes);
                        task.TrySetResult(s);
                    });
                }
                catch (Exception e)
                {
                    task.TrySetException(e);
                }
            }

            return task.Task;
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

        public override void StopController()
        {
            base.StopController();
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
            Settings.ScreenshotManager = null;
        }
    }
}