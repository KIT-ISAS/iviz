using System;
using System.Runtime.Serialization;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class OdometryConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.PointCloud;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public float Scale { get; set; } = 1.0f;
        [DataMember] public bool ShowTrail { get; set; } = true;
        [DataMember] public SerializableColor Color { get; set; } = UnityEngine.Color.red;
        [DataMember] public float TrailTime { get; set; } = 2.0f;
    }

    public class OdometryListener : TopicListener
    {
        DisplayClickableNode displayNode;
        SimpleDisplayNode trailNode;
        AxisResource axis;
        TrailResource trail;
        MeshMarkerResource sphere;

        public override DisplayData DisplayData
        {
            get => displayNode.DisplayData;
            set => displayNode.DisplayData = value;
        }

        readonly OdometryConfiguration config = new OdometryConfiguration();
        public OdometryConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                Visible = value.Visible;
                Scale = value.Scale;
                ShowTrail = value.ShowTrail;
                Color = value.Color;
                TrailTime = value.TrailTime;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                axis.Visible = value;
                trail.Visible = value && ShowTrail;
            }
        }

        public bool ShowTrail
        {
            get => config.ShowTrail;
            set
            {
                config.ShowTrail = value;
                trail.Visible = value && Visible;
            }
        }

        public Color Color
        {
            get => config.Color;
            set
            {
                config.Color = value;
                trail.Color = value;
                if (sphere != null)
                {
                    sphere.Color = value;
                }
            }
        }

        public float Scale
        {
            get => config.Scale;
            set
            {
                config.Scale = value;
                displayNode.transform.localScale = value * UnityEngine.Vector3.one;
                trail.Scale = 0.05f * value;
            }
        }

        public float TrailTime
        {
            get => config.TrailTime;
            set
            {
                config.TrailTime = value;
                trail.TimeWindowInMs = (int)(value * 1000);
            }
        }

        void Awake()
        {
            displayNode = DisplayClickableNode.Instantiate("DisplayNode");

            trailNode = SimpleDisplayNode.Instantiate("TrailNode", transform);
            trailNode.Parent = TFListener.BaseFrame;
            trail = trailNode.gameObject.AddComponent<TrailResource>();
            trail.DataSource = () => displayNode.transform.position;

        }

        public override void StartListening()
        {
            base.StartListening();
            switch(config.Type)
            {
                case PoseStamped.RosMessageType:
                    Listener = new RosListener<PoseStamped>(config.Topic, Handler);
                    name = "Odometry:" + config.Topic;
                    displayNode.SetName($"[{config.Topic}]");

                    axis = ResourcePool.GetOrCreate<AxisResource>(Resource.Markers.Axis);
                    displayNode.Target = axis;
                    break;

                case PointStamped.RosMessageType:
                    Listener = new RosListener<PointStamped>(config.Topic, Handler);
                    name = "Odometry:" + config.Topic;
                    displayNode.SetName($"[{config.Topic}]");

                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Markers.Sphere);
                    sphere.transform.localScale = 0.125f * UnityEngine.Vector3.one;
                    sphere.Color = Color;
                    displayNode.Target = sphere;
                    break;
            }
        }

        void Handler(PoseStamped msg)
        {
            displayNode.SetParent(msg.Header.FrameId);
            displayNode.transform.SetLocalPose(msg.Pose.Ros2Unity());
        }

        void Handler(PointStamped msg)
        {
            displayNode.SetParent(msg.Header.FrameId);
            displayNode.transform.localPosition = msg.Point.Ros2Unity();
        }
    }
}