#nullable enable

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Resources;
using Iviz.Ros;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class GridMapListener : ListenerController
    {
        const int MaxGridSize = 4096;

        readonly FrameNode node;
        readonly GridMapDisplay resource;

        int numCellsX;
        int numCellsY;
        float cellSize;

        public Vector2 MeasuredIntensityBounds => resource.MeasuredIntensityBounds;

        public override TfFrame? Frame => node.Parent;

        readonly GridMapConfiguration config = new();

        public GridMapConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                IntensityChannel = value.IntensityChannel;
                Colormap = value.Colormap;
                ForceMinMax = value.ForceMinMax;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
                Smoothness = value.Smoothness;
                Metallic = value.Metallic;
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
        
        public float Smoothness
        {
            get => config.Smoothness;
            set
            {
                config.Smoothness = value;
                resource.Smoothness = value;
            }
        }

        public float Metallic
        {
            get => config.Metallic;
            set
            {
                config.Metallic = value;
                resource.Metallic = value;
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

        public GridMapListener(GridMapConfiguration? config, string topic)
        {
            FieldNames = fieldNames.AsReadOnly();

            node = new FrameNode("GridMapNode");
            resource = ResourcePool.Rent<GridMapDisplay>(Resource.Displays.GridMap, node.Transform);

            Config = config ?? new GridMapConfiguration
            {
                Topic = topic,
                Id = topic,
            };

            Listener = new Listener<GridMap>(Config.Topic, Handler);
        }

        void Handler(GridMap msg)
        {
            static bool IsInvalidSize(double x) => x.IsInvalid() || x <= 0;

            if (IsInvalidSize(msg.Info.LengthX) ||
                IsInvalidSize(msg.Info.LengthY) ||
                IsInvalidSize(msg.Info.Resolution))
            {
                RosLogger.Info($"{this}: Message info has invalid values!");
                return;
            }

            int width = (int)(msg.Info.LengthX / msg.Info.Resolution + 0.5);
            int height = (int)(msg.Info.LengthY / msg.Info.Resolution + 0.5);

            if (width > MaxGridSize || height > MaxGridSize)
            {
                RosLogger.Info($"{this}: Gridmap is too large! Iviz only supports gridmap sizes " +
                               $"up to {MaxGridSize.ToString()}");
                return;
            }

            if (msg.Info.Pose.IsInvalid())
            {
                RosLogger.Info($"{this}: Pose contains invalid values!");
                return;
            }

            if (msg.OuterStartIndex != 0 || msg.InnerStartIndex != 0)
            {
                RosLogger.Info($"{this}: Nonzero start indices not implemented!");
                return;
            }

            fieldNames.Clear();
            fieldNames.AddRange(msg.Layers);

            int layer = string.IsNullOrEmpty(IntensityChannel) ? 0 : fieldNames.IndexOf(IntensityChannel);
            if (layer == -1 || layer >= msg.Data.Length)
            {
                RosLogger.Info($"{this}: Gridmap layer {layer.ToString()}is missing!");
                return;
            }

            int length = msg.Data[layer].Data.Length;
            if (length != width * height)
            {
                RosLogger.Error($"{this}: Gridmap layer size does not match. " +
                                $"Expected {(width * height).ToString()} entries, but got {length.ToString()}!");
                return;
            }
            
            node.AttachTo(msg.Info.Header);
            node.Transform.SetLocalPose(msg.Info.Pose.Ros2Unity());

            resource.Set(width, height, (float)msg.Info.LengthX, (float)msg.Info.LengthY, msg.Data[layer].Data);

            numCellsX = width;
            numCellsY = height;
            cellSize = (float)msg.Info.Resolution;
        }

        public override void Dispose()
        {
            base.Dispose();
            resource.ReturnToPool();
            node.Dispose();
        }
    }
}