using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class PathConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Path;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public float Width { get; set; } = 0.03f;
        [DataMember] public bool ShowAxes { get; set; } = false;
        [DataMember] public float AxisLength { get; set; } = 0.125f;
        [DataMember] public bool ShowLines { get; set; } = true;
        [DataMember] public SerializableColor LineColor { get; set; } = Color.yellow;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public class PathListener : ListenerController
    {
        DisplayNode node;
        LineResource resource;

        public override ModuleData ModuleData { get; set; }

        public int Size { get; private set; }

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
                resource.Visible = value;
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
                    Listener.MaxQueueSize = (int)value;
                }
            }
        }

        readonly List<Pose> savedPoses = new List<Pose>();
        readonly List<LineWithColor> lines = new List<LineWithColor>();

        void Awake()
        {
            node = SimpleDisplayNode.Instantiate("PathNode", transform);
            resource = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, node.transform);
            Config = new PathConfiguration();
        }

        public override void StartListening()
        {
            base.StartListening();
            switch (config.Type)
            {
                case Msgs.NavMsgs.Path.RosMessageType:
                    Listener = new RosListener<Msgs.NavMsgs.Path>(config.Topic, Handler);
                    break;
                case Msgs.GeometryMsgs.PoseArray.RosMessageType:
                    Listener = new RosListener<Msgs.GeometryMsgs.PoseArray>(config.Topic, Handler);
                    break;
            }
            Listener.MaxQueueSize = (int)MaxQueueSize;
            name = "[" + config.Topic + "]";
            node.name = name;
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
                savedPoses.Add(ps.Ros2Unity());
            }
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
                Vector3 xDir = new Vector3(1, 0, 0).Ros2Unity() * AxisLength;
                Vector3 yDir = new Vector3(0, 1, 0).Ros2Unity() * AxisLength;
                Vector3 zDir = new Vector3(0, 0, 1).Ros2Unity() * AxisLength;
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
            Destroy(node.gameObject);
        }
    }
}



