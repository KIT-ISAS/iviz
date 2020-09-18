using System.Collections.Generic;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Controllers
{
    public sealed class ARFoundationController : ARController
    {
        static ARSessionInfo savedSessionInfo;
        
        [SerializeField] Camera arCamera = null;
        [SerializeField] ARSessionOrigin arSessionOrigin = null;
        [SerializeField] Light arLight = null;
        
        Camera mainCamera;
        ARPlaneManager planeManager;
        ARTrackedImageManager tracker;
        ARRaycastManager raycaster;
        ARMarkerResource resource;

        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                resource.Visible = value && UseMarker;
                mainCamera.gameObject.SetActive(!value);
                arCamera.gameObject.SetActive(value);
                arLight.gameObject.SetActive(value);
                TFListener.MainCamera = value ? arCamera : mainCamera;
                canvas.worldCamera = TFListener.MainCamera;
            }
        }

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
                    TfRoot.SetPose(RegisteredPose);
                    tracker.trackedImagesChanged += OnTrackedImagesChanged;
                }
                else
                {
                    WorldOffset = WorldOffset;
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
            planeManager.planesChanged += OnPlanesChanged;
            
            tracker = arSessionOrigin.GetComponent<ARTrackedImageManager>();
            raycaster = arSessionOrigin.GetComponent<ARRaycastManager>();

            var cameraManager = arCamera.GetComponent<ARCameraManager>();
            cameraManager.frameReceived += args =>
            {
                UpdateLights(args.lightEstimation);
            };
            
            resource = ResourcePool.GetOrCreate<ARMarkerResource>(Resource.Displays.ARMarkerResource);
            node.Target = resource;
            MarkerFound = false;

            Config = new ARConfiguration();
        }
        
        void OnPlanesChanged(ARPlanesChangedEventArgs obj)
        {
            forceAnchorRebuild = true;
        }

        public override bool FindRayHit(in Ray ray, out Vector3 anchor, out Vector3 normal)
        {
            if (arSessionOrigin?.trackablesParent == null)
            {
                // not initialized yet!
                anchor = ray.origin;
                normal = Vector3.zero;
                return false;
            }
            
            List<ARRaycastHit> results = new List<ARRaycastHit>();
            raycaster.Raycast(ray, results, TrackableType.PlaneWithinBounds);
            if (results.Count == 0)
            {
                anchor = ray.origin;
                normal = Vector3.zero;
                return false;
            } 

            Vector3 origin = ray.origin;
            var minHit = results.Count == 1 ? results[0] : 
                results.Select(hit => ((hit.pose.position - origin).sqrMagnitude, hit)).Min().hit;
            var plane = planeManager.GetPlane(minHit.trackableId);
            anchor = minHit.pose.position;
            normal = plane.normal;
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
            
            WorldOffset = Vector3.zero;
            WorldAngle = 0;

            Pose expectedPose = TFListener.RelativePose(resource.transform.AsPose());
            Pose registeredPose = newPose.Value.Multiply(expectedPose.Inverse());
            Quaternion corrected = new Quaternion(0, registeredPose.rotation.y, 0, registeredPose.rotation.w).normalized;

            //Debug.Log("Registration! " + registeredPose.rotation + " -> " + corrected);

            registeredPose.rotation = corrected;
            
            RegisteredPose = registeredPose;
            
            MarkerFound = true;
            TfRoot.SetPose(RootPose);
            
            ModuleData.ResetPanel();
        }
    }
}