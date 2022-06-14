#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Resources;
using Iviz.Ros;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class GridMapListener : ListenerController
    {
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

        public bool UseNormals
        {
            get => config.UseNormals;
            set
            {
                config.UseNormals = value;
                resource.UseNormals = value;
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

            var info = msg.Info;
            if (IsInvalidSize(info.LengthX) ||
                IsInvalidSize(info.LengthY) ||
                IsInvalidSize(info.Resolution))
            {
                RosLogger.Error($"{this}: {nameof(GridMapInfo)} has invalid values!");
                return;
            }

            int widthByResolution = (int)(info.LengthX / info.Resolution + 0.5);
            int heightByResolution = (int)(info.LengthY / info.Resolution + 0.5);

            int maxGridSize = Settings.MaxTextureSize;
            if (widthByResolution > maxGridSize || heightByResolution > maxGridSize)
            {
                // quit quickly
                RosLogger.Error($"{this}: Gridmap is too large! Iviz only supports gridmap sizes " +
                                $"up to {maxGridSize.ToString()}");
                return;
            }

            if (info.Pose.IsInvalid())
            {
                RosLogger.Error($"{this}: Pose contains invalid values!");
                return;
            }

            if (msg.OuterStartIndex != 0 || msg.InnerStartIndex != 0)
            {
                RosLogger.Error($"{this}: Nonzero start indices not implemented!");
                return;
            }

            fieldNames.Clear();
            fieldNames.AddRange(msg.Layers);

            int layer = string.IsNullOrWhiteSpace(IntensityChannel) ? 0 : fieldNames.IndexOf(IntensityChannel);
            if (layer == -1 || layer >= msg.Data.Length)
            {
                RosLogger.Error($"{this}: Gridmap layer {layer.ToString()} is missing!");
                return;
            }

            var multiArray = msg.Data[layer];
            var layout = multiArray.Layout;
            int dataLength = multiArray.Data.Length;

            if (layout.Dim.Length == 0)
            {
                RosLogger.Error($"{this}: MultiArray layout has not been set!");
                return;
            }

            if (layout.Dim.Length != 2)
            {
                RosLogger.Error($"{this}: Only layouts with 2 dimensions are supported");
                return;
            }

            uint height = layout.Dim[0].Size;
            uint width = layout.Dim[1].Size;
            uint numElements = width * height;

            if (width > maxGridSize || height > maxGridSize)
            {
                RosLogger.Error($"{this}: Gridmap is too large! Iviz only supports gridmap sizes " +
                                $"up to {maxGridSize.ToString()}");
                return;
            }

            uint expectedLength = numElements + layout.DataOffset;
            if (dataLength < expectedLength)
            {
                RosLogger.Error($"{this}: Gridmap layer size does not match. " +
                                $"Expected {expectedLength.ToString()} entries, but got {dataLength.ToString()}!");
                return;
            }

            if (layout.Dim[0].Stride < numElements || layout.Dim[1].Stride < width)
            {
                RosLogger.Error($"{this}: Strides are set incorrectly");
                return;
            }
            
            if (layout.Dim[0].Stride > width * height || layout.Dim[1].Stride > width)
            {
                RosLogger.Error($"{this}: Padded strides are not supported");
                return;
            }

            const string rowMajorLabel = "column_index";
            if (layout.Dim[0].Label != rowMajorLabel)
            {
                RosLogger.Error($"{this}: For rviz compatibility, the matrix data should be row-major. " +
                                $"Indicate this by setting the first label in the layout to \"{rowMajorLabel}\".");
                return;
            }

            node.AttachTo(info.Header);
            
            var origin = info.Pose.Ros2Unity();
            Pose validatedOrigin;
            if (!origin.IsUsable())
            {
                RosLogger.Warn($"{this}: Cannot use ({origin.position.x.ToString(BuiltIns.Culture)}, " +
                               $"{origin.position.y.ToString(BuiltIns.Culture)}, " +
                               $"{origin.position.z.ToString(BuiltIns.Culture)}) " +
                               "as position. Values too large!");
                validatedOrigin = Pose.identity;
            }
            else
            {
                validatedOrigin = origin;
            }
            
            node.Transform.SetLocalPose(validatedOrigin);

            int offset = (int)layout.DataOffset;
            int size = (int)(width * height);

            if (size != 0)
            {
                resource.Set((int)width, (int)height,
                    (float)info.LengthX, (float)info.LengthY,
                    multiArray.Data.AsSpan().Slice(offset, size));
            }
            else
            {
                resource.Reset();
            }

            numCellsX = widthByResolution;
            numCellsY = heightByResolution;
            cellSize = (float)info.Resolution;
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