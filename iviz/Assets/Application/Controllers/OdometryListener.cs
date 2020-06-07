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
        [DataMember] public bool ShowTrail { get; set; } = true;
        [DataMember] public bool ShowAxis { get; set; } = true;
        [DataMember] public float Scale { get; set; } = 1.0f;
        [DataMember] public bool ShowVector { get; set; } = true;
        [DataMember] public float VectorScale { get; set; } = 1.0f;
        [DataMember] public SerializableColor Color { get; set; } = UnityEngine.Color.red;
        [DataMember] public float TrailTime { get; set; } = 2.0f;
    }

    public class OdometryListener : TopicListener
    {
        DisplayClickableNode displayNode;
        SimpleDisplayNode trailNode;
        AxisFrameResource axis;
        TrailResource trail;
        MeshMarkerResource sphere;
        ArrowResource arrow;

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
                ShowAxis = value.ShowAxis;
                ShowVector = value.ShowVector;
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
                if (axis != null)
                {
                    axis.Visible = value && ShowAxis;
                }
                if (sphere != null)
                {
                    sphere.Visible = value && ShowAxis;
                }
                trail.Visible = value && ShowTrail;
                if (arrow != null)
                {
                    arrow.Visible = value && ShowVector;
                }
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

        public bool ShowAxis
        {
            get => config.ShowAxis;
            set
            {
                config.ShowAxis = value;
                if (axis != null)
                {
                    axis.Visible = value && Visible;
                }
                if (sphere != null)
                {
                    sphere.Visible = value && Visible;
                }
            }
        }

        public bool ShowVector
        {
            get => config.ShowVector;
            set
            {
                config.ShowVector = value;
                if (arrow != null)
                {
                    arrow.Visible = value && Visible;
                }
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
                if (arrow != null)
                {
                    arrow.Color = value;
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

        public float VectorScale
        {
            get => config.VectorScale;
            set
            {
                config.VectorScale = value;
                if (arrow != null)
                {
                    arrow.transform.localScale = value * UnityEngine.Vector3.one;
                }
            }
        }

        void Awake()
        {
            displayNode = DisplayClickableNode.Instantiate("DisplayNode");

            trailNode = SimpleDisplayNode.Instantiate("TrailNode", transform);
            trailNode.Parent = TFListener.BaseFrame;
            trail = trailNode.gameObject.AddComponent<TrailResource>();
            trail.DataSource = () => displayNode.transform.position;

            Config = new OdometryConfiguration();
        }

        public override void StartListening()
        {
            base.StartListening();

            name = "Odometry:" + config.Topic;
            displayNode.SetName($"[{config.Topic}]");

            switch (config.Type)
            {
                case PoseStamped.RosMessageType:
                    Listener = new RosListener<PoseStamped>(config.Topic, Handler);
                    axis = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Markers.Axis);
                    displayNode.Target = axis;
                    break;

                case PointStamped.RosMessageType:
                    Listener = new RosListener<PointStamped>(config.Topic, Handler);

                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Markers.Sphere);
                    sphere.transform.localScale = 0.125f * UnityEngine.Vector3.one;
                    sphere.Color = Color;
                    displayNode.Target = sphere;
                    break;

                case WrenchStamped.RosMessageType:
                    Listener = new RosListener<WrenchStamped>(config.Topic, Handler);
                    axis = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Markers.Axis);
                    displayNode.Target = axis;
                    arrow = ResourcePool.GetOrCreate<ArrowResource>(Resource.Markers.Arrow);
                    arrow.Parent = displayNode.transform.parent;
                    break;

                case TwistStamped.RosMessageType:
                    Listener = new RosListener<TwistStamped>(config.Topic, Handler);
                    axis = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Markers.Axis);
                    displayNode.Target = axis;
                    arrow = ResourcePool.GetOrCreate<ArrowResource>(Resource.Markers.Arrow);
                    arrow.Parent = displayNode.transform.parent;
                    break;
            }
        }

        void Handler(PoseStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);
            displayNode.transform.SetLocalPose(msg.Pose.Ros2Unity());
        }

        void Handler(PointStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);
            displayNode.transform.localPosition = msg.Point.Ros2Unity();
        }

        void Handler(WrenchStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);

            UnityEngine.Vector3 dir = msg.Wrench.Force.Ros2Unity();
            arrow.Set(UnityEngine.Vector3.zero, dir);
            trail.DataSource = () => displayNode.transform.TransformPoint(dir);
        }

        void Handler(TwistStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);

            UnityEngine.Vector3 dir = msg.Twist.Linear.Ros2Unity();
            arrow.Set(UnityEngine.Vector3.zero, dir);
            trail.DataSource = () => displayNode.transform.TransformPoint(dir);
        }
    }
}