using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using RosSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class LaserScanConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.LaserScan;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public float PointSize { get; set; } = 0.03f;
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.hsv;
        [DataMember] public bool UseIntensity { get; set; } = false;
        [DataMember] public bool UseLines { get; set; } = false;

        [DataMember] public bool ForceMinMax { get; set; } = false;
        [DataMember] public float MinIntensity { get; set; } = 0;
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; } = false;
    }

    public class LaserScanListener : TopicListener
    {
        RadialScanResource resource;
        DisplayNode node;

        public override DisplayData DisplayData { get; set; }

        public Vector2 MeasuredIntensityBounds => resource.MeasuredIntensityBounds;

        public int Size => resource.Size;

        readonly LaserScanConfiguration config = new LaserScanConfiguration();
        public LaserScanConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                PointSize = value.PointSize;
                Colormap = value.Colormap;
                UseIntensity = value.UseIntensity;
                ForceMinMax = value.ForceMinMax;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
                UseLines = value.UseLines;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                resource.Visible = value;
            }
        }

        public float PointSize
        {
            get => config.PointSize;
            set
            {
                config.PointSize = value;
                resource.PointSize = value;
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                resource.Colormap = value;
            }
        }

        public bool UseIntensity
        {
            get => config.UseIntensity;
            set
            {
                config.UseIntensity = value;
                resource.UseIntensityNoRange = value;
            }
        }

        public bool ForceMinMax
        {
            get => config.ForceMinMax;
            set
            {
                config.ForceMinMax = value;
                resource.ForceMinMax = value;
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                resource.FlipMinMax = value;
            }
        }


        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                resource.MinIntensity = value;
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                resource.MaxIntensity = value;
            }
        }

        public bool UseLines
        {
            get => config.UseLines;
            set
            {
                config.UseLines = value;
                resource.UseLines = value;
            }
        }

        void Awake()
        {
            transform.parent = TFListener.ListenersFrame.transform;

            node = SimpleDisplayNode.Instantiate("LaserScanNode", transform);
            resource = ResourcePool.GetOrCreate<RadialScanResource>(Resource.Markers.RadialScanResource, node.transform);
            Config = new LaserScanConfiguration();
        }

        public override void StartListening()
        {
            base.StartListening();
            Listener = new RosListener<LaserScan>(config.Topic, Handler);
            name = "[" + config.Topic + "]";
            node.name = "[" + config.Topic + "]";
        }

        void Handler(LaserScan msg)
        {
            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp.ToDateTime());

            if (float.IsNaN(msg.AngleMin) || float.IsNaN(msg.AngleMax) ||
                float.IsNaN(msg.RangeMin) || float.IsNaN(msg.RangeMax) ||
                float.IsNaN(msg.AngleIncrement))
            {
                Logger.Info("LaserScanListener: NaN in header!");
                return;
            }

            if (msg.AngleMin >= msg.AngleMax || msg.RangeMin >= msg.RangeMax)
            {
                Logger.Info("LaserScanListener: Invalid angle or range dimensions!");
                return;
            }

            if (msg.Intensities.Length != 0 && msg.Intensities.Length != msg.Ranges.Length)
            {
                Logger.Info("LaserScanListener: Invalid intensities length!");
                return;
            }

            resource.Set(msg.AngleMin, msg.AngleIncrement, msg.RangeMin, msg.RangeMax, msg.Ranges, msg.Intensities);
        }

        public override void Stop()
        {
            base.Stop();

            resource.Stop();
            ResourcePool.Dispose(Resource.Markers.RadialScanResource, resource.gameObject);

            node.Stop();
            Destroy(node);
        }
    }
}
