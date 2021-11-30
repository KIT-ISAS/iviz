#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class MagnitudeConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Magnitude;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool TrailVisible { get; set; } = false;
        [DataMember] public bool AngleVisible { get; set; } = true;
        [DataMember] public bool FrameVisible { get; set; } = true;
        [DataMember] public float Scale { get; set; } = 1.0f;
        [DataMember] public bool PreferUdp { get; set; } = true;
        [DataMember] public bool VectorVisible { get; set; } = true;
        [DataMember] public float VectorScale { get; set; } = 1.0f;
        [DataMember] public float ScaleMultiplierPow10 { get; set; } = 0;
        [DataMember] public SerializableColor Color { get; set; } = UnityEngine.Color.red;
        [DataMember] public float TrailTime { get; set; } = 2.0f;
    }

    public sealed class MagnitudeListener : ListenerController
    {
        readonly FrameNode frameNode;
        readonly TrailResource trail;
        FrameNode? childNode;
        AxisFrameResource? axisFrame;
        MeshMarkerResource? sphere;
        ArrowResource? arrow;
        AngleAxisResource? angleAxis;
        
        Vector3 dirForDataSource;

        readonly MagnitudeConfiguration config = new MagnitudeConfiguration();

        public override IModuleData ModuleData { get; }
        public override TfFrame? Frame => frameNode.Parent;

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
                PreferUdp = value.PreferUdp;
            }
        }
        
        public bool PreferUdp
        {
            get => config.PreferUdp;
            set
            {
                config.PreferUdp = value;
                if (Listener != null)
                {
                    Listener.TransportHint = value ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;
                }
            }
        }        

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                if (axisFrame != null)
                {
                    axisFrame.Visible = value && FrameVisible;
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
                if (axisFrame != null)
                {
                    axisFrame.Visible = value && Visible;
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
                frameNode.Transform.localScale = value * Vector3.one;
                trail.ElementScale = 0.02f * value;
                
                if (axisFrame != null)
                {
                    axisFrame.Transform.localScale = value * Vector3.one;
                }

                if (sphere != null)
                {
                    sphere.Transform.localScale = 0.05f * value * Vector3.one;
                }

                if (angleAxis != null)
                {
                    angleAxis.Transform.localScale = value * Vector3.one;
                }                
            }
        }

        float scaleMultiplier = 1;
        public float VectorScaleMultiplierPow10
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

        public MagnitudeListener(IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            frameNode = FrameNode.Instantiate("DisplayNode");

            trail = ResourcePool.RentDisplay<TrailResource>();
            trail.DataSource = () => frameNode.Transform.position;

            Config = new MagnitudeConfiguration();
        }

        public void StartListening()
        {
            frameNode.name = $"[{config.Topic}]";

            var transportHint = PreferUdp ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;
            switch (config.Type)
            {
                case PoseStamped.RosMessageType:
                    Listener = new Listener<PoseStamped>(config.Topic, Handler, transportHint);
                    goto case Msgs.GeometryMsgs.Pose.RosMessageType;

                case Msgs.GeometryMsgs.Pose.RosMessageType:
                    Listener ??= new Listener<Msgs.GeometryMsgs.Pose>(config.Topic, Handler, transportHint);
                    RentFrame(frameNode);
                    break;

                case PointStamped.RosMessageType:
                    Listener = new Listener<PointStamped>(config.Topic, Handler, transportHint);
                    goto case Point.RosMessageType;

                case Point.RosMessageType:
                    Listener ??= new Listener<Point>(config.Topic, Handler, transportHint);

                    RentSphere(frameNode);
                    break;

                case WrenchStamped.RosMessageType:
                    Listener = new Listener<WrenchStamped>(config.Topic, Handler, transportHint);
                    goto case Wrench.RosMessageType;

                case Wrench.RosMessageType:
                    Listener ??= new Listener<Wrench>(config.Topic, Handler, transportHint);
                    RentFrame(frameNode);
                    RentArrow(frameNode);
                    RentAngleAxis(frameNode);
                    RentSphere(frameNode);
                    trail.DataSource = TrailDataSource;
                    break;

                case TwistStamped.RosMessageType:
                    Listener = new Listener<TwistStamped>(config.Topic, Handler, transportHint);
                    goto case Twist.RosMessageType;

                case Twist.RosMessageType:
                    Listener ??= new Listener<Twist>(config.Topic, Handler, transportHint);
                    RentFrame(frameNode);
                    RentArrow(frameNode);
                    RentAngleAxis(frameNode);
                    RentSphere(frameNode);
                    trail.DataSource = TrailDataSource;
                    break;

                case Odometry.RosMessageType:
                    Listener = new Listener<Odometry>(config.Topic, Handler, transportHint);
                    RentFrame(frameNode);
                    RentSphere(frameNode);
                    childNode = FrameNode.Instantiate($"[{config.Topic} | Child]");
                    RentArrow(childNode);
                    RentAngleAxis(childNode);
                    trail.DataSource = TrailDataSource;
                    break;
            }
        }

        void RentFrame(FrameNode node)
        {
            axisFrame = ResourcePool.RentDisplay<AxisFrameResource>(node.Transform);
            axisFrame.ShadowsEnabled = false;
        }
        
        void RentArrow(FrameNode node)
        {
            arrow = ResourcePool.RentDisplay<ArrowResource>(node.Transform);
            arrow.Color = Color;
            arrow.Set(Vector3.one * 0.01f);
            arrow.ShadowsEnabled = false;
        }

        void RentAngleAxis(FrameNode node)
        {
            angleAxis = ResourcePool.RentDisplay<AngleAxisResource>(node.Transform);
            angleAxis.Color = Color.yellow;
        }
        
        void RentSphere(FrameNode node)
        {
            sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, node.Transform);
            sphere.Transform.localScale = 0.05f * Vector3.one;
            sphere.Color = Color;
            sphere.ShadowsEnabled = false;
        }

        void Handler(PoseStamped msg)
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

        void Handler(PointStamped msg)
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

        void Handler(WrenchStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Wrench);
        }

        Vector3 TrailDataSource() => frameNode.Transform.TransformPoint(dirForDataSource * (VectorScale * scaleMultiplier));

        void Handler(Wrench msg)
        {
            if (msg.Force.HasNaN() || msg.Torque.HasNaN())
            {
                return;
            }

            Vector3 dir = msg.Force.Ros2Unity();
            if (arrow != null)
            {
                arrow.Set(Vector3.zero, dir * (VectorScale * scaleMultiplier));
            }

            if (angleAxis != null)
            {
                angleAxis.Set(msg.Torque.Ros2Unity());
            }

            dirForDataSource = dir;
        }

        void Handler(TwistStamped msg)
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
            if (arrow != null)
            {
                arrow.Set(Vector3.zero, dir * (VectorScale * scaleMultiplier));
            }

            if (angleAxis != null)
            {
                angleAxis.Set(angular.RosRpy2Unity());
            }

            dirForDataSource = dir;
        }

        void Handler(Odometry msg)
        {
            frameNode.AttachTo(msg.Header);
            if (childNode != null)
            {
                childNode.AttachTo(msg.ChildFrameId);
            }

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
            if (arrow != null)
            {
                arrow.Set(Vector3.zero, dir * (VectorScale * scaleMultiplier));
            }

            if (angleAxis != null)
            {
                angleAxis.Set(angular.RosRpy2Unity());
            }

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

            frameNode.DestroySelf();

            trail.ReturnToPool();

            if (childNode != null)
            {
                childNode.DestroySelf();
            }

            axisFrame.ReturnToPool();
            angleAxis.ReturnToPool();
            arrow.ReturnToPool();
            sphere.ReturnToPool(Resource.Displays.Sphere);
        }
    }
}