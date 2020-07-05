using UnityEngine;
using System.Collections;
using Iviz.RoslibSharp;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using Iviz.Resources;
using UnityEngine.XR.ARFoundation;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.App.Displays;
using TMPro;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class ARConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.AR;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public float WorldScale { get; set; } = 1.0f;
        [DataMember] public SerializableVector3 Offset { get; set; } = Vector3.zero;
        [DataMember] public bool SearchMarker { get; set; } = false;
        [DataMember] public float MarkerSize { get; set; } = 0.198f;
        [DataMember] public bool MarkerHorizontal { get; set; } = true;
        [DataMember] public int MarkerAngle { get; set; } = 0;
        [DataMember] public string MarkerFrame { get; set; } = "";
        [DataMember] public SerializableVector3 MarkerOffset { get; set; } = Vector3.zero;
        //[DataMember] public bool PublishPose { get; set; } = false;
        //[DataMember] public bool PublishMarkers { get; set; } = false;
    }

    public sealed class ARController : MonoBehaviour, IController, IHasFrame
    {
        [SerializeField] Camera ARCamera = null;
        [SerializeField] ARSessionOrigin ARSessionOrigin = null;
        [SerializeField] Canvas Canvas = null;
        [SerializeField] Camera MainCamera = null;

        ARPlaneManager planeManager;
        ARTrackedImageManager tracker;
        ARRaycastManager raycaster;

        DisplayClickableNode node;
        ARMarkerResource resource;

        readonly MeshToMarkerHelper helper = new MeshToMarkerHelper("ar");
        DateTime lastMarkerUpdate = DateTime.MinValue;

        GameObject TFRoot => TFListener.RootFrame.gameObject;

        const string HeadPoseTopic = "ar_head";
        const string MarkersTopic = "ar_markers";

        static string HeadFrameName => ConnectionManager.Connection.MyId + "/ar_head";

        public RosSender<Msgs.GeometryMsgs.PoseStamped> RosSenderHead { get; private set; }
        public RosSender<MarkerArray> RosSenderMarkers { get; private set; }
        public ModuleData ModuleData { get; set; }

        readonly ARConfiguration config = new ARConfiguration();
        public ARConfiguration Config
        {
            get => config;
            set
            {
                Visible = value.Visible;
                Offset = value.Offset;
                WorldScale = value.WorldScale;
                //PublishPose = value.PublishPose;
                //PublishPlanesAsMarkers = value.PublishMarkers;
                UseMarker = value.SearchMarker;
                MarkerSize = value.MarkerSize;
                MarkerHorizontal = value.MarkerHorizontal;
                MarkerAngle = value.MarkerAngle;
                MarkerFrame = value.MarkerFrame;
                MarkerOffset = value.MarkerOffset;
            }
        }

        public Vector3 Offset
        {
            get => config.Offset;
            set
            {
                config.Offset = value;
                //Debug.Log(value);
                TFRoot.transform.SetPose(RootPose);
            }
        }

        public float WorldScale
        {
            get => config.WorldScale;
            set
            {
                config.WorldScale = value;
                TFRoot.transform.localScale = value * Vector3.one;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                resource.Visible = value && UseMarker;
                MainCamera.gameObject.SetActive(!value);
                ARCamera.gameObject.SetActive(value);
                Canvas.worldCamera = value ? ARCamera : MainCamera;
                TFListener.MainCamera = value ? ARCamera : MainCamera;

                if (value)
                {
                    TFRoot.transform.SetPose(RootPose);
                }
                else
                {
                    TFRoot.transform.SetPose(Pose.identity);
                }
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

        bool markerFound;
        public bool MarkerFound
        {
            get => markerFound;
            private set
            {
                markerFound = value;
            }
        }

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
                    TFRoot.transform.SetPose(RegisteredPose);
                    tracker.trackedImagesChanged += OnTrackedImagesChanged;
                }
                else
                {
                    Offset = Offset;
                    tracker.trackedImagesChanged -= OnTrackedImagesChanged;
                }
            }
        }

        public float MarkerSize
        {
            get => config.MarkerSize;
            set
            {
                config.MarkerSize = value;
                resource.Scale = value;
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

        public Pose RegisteredPose { get; set; } = Pose.identity;

        public Pose RootPose
        {
            get
            {
                Pose pose = RegisteredPose;
                pose.position += pose.rotation * Offset.Ros2Unity();
                return pose;
            }
        }

        void Awake()
        {
            if (Canvas == null)
            {
                Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            if (MainCamera == null)
            {
                MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
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
            tracker = ARSessionOrigin.GetComponent<ARTrackedImageManager>();
            raycaster = ARSessionOrigin.GetComponent<ARRaycastManager>();

            node = DisplayClickableNode.Instantiate("AR Node");
            resource = ResourcePool.GetOrCreate<ARMarkerResource>(Resource.Displays.ARMarkerResource);
            node.Target = resource;
            MarkerFound = false;

            Config = new ARConfiguration();
        }

        void UpdateARAnchors()
        {
            bool FindAnchorFn(in Vector3 position, out Vector3 anchor, out Vector3 normal)
            {
                List<ARRaycastHit> results = new List<ARRaycastHit>();
                raycaster.Raycast(new Ray(position, Vector3.down), results, TrackableType.PlaneWithinBounds);
                if (results.Count == 0)
                {
                    anchor = Vector3.zero;
                    normal = Vector3.zero;
                    return false;
                }

                var hit = results[0];
                var plane = planeManager.GetPlane(hit.trackableId);
                anchor = hit.pose.position;
                normal = plane.normal;
                return true;
            }
            
            foreach (TFFrame frame in TFListener.Instance.Frames.Values)
            {
                frame.UpdateAnchor(FindAnchorFn);    
            }            
        }

        //uint headSeq = 0;
        public void Update()
        {
            UpdateARAnchors();
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
                Debug.Log("Added " + obj.added.Count + " images!");
                newPose = obj.added[0].transform.AsPose();
            }
            if (obj.updated.Count != 0)
            {
                Debug.Log("Updated " + obj.updated.Count + " images!");
                newPose = obj.updated[0].transform.AsPose();
            }
            if (newPose == null)
            {
                return;
            }

            Pose expectedPose = TFListener.RelativePose(resource.transform.AsPose());
            RegisteredPose = newPose.Value.Multiply(expectedPose.Inverse());

            MarkerFound = true;
            TFRoot.transform.SetPose(RootPose);
        }

        public void Stop()
        {
            Visible = false;
            RosSenderHead?.Stop();
            RosSenderMarkers?.Stop();

            WorldScale = 1;
            TFRoot.transform.SetPose(Pose.identity);
        }
    }
}
