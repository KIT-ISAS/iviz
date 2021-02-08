using System;
using System.Collections.Generic;
using Iviz.Roslib;
using System.Linq;
using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;
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
        [SerializeField] ARSessionOrigin arSessionOrigin = null;
        [SerializeField] Light arLight = null;
        [SerializeField] ARCameraFovDisplay fovDisplay = null;
        [SerializeField] AxisFrameResource setupModeFrame = null;

        Camera mainCamera;
        ARSession arSession;
        ARPlaneManager planeManager;
        ARTrackedImageManager tracker;
        ARRaycastManager raycaster;
        ARMarkerResource resource;
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
                if (arSession == null || arSession.subsystem == null)
                {
                    return "(No AR Subsystem)";
                }

                string trackingState;
                switch (arSession.subsystem.trackingState)
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
                    planeManager.trackables.count == 0
                        ? "Planes: None"
                        : "Planes: " + planeManager.trackables.count;

                return trackingState + "\n" + numPlanes;
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
                resource.Visible = value && UseMarker;
                mainCamera.gameObject.SetActive(!value);
                arCamera.enabled = value;
                arLight.gameObject.SetActive(value);
                ArSet.Visible = value;
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
                if (value)
                {
                    arCamera.cullingMask = 1 << LayerType.ARSetupMode;
                    ArSet.Visible = false;
                    ArSet.State = true;
                    ArInfoPanel.SetActive(true);
                }
                else
                {
                    arCamera.cullingMask = defaultCullingMask;
                    Pose sourcePose = setupModeFrame.transform.AsPose();
                    Pose pose = new Pose
                    {
                        position = sourcePose.position,
                        rotation = Quaternion.Euler(0, sourcePose.rotation.eulerAngles.y - 90, 0)
                    };

                    //Debug.Log(sourcePose);
                    SetWorldPose(pose, RootMover.Setup);
                }
            }
        }

        //bool hasSetupModePose;
        //bool markerFound;

        public override bool UseMarker
        {
            get => base.UseMarker;
            set
            {
                base.UseMarker = value;
                tracker.enabled = value;
                resource.Visible = Visible && value;
                if (value)
                {
                    tracker.trackedImagesChanged += OnTrackedImagesChanged;
                }
                else
                {
                    tracker.trackedImagesChanged -= OnTrackedImagesChanged;
                }
            }
        }

        public override bool MarkerHorizontal
        {
            get => base.MarkerHorizontal;
            set
            {
                base.MarkerHorizontal = value;
                resource.Horizontal = value;
            }
        }

        public override int MarkerAngle
        {
            get => base.MarkerAngle;
            set
            {
                base.MarkerAngle = value;
                resource.Angle = value; // deg
            }
        }

        public override string MarkerFrame
        {
            get => base.MarkerFrame;
            set
            {
                base.MarkerFrame = value;
                node.AttachTo(base.MarkerFrame);
            }
        }

        public override Vector3 MarkerOffset
        {
            get => base.MarkerOffset;
            set
            {
                base.MarkerOffset = value;
                resource.Offset = value.Ros2Unity();
            }
        }

        protected override bool PinRootMarker
        {
            get => base.PinRootMarker;
            set
            {
                //Debug.Log("pin changed");
                if (value && !base.PinRootMarker)
                {
                    //Debug.Log("pin changed to true!");
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

            if (arSessionOrigin == null)
            {
                arSessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
            }

            arSession = GetComponentInChildren<ARSession>();
            occlusionManager = GetComponentInChildren<AROcclusionManager>();
            planeManager = arSessionOrigin.GetComponent<ARPlaneManager>();
            tracker = arSessionOrigin.GetComponent<ARTrackedImageManager>();
            raycaster = arSessionOrigin.GetComponent<ARRaycastManager>();
            anchorManager = arSessionOrigin.GetComponent<ARAnchorManager>();
            lastAnchorMoved = Time.time;

            defaultCullingMask = arCamera.cullingMask;
            var cameraManager = arCamera.GetComponent<ARCameraManager>();

            cameraManager.frameReceived += args => { UpdateLights(args.lightEstimation); };

            resource = ResourcePool.GetOrCreate<ARMarkerResource>(Resource.Displays.ARMarkerResource);
            resource.Parent = node.transform;

            Config = new ARConfiguration();

            if (setupModeFrame == null)
            {
                setupModeFrame = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(arCamera.transform);
                setupModeFrame.Layer = LayerType.ARSetupMode;
            }

            SetupModeEnabled = true;
            setupModeFrame.AxisLength = 0.5f * TfListener.Instance.FrameSize;

            ArSet.Clicked += ArSetOnClicked;
            ArSet.State = true;
            ArSet.Visible = false;
            ArInfoPanel.SetActive(true);

            WorldPoseChanged += OnWorldPoseChanged;

            OcclusionQuality = OcclusionQualityType.Off;
        }

        void ArSetOnClicked()
        {
            SetupModeEnabled = !SetupModeEnabled;
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

        void Update()
        {
            if (SetupModeEnabled)
            {
                Transform cameraTransform = arCamera.transform;
                setupModeFrame.transform.rotation = Quaternion.Euler(0, 90 + cameraTransform.rotation.eulerAngles.y, 0);
                Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
                if (TryGetClosestPlane(ray, out ARRaycastHit hit))
                {
                    setupModeFrame.transform.position = hit.pose.position;
                    setupModeFrame.Tint = Color.white;
                    //hasSetupModePose = true;
                    ArSet.Visible = true;
                    ArInfoPanel.SetActive(false);
                }
                else
                {
                    setupModeFrame.transform.localPosition = new Vector3(0, 0, 0.5f);
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
                if (TryGetClosestPlane(ray, out ARRaycastHit hit))
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

        bool TryGetClosestPlane(in Ray ray, out ARRaycastHit hit)
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

        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
        {
            Pose? newPose = null;

            if (obj.added.Count != 0)
            {
                newPose = obj.added[0].transform.AsPose();
            }

            if (obj.updated.Count != 0)
            {
                newPose = obj.updated[0].transform.AsPose();
            }

            if (newPose == null)
            {
                return;
            }

            Pose expectedPose = TfListener.RelativePoseToOrigin(resource.transform.AsPose());
            Pose registeredPose = newPose.Value.Multiply(expectedPose.Inverse());

            Quaternion corrected =
                new Quaternion(0, registeredPose.rotation.y, 0, registeredPose.rotation.w).normalized;
            registeredPose.rotation = corrected;

            SetWorldPose(registeredPose, RootMover.ImageMarker);

            //markerFound = true;
        }

        public override void StopController()
        {
            base.StopController();
            ArSet.Clicked -= ArSetOnClicked;
            WorldPoseChanged -= OnWorldPoseChanged;
            Destroy(fovDisplay.gameObject);

            ArSet.Visible = false;
            ArInfoPanel.SetActive(false);
        }
    }
}