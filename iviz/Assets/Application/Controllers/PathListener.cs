using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class PathConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Path;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public float Width { get; set; } = 0.01f;
        [DataMember] public bool ShowAxes { get; set; } = false;
        [DataMember] public float AxisLength { get; set; } = 0.125f;
        [DataMember] public bool ShowLines { get; set; } = true;
        [DataMember] public SerializableColor LineColor { get; set; } = Color.yellow;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class PathListener : ListenerController
    {
        readonly DisplayNode node;
        readonly LineResource resource;

        public override IModuleData ModuleData { get; }

        public override TFFrame Frame => node.Parent;

        readonly PathConfiguration config = new PathConfiguration();

        public PathConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                Visible = value.Visible;
                Width = value.Width;
                ShowAxes = value.ShowAxes;
                MaxQueueSize = value.MaxQueueSize;
                AxisLength = value.AxisLength;
                LineColor = value.LineColor;
                ShowLines = value.ShowLines;
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

        public float Width
        {
            get => config.Width;
            set
            {
                config.Width = value;
                resource.LineScale = value;
            }
        }

        public bool ShowAxes
        {
            get => config.ShowAxes;
            set
            {
                config.ShowAxes = value;
                //resource.Visible = value;
                ProcessPoses();
            }
        }

        public bool ShowLines
        {
            get => config.ShowLines;
            set
            {
                config.ShowLines = value;
                ProcessPoses();
            }
        }

        public Color LineColor
        {
            get => config.LineColor;
            set
            {
                config.LineColor = value;
                if (ShowLines)
                {
                    ProcessPoses();
                }
            }
        }

        public float AxisLength
        {
            get => config.AxisLength;
            set
            {
                config.AxisLength = value;
                if (ShowAxes)
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

        public PathListener(IModuleData moduleData)
        {
            ModuleData = moduleData;
            
            node = SimpleDisplayNode.Instantiate("PathNode");
            resource = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, node.transform);
            resource.LineScale = 0.005f;
            resource.Tint = Color.white;
            resource.UseAlpha = false;
            Config = new PathConfiguration();
        }

        public override void StartListening()
        {
            switch (config.Type)
            {
                case Msgs.NavMsgs.Path.RosMessageType:
                    Listener = new RosListener<Msgs.NavMsgs.Path>(config.Topic, Handler);
                    break;
                case Msgs.GeometryMsgs.PoseArray.RosMessageType:
                    Listener = new RosListener<Msgs.GeometryMsgs.PoseArray>(config.Topic, Handler);
                    ShowLines = false;
                    break;
                case Msgs.GeometryMsgs.PolygonStamped.RosMessageType:
                    Listener = new RosListener<Msgs.GeometryMsgs.PolygonStamped>(config.Topic, Handler);
                    ShowAxes = false;
                    break;
                case Msgs.GeometryMsgs.Polygon.RosMessageType:
                    node.Parent = TFListener.MapFrame;
                    Listener = new RosListener<Msgs.GeometryMsgs.Polygon>(config.Topic, Handler);
                    ShowAxes = false;
                    break;
            }

            Listener.MaxQueueSize = (int) MaxQueueSize;
            node.name = "[" + config.Topic + "]";;
        }


        void Handler(Msgs.NavMsgs.Path msg)
        {
            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

            string topHeader = msg.Header.FrameId;
            Msgs.time topStamp = msg.Header.Stamp;
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
                    Pose newPose = node.Parent.GetPose(stamp.ToTimeSpan());
                    Pose pose = topPoseInv.Multiply(newPose.Multiply(ps.Pose.Ros2Unity()));
                    savedPoses.Add(pose);
                }
                else if (TFListener.TryGetFrame(ps.Header.FrameId, out TFFrame frame))
                {
                    Pose newPose = frame.GetPose(stamp.ToTimeSpan());
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

        void Handler(Msgs.GeometryMsgs.PoseArray msg)
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

        void Handler(Msgs.GeometryMsgs.PolygonStamped msg)
        {
            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);
            Handler(msg.Polygon);
        }        

        void Handler(Msgs.GeometryMsgs.Polygon msg)
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
            if (ShowLines)
            {
                for (int i = 0; i < savedPoses.Count - 1; i++)
                {
                    lines.Add(new LineWithColor(savedPoses[i].position, savedPoses[i + 1].position, LineColor));
                }
            }

            if (ShowAxes)
            {
                Vector3 xDir = Vector3.right.Ros2Unity() * AxisLength;
                Vector3 yDir = Vector3.up.Ros2Unity() * AxisLength;
                Vector3 zDir = Vector3.forward.Ros2Unity() * AxisLength;
                for (int i = 0; i < savedPoses.Count; i++)
                {
                    Vector3 p = savedPoses[i].position;
                    Quaternion q = savedPoses[i].rotation;
                    lines.Add(new LineWithColor(p, p + q * xDir, Color.red));
                    lines.Add(new LineWithColor(p, p + q * yDir, Color.green));
                    lines.Add(new LineWithColor(p, p + q * zDir, Color.blue));
                }
            }

            resource.LinesWithColor = lines;
        }

        public override void Stop()
        {
            base.Stop();

            ResourcePool.Dispose(Resource.Displays.Line, resource.gameObject);
            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
        }
    }
}