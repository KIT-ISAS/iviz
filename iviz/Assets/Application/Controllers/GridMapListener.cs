#nullable enable

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class GridMapListener : ListenerController
    {
        const int MaxGridSize = 4096;

        readonly FrameNode node;
        readonly FrameNode link;
        readonly GridMapResource resource;

        int numCellsX;
        int numCellsY;
        float cellSize;

        public override IModuleData ModuleData { get; }

        public Vector2 MeasuredIntensityBounds => resource.MeasuredIntensityBounds;

        public override TfFrame? Frame => node.Parent;

        readonly GridMapConfiguration config = new();

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
                Tint = value.Tint;
            }
        }

        public override bool Visible
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

        public ColormapId Colormap
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

        public Color Tint
        {
            get => config.Tint;
            set
            {
                config.Tint = value;
                resource.Tint = value;
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

        public string Description
        {
            get
            {
                string minIntensityStr = MeasuredIntensityBounds.x.ToString("#,0.##", UnityUtils.Culture);
                string maxIntensityStr = MeasuredIntensityBounds.y.ToString("#,0.##", UnityUtils.Culture);

                return $"<b>{numCellsX.ToString("N0")}x{numCellsY.ToString("N0")} cells | " +
                       $"{cellSize.ToString("#,0.###")} m/cell</b>\n" +
                       $"[{minIntensityStr} .. {maxIntensityStr}]";
            }
        }

        readonly List<string> fieldNames = new();

        public ReadOnlyCollection<string> FieldNames { get; }

        public override IListener Listener { get; }

        public GridMapListener(IModuleData moduleData, GridMapConfiguration? config, string topic)
        {
            ModuleData = moduleData ?? throw new System.ArgumentNullException(nameof(moduleData));

            FieldNames = fieldNames.AsReadOnly();

            node = FrameNode.Instantiate("[GridMapNode]");
            link = FrameNode.Instantiate("[GridMapLink]");
            link.Transform.parent = node.Transform;
            resource = ResourcePool.Rent<GridMapResource>(Resource.Displays.GridMap, link.Transform);

            Config = config ?? new GridMapConfiguration
            {
                Topic = topic,
            };

            Listener = new Listener<GridMap>(Config.Topic, Handler);
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
                RosLogger.Debug("GridMapListener: Message info has NaN!");
                return;
            }

            int width = (int)(msg.Info.LengthX / msg.Info.Resolution + 0.5);
            int height = (int)(msg.Info.LengthY / msg.Info.Resolution + 0.5);

            if (width > MaxGridSize || height > MaxGridSize)
            {
                RosLogger.Debug("GridMapListener: Gridmap is too large!");
                return;
            }

            if (msg.Data.Length == 0)
            {
                RosLogger.Debug("GridMapListener: Empty gridmap!");
                return;
            }

            fieldNames.Clear();
            fieldNames.AddRange(msg.Layers);

            int layer = string.IsNullOrEmpty(IntensityChannel) ? 0 : fieldNames.IndexOf(IntensityChannel);
            if (layer == -1 || layer >= msg.Data.Length)
            {
                RosLogger.Debug("GridMapListener: Gridmap layer is not available!");
                return;
            }

            int length = msg.Data[layer].Data.Length;
            if (length != width * height)
            {
                RosLogger.Error(
                    $"{this}: Gridmap layer size does not match. Expected {width * height}, but got {length}");
                return;
            }

            node.AttachTo(msg.Info.Header);
            link.transform.SetLocalPose(msg.Info.Pose.Ros2Unity());

            resource.Set(width, height,
                (float)msg.Info.LengthX, (float)msg.Info.LengthY,
                msg.Data[layer].Data, length);

            numCellsX = width;
            numCellsY = height;
            cellSize = (float)msg.Info.Resolution;
        }

        public override void Dispose()
        {
            base.Dispose();

            resource.ReturnToPool();

            link.DestroySelf();
            node.DestroySelf();
        }
    }
}