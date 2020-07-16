using UnityEngine;
using System.Collections;
using Iviz.RoslibSharp;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Application.Displays;
using Iviz.Resources;
using UnityEngine.XR.ARFoundation;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.App.Displays;
using Iviz.RoslibSharp.XmlRpc;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class ARConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.AR;
        [DataMember] public bool Visible { get; set; } = true;
        /* NonSerializable */ public float WorldScale { get; set; } = 1.0f;
        /* NonSerializable */ public SerializableVector3 WorldOffset { get; set; } = ARController.DefaultWorldOffset;
        /* NonSerializable */ public float WorldAngle { get; set; } = 0;
        [DataMember] public bool SearchMarker { get; set; } = false;
        [DataMember] public bool MarkerHorizontal { get; set; } = true;
        [DataMember] public int MarkerAngle { get; set; } = 0;
        [DataMember] public string MarkerFrame { get; set; } = "";
        [DataMember] public SerializableVector3 MarkerOffset { get; set; } = Vector3.zero;
        //[DataMember] public bool PublishPose { get; set; } = false;
        //[DataMember] public bool PublishMarkers { get; set; } = false;
    }
    
    [DataContract]
    public sealed class ARSessionInfo : JsonToString
    {
        [DataMember] public float WorldScale { get; set; } = 1.0f;
        [DataMember] public SerializableVector3 WorldOffset { get; set; } = Vector3.zero;
        [DataMember] public float WorldAngle { get; set; } = 0;
    }

    public sealed class ARController : MonoBehaviour, IController, IHasFrame, IAnchorProvider
    {
        public static readonly Vector3 DefaultWorldOffset = new Vector3(0.5f, 0, -0.2f);
        
        static ARSessionInfo savedSessionInfo;
        
        [SerializeField] Camera ARCamera = null;
        [SerializeField] ARSessionOrigin ARSessionOrigin = null;
        [SerializeField] Light ARLight = null;
        [SerializeField] Canvas canvas = null;
        [SerializeField] Camera mainCamera = null;

        ARPlaneManager planeManager;
        ARTrackedImageManager tracker;
        ARRaycastManager raycaster;

        DisplayClickableNode node;
        ARMarkerResource resource;

        //readonly MeshToMarkerHelper helper = new MeshToMarkerHelper("ar");
        //DateTime lastMarkerUpdate = DateTime.MinValue;

        static Transform TFRoot => TFListener.RootFrame.transform;

        const string HeadPoseTopic = "ar_head";
        const string MarkersTopic = "ar_markers";

        static string HeadFrameName => ConnectionManager.Connection.MyId + "/ar_head";

        //public RosSender<Msgs.GeometryMsgs.PoseStamped> RosSenderHead { get; private set; }
        //public RosSender<MarkerArray> RosSenderMarkers { get; private set; }
        public ModuleData ModuleData { get; set; }

        readonly ARConfiguration config = new ARConfiguration();
        public ARConfiguration Config
        {
            get => config;
            set
            {
                Visible = value.Visible;
                //WorldOffset = value.WorldOffset;
                WorldScale = value.WorldScale;
                //PublishPose = value.PublishPose;
                //PublishPlanesAsMarkers = value.PublishMarkers;
                UseMarker = value.SearchMarker;
                MarkerHorizontal = value.MarkerHorizontal;
                MarkerAngle = value.MarkerAngle;
                MarkerFrame = value.MarkerFrame;
                MarkerOffset = value.MarkerOffset;
            }
        }

        public Vector3 WorldOffset
        {
            get => config.WorldOffset;
            set
            {
                config.WorldOffset = value;
                TFRoot.SetPose(RootPose);
            }
        }
        
        public float WorldAngle
        {
            get => config.WorldAngle;
            set
            {
                config.WorldAngle = value;
                TFRoot.SetPose(RootPose);
            }
        }

        public float WorldScale
        {
            get => config.WorldScale;
            set
            {
                config.WorldScale = value;
                TFRoot.localScale = value * Vector3.one;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                resource.Visible = value && UseMarker;
                mainCamera.gameObject.SetActive(!value);
                ARCamera.gameObject.SetActive(value);
                //TFListener.MainLight.gameObject.SetActive(!value);
                ARLight.gameObject.SetActive(value);
                canvas.worldCamera = value ? ARCamera : mainCamera;
                TFListener.MainCamera = value ? ARCamera : mainCamera;

                TFRoot.SetPose(value ? RootPose : Pose.identity);

                TFListener.RootControl.InteractionMode = value
                    ? InteractiveControl.InteractionModeType.Frame
                    : InteractiveControl.InteractionModeType.Disabled;
                //TFListener.RootControl.InteractionMode = InteractiveControl.InteractionModeType.Frame;
                
                foreach (var module in DisplayListPanel.Instance.ModuleDatas)
                {
                    module.OnARModeChanged(value);
                }
                
                TFListener.MapFrame.UpdateAnchor(null, value);
            }
        }

        /*
        private void AnchorManager_anchorsChanged(ARAnchorsChangedEventArgs obj)
        {
            if (obj.added.Count != 0)
            {
                Debug.Log("Added " + obj.added.Count + " anchors!");
                trackedObject = obj.added[0].gameObject;
            }
            if (obj.updated.Count != 0)
            {
                Debug.Log("Updated " + obj.updated.Count + " anchors!");
                trackedObject = obj.updated[0].gameObject;
            }
        }
        */

        /*
        public bool PublishPose
        {
            get => config.PublishPose;
            set
            {
                config.PublishPose = value;
                if (value && RosSenderHead == null)
                {
                    RosSenderHead = new RosSender<Msgs.GeometryMsgs.PoseStamped>(HeadPoseTopic);
                }
            }
        }
        */

        /*
        public bool PublishPlanesAsMarkers
        {
            get => config.PublishMarkers;
            set
            {
                config.PublishMarkers = value;
                planeManager.enabled = value;
                if (value && RosSenderMarkers == null)
                {
                    RosSenderMarkers = new RosSender<MarkerArray>(MarkersTopic);
                }
            }
        }
        */

        bool MarkerFound { get; set; }

        public bool UseMarker
        {
            get => config.SearchMarker;
            set
            {
                config.SearchMarker = value;
                tracker.enabled = value;
                resource.Visible = Visible && value;
                if (value)
                {
                    TFRoot.SetPose(RegisteredPose);
                    tracker.trackedImagesChanged += OnTrackedImagesChanged;
                }
                else
                {
                    WorldOffset = WorldOffset;
                    tracker.trackedImagesChanged -= OnTrackedImagesChanged;
                }
            }
        }

        public bool MarkerHorizontal
        {
            get => config.MarkerHorizontal;
            set
            {
                config.MarkerHorizontal = value;
                resource.Horizontal = value;
            }
        }

        public int MarkerAngle
        {
            get => config.MarkerAngle;
            set
            {
                config.MarkerAngle = value;
                resource.Angle = value; // deg
            }
        }

        public string MarkerFrame
        {
            get => config.MarkerFrame;
            set
            {
                config.MarkerFrame = value;
                node.AttachTo(config.MarkerFrame);
            }
        }

        public TFFrame Frame => node.Parent;

        public Vector3 MarkerOffset
        {
            get => config.MarkerOffset;
            set
            {
                config.MarkerOffset = value;
                resource.Offset = value.Ros2Unity();
            }
        }

        Pose RegisteredPose { get; set; } = Pose.identity;

        Pose RootPose
        {
            get
            {
                //Pose pose = RegisteredPose;
                Pose offsetPose = new Pose(
                    WorldOffset.Ros2Unity(),
                    Quaternion.AngleAxis(WorldAngle, Vector3.up)
                    );
                //Debug.Log("setting " + WorldOffset.Ros2Unity());
                //offsetPose.position += offsetPose.rotation * WorldOffset.Ros2Unity();
                return RegisteredPose.Multiply(offsetPose);
            }
        }

        void Awake()
        {
            if (canvas == null)
            {
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            if (mainCamera == null)
            {
                mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            }
            if (ARCamera == null)
            {
                ARCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
            }
            if (ARSessionOrigin == null)
            {
                ARSessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
            }

            planeManager = ARSessionOrigin.GetComponent<ARPlaneManager>();
            planeManager.planesChanged += OnPlanesChanged;
            
            tracker = ARSessionOrigin.GetComponent<ARTrackedImageManager>();
            raycaster = ARSessionOrigin.GetComponent<ARRaycastManager>();

            var cameraManager = ARCamera.GetComponent<ARCameraManager>();
            cameraManager.frameReceived += args =>
            {
                UpdateLights(args.lightEstimation);
            };
            
            node = DisplayClickableNode.Instantiate("AR Node");
            resource = ResourcePool.GetOrCreate<ARMarkerResource>(Resource.Displays.ARMarkerResource);
            node.Target = resource;
            MarkerFound = false;

            Config = new ARConfiguration();
            //Visible = true;

            if (savedSessionInfo != null)
            {
                WorldAngle = savedSessionInfo.WorldAngle;
                WorldOffset = savedSessionInfo.WorldOffset;
                WorldScale = savedSessionInfo.WorldScale;
            }
        }

        bool forceAnchorRebuild;
        void OnPlanesChanged(ARPlanesChangedEventArgs obj)
        {
            forceAnchorRebuild = true;
            //Debug.Log("force rebuild");
        }

        void HideAnchors()
        {
            TFListener.MapFrame.UpdateAnchor(null);
        }

        public bool FindAnchor(in Vector3 position, out Vector3 anchor, out Vector3 normal)
        {
            List<ARRaycastHit> results = new List<ARRaycastHit>();
            Vector3 origin = position + 0.25f * Vector3.up;
            raycaster.Raycast(new Ray(origin, Vector3.down), results, TrackableType.PlaneWithinBounds);
            if (results.Count == 0)
            {
                anchor = position;
                normal = Vector3.zero;
                return false;
            }

            var hit = results[0];
            var plane = planeManager.GetPlane(hit.trackableId);
            anchor = hit.pose.position;
            normal = plane.normal;
            return true;
        }
        
        void UpdateAnchors()
        {
            
            //foreach (TFFrame frame in TFListener.Instance.Frames.Values)
            //{
            //    frame.UpdateAnchor(FindAnchorFn, forceAnchorRebuild);    
            //}
            
            TFListener.MapFrame.UpdateAnchor(this, forceAnchorRebuild);
            forceAnchorRebuild = false;
        }

        void UpdateLights(ARLightEstimationData lightEstimation)
        {
            ARLight.intensity = 1;
            /*
            if (lightEstimation.averageBrightness.HasValue)
            {
                ARLight.intensity = lightEstimation.averageBrightness.Value;
            }
            */

            /*
            if (lightEstimation.averageColorTemperature.HasValue)
            {
                ARLight.colorTemperature = lightEstimation.averageColorTemperature.Value;
            } 
            */           
            
            /*
            if (lightEstimation.colorCorrection.HasValue)
            {
                ARLight.color = lightEstimation.colorCorrection.Value;
            }
            */

            if (lightEstimation.mainLightDirection.HasValue)
            {
                ARLight.transform.rotation = Quaternion.LookRotation(lightEstimation.mainLightDirection.Value);
            }

            if (lightEstimation.mainLightColor.HasValue)
            {
                ARLight.color = lightEstimation.mainLightColor.Value;
            }

            if (lightEstimation.ambientSphericalHarmonics.HasValue)
            {
                var sphericalHarmonics = lightEstimation.ambientSphericalHarmonics;
                RenderSettings.ambientMode = AmbientMode.Skybox;
                RenderSettings.ambientProbe = sphericalHarmonics.Value;
            }            
        }
        
        //uint headSeq = 0;
        public void Update()
        {
            UpdateAnchors();
            /*
            bool FindAnchorFn(in Vector3 position, out Vector3 anchor, out Vector3 normal)
            {
                //Debug.Log("was here");
                anchor = new Vector3(position.x, -1, position.z);
                normal = Vector3.up;
                return true;
            }
            */
            

            /*
            if (PublishPose && Visible)
            {
                TFListener.Publish(null, HeadFrameName, ARCamera.transform.AsPose());

                Msgs.GeometryMsgs.PoseStamped pose = new Msgs.GeometryMsgs.PoseStamped
                (
                    Header: RosUtils.CreateHeader(headSeq++),
                    Pose: ARCamera.transform.AsPose().Unity2RosPose()
                );
                RosSenderHead.Publish(pose);
            }
            if (PublishPlanesAsMarkers && Visible)
            {
                DateTime now = DateTime.Now;
                if ((now - lastMarkerUpdate).TotalSeconds > 2.5)
                {
                    lastMarkerUpdate = now;

                    int i = 0;
                    var trackables = planeManager.trackables;
                    Marker[] markers = new Marker[2 * trackables.count + 1];
                    markers[i++] = helper.CreateDeleteAll();
                    foreach (var trackable in trackables)
                    {
                        Mesh mesh = trackable?.gameObject.GetComponent<MeshFilter>()?.mesh;
                        if (mesh == null)
                        {
                            continue;
                        }
                        Marker[] meshMarkers = helper.MeshToMarker(mesh, trackable.transform.AsPose());
                        if (meshMarkers.Length == 2)
                        {
                            markers[i] = meshMarkers[0];
                            markers[i].Id = i;
                            i++;

                            markers[i] = meshMarkers[1];
                            markers[i].Id = i;
                            i++;
                        }
                    }
                    RosSenderMarkers.Publish(new MarkerArray(markers));
                }
            }
            */
            /*
            if (trackedObject != null)
            {
                Debug.Log(trackedObject.name + " -> " + trackedObject.transform.AsPose());
            }
            */
        }

        //GameObject trackedObject;

        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
        {
            Pose? newPose = null;
            
            if (obj.added.Count != 0)
            {
                //Debug.Log("Added " + obj.added.Count + " images!");
                newPose = obj.added[0].transform.AsPose();
            }
            if (obj.updated.Count != 0)
            {
                //Debug.Log("Updated " + obj.updated.Count + " images!");
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
            TFRoot.SetPose(RootPose);
            
            ModuleData.ResetPanel();
        }

        public void Stop()
        {
            savedSessionInfo = new ARSessionInfo()
            {
                WorldAngle = WorldAngle,
                WorldOffset = WorldOffset,
                WorldScale = WorldScale
            };
            
            Visible = false;
            //RosSenderHead?.Stop();
            //RosSenderMarkers?.Stop();

            WorldScale = 1;
            TFRoot.SetPose(Pose.identity);
        }

        void IController.Reset()
        {
        }
    }
}
