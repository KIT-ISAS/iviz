using System;
using System.Runtime.Serialization;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class MagnitudeConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Magnitude;
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

    public sealed class MagnitudeListener : ListenerController
    {
        SimpleDisplayNode displayNode;
        SimpleDisplayNode childNode;
        SimpleDisplayNode trailNode;
        AxisFrameResource axis;
        TrailResource trail;
        MeshMarkerResource sphere;
        ArrowResource arrow;

        public override ModuleData ModuleData { get; set; }

        public override TFFrame Frame => displayNode.Parent;

        readonly MagnitudeConfiguration config = new MagnitudeConfiguration();
        public MagnitudeConfiguration Config
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
                VectorScale = value.VectorScale;
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
                trail.Scale = 0.02f * value;
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
                    arrow.Scale = value;
                }
            }
        }

        void Awake()
        {
            displayNode = SimpleDisplayNode.Instantiate("DisplayNode");

            trailNode = SimpleDisplayNode.Instantiate("TrailNode", transform);
            trailNode.Parent = TFListener.BaseFrame;
            trail = trailNode.gameObject.AddComponent<TrailResource>();
            trail.DataSource = () => displayNode.transform.position;

            Config = new MagnitudeConfiguration();
        }

        public override void StartListening()
        {
            base.StartListening();

            name = "Magnitude:" + config.Topic;
            displayNode.name = $"[{config.Topic}]";
            displayNode.AttachTo("");
            //displayNode.DisplayData = DisplayData;

            switch (config.Type)
            {
                case PoseStamped.RosMessageType:
                    Listener = new RosListener<PoseStamped>(config.Topic, Handler);
                    goto case Msgs.GeometryMsgs.Pose.RosMessageType;

                case Msgs.GeometryMsgs.Pose.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new RosListener<Msgs.GeometryMsgs.Pose>(config.Topic, Handler);
                    }
                    axis = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Displays.AxisFrameResource, displayNode.transform);
                    break;

                case PointStamped.RosMessageType:
                    Listener = new RosListener<PointStamped>(config.Topic, Handler);
                    goto case Point.RosMessageType;

                case Point.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new RosListener<Point>(config.Topic, Handler);
                    }

                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Sphere, displayNode.transform);
                    sphere.transform.localScale = 0.1f * UnityEngine.Vector3.one;
                    sphere.Color = Color;
                    break;

                case WrenchStamped.RosMessageType:
                    Listener = new RosListener<WrenchStamped>(config.Topic, Handler);
                    goto case Wrench.RosMessageType;

                case Wrench.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new RosListener<Wrench>(config.Topic, Handler);
                    }
                    axis = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Displays.AxisFrameResource, displayNode.transform);
                    arrow = ResourcePool.GetOrCreate<ArrowResource>(Resource.Displays.Arrow, displayNode.transform);
                    arrow.Color = Color;
                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Sphere, displayNode.transform);
                    sphere.transform.localScale = 0.1f * UnityEngine.Vector3.one;
                    sphere.Color = Color;
                    break;

                case TwistStamped.RosMessageType:
                    Listener = new RosListener<TwistStamped>(config.Topic, Handler);
                    goto case Twist.RosMessageType;

                case Twist.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new RosListener<Twist>(config.Topic, Handler);
                    }
                    axis = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Displays.AxisFrameResource, displayNode.transform);
                    arrow = ResourcePool.GetOrCreate<ArrowResource>(Resource.Displays.Arrow, displayNode.transform);
                    arrow.Color = Color;
                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Sphere, displayNode.transform);
                    sphere.transform.localScale = 0.1f * UnityEngine.Vector3.one;
                    sphere.Color = Color;
                    break;

                case Odometry.RosMessageType:
                    Listener = new RosListener<Odometry>(config.Topic, Handler);
                    axis = ResourcePool.GetOrCreate<AxisFrameResource>(Resource.Displays.AxisFrameResource, displayNode.transform);

                    childNode = SimpleDisplayNode.Instantiate("ChildNode");
                    arrow = ResourcePool.GetOrCreate<ArrowResource>(Resource.Displays.Arrow, childNode.transform);
                    arrow.Color = Color;

                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Sphere, displayNode.transform);
                    sphere.transform.localScale = 0.1f * UnityEngine.Vector3.one;
                    sphere.Color = Color;
                    break;
            }
        }

        void Handler(PoseStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);
            Handler(msg.Pose);
        }

        void Handler(Msgs.GeometryMsgs.Pose msg)
        {
            if (msg.HasNaN())
            {
                return;
            }
            displayNode.transform.SetLocalPose(msg.Ros2Unity());
        }

        void Handler(PointStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);
            Handler(msg.Point);
        }

        void Handler(Point msg)
        {
            if (msg.HasNaN())
            {
                return;
            }
            displayNode.transform.localPosition = msg.Ros2Unity();
        }

        void Handler(WrenchStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);
            Handler(msg.Wrench);
        }

        void Handler(Wrench msg)
        {
            if (msg.Force.HasNaN() || msg.Torque.HasNaN())
            {
                return;
            }

            UnityEngine.Vector3 dir = msg.Force.Ros2Unity();
            arrow.Set(UnityEngine.Vector3.zero, dir);
            trail.DataSource = () => displayNode.transform.TransformPoint(dir * VectorScale);
        }

        void Handler(TwistStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);
            Handler(msg.Twist);
        }

        void Handler(Twist msg)
        {
            if (msg.Angular.HasNaN() || msg.Linear.HasNaN())
            {
                return;
            }

            UnityEngine.Vector3 dir = msg.Linear.Ros2Unity();
            arrow.Set(UnityEngine.Vector3.zero, dir);
            trail.DataSource = () => displayNode.transform.TransformPoint(dir * VectorScale);
        }

        void Handler(Odometry msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);
            childNode.AttachTo(msg.ChildFrameId);

            if (msg.Pose.Pose.HasNaN())
            {
                return;
            }
            if (msg.Twist.Twist.Angular.HasNaN() || msg.Twist.Twist.Linear.HasNaN())
            {
                return;
            }
            displayNode.transform.SetLocalPose(msg.Pose.Pose.Ros2Unity());

            UnityEngine.Vector3 dir = msg.Twist.Twist.Linear.Ros2Unity();
            arrow.Set(UnityEngine.Vector3.zero, dir);
            trail.DataSource = () => displayNode.transform.TransformPoint(dir * VectorScale);
        }

        public override void Stop()
        {
            base.Stop();

            trail.DataSource = null;

            displayNode.Stop();
            trailNode.Stop();
            Destroy(displayNode.gameObject);
            Destroy(trailNode.gameObject);

            if (childNode != null)
            {
                childNode.Stop();
                Destroy(childNode.gameObject);
            }

            if (axis != null)
            {
                ResourcePool.Dispose(Resource.Displays.AxisFrameResource, axis.gameObject);
            }
            if (arrow != null)
            {
                ResourcePool.Dispose(Resource.Displays.Arrow, arrow.gameObject);
            }
            if (sphere != null)
            {
                ResourcePool.Dispose(Resource.Displays.Sphere, sphere.gameObject);
            }
        }
    }
}