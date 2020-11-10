using System;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Displays;
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
        [DataMember] public bool FrameVisible { get; set; } = true;
        [DataMember] public float Scale { get; set; } = 1.0f;
        [DataMember] public bool VectorVisible { get; set; } = true;
        [DataMember] public float VectorScale { get; set; } = 1.0f;
        [DataMember] public SerializableColor Color { get; set; } = UnityEngine.Color.red;
        [DataMember] public float TrailTime { get; set; } = 2.0f;
    }

    public sealed class MagnitudeListener : ListenerController
    {
        readonly SimpleDisplayNode displayNode;
        SimpleDisplayNode childNode;
        AxisFrameResource axis;
        readonly TrailResource trail;
        MeshMarkerResource sphere;
        ArrowResource arrow;
        AngleAxisResource angleAxis;

        public override IModuleData ModuleData { get; }

        public override TfFrame Frame => displayNode.Parent;

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
                TrailVisible = value.TrailVisible;
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
                if (!(axis is null))
                {
                    axis.Visible = value && FrameVisible;
                }

                if (!(sphere is null))
                {
                    sphere.Visible = value && FrameVisible;
                }

                trail.Visible = value && TrailVisible;
                if (!(arrow is null))
                {
                    arrow.Visible = value && VectorVisible;
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
                if (!(axis is null))
                {
                    axis.Visible = value && Visible;
                }

                if (!(sphere is null))
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
                if (!(arrow is null))
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
                if (!(sphere is null))
                {
                    sphere.Color = value;
                }

                if (!(arrow is null))
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
                displayNode.transform.localScale = value * Vector3.one;
                trail.ElementScale = 0.02f * value;
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

            displayNode = SimpleDisplayNode.Instantiate("DisplayNode");

            trail = ResourcePool.GetOrCreateDisplay<TrailResource>();
            trail.DataSource = () => displayNode.transform.position;

            Config = new MagnitudeConfiguration();
        }

        public override void StartListening()
        {
            //name = "Magnitude:" + config.Topic;
            displayNode.name = $"[{config.Topic}]";
            displayNode.AttachTo("");
            //displayNode.DisplayData = DisplayData;

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

                    axis = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(displayNode.transform);
                    break;

                case PointStamped.RosMessageType:
                    Listener = new Listener<PointStamped>(config.Topic, Handler);
                    goto case Point.RosMessageType;

                case Point.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new Listener<Point>(config.Topic, Handler);
                    }

                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Sphere,
                        displayNode.transform);
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

                    axis = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(displayNode.transform);
                    arrow = ResourcePool.GetOrCreateDisplay<ArrowResource>(displayNode.transform);
                    arrow.Color = Color;
                    arrow.Set(Vector3.one * 0.01f);
                    angleAxis = ResourcePool.GetOrCreateDisplay<AngleAxisResource>(displayNode.transform);
                    angleAxis.Color = Color.yellow;
                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Sphere,
                        displayNode.transform);
                    sphere.transform.localScale = 0.05f * Vector3.one;
                    sphere.Color = Color;
                    break;

                case TwistStamped.RosMessageType:
                    Listener = new Listener<TwistStamped>(config.Topic, Handler);
                    goto case Twist.RosMessageType;

                case Twist.RosMessageType:
                    if (Listener == null)
                    {
                        Listener = new Listener<Twist>(config.Topic, Handler);
                    }

                    axis = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(displayNode.transform);
                    arrow = ResourcePool.GetOrCreateDisplay<ArrowResource>(displayNode.transform);
                    arrow.Color = Color;
                    arrow.Set(Vector3.one * 0.01f);
                    angleAxis = ResourcePool.GetOrCreateDisplay<AngleAxisResource>(displayNode.transform);
                    angleAxis.Color = Color.yellow;
                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Sphere,
                        displayNode.transform);
                    sphere.transform.localScale = 0.05f * Vector3.one;
                    sphere.Color = Color;
                    break;

                case Odometry.RosMessageType:
                    Listener = new Listener<Odometry>(config.Topic, Handler);
                    axis = ResourcePool.GetOrCreateDisplay<AxisFrameResource>(displayNode.transform);
                    childNode = SimpleDisplayNode.Instantiate("ChildNode");
                    arrow = ResourcePool.GetOrCreateDisplay<ArrowResource>(childNode.transform);
                    arrow.Color = Color;
                    arrow.Set(Vector3.one * 0.01f);
                    angleAxis = ResourcePool.GetOrCreateDisplay<AngleAxisResource>(childNode.transform);
                    angleAxis.Color = Color.yellow;
                    sphere = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Sphere,
                        displayNode.transform);
                    sphere.transform.localScale = 0.05f * Vector3.one;
                    sphere.Color = Color;
                    break;
            }
        }

        void Handler([NotNull] PoseStamped msg)
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

        void Handler([NotNull] PointStamped msg)
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

        void Handler([NotNull] WrenchStamped msg)
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

            Vector3 dir = msg.Force.Ros2Unity();
            arrow.Set(Vector3.zero, dir * VectorScale);
            angleAxis.Set(msg.Torque.Ros2Unity());
            trail.DataSource = () => displayNode.transform.TransformPoint(dir * VectorScale);
        }

        void Handler([NotNull] TwistStamped msg)
        {
            displayNode.AttachTo(msg.Header.FrameId);
            Handler(msg.Twist);
        }

        static Quaternion AngularToQuaternion(float angularX, float angularY, float angularZ)
        {
            //Debug.Log("In message: " + angularX + " " + angularY + " " + angularZ);
            /*
            return Quaternion.AngleAxis(angularX * Mathf.Rad2Deg, Vector3.right) *
                   Quaternion.AngleAxis(angularY * Mathf.Rad2Deg, Vector3.up) *
                   Quaternion.AngleAxis(angularZ * Mathf.Rad2Deg, Vector3.forward);
                   */
            return new Vector3(angularX, angularY, angularZ).RosRpy2Unity();
        }

        void Handler(Twist msg)
        {
            if (msg.Angular.HasNaN() || msg.Linear.HasNaN())
            {
                return;
            }

            Vector3 dir = msg.Linear.Ros2Unity();
            arrow.Set(Vector3.zero, dir * VectorScale);
            angleAxis.Set(AngularToQuaternion((float) msg.Angular.X, (float) msg.Angular.Y, (float) msg.Angular.Z));
            trail.DataSource = () => displayNode.transform.TransformPoint(dir * VectorScale);
        }

        void Handler([NotNull] Odometry msg)
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

            Vector3 dir = msg.Twist.Twist.Linear.Ros2Unity();
            arrow.Set(Vector3.zero, dir * VectorScale);
            angleAxis.Set(AngularToQuaternion(
                (float) msg.Twist.Twist.Angular.X,
                (float) msg.Twist.Twist.Angular.Y,
                (float) msg.Twist.Twist.Angular.Z));
            trail.DataSource = () => displayNode.transform.TransformPoint(dir * VectorScale);
        }

        public override void ResetController()
        {
            base.ResetController();
            arrow?.Reset();
            trail.Reset();
            angleAxis?.Reset();
        }

        public override void StopController()
        {
            base.StopController();

            trail.DataSource = null;

            displayNode.Stop();
            UnityEngine.Object.Destroy(displayNode.gameObject);

            trail?.DisposeDisplay();

            if (childNode != null)
            {
                childNode.Stop();
                UnityEngine.Object.Destroy(childNode.gameObject);
            }

            axis?.DisposeDisplay();
            angleAxis?.DisposeDisplay();
            arrow?.DisposeDisplay();
            sphere?.DisposeResource(Resource.Displays.Sphere);
        }
    }
}