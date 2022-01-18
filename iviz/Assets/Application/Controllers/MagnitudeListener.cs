#nullable enable

using System;
using Iviz.Common;
using Iviz.Common.Configurations;
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
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class MagnitudeListener : ListenerController
    {
        readonly FrameNode frameNode;
        readonly TrailResource trail;
        readonly FrameNode? childNode;
        readonly AxisFrameResource? axisFrame;
        readonly MeshMarkerResource? sphere;
        readonly ArrowResource? arrow;
        readonly AngleAxisResource? angleAxis;
        readonly MagnitudeConfiguration config = new();
        Vector3 cachedDirection;

        public override TfFrame? Frame => frameNode.Parent;

        public MagnitudeConfiguration Config
        {
            get => config;
            private set
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

        bool PreferUdp
        {
            get => config.PreferUdp;
            set => config.PreferUdp = value;
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
            set => config.VectorScale = value;
        }

        public override IListener Listener { get; }

        public MagnitudeListener(MagnitudeConfiguration? config, string topic, string type)
        {
            Config = config ?? new MagnitudeConfiguration
            {
                Topic = topic,
                Type = type
            };

            frameNode = new FrameNode($"[{Config.Topic}]");
            
            trail = ResourcePool.RentDisplay<TrailResource>();
            trail.DataSource = () => frameNode.Transform.position;

            var transportHint = PreferUdp ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;
            Listener = Config.Type switch
            {
                PoseStamped.RosMessageType => CreateListener<PoseStamped>(Handler),
                Msgs.GeometryMsgs.Pose.RosMessageType => CreateListener<Msgs.GeometryMsgs.Pose>(Handler),
                PointStamped.RosMessageType => CreateListener<PointStamped>(Handler),
                Point.RosMessageType => CreateListener<Point>(Handler),
                WrenchStamped.RosMessageType => CreateListener<WrenchStamped>(Handler),
                Wrench.RosMessageType => CreateListener<Wrench>(Handler),
                TwistStamped.RosMessageType => CreateListener<TwistStamped>(Handler),
                Twist.RosMessageType => CreateListener<Twist>(Handler),
                Odometry.RosMessageType => CreateListener<Odometry>(Handler),
                _ => throw new InvalidOperationException("Invalid message type")
            };

            Listener<T> CreateListener<T>(Action<T> a) where T : IMessage, IDeserializable<T>, new() =>
                new(Config.Topic, a, transportHint);

            switch (Config.Type)
            {
                case PoseStamped.RosMessageType:
                case Msgs.GeometryMsgs.Pose.RosMessageType:
                    RentFrame(frameNode, out axisFrame);
                    break;
                case PointStamped.RosMessageType:
                case Point.RosMessageType:
                    RentSphere(frameNode, Color, out sphere);
                    break;
                case WrenchStamped.RosMessageType:
                case Wrench.RosMessageType:
                    RentFrame(frameNode, out axisFrame);
                    RentArrow(frameNode, Color, out arrow);
                    RentAngleAxis(frameNode, out angleAxis);
                    RentSphere(frameNode, Color, out sphere);
                    trail.DataSource = TrailDataSource;
                    break;
                case TwistStamped.RosMessageType:
                case Twist.RosMessageType:
                    RentFrame(frameNode, out axisFrame);
                    RentArrow(frameNode, Color, out arrow);
                    RentAngleAxis(frameNode, out angleAxis);
                    RentSphere(frameNode, Color, out sphere);
                    trail.DataSource = TrailDataSource;
                    break;
                case Odometry.RosMessageType:
                    RentFrame(frameNode, out axisFrame);
                    RentSphere(frameNode, Color, out sphere);
                    childNode = new FrameNode($"[{Config.Topic} | Child]");
                    RentArrow(childNode, Color, out arrow);
                    RentAngleAxis(childNode, out angleAxis);
                    trail.DataSource = TrailDataSource;
                    break;
            }
        }

        static void RentFrame(FrameNode node, out AxisFrameResource axisFrame)
        {
            axisFrame = ResourcePool.RentDisplay<AxisFrameResource>(node.Transform);
            axisFrame.EnableShadows = false;
        }

        static void RentArrow(FrameNode node, Color color, out ArrowResource arrow)
        {
            arrow = ResourcePool.RentDisplay<ArrowResource>(node.Transform);
            arrow.Color = color;
            arrow.Set(Vector3.one * 0.01f);
            arrow.EnableShadows = false;
        }

        static void RentAngleAxis(FrameNode node, out AngleAxisResource angleAxis)
        {
            angleAxis = ResourcePool.RentDisplay<AngleAxisResource>(node.Transform);
            angleAxis.Color = Color.yellow;
        }

        static void RentSphere(FrameNode node, Color color, out MeshMarkerResource sphere)
        {
            sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, node.Transform);
            sphere.Transform.localScale = 0.05f * Vector3.one;
            sphere.Color = color;
            sphere.EnableShadows = false;
        }

        void Handler(PoseStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Pose);
        }

        void Handler(Msgs.GeometryMsgs.Pose msg)
        {
            if (msg.IsInvalid())
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
            if (msg.IsInvalid())
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

        Vector3 TrailDataSource() =>
            frameNode.Transform.TransformPoint(cachedDirection * VectorScale);

        void Handler(Wrench msg)
        {
            if (msg.Force.IsInvalid() || msg.Torque.IsInvalid())
            {
                return;
            }

            var dir = msg.Force.Ros2Unity();
            if (arrow != null)
            {
                arrow.Set(Vector3.zero, dir * VectorScale);
            }

            if (angleAxis != null)
            {
                angleAxis.Set(msg.Torque.Ros2Unity());
            }

            cachedDirection = dir;
        }

        void Handler(TwistStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Twist);
        }

        void Handler(Twist msg)
        {
            var (linear, angular) = msg;
            if (angular.IsInvalid() || linear.IsInvalid())
            {
                return;
            }

            var dir = linear.Ros2Unity();
            if (arrow != null)
            {
                arrow.Set(Vector3.zero, dir * VectorScale);
            }

            if (angleAxis != null)
            {
                angleAxis.Set(angular.RosRpy2Unity());
            }

            cachedDirection = dir;
        }

        void Handler(Odometry msg)
        {
            frameNode.AttachTo(msg.Header);
            if (childNode != null)
            {
                childNode.AttachTo(msg.ChildFrameId);
            }

            if (msg.Pose.Pose.IsInvalid())
            {
                return;
            }

            var (linear, angular) = msg.Twist.Twist;
            if (angular.IsInvalid() || linear.IsInvalid())
            {
                return;
            }

            frameNode.Transform.SetLocalPose(msg.Pose.Pose.Ros2Unity());

            var dir = linear.Ros2Unity();
            if (arrow != null)
            {
                arrow.Set(Vector3.zero, dir * VectorScale);
            }

            if (angleAxis != null)
            {
                angleAxis.Set(angular.RosRpy2Unity());
            }

            cachedDirection = dir;
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

        public override void Dispose()
        {
            base.Dispose();

            trail.DataSource = null;

            frameNode.Dispose();

            trail.ReturnToPool();

            if (childNode != null)
            {
                childNode.Dispose();
            }

            axisFrame.ReturnToPool();
            angleAxis.ReturnToPool();
            arrow.ReturnToPool();
            sphere.ReturnToPool(Resource.Displays.Sphere);
        }
    }
}