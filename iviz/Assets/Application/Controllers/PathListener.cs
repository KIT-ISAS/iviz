using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class PathConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.Path;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public float LineWidth { get; set; } = 0.01f;
        [DataMember] public bool FramesVisible { get; set; } = false;
        [DataMember] public float FrameSize { get; set; } = 0.125f;
        [DataMember] public bool LinesVisible { get; set; } = true;
        [DataMember] public SerializableColor LineColor { get; set; } = Color.yellow;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class PathListener : ListenerController
    {
        readonly FrameNode node;
        readonly LineResource resource;

        public override IModuleData ModuleData { get; }

        public override TfFrame Frame => node.Parent;

        readonly PathConfiguration config = new PathConfiguration();

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
                MaxQueueSize = value.MaxQueueSize;
                FrameSize = value.FrameSize;
                LineColor = value.LineColor;
                LinesVisible = value.LinesVisible;
            }
        }

        public bool Visible
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

        public uint MaxQueueSize
        {
            get => config.MaxQueueSize;
            set
            {
                config.MaxQueueSize = value;
                if (Listener != null)
                {
                    Listener.MaxQueueSize = (int) value;
                }
            }
        }

        readonly List<Pose> savedPoses = new List<Pose>();
        readonly List<LineWithColor> lines = new List<LineWithColor>();

        public PathListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            node = FrameNode.Instantiate("PathNode");
            resource = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, node.Transform);
            resource.ElementScale = 0.005f;
            resource.Tint = Color.white;
            Config = new PathConfiguration();
        }

        public override void StartListening()
        {
            switch (config.Type)
            {
                case Msgs.NavMsgs.Path.RosMessageType:
                    Listener = new Listener<Msgs.NavMsgs.Path>(config.Topic, Handler);
                    break;
                case Msgs.GeometryMsgs.PoseArray.RosMessageType:
                    Listener = new Listener<Msgs.GeometryMsgs.PoseArray>(config.Topic, Handler);
                    LinesVisible = false;
                    break;
                case Msgs.GeometryMsgs.PolygonStamped.RosMessageType:
                    Listener = new Listener<Msgs.GeometryMsgs.PolygonStamped>(config.Topic, Handler);
                    FramesVisible = false;
                    break;
                case Msgs.GeometryMsgs.Polygon.RosMessageType:
                    node.Parent = TfListener.MapFrame;
                    Listener = new Listener<Msgs.GeometryMsgs.Polygon>(config.Topic, Handler);
                    FramesVisible = false;
                    break;
            }

            Listener.MaxQueueSize = (int) MaxQueueSize;
            node.name = $"[{config.Topic}]";
        }


        void Handler([NotNull] Msgs.NavMsgs.Path msg)
        {
            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

            string topHeader = msg.Header.FrameId;
            Msgs.time topStamp = msg.Header.Stamp;

            if (node.Parent == null)
            {
                // shouldn't happen
                return;
            }

            Pose topPoseInv = node.Parent.WorldPose.Inverse();

            savedPoses.Clear();
            foreach (Msgs.GeometryMsgs.PoseStamped ps in msg.Poses)
            {
                string header = ps.Header.FrameId;
                Msgs.time stamp = ps.Header.Stamp;

                if (topHeader == header && topStamp == stamp)
                {
                    savedPoses.Add(ps.Pose.Ros2Unity());
                    continue;
                }

                if (topHeader == header)
                {
                    Pose newPose = node.Parent.LookupPose(stamp.ToTimeSpan());
                    Pose pose = topPoseInv.Multiply(newPose.Multiply(ps.Pose.Ros2Unity()));
                    savedPoses.Add(pose);
                }
                else if (TfListener.TryGetFrame(ps.Header.FrameId, out TfFrame frame))
                {
                    Pose newPose = frame.LookupPose(stamp.ToTimeSpan());
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
            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

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
            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);
            Handler(msg.Polygon);
        }

        void Handler([NotNull] Msgs.GeometryMsgs.Polygon msg)
        {
            savedPoses.Clear();
            foreach (Msgs.GeometryMsgs.Point32 p in msg.Points)
            {
                if (p.HasNaN())
                {
                    continue;
                }

                savedPoses.Add(new Pose(p.Ros2Unity(), Quaternion.identity));
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

            resource.Set(lines, lines.Count, LineColor.a < 1);
        }

        public override void StopController()
        {
            base.StopController();
            resource.DisposeDisplay();
            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
        }
    }
}