using System;
using System.Runtime.Serialization;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Roslib;
using UnityEngine;

namespace Iviz.Controllers
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
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class LaserScanListener : ListenerController
    {
        readonly RadialScanResource resource;
        readonly DisplayNode node;

        public override IModuleData ModuleData { get; }

        public override TFFrame Frame => node.Parent;

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
                UseLines = value.UseLines;
                ForceMinMax = value.ForceMinMax;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
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
                resource.UseIntensityNotRange = value;
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

        public LaserScanListener(IModuleData moduleData)
        {
            ModuleData = moduleData;
            //transform.parent = TFListener.ListenersFrame.transform;

            node = SimpleDisplayNode.Instantiate("[LaserScanNode]");
            resource = ResourcePool.GetOrCreate<RadialScanResource>(Resource.Displays.RadialScanResource, node.transform);
            Config = new LaserScanConfiguration();
        }

        public override void StartListening()
        {
            Listener = new RosListener<LaserScan>(config.Topic, Handler);
            Listener.MaxQueueSize = (int)MaxQueueSize;
            //name = "[" + config.Topic + "]";
            node.name = "[" + config.Topic + "]";
        }

        void Handler(LaserScan msg)
        {
            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

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
            ResourcePool.Dispose(Resource.Displays.RadialScanResource, resource.gameObject);

            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
        }
    }
}
