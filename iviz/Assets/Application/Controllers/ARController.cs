using UnityEngine;
using System.Collections;
using Iviz.RoslibSharp;
using System.Runtime.Serialization;
using System;
using Iviz.Resources;
using UnityEngine.XR.ARFoundation;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class ARConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.AR;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public SerializableVector3 Origin { get; set; } = Vector3.zero;
        [DataMember] public bool SearchMarker { get; set; } = false;
        [DataMember] public float WorldScale { get; set; } = 1.0f;
        [DataMember] public bool PublishPose { get; set; } = true;
        [DataMember] public bool PublishMarkers { get; set; } = true;
    }

    public class ARController : MonoBehaviour, IController
    {
        public Canvas Canvas;
        public Camera MainCamera;
        ARPlaneManager planeManager;

        readonly MeshToMarkerHelper helper = new MeshToMarkerHelper("ar");
        DateTime lastMarkerUpdate = DateTime.MinValue;

        GameObject TFRoot => TFListener.Instance.gameObject;

        public Camera ARCamera;
        public ARSessionOrigin ARSessionOrigin;

        public string HeadFrameName => $"{ConnectionManager.MyId}/ar_head";
        public string HeadPoseTopic => $"{ConnectionManager.MyId}/ar_head";
        public string MarkersTopic => $"{ConnectionManager.MyId}/ar_markers";

        RosSender<Msgs.GeometryMsgs.PoseStamped> rosSenderHead;
        RosSender<MarkerArray> rosSenderMarkers;

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
                MainCamera.gameObject.SetActive(!value);
                ARCamera.gameObject.SetActive(value);
                Canvas.worldCamera = value ? ARCamera : MainCamera;
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
                    if (rosSenderHead != null && rosSenderHead.Topic != HeadPoseTopic)
                    {
                        rosSenderHead.Stop();
                        rosSenderHead = null;
                    }
                    if (rosSenderHead == null)
                    {
                        rosSenderHead = new RosSender<Msgs.GeometryMsgs.PoseStamped>(HeadPoseTopic);
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
                    if (rosSenderMarkers != null && rosSenderMarkers.Topic != MarkersTopic)
                    {
                        rosSenderMarkers.Stop();
                        rosSenderMarkers = null;
                    }
                    if (rosSenderMarkers == null)
                    {
                        rosSenderMarkers = new RosSender<MarkerArray>(MarkersTopic);
                    }

                }
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
            planeManager = ARSessionOrigin.GetComponent<ARPlaneManager>();
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
                rosSenderHead.Publish(pose);
            }
            if (PublishMarkers)
            {
                DateTime now = DateTime.Now;
                if ((now - lastMarkerUpdate).TotalSeconds > 2.5)
                {
                    lastMarkerUpdate = now;

                    int i = 0;
                    //Debug.Log("planeManager: " + planeManager);
                    var trackables = planeManager.trackables;
                    //Debug.Log("trackables: " + trackables);
                    Marker[] markers = new Marker[2 * trackables.count];
                    foreach (var trackable in trackables)
                    {
                        Mesh mesh = trackable?.gameObject.GetComponent<MeshFilter>()?.mesh;
                        //Debug.Log("mesh: " + mesh);
                        Marker[] meshMarkers = helper.MeshToMarker(mesh, trackable.transform.AsPose());
                        markers[i++] = meshMarkers[0];
                        markers[i++] = meshMarkers[1];
                    }
                    //Debug.Log("sender: " + rosSenderMarkers);
                    rosSenderMarkers.Publish(new MarkerArray(markers));
                }
            }

        }

        public void Stop()
        {
            Visible = false;
        }
    }
}