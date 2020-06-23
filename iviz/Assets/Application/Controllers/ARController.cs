using UnityEngine;
using System.Collections;
using Iviz.RoslibSharp;
using System.Runtime.Serialization;
using System;
using Iviz.Resources;
using UnityEngine.XR.ARFoundation;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.App.Displays;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class ARConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.AR;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public float WorldScale { get; set; } = 1.0f;
        [DataMember] public SerializableVector3 Origin { get; set; } = new Vector3(0, 0, 1.5f);
        [DataMember] public bool SearchMarker { get; set; } = false;
        [DataMember] public float MarkerSize { get; set; } = 0.198f;
        [DataMember] public bool MarkerHorizontal { get; set; } = true;
        [DataMember] public int MarkerAngle { get; set; } = 0;
        [DataMember] public string MarkerFrame { get; set; } = "";
        [DataMember] public SerializableVector3 MarkerOffset { get; set; } = Vector3.zero;
        [DataMember] public bool PublishPose { get; set; } = false;
        [DataMember] public bool PublishMarkers { get; set; } = false;
    }

    public sealed class ARController : MonoBehaviour, IController, IHasFrame
    {
        [SerializeField] Camera ARCamera;
        [SerializeField] ARSessionOrigin ARSessionOrigin;
        [SerializeField] Canvas Canvas;
        [SerializeField] Camera MainCamera;

        ARPlaneManager planeManager;
        ARTrackedImageManager tracker;

        DisplayClickableNode node;
        ARMarkerResource resource;

        readonly MeshToMarkerHelper helper = new MeshToMarkerHelper("ar");
        DateTime lastMarkerUpdate = DateTime.MinValue;

        GameObject TFRoot => TFListener.RootFrame.gameObject;

        public string HeadFrameName => ConnectionManager.Connection.MyId + "/ar_head";
        public const string HeadPoseTopic = "ar_head";
        public const string MarkersTopic = "ar_markers";

        public RosSender<Msgs.GeometryMsgs.PoseStamped> RosSenderHead { get; private set; }
        public RosSender<MarkerArray> RosSenderMarkers { get; private set; }

        public ModuleData ModuleData { get; set; }

        readonly ARConfiguration config = new ARConfiguration();
        public ARConfiguration Config
        {
            get => config;
            set
            {
                Visible = config.Visible;
                Origin = config.Origin;
                WorldScale = config.WorldScale;
                PublishPose = config.PublishPose;
                PublishPlanesAsMarkers = config.PublishMarkers;
                SearchMarker = config.SearchMarker;
                MarkerSize = config.MarkerSize;
                MarkerHorizontal = config.MarkerHorizontal;
                MarkerAngle = config.MarkerAngle;
                MarkerFrame = config.MarkerFrame;
                MarkerOffset = config.MarkerOffset;
            }
        }

        public Vector3 Origin
        {
            get => config.Origin;
            set
            {
                config.Origin = value;
                if (!SearchMarker)
                {
                    TFRoot.transform.position = -value.Ros2Unity();
                }
                //ARSessionOrigin.transform.position = value.Ros2Unity();
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
                resource.Visible = value && MarkerFound;
                MainCamera.gameObject.SetActive(!value);
                ARCamera.gameObject.SetActive(value);
                Canvas.worldCamera = value ? ARCamera : MainCamera;
                TFListener.MainCamera = value ? ARCamera : MainCamera;
            }
        }

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

        bool markerFound;
        public bool MarkerFound
        {
            get => markerFound;
            private set
            {
                markerFound = value;
                resource.Visible = Visible && value;
            }
        }

        public bool SearchMarker
        {
            get => config.SearchMarker;
            set
            {
                config.SearchMarker = value;
                tracker.enabled = value;
                if (value)
                {
                    TFRoot.transform.SetPose(Pose.identity);
                    tracker.trackedImagesChanged += OnTrackedImagesChanged;
                }
                else
                {
                    Origin = Origin;
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

            node = DisplayClickableNode.Instantiate("AR Node");
            resource = ResourcePool.GetOrCreate<ARMarkerResource>(Resource.Displays.ARMarkerResource);
            node.Target = resource;
            MarkerFound = false;

            Config = new ARConfiguration();
        }

        uint headSeq = 0;
        public void Update()
        {
            if (PublishPose)
            {
                TFListener.Publish(null, HeadFrameName, ARCamera.transform.AsPose());

                Msgs.GeometryMsgs.PoseStamped pose = new Msgs.GeometryMsgs.PoseStamped
                (
                    Header: RosUtils.CreateHeader(headSeq++),
                    Pose: ARCamera.transform.AsPose().Unity2RosPose()
                );
                RosSenderHead.Publish(pose);
            }
            if (PublishPlanesAsMarkers)
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

        }

        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
        {
            Pose newPose = new Pose();
            if (obj.added.Count != 0)
            {
                Debug.Log("Added " + obj.added.Count + " images!");
                /*
                transform.position = obj.added[0].transform.position;
                transform.rotation = obj.added[0].transform.rotation;
                */
                newPose = obj.added[0].transform.AsPose();
            }
            if (obj.updated.Count != 0)
            {
                Debug.Log("Updated " + obj.updated.Count + " images!");
                /*
                transform.position = obj.updated[0].transform.position;
                transform.rotation = obj.updated[0].transform.rotation;
                */
                newPose = obj.updated[0].transform.AsPose();
            }
            Pose expectedPose = TFListener.RelativePose(resource.transform.AsPose());
            Pose change = newPose.Multiply(expectedPose.Inverse());

            MarkerFound = true;
            TFRoot.transform.SetPose(change);

            //Debug.Log("Found pose: " + newPose + "\nExpected pose: " + expectedPose + "\nChange: " + change);
            //Debug.Log("Found pose: " + newPose + "\nCamera pose: " + ARCamera.transform.AsPose());
        }

        public void Stop()
        {
            Visible = false;
            RosSenderHead.Stop();
            RosSenderMarkers.Stop();
        }
    }
}
