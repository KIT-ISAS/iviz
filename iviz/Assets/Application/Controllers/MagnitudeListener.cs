using System;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class MagnitudeConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.Magnitude;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool TrailVisible { get; set; } = false;
        [DataMember] public bool AngleVisible { get; set; } = true;
        [DataMember] public bool FrameVisible { get; set; } = true;
        [DataMember] public float Scale { get; set; } = 1.0f;
        [DataMember] public bool VectorVisible { get; set; } = true;
        [DataMember] public float VectorScale { get; set; } = 1.0f;
        [DataMember] public float ScaleMultiplierPow10 { get; set; } = 0;
        [DataMember] public SerializableColor Color { get; set; } = UnityEngine.Color.red;
        [DataMember] public float TrailTime { get; set; } = 2.0f;
    }

    public sealed class MagnitudeListener : ListenerController
    {
        readonly FrameNode frameNode;
        FrameNode childNode;
        AxisFrameResource axis;
        readonly TrailResource trail;
        MeshMarkerResource sphere;
        ArrowResource arrow;
        AngleAxisResource angleAxis;
        
        Vector3 dirForDataSource;

        readonly MagnitudeConfiguration config = new MagnitudeConfiguration();

        public override IModuleData ModuleData { get; }
        public override TfFrame Frame => frameNode.Parent;

        public MagnitudeConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                Visible = value.Visible;
                Scale = value.Scale;
                TrailVisible = value.TrailVisible;
                AngleVisible = value.AngleVisible;
                FrameVisible = value.FrameVisible;
                VectorVisible = value.VectorVisible;
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
                    axis.Visible = value && FrameVisible;
                }

                if (sphere != null)
                {
                    sphere.Visible = value && FrameVisible;
                }

                trail.Visible = value && TrailVisible;
                if (arrow != null)
                {
                    arrow.Visible = value && VectorVisible;
                }

                if (angleAxis != null)
                {
                    angleAxis.Visible = value && AngleVisible;
                }
            }
        }

        public bool TrailVisible
        {
            get => config.TrailVisible;
            set
            {
                config.TrailVisible = value;
                trail.Visible = value && Visible;
            }
        }

        public bool FrameVisible
        {
            get => config.FrameVisible;
            set
            {
                config.FrameVisible = value;
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

        public bool VectorVisible
        {
            get => config.VectorVisible;
            set
            {
                config.VectorVisible = value;
                if (arrow != null)
                {
                    arrow.Visible = value && Visible;
                }
            }
        }
        
        public bool AngleVisible
        {
            get => config.AngleVisible;
            set
            {
                config.AngleVisible = value;
                if (angleAxis != null)
                {
                    angleAxis.Visible = value && Visible;
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
                frameNode.Transform.localScale = value * scaleMultiplier * Vector3.one;
                trail.ElementScale = 0.02f * value * scaleMultiplier;
            }
        }

        float scaleMultiplier = 1;
        public float ScaleMultiplierPow10
        {
            get => config.ScaleMultiplierPow10;
            set
            {
                config.ScaleMultiplierPow10 = value;
                scaleMultiplier = Mathf.Pow(10, value);
                Scale = Scale;
                VectorScale = VectorScale;
            }
        }


        public float TrailTime
        {
            get => config.TrailTime;
            set
            {
                config.TrailTime = value;
                trail.TimeWindowInMs = (int) (value * 1000);
            }
        }

        public float VectorScale
        {
            get => config.VectorScale;
            set => config.VectorScale = value;
        }

        public MagnitudeListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            frameNode = FrameNode.Instantiate("DisplayNode");

            trail = ResourcePool.RentDisplay<TrailResource>();
            trail.DataSource = () => frameNode.Transform.position;

            Config = new MagnitudeConfiguration();
        }

        public override void StartListening()
        {
            frameNode.name = $"[{config.Topic}]";
            frameNode.AttachTo("");

            switch (config.Type)
            {
                case PoseStamped.RosMessageType:
                    Listener = new Listener<PoseStamped>(config.Topic, Handler);
                    goto case Msgs.GeometryMsgs.Pose.RosMessageType;

                case Msgs.GeometryMsgs.Pose.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new Listener<Msgs.GeometryMsgs.Pose>(config.Topic, Handler);
                    }

                    axis = ResourcePool.RentDisplay<AxisFrameResource>(frameNode.Transform);
                    break;

                case PointStamped.RosMessageType:
                    Listener = new Listener<PointStamped>(config.Topic, Handler);
                    goto case Point.RosMessageType;

                case Point.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new Listener<Point>(config.Topic, Handler);
                    }

                    sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere,
                        frameNode.Transform);
                    sphere.transform.localScale = 0.05f * Vector3.one;
                    sphere.Color = Color;
                    break;

                case WrenchStamped.RosMessageType:
                    Listener = new Listener<WrenchStamped>(config.Topic, Handler);
                    goto case Wrench.RosMessageType;

                case Wrench.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new Listener<Wrench>(config.Topic, Handler);
                    }

                    axis = ResourcePool.RentDisplay<AxisFrameResource>(frameNode.Transform);
                    arrow = ResourcePool.RentDisplay<ArrowResource>(frameNode.Transform);
                    arrow.Color = Color;
                    arrow.Set(Vector3.one * 0.01f);
                    angleAxis = ResourcePool.RentDisplay<AngleAxisResource>(frameNode.Transform);
                    angleAxis.Color = Color.yellow;
                    sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere,
                        frameNode.Transform);
                    sphere.transform.localScale = 0.05f * Vector3.one;
                    sphere.Color = Color;
                    trail.DataSource = TrailDataSource;
                    break;

                case TwistStamped.RosMessageType:
                    Listener = new Listener<TwistStamped>(config.Topic, Handler);
                    goto case Twist.RosMessageType;

                case Twist.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new Listener<Twist>(config.Topic, Handler);
                    }

                    axis = ResourcePool.RentDisplay<AxisFrameResource>(frameNode.Transform);
                    arrow = ResourcePool.RentDisplay<ArrowResource>(frameNode.Transform);
                    arrow.Color = Color;
                    arrow.Set(Vector3.one * 0.01f);
                    angleAxis = ResourcePool.RentDisplay<AngleAxisResource>(frameNode.Transform);
                    angleAxis.Color = Color.yellow;
                    sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere,
                        frameNode.Transform);
                    sphere.transform.localScale = 0.05f * Vector3.one;
                    sphere.Color = Color;
                    trail.DataSource = TrailDataSource;
                    break;

                case Odometry.RosMessageType:
                    Listener = new Listener<Odometry>(config.Topic, Handler);
                    axis = ResourcePool.RentDisplay<AxisFrameResource>(frameNode.Transform);
                    childNode = FrameNode.Instantiate("ChildNode");
                    arrow = ResourcePool.RentDisplay<ArrowResource>(childNode.Transform);
                    arrow.Color = Color;
                    arrow.Set(Vector3.one * 0.01f);
                    angleAxis = ResourcePool.RentDisplay<AngleAxisResource>(childNode.Transform);
                    angleAxis.Color = Color.yellow;
                    sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere,
                        frameNode.Transform);
                    sphere.transform.localScale = 0.05f * Vector3.one;
                    sphere.Color = Color;
                    trail.DataSource = TrailDataSource;
                    break;
            }
        }

        void Handler([NotNull] PoseStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Pose);
        }

        void Handler(Msgs.GeometryMsgs.Pose msg)
        {
            if (msg.HasNaN())
            {
                return;
            }

            frameNode.Transform.SetLocalPose(msg.Ros2Unity());
        }

        void Handler([NotNull] PointStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Point);
        }

        void Handler(Point msg)
        {
            if (msg.HasNaN())
            {
                return;
            }

            frameNode.Transform.localPosition = msg.Ros2Unity();
        }

        void Handler([NotNull] WrenchStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Wrench);
        }

        Vector3 TrailDataSource() => frameNode.Transform.TransformPoint(dirForDataSource * (VectorScale * scaleMultiplier));

        void Handler([NotNull] Wrench msg)
        {
            if (msg.Force.HasNaN() || msg.Torque.HasNaN())
            {
                return;
            }

            Vector3 dir = msg.Force.Ros2Unity();
            arrow.Set(Vector3.zero, dir * (VectorScale * scaleMultiplier));
            angleAxis.Set(msg.Torque.Ros2Unity());
            dirForDataSource = dir;
        }

        void Handler([NotNull] TwistStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Twist);
        }

        void Handler(Twist msg)
        {
            var (linear, angular) = msg;
            if (angular.HasNaN() || linear.HasNaN())
            {
                return;
            }

            Vector3 dir = linear.Ros2Unity();
            arrow.Set(Vector3.zero, dir * (VectorScale * scaleMultiplier));
            angleAxis.Set(angular.RosRpy2Unity());
            dirForDataSource = dir;
        }

        void Handler([NotNull] Odometry msg)
        {
            frameNode.AttachTo(msg.Header);
            childNode.AttachTo(msg.ChildFrameId);

            if (msg.Pose.Pose.HasNaN())
            {
                return;
            }

            var (linear, angular) = msg.Twist.Twist;
            if (angular.HasNaN() || linear.HasNaN())
            {
                return;
            }

            frameNode.Transform.SetLocalPose(msg.Pose.Pose.Ros2Unity());

            Vector3 dir = linear.Ros2Unity();
            arrow.Set(Vector3.zero, dir * (VectorScale * scaleMultiplier));
            angleAxis.Set(angular.RosRpy2Unity());
            dirForDataSource = dir;
        }

        public override void ResetController()
        {
            base.ResetController();
            if (arrow != null)
            {
                arrow.Reset();
            }

            trail.Reset();

            if (angleAxis != null)
            {
                angleAxis.Reset();
            }
        }

        public override void StopController()
        {
            base.StopController();

            trail.DataSource = null;

            frameNode.Stop();
            UnityEngine.Object.Destroy(frameNode.gameObject);

            trail.ReturnToPool();

            if (childNode != null)
            {
                childNode.Stop();
                UnityEngine.Object.Destroy(childNode.gameObject);
            }

            axis.ReturnToPool();
            angleAxis.ReturnToPool();
            arrow.ReturnToPool();
            sphere.ReturnToPool(Resource.Displays.Sphere);
        }
    }
}