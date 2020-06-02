using System;
using System.Runtime.Serialization;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class OdometryConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.PointCloud;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool ShowTrail { get; set; } = true;
        [DataMember] public SerializableColor TrailColor { get; set; } = Color.red;
        [DataMember] public float TrailTime { get; set; } = 2.0f;
    }

    public class OdometryListener : TopicListener
    {
        DisplayClickableNode axisNode;
        SimpleDisplayNode trailNode;
        AxisResource axis;
        TrailResource trail;

        public override DisplayData DisplayData
        {
            get => axisNode.DisplayData;
            set => axisNode.DisplayData = value;
        }

        readonly OdometryConfiguration config = new OdometryConfiguration();
        public OdometryConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                Visible = value.Visible;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                axis.Visible = value;
                trail.Visible = value && ShowTrail;
            }
        }

        public bool ShowTrail
        {
            get => config.ShowTrail;
            set
            {
                config.ShowTrail = value;
                trail.Visible = value && Visible;
            }
        }

        public Color TrailColor
        {
            get => config.TrailColor;
            set
            {
                config.TrailColor = value;
                trail.Color = value;
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

        void Awake()
        {
            axisNode = DisplayClickableNode.Instantiate("AxisNode");
            axis = ResourcePool.GetOrCreate<AxisResource>(Resource.Markers.Axis);
            axisNode.Target = axis;

            trailNode = SimpleDisplayNode.Instantiate("TrailNode", transform);
            trailNode.Parent = TFListener.BaseFrame;
            trail = trailNode.gameObject.AddComponent<TrailResource>();

            trail.DataSource = () => axisNode.transform.position;
        }

        public override void StartListening()
        {
            base.StartListening();
            Listener = new RosListener<PoseStamped>(config.Topic, Handler);
            name = "Odometry:" + config.Topic;
            axisNode.name = "Odometry:" + config.Topic;
        }

        void Handler(PoseStamped msg)
        {
            axisNode.SetParent(msg.Header.FrameId);
            axisNode.transform.SetLocalPose(msg.Pose.Ros2Unity());
        }
    }
}