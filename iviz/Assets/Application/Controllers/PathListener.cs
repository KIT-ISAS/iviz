using System;
using System.Collections.Generic;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    [Obsolete]
    public sealed class PathListener : ListenerController
    {
        readonly FrameNode node;
        readonly LineDisplay resource;

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
                LineColor = value.LineColor.ToUnity();
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
            get => config.LineColor.ToUnity();
            set
            {
                config.LineColor = value.ToRos();
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

        Listener listener;
        public override Listener Listener => listener;

        public PathListener()
        {
            node = new FrameNode("PathNode");
            resource = ResourcePool.Rent<LineDisplay>(Resource.Displays.Line, node.Transform);
            resource.ElementScale = 0.005f;
            resource.Tint = Color.white;
            Config = new PathConfiguration();
        }

        public void StartListening()
        {
            switch (config.Type)
            {
                case Path.MessageType:
                    listener = new Listener<Path>(config.Topic, Handler);
                    break;
                case PoseArray.MessageType:
                    listener = new Listener<PoseArray>(config.Topic, Handler);
                    LinesVisible = false;
                    break;
                case PolygonStamped.MessageType:
                    listener = new Listener<PolygonStamped>(config.Topic, Handler);
                    FramesVisible = false;
                    break;
                case Polygon.MessageType:
                    node.Parent = TfModule.DefaultFrame;
                    listener = new Listener<Polygon>(config.Topic, Handler);
                    FramesVisible = false;
                    break;
            }

            node.Name = $"[{config.Topic}]";
        }


        void Handler([NotNull] Path msg)
        {
            node.AttachTo(msg.Header);

            string topHeader = msg.Header.FrameId;
            time topStamp = msg.Header.Stamp;

            if (node.Parent == null)
            {
                // shouldn't happen
                return;
            }

            Pose topPoseInv = node.Parent.OriginWorldPose.Inverse();

            savedPoses.Clear();
            foreach (PoseStamped ps in msg.Poses)
            {
                string header = ps.Header.FrameId;
                var stamp = ps.Header.Stamp;

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
                else if (TfModule.TryGetFrame(header, out TfFrame frame))
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

        void Handler([NotNull] PoseArray msg)
        {
            node.AttachTo(msg.Header);

            savedPoses.Clear();
            foreach (Msgs.GeometryMsgs.Pose ps in msg.Poses)
            {
                if (ps.IsInvalid())
                {
                    continue;
                }

                savedPoses.Add(ps.Ros2Unity());
            }

            ProcessPoses();
        }

        void Handler([NotNull] PolygonStamped msg)
        {
            node.AttachTo(msg.Header);
            Handler(msg.Polygon);
        }

        void Handler([NotNull] Polygon msg)
        {
            savedPoses.Clear();
            foreach (var p in msg.Points)
            {
                if (p.IsInvalid())
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
            node.Dispose();
            lines.Dispose();
        }
    }
}