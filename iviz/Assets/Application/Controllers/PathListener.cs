using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class PathListener : ListenerController
    {
        readonly FrameNode node;
        readonly LineResource resource;

        public override IModuleData ModuleData { get; }

        public override TfFrame Frame => node.Parent;

        readonly PathConfiguration config = new();

        public PathConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                Visible = value.Visible;
                LineWidth = value.LineWidth;
                FramesVisible = value.FramesVisible;
                FrameSize = value.FrameSize;
                LineColor = value.LineColor;
                LinesVisible = value.LinesVisible;
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                resource.Visible = value;
                if (value)
                {
                    ProcessPoses();
                }
            }
        }

        public float LineWidth
        {
            get => config.LineWidth;
            set
            {
                config.LineWidth = value;
                resource.ElementScale = value;
            }
        }

        public bool FramesVisible
        {
            get => config.FramesVisible;
            set
            {
                config.FramesVisible = value;
                //resource.Visible = value;
                ProcessPoses();
            }
        }

        public bool LinesVisible
        {
            get => config.LinesVisible;
            set
            {
                config.LinesVisible = value;
                ProcessPoses();
            }
        }

        public Color LineColor
        {
            get => config.LineColor;
            set
            {
                config.LineColor = value;
                if (LinesVisible)
                {
                    ProcessPoses();
                }
            }
        }

        public float FrameSize
        {
            get => config.FrameSize;
            set
            {
                config.FrameSize = value;
                if (FramesVisible)
                {
                    ProcessPoses();
                }
            }
        }

        readonly List<Pose> savedPoses = new();
        readonly NativeList<LineWithColor> lines = new();

        IListener listener;
        public override IListener Listener => listener;

        public PathListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            node = FrameNode.Instantiate("PathNode");
            resource = ResourcePool.Rent<LineResource>(Resource.Displays.Line, node.Transform);
            resource.ElementScale = 0.005f;
            resource.Tint = Color.white;
            Config = new PathConfiguration();
        }

        public void StartListening()
        {
            switch (config.Type)
            {
                case Msgs.NavMsgs.Path.RosMessageType:
                    listener = new Listener<Msgs.NavMsgs.Path>(config.Topic, Handler);
                    break;
                case Msgs.GeometryMsgs.PoseArray.RosMessageType:
                    listener = new Listener<Msgs.GeometryMsgs.PoseArray>(config.Topic, Handler);
                    LinesVisible = false;
                    break;
                case Msgs.GeometryMsgs.PolygonStamped.RosMessageType:
                    listener = new Listener<Msgs.GeometryMsgs.PolygonStamped>(config.Topic, Handler);
                    FramesVisible = false;
                    break;
                case Msgs.GeometryMsgs.Polygon.RosMessageType:
                    node.Parent = TfListener.DefaultFrame;
                    listener = new Listener<Msgs.GeometryMsgs.Polygon>(config.Topic, Handler);
                    FramesVisible = false;
                    break;
            }

            node.name = $"[{config.Topic}]";
        }


        void Handler([NotNull] Msgs.NavMsgs.Path msg)
        {
            node.AttachTo(msg.Header);

            string topHeader = msg.Header.FrameId;
            Msgs.time topStamp = msg.Header.Stamp;

            if (node.Parent == null)
            {
                // shouldn't happen
                return;
            }

            Pose topPoseInv = node.Parent.OriginWorldPose.Inverse();

            savedPoses.Clear();
            foreach (Msgs.GeometryMsgs.PoseStamped ps in msg.Poses)
            {
                string header = ps.Header.FrameId ?? "";
                Msgs.time stamp = ps.Header.Stamp;

                if (topHeader == header && topStamp == stamp)
                {
                    savedPoses.Add(ps.Pose.Ros2Unity());
                    continue;
                }

                if (topHeader == header)
                {
                    Pose newPose = node.Parent.OriginWorldPose;
                    Pose pose = topPoseInv.Multiply(newPose.Multiply(ps.Pose.Ros2Unity()));
                    savedPoses.Add(pose);
                }
                else if (TfListener.TryGetFrame(header, out TfFrame frame))
                {
                    Pose newPose = frame.OriginWorldPose;
                    Pose pose = topPoseInv.Multiply(newPose.Multiply(ps.Pose.Ros2Unity()));
                    savedPoses.Add(pose);
                }
                else
                {
                    // sorry!
                    savedPoses.Add(ps.Pose.Ros2Unity());
                }
            }

            ProcessPoses();
        }

        void Handler([NotNull] Msgs.GeometryMsgs.PoseArray msg)
        {
            node.AttachTo(msg.Header);

            savedPoses.Clear();
            foreach (Msgs.GeometryMsgs.Pose ps in msg.Poses)
            {
                if (ps.HasNaN())
                {
                    continue;
                }

                savedPoses.Add(ps.Ros2Unity());
            }

            ProcessPoses();
        }

        void Handler([NotNull] Msgs.GeometryMsgs.PolygonStamped msg)
        {
            node.AttachTo(msg.Header);
            Handler(msg.Polygon);
        }

        void Handler([NotNull] Msgs.GeometryMsgs.Polygon msg)
        {
            savedPoses.Clear();
            foreach (var p in msg.Points)
            {
                if (p.HasNaN())
                {
                    continue;
                }

                savedPoses.Add(Pose.identity.WithPosition(p.Ros2Unity()));
            }

            savedPoses.Add(savedPoses[0]);

            ProcessPoses();
        }

        void ProcessPoses()
        {
            if (!Visible)
            {
                return;
            }

            lines.Clear();
            if (LinesVisible)
            {
                for (int i = 0; i < savedPoses.Count - 1; i++)
                {
                    lines.Add(new LineWithColor(savedPoses[i].position, savedPoses[i + 1].position, LineColor));
                }
            }

            if (FramesVisible)
            {
                Vector3 xDir = Vector3.right.Ros2Unity() * FrameSize;
                Vector3 yDir = Vector3.up.Ros2Unity() * FrameSize;
                Vector3 zDir = Vector3.forward.Ros2Unity() * FrameSize;
                foreach (Pose savedPose in savedPoses)
                {
                    Vector3 p = savedPose.position;
                    Quaternion q = savedPose.rotation;
                    lines.Add(new LineWithColor(p, p + q * xDir, Color.red));
                    lines.Add(new LineWithColor(p, p + q * yDir, Color.green));
                    lines.Add(new LineWithColor(p, p + q * zDir, Color.blue));
                }
            }

            resource.Set(lines, LineColor.a < 1);
        }

        public override void Dispose()
        {
            base.Dispose();
            resource.ReturnToPool();
            node.DestroySelf();
            lines.Dispose();
        }
    }
}