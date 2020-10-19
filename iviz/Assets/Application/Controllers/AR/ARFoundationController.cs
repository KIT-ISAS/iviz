using System.Collections.Generic;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Controllers
{
    public sealed class ARFoundationController : ARController
    {
        const float AnchorPauseTimeInSec = 2;
        const int SetupModeLayer = 14;
        
        static ARSessionInfo savedSessionInfo;

        [SerializeField] Camera arCamera = null;
        [SerializeField] ARSessionOrigin arSessionOrigin = null;
        [SerializeField] Light arLight = null;
        [SerializeField] ARCameraFovDisplay fovDisplay = null;
        [SerializeField] AxisFrameResource setupModeFrame = null;
        
        Camera mainCamera;
        ARPlaneManager planeManager;
        ARTrackedImageManager tracker;
        ARRaycastManager raycaster;
        ARMarkerResource resource;
        ARAnchorManager anchorManager;
        ARAnchorResource worldAnchor;
        
        
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
                TFListener.MainCamera = value ? arCamera : mainCamera;
                canvas.worldCamera = TFListener.MainCamera;
            }
        }

        bool setupModeEnabled = true;
        bool SetupModeEnabled
        {
            get => setupModeEnabled;
            set
            {
                setupModeEnabled = value;
                arCamera.cullingMask = value ? (1 << SetupModeLayer) : ~(1 << SetupModeLayer);
            }
        }
        
        public bool HasSetupModePose { get; private set; }

        bool MarkerFound { get; set; }

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

        public override bool PinRootMarker
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

            planeManager = arSessionOrigin.GetComponent<ARPlaneManager>();

            tracker = arSessionOrigin.GetComponent<ARTrackedImageManager>();
            raycaster = arSessionOrigin.GetComponent<ARRaycastManager>();
            anchorManager = arSessionOrigin.GetComponent<ARAnchorManager>();
            lastAnchorMoved = Time.time;

            var cameraManager = arCamera.GetComponent<ARCameraManager>();
            cameraManager.frameReceived += args => { UpdateLights(args.lightEstimation); };

            resource = ResourcePool.GetOrCreate<ARMarkerResource>(Resource.Displays.ARMarkerResource);
            node.Target = resource;

            MarkerFound = false;

            Config = new ARConfiguration();

            if (setupModeFrame == null)
            {
                setupModeFrame = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(arCamera.transform);
                setupModeFrame.Layer = SetupModeLayer;
            }
            
            SetupModeEnabled = true;
            setupModeFrame.AxisLength = TFListener.Instance.FrameSize;

            WorldPoseChanged += OnWorldPoseChanged;
        }

        bool IsSamePose(in Pose b)
        {
            return Vector3.Distance(WorldPosition, b.position) < 0.001f &&
                   Mathf.Abs(WorldAngle - AngleFromPose(b)) < 0.1f;
        }
        
        void OnWorldPoseChanged(RootMover mover)
        {
            if (mover == RootMover.Anchor||
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
                if (FindClosestPlane(ray, out ARRaycastHit hit, out _))
                {
                    setupModeFrame.transform.position = hit.pose.position;
                    setupModeFrame.Tint = Color.white;
                    HasSetupModePose = true;
                }
                else
                {
                    setupModeFrame.transform.localPosition = new Vector3(0, 0, 0.5f);
                    setupModeFrame.Tint = new Color(1, 1, 1, 0.5f);
                    HasSetupModePose = false;
                }
            }
            
            if (lastAnchorMoved == null || Time.time - lastAnchorMoved.Value < AnchorPauseTimeInSec)
            {
                return;
            }

            lastAnchorMoved = null;

            if (PinRootMarker)
            {
                Vector3 origin = WorldPosition + 0.05f * Vector3.up;
                Ray ray = new Ray(origin, Vector3.down);
                if (FindClosestPlane(ray, out ARRaycastHit hit, out ARPlane plane))
                {
                    Pose pose = new Pose(hit.pose.position, WorldPose.rotation);
                    worldAnchor = anchorManager.AttachAnchor(plane, pose).GetComponent<ARAnchorResource>();
                    worldAnchor.Moved += OnWorldAnchorMoved;

                    SetWorldPose(pose, RootMover.Anchor);
                }
                else
                {
                    lastAnchorMoved = Time.time + 2;
                }
            }
            else
            {
                worldAnchor = anchorManager.AddAnchor(WorldPose).GetComponent<ARAnchorResource>();
                worldAnchor.Moved += OnWorldAnchorMoved;
            }
        }

        void OnWorldAnchorMoved(Pose newPose)
        {
            SetWorldPose(newPose, RootMover.Anchor);
        }

        protected override bool FindRayHit(in Ray ray, out Vector3 anchor, out Vector3 normal)
        {
            if (!FindClosestPlane(ray, out ARRaycastHit hit, out ARPlane plane))
            {
                anchor = ray.origin;
                normal = Vector3.zero;
                return false;
            }

            anchor = hit.pose.position;
            normal = plane.normal;
            return true;
        }

        bool FindClosestPlane(in Ray ray, out ARRaycastHit hit, out ARPlane plane)
        {
            if (arSessionOrigin == null || arSessionOrigin.trackablesParent == null)
            {
                // not initialized yet!
                plane = default;
                hit = default;
                return false;
            }

            List<ARRaycastHit> results = new List<ARRaycastHit>();
            raycaster.Raycast(ray, results, TrackableType.PlaneWithinBounds);
            if (results.Count == 0)
            {
                plane = default;
                hit = default;
                return false;
            }

            Vector3 origin = ray.origin;
            hit = results.Count == 1
                ? results[0]
                : results.Select(rayHit => ((rayHit.pose.position - origin).sqrMagnitude, rayHit)).Min().rayHit;
            plane = planeManager.GetPlane(hit.trackableId);
            return true;
        }


        void UpdateLights(in ARLightEstimationData lightEstimation)
        {
            arLight.intensity = 1;

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

            Pose expectedPose = TFListener.RelativePoseToRoot(resource.transform.AsPose());
            Pose registeredPose = newPose.Value.Multiply(expectedPose.Inverse());

            Quaternion corrected =
                new Quaternion(0, registeredPose.rotation.y, 0, registeredPose.rotation.w).normalized;
            registeredPose.rotation = corrected;

            SetWorldPose(registeredPose, RootMover.ImageMarker);

            MarkerFound = true;
        }

        public override void StopController()
        {
            base.StopController();
            Destroy(fovDisplay.gameObject);
        }
    }
}