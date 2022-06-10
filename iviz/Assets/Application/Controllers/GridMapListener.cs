#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
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
                Tint = value.Tint.ToUnity();
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
                resource.OverrideIntensityBounds = value;
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
            get => config.Tint.ToUnity();
            set
            {
                config.Tint = value.ToRos();
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
                string minIntensityStr = UnityUtils.FormatFloat(MeasuredIntensityBounds.x);
                string maxIntensityStr = UnityUtils.FormatFloat(MeasuredIntensityBounds.y);
                string cellSizeStr = UnityUtils.FormatFloat(cellSize);
                
                return $"<b>{numCellsX.ToString("N0")}x{numCellsY.ToString("N0")} cells | " +
                       $"{cellSizeStr} m/cell</b>\n" +
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

            Listener = new Listener<GridMap>(Config.Topic, Handle);
        }

        void Handle(GridMap msg)
        {
            static bool IsInvalidSize(double x) => x.IsInvalid() || x <= 0;

            if (IsInvalidSize(msg.Info.LengthX) ||
                IsInvalidSize(msg.Info.LengthY) ||
                IsInvalidSize(msg.Info.Resolution))
            {
                RosLogger.Info($"{this}: Message info has invalid values!");
                return;
            }

            int widthByResolution = (int)(msg.Info.LengthX / msg.Info.Resolution + 0.5);
            int heightByResolution = (int)(msg.Info.LengthY / msg.Info.Resolution + 0.5);

            if (widthByResolution > MaxGridSize || heightByResolution > MaxGridSize)
            {
                // quit quickly
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

            int layer = string.IsNullOrWhiteSpace(IntensityChannel) ? 0 : fieldNames.IndexOf(IntensityChannel);
            if (layer == -1 || layer >= msg.Data.Length)
            {
                RosLogger.Info($"{this}: Gridmap layer {layer.ToString()} is missing!");
                return;
            }

            var multiArray = msg.Data[layer];
            int length = multiArray.Data.Length;

            if (multiArray.Layout.Dim.Length == 0)
            {
                RosLogger.Error($"{this}: MultiArray layout has not been set!");
                return;
            }

            if (multiArray.Layout.Dim.Length != 2)
            {
                RosLogger.Error($"{this}: Only layouts with 2 dimensions are supported");
                return;
            }

            uint height = multiArray.Layout.Dim[0].Size;
            uint width = multiArray.Layout.Dim[1].Size;

            if (width > MaxGridSize || height > MaxGridSize)
            {
                RosLogger.Info($"{this}: Gridmap is too large! Iviz only supports gridmap sizes " +
                               $"up to {MaxGridSize.ToString()}");
                return;
            }

            uint expectedLength = width * height + multiArray.Layout.DataOffset;
            if (length < expectedLength)
            {
                RosLogger.Error($"{this}: Gridmap layer size does not match. " +
                                $"Expected {expectedLength.ToString()} entries, but got {length.ToString()}!");
                return;
            }

            if (multiArray.Layout.Dim[0].Stride != width * height
                || multiArray.Layout.Dim[1].Stride != width)
            {
                RosLogger.Error($"{this}: Padded strides are not supported");
                return;
            }

            const string rowMajorLabel = "column_index";
            if (multiArray.Layout.Dim[0].Label != rowMajorLabel)
            {
                RosLogger.Error($"{this}: For rviz compatibility, the matrix data should be row-major. " +
                                $"Indicate this by setting the first label in the layout to \"{rowMajorLabel}\".");
                return;
            }

            node.AttachTo(msg.Info.Header);
            node.Transform.SetLocalPose(msg.Info.Pose.Ros2Unity());

            int offset = (int)multiArray.Layout.DataOffset;
            int size = (int)(width * height);

            if (size != 0)
            {
                resource.Set((int)width, (int)height,
                    (float)msg.Info.LengthX, (float)msg.Info.LengthY,
                    multiArray.Data.AsSpan().Slice(offset, size));
            }
            else
            {
                resource.Reset();
            }

            numCellsX = widthByResolution;
            numCellsY = heightByResolution;
            cellSize = (float)msg.Info.Resolution;
        }

        public override void ResetController()
        {
            base.ResetController();
            resource.Reset();
        }

        public override void Dispose()
        {
            base.Dispose();
            resource.ReturnToPool();
            node.Dispose();
        }
    }
}