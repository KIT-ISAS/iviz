#nullable enable

using System;
using Iviz.App;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using UnityEngine;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class MagnitudeListener : ListenerController, IMagnitudeDataSource, ITrailDataSource
    {
        readonly FrameNode frameNode;
        readonly TrailDisplay trail;
        readonly FrameNode? childNode;
        readonly AxisFrameDisplay? axisFrame;
        readonly MeshMarkerDisplay? sphere;
        readonly ArrowDisplay? arrow;
        readonly AngleAxisDisplay? angleAxis;
        readonly MagnitudeConfiguration config = new();
        Vector3 cachedDirection;

        public override TfFrame? Frame => frameNode.Parent;
        public Magnitude? Magnitude { get; private set; }

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
                VectorColor = value.VectorColor.ToUnity();
                AngleColor = value.AngleColor.ToUnity();
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

        public Color VectorColor
        {
            get => config.VectorColor.ToUnity();
            set
            {
                config.VectorColor = value.ToRos();
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

        public Color AngleColor
        {
            get => config.AngleColor.ToUnity();
            set
            {
                config.AngleColor = value.ToRos();
                if (angleAxis != null)
                {
                    angleAxis.Color = value;
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

        public override Listener Listener { get; }

        public Vector3 TrailPosition => frameNode.Transform.TransformPoint(cachedDirection * VectorScale);

        public MagnitudeListener(MagnitudeConfiguration? config, string topic, string type)
        {
            trail = ResourcePool.RentDisplay<TrailDisplay>();
            frameNode = new FrameNode("MagnitudeListener");

            Config = config ?? new MagnitudeConfiguration
            {
                Topic = topic,
                Type = type
            };

            trail.DataSource = this;
            Listener = Config.Type switch
            {
                PoseStamped.MessageType => CreateListener<PoseStamped>(Handler),
                //Pose.MessageType => CreateListener<Pose>(Handler),
                PointStamped.MessageType => CreateListener<PointStamped>(Handler),
                //Point.MessageType => CreateListener<Point>(Handler),
                WrenchStamped.MessageType => CreateListener<WrenchStamped>(Handler),
                //Wrench.MessageType => CreateListener<Wrench>(Handler),
                TwistStamped.MessageType => CreateListener<TwistStamped>(Handler),
                Twist.MessageType => CreateListener<Twist>(Handler),
                Odometry.MessageType => CreateListener<Odometry>(Handler),
                _ => Listener.ThrowUnsupportedMessageType(Config.Type)
            };

            Listener<T> CreateListener<T>(Action<T> a) where T : IMessage, new() =>
                new(Config.Topic, a, profile: RosSubscriptionProfile.SpammyCanDrop);

            switch (Config.Type)
            {
                case PoseStamped.MessageType:
                    //case Pose.MessageType:
                    RentFrame(frameNode, out axisFrame);
                    break;
                case PointStamped.MessageType:
                    //case Point.MessageType:
                    RentSphere(frameNode, VectorColor, out sphere);
                    break;
                case WrenchStamped.MessageType:
                    //case Wrench.MessageType:
                    RentFrame(frameNode, out axisFrame);
                    RentArrow(frameNode, VectorColor, out arrow);
                    RentAngleAxis(frameNode, AngleColor, out angleAxis);
                    RentSphere(frameNode, VectorColor, out sphere);
                    break;
                case TwistStamped.MessageType:
                case Twist.MessageType:
                    RentFrame(frameNode, out axisFrame);
                    RentArrow(frameNode, VectorColor, out arrow);
                    RentAngleAxis(frameNode, AngleColor, out angleAxis);
                    RentSphere(frameNode, VectorColor, out sphere);
                    break;
                case Odometry.MessageType:
                    RentFrame(frameNode, out axisFrame);
                    RentSphere(frameNode, VectorColor, out sphere);
                    childNode = new FrameNode($"{Config.Topic} | Child");
                    RentArrow(childNode, VectorColor, out arrow);
                    RentAngleAxis(childNode, AngleColor, out angleAxis);
                    break;
            }
        }

        static void RentFrame(FrameNode node, out AxisFrameDisplay axisFrame)
        {
            axisFrame = ResourcePool.RentDisplay<AxisFrameDisplay>(node.Transform);
            axisFrame.EnableShadows = false;
        }

        static void RentArrow(FrameNode node, Color color, out ArrowDisplay arrow)
        {
            arrow = ResourcePool.RentDisplay<ArrowDisplay>(node.Transform);
            arrow.Color = color;
            arrow.Set(Vector3.one * 0.01f);
            arrow.EnableShadows = false;
        }

        static void RentAngleAxis(FrameNode node, Color color, out AngleAxisDisplay angleAxis)
        {
            angleAxis = ResourcePool.RentDisplay<AngleAxisDisplay>(node.Transform);
            angleAxis.Color = color;
        }

        static void RentSphere(FrameNode node, Color color, out MeshMarkerDisplay sphere)
        {
            sphere = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Sphere, node.Transform);
            sphere.Transform.localScale = 0.05f * Vector3.one;
            sphere.Color = color;
            sphere.EnableShadows = false;
        }

        void Handler(PoseStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Pose);
        }

        void Handler(Pose msg)
        {
            Magnitude = new Magnitude("Pose", msg.Position);

            if (msg.IsInvalid())
            {
                RosLogger.Debug($"{this}: Pose contains invalid values. Ignoring.");
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
            Magnitude = new Magnitude("Point", msg, referencePoint: msg);

            if (msg.IsInvalid())
            {
                RosLogger.Debug($"{this}: Point contains invalid values. Ignoring.");
                return;
            }

            var position = msg.Ros2Unity();
            frameNode.Transform.localPosition = position;
        }

        void Handler(WrenchStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Wrench);
        }

        void Handler(Wrench msg)
        {
            Magnitude = new Magnitude("Wrench", msg.Force, msg.Torque);

            if (msg.Force.IsInvalid() || msg.Torque.IsInvalid())
            {
                RosLogger.Debug($"{this}: Wrench contains invalid values. Ignoring.");
                return;
            }

            var direction = msg.Force.Ros2Unity();
            var torque = msg.Torque.Ros2Unity();

            if (arrow != null)
            {
                arrow.Set(Vector3.zero, direction * VectorScale);
            }

            if (angleAxis != null)
            {
                angleAxis.Set(torque);
            }

            cachedDirection = direction;
        }

        void Handler(TwistStamped msg)
        {
            frameNode.AttachTo(msg.Header);
            Handler(msg.Twist);
        }

        void Handler(Twist msg)
        {
            Magnitude = new Magnitude("Twist", msg.Linear, msg.Angular);

            var (linear, angular) = msg;
            if (angular.IsInvalid() || linear.IsInvalid())
            {
                RosLogger.Debug($"{this}: Twist contains invalid values. Ignoring.");
                return;
            }

            var direction = linear.Ros2Unity();
            if (arrow != null)
            {
                arrow.Set(Vector3.zero, direction * VectorScale);
            }

            if (angleAxis != null)
            {
                angleAxis.Set(angular.RosRpy2Unity());
            }

            cachedDirection = direction;
        }

        void Handler(Odometry msg)
        {
            var (linear, angular) = msg.Twist.Twist;

            Magnitude = new Magnitude("Odometry", linear, angular, msg.ChildFrameId);

            frameNode.AttachTo(msg.Header);
            childNode?.AttachTo(msg.ChildFrameId);

            if (msg.Pose.Pose.IsInvalid())
            {
                RosLogger.Debug($"{this}: Odometry pose contains invalid values. Ignoring.");
                return;
            }

            if (angular.IsInvalid() || linear.IsInvalid())
            {
                RosLogger.Debug($"{this}: Odometry twist contains invalid values. Ignoring.");
                return;
            }

            frameNode.Transform.SetLocalPose(msg.Pose.Pose.Ros2Unity());

            var direction = linear.Ros2Unity();
            if (arrow != null)
            {
                arrow.Set(Vector3.zero, direction * VectorScale);
            }

            if (angleAxis != null)
            {
                angleAxis.Set(angular.RosRpy2Unity());
            }

            cachedDirection = direction;
        }

        public override void ResetController()
        {
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
            trail.ReturnToPool();
            axisFrame.ReturnToPool();
            angleAxis.ReturnToPool();
            arrow.ReturnToPool();
            sphere.ReturnToPool(Resource.Displays.Sphere);

            frameNode.Dispose();
            childNode?.Dispose();
        }
    }
}