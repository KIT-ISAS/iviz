using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Iviz.Displays;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Controllers
{
    [DataContract]
    public class GridMapConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.GridMap;
        [DataMember] public bool Visible { get; set; } = true;

        [DataMember] public string Topic { get; set; } = "";

        [DataMember] public string IntensityChannel { get; set; } = "";
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.hsv;
        [DataMember] public bool ForceMinMax { get; set; } = false;
        [DataMember] public float MinIntensity { get; set; } = 0;
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; } = false;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class GridMapListener : ListenerController
    {
        const int MaxGridSize = 4096;

        readonly DisplayNode node;
        readonly DisplayNode link;
        readonly GridMapResource resource;

        public override IModuleData ModuleData { get; }

        public Vector2 MeasuredIntensityBounds { get; private set; }

        public override TFFrame Frame => node.Parent;

        readonly GridMapConfiguration config = new GridMapConfiguration();

        public GridMapConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                IntensityChannel = value.IntensityChannel;
                Colormap = value.Colormap;
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

        public string IntensityChannel
        {
            get => config.IntensityChannel;
            set => config.IntensityChannel = value;
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

        public bool ForceMinMax
        {
            get => config.ForceMinMax;
            set
            {
                config.ForceMinMax = value;
                resource.IntensityBounds =
                    config.ForceMinMax ? new Vector2(MinIntensity, MaxIntensity) : MeasuredIntensityBounds;
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
                if (config.ForceMinMax)
                {
                    resource.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                if (config.ForceMinMax)
                {
                    resource.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
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

        readonly List<string> fieldNames = new List<string>();

        public ReadOnlyCollection<string> FieldNames { get; }

        public GridMapListener(IModuleData moduleData)
        {
            ModuleData = moduleData;
         
            FieldNames = new ReadOnlyCollection<string>(fieldNames);
            
            node = SimpleDisplayNode.Instantiate("[GridMapNode]");
            link = SimpleDisplayNode.Instantiate("[GridMapLink]", node.transform);
            resource = ResourcePool.GetOrCreate<GridMapResource>(Resource.Displays.GridMap, link.transform);

            Config = new GridMapConfiguration();
        }

        public override void StartListening()
        {
            Listener = new RosListener<GridMap>(config.Topic, Handler);
            Listener.MaxQueueSize = (int) MaxQueueSize;
            //name = "[" + config.Topic + "]";
        }

        static bool IsInvalidSize(double x)
        {
            return double.IsNaN(x) || x <= 0;
        }

        void Handler(GridMap msg)
        {
            if (IsInvalidSize(msg.Info.LengthX) ||
                IsInvalidSize(msg.Info.LengthY) ||
                IsInvalidSize(msg.Info.Resolution) ||
                msg.Info.Pose.HasNaN())
            {
                Debug.Log("GridMapListener: Message info has NaN!");
                return;
            }

            int width = (int) (msg.Info.LengthX / msg.Info.Resolution + 0.5);
            int height = (int) (msg.Info.LengthY / msg.Info.Resolution + 0.5);

            if (width > MaxGridSize || height > MaxGridSize)
            {
                Debug.Log("GridMapListener: Gridmap is too large!");
                return;
            }

            if (msg.Data.Length == 0)
            {
                Debug.Log("GridMapListener: Empty gridmap!");
                return;
            }

            fieldNames.Clear();
            fieldNames.AddRange(msg.Layers);

            int layer = string.IsNullOrEmpty(IntensityChannel) ? 0 : fieldNames.IndexOf(IntensityChannel);
            if (layer == -1 || layer >= msg.Data.Length)
            {
                Debug.Log("GridMapListener: Gridmap layer is not available!");
                return;
            }
            
            if (msg.Data[layer].Data.Length < width * height)
            {
                Debug.Log("GridMapListener: Gridmap layer is too small!");
                return;
            }

            node.AttachTo(msg.Info.Header.FrameId, msg.Info.Header.Stamp);
            link.transform.SetLocalPose(msg.Info.Pose.Ros2Unity());

            resource.Set(width, height,
                (float) msg.Info.LengthX, (float) msg.Info.LengthY, msg.Data[layer].Data);
            MeasuredIntensityBounds = resource.IntensityBounds;
            if (ForceMinMax)
            {
                resource.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
            }
        }

        public override void Stop()
        {
            base.Stop();

            ResourcePool.Dispose(Resource.Displays.GridMap, resource.gameObject);

            link.Stop();
            UnityEngine.Object.Destroy(link.gameObject);
            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
        }
    }
}