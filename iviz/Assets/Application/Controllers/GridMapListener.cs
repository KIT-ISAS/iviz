using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    [DataContract]
    public class GridMapConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.GridMap;
        [DataMember] public bool Visible { get; set; } = true;

        [DataMember] public string Topic { get; set; } = "";

        [DataMember] public string IntensityChannel { get; set; } = "";
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.hsv;
        [DataMember] public bool ForceMinMax { get; set; }
        [DataMember] public float MinIntensity { get; set; }
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; }
    }

    public sealed class GridMapListener : ListenerController
    {
        const int MaxGridSize = 4096;

        readonly FrameNode node;
        readonly FrameNode link;
        readonly GridMapResource resource;

        public override IModuleData ModuleData { get; }

        public Vector2 MeasuredIntensityBounds { get; private set; }

        public override TfFrame Frame => node.Parent;

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

        readonly List<string> fieldNames = new List<string>();

        public ReadOnlyCollection<string> FieldNames { get; }

        public GridMapListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new System.ArgumentNullException(nameof(moduleData));
         
            FieldNames = new ReadOnlyCollection<string>(fieldNames);
            
            node = FrameNode.Instantiate("[GridMapNode]");
            link = FrameNode.Instantiate("[GridMapLink]");
            link.transform.parent = node.Transform;
            resource = ResourcePool.Rent<GridMapResource>(Resource.Displays.GridMap, link.transform);

            Config = new GridMapConfiguration();
        }

        public override void StartListening()
        {
            Listener = new Listener<GridMap>(config.Topic, Handler);
        }

        static bool IsInvalidSize(double x)
        {
            return double.IsNaN(x) || x <= 0;
        }

        void Handler([NotNull] GridMap msg)
        {
            if (IsInvalidSize(msg.Info.LengthX) ||
                IsInvalidSize(msg.Info.LengthY) ||
                IsInvalidSize(msg.Info.Resolution) ||
                msg.Info.Pose.HasNaN())
            {
                Logger.Debug("GridMapListener: Message info has NaN!");
                return;
            }

            int width = (int) (msg.Info.LengthX / msg.Info.Resolution + 0.5);
            int height = (int) (msg.Info.LengthY / msg.Info.Resolution + 0.5);

            if (width > MaxGridSize || height > MaxGridSize)
            {
                Logger.Debug("GridMapListener: Gridmap is too large!");
                return;
            }

            if (msg.Data.Length == 0)
            {
                Logger.Debug("GridMapListener: Empty gridmap!");
                return;
            }

            fieldNames.Clear();
            fieldNames.AddRange(msg.Layers);

            int layer = string.IsNullOrEmpty(IntensityChannel) ? 0 : fieldNames.IndexOf(IntensityChannel);
            if (layer == -1 || layer >= msg.Data.Length)
            {
                Logger.Debug("GridMapListener: Gridmap layer is not available!");
                return;
            }

            int length = msg.Data[layer].Data.Length;
            if (length != width * height)
            {
                Logger.Error($"{this}: Gridmap layer size does not match. Expected {width * height}, but got {length}");
                return;
            }

            node.AttachTo(msg.Info.Header);
            link.transform.SetLocalPose(msg.Info.Pose.Ros2Unity());

            resource.Set(width, height,
                (float) msg.Info.LengthX, (float) msg.Info.LengthY,
                msg.Data[layer].Data, length);
            MeasuredIntensityBounds = resource.IntensityBounds;
            if (ForceMinMax)
            {
                resource.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
            }
        }

        public override void StopController()
        {
            base.StopController();

            resource.ReturnToPool();

            link.Stop();
            Object.Destroy(link.gameObject);
            node.Stop();
            Object.Destroy(node.gameObject);
        }
    }
}