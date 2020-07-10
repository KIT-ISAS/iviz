
using System;
using System.Runtime.Serialization;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class GridMapConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.GridMap;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        //[DataMember] public string IntensityChannel { get; set; } = "x";
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.hsv;
        [DataMember] public bool ForceMinMax { get; set; } = false;
        [DataMember] public float MinIntensity { get; set; } = 0;
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; } = false;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }
    
    public class GridMapListener : ListenerController
    {
        DisplayNode node;
        GridMapResource resource;
        
        public override ModuleData ModuleData { get; set; }

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
                //IntensityChannel = value.IntensityChannel;
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

        /*
        public string IntensityChannel
        {
            get => config.IntensityChannel;
            set => config.IntensityChannel = value;
        } 
        */
        
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
                if (config.ForceMinMax)
                {
                    resource.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
                else
                {
                    resource.IntensityBounds = MeasuredIntensityBounds;
                }
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
                    Listener.MaxQueueSize = (int)value;
                }
            }
        }

        void Awake()
        {
            node = SimpleDisplayNode.Instantiate("[GridMapNode]");
            resource = ResourcePool.GetOrCreate<GridMapResource>(Resource.Displays.PointList, node.transform);

            Config = new GridMapConfiguration();            
        }
        
        public override void StartListening()
        {
            Listener = new RosListener<GridMap>(config.Topic, Handler);
            Listener.MaxQueueSize = (int)MaxQueueSize;
            name = "[" + config.Topic + "]";
        }
        
        void Handler(GridMap msg)
        {
            node.AttachTo(msg.Info.Header.FrameId, msg.Info.Header.Stamp);

            int width = (int)(msg.Info.LengthX / msg.Info.Resolution + 0.5);
            int height = (int)(msg.Info.LengthY / msg.Info.Resolution + 0.5);

            resource.Set(width, height, (float)msg.Info.LengthX, (float)msg.Info.LengthY, msg.Data[0].Data);
        }

        public override void Stop()
        {
            base.Stop();

            ResourcePool.Dispose(Resource.Displays.GridMap, resource.gameObject);

            node.Stop();
            Destroy(node.gameObject);
        }
    }
}