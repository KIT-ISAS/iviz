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
    public class ARConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.AR;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public float WorldScale { get; set; } = 1.0f;
        [DataMember] public SerializableVector3 Origin { get; set; } = new Vector3(0, 0, 1.5f);
        [DataMember] public bool SearchMarker { get; set; } = false;
        [DataMember] public float MarkerSize { get; set; } = 0.19f;
        [DataMember] public bool MarkerHorizontal { get; set; } = false;
        [DataMember] public int MarkerAngle { get; set; } = 0;
        [DataMember] public string MarkerFrame { get; set; } = "";
        [DataMember] public SerializableVector3 MarkerOffset { get; set; } = Vector3.zero;
        [DataMember] public bool PublishPose { get; set; } = true;
        [DataMember] public bool PublishMarkers { get; set; } = true;
    }

    public class ARController : MonoBehaviour, IController
    {
        public Canvas Canvas;
        public Camera MainCamera;
        ARPlaneManager planeManager;

        DisplayClickableNode node;
        ARMarkerResource resource;

        readonly MeshToMarkerHelper helper = new MeshToMarkerHelper("ar");
        DateTime lastMarkerUpdate = DateTime.MinValue;

        GameObject TFRoot => TFListener.Instance.gameObject;

        public Camera ARCamera;
        public ARSessionOrigin ARSessionOrigin;

        public string HeadFrameName => $"{ConnectionManager.MyId}/ar_head";
        public string HeadPoseTopic => $"{ConnectionManager.MyId}/ar_head";
        public string MarkersTopic => $"{ConnectionManager.MyId}/ar_markers";

        public RosSender<Msgs.GeometryMsgs.PoseStamped> RosSenderHead { get; private set; }
        public RosSender<MarkerArray> RosSenderMarkers { get; private set; }

        public DisplayData DisplayData { get; set; }

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
                PublishMarkers = config.PublishMarkers;
            }
        }

        public Vector3 Origin
        {
            get => config.Origin;
            set
            {
                config.Origin = value;
                ARSessionOrigin.gameObject.transform.position = value.Ros2Unity();
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
                resource.Visible = MarkerFound && value;
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
                if (value)
                {
                    if (RosSenderHead != null && RosSenderHead.Topic != HeadPoseTopic)
                    {
                        RosSenderHead.Stop();
                        RosSenderHead = null;
                    }
                    if (RosSenderHead == null)
                    {
                        RosSenderHead = new RosSender<Msgs.GeometryMsgs.PoseStamped>(HeadPoseTopic);
                    }
                }
            }
        }

        public bool PublishMarkers
        {
            get => config.PublishMarkers;
            set
            {
                config.PublishMarkers = value;
                planeManager.gameObject.SetActive(value);
                if (value)
                {
                    if (RosSenderMarkers != null && RosSenderMarkers.Topic != MarkersTopic)
                    {
                        RosSenderMarkers.Stop();
                        RosSenderMarkers = null;
                    }
                    if (RosSenderMarkers == null)
                    {
                        RosSenderMarkers = new RosSender<MarkerArray>(MarkersTopic);
                    }

                }
            }
        }

        public bool MarkerFound { get; private set; }

        public bool SearchMarker
        {
            get => config.SearchMarker;
            set
            {
                config.SearchMarker = value;
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
                resource.Angle = 45 * value; // deg
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

        public Vector3 MarkerOffset
        {
            get => config.MarkerOffset;
            set
            {
                config.MarkerOffset = value;
                resource.Offset = value;
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
            Config = new ARConfiguration();

            node = DisplayClickableNode.Instantiate("AR Node");
            resource = ResourcePool.GetOrCreate<ARMarkerResource>(Resource.Markers.ARMarkerResource);
            node.Target = resource;
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
            if (PublishMarkers)
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

        public void Stop()
        {
            Visible = false;
        }
    }
}
