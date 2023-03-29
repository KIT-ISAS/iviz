#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class PointCloudListener : ListenerController
    {
        readonly PointCloudConfiguration config = new();
        readonly List<string> fieldNames = new() { "x", "y", "z" };
        readonly FrameNode node;
        readonly PointListDisplay pointCloud;
        readonly MeshListDisplay meshCloud;

        PointCloud2? lastMessage;
        float4[] pointBuffer = Array.Empty<float4>();
        bool isProcessing;

        bool IsProcessing
        {
            get => isProcessing;
            set
            {
                isProcessing = value;
                Listener.SetPause(value);
            }
        }

        public Vector2 MeasuredIntensityBounds =>
            PointCloudType == PointCloudType.Points
                ? pointCloud.MeasuredIntensityBounds
                : meshCloud.MeasuredIntensityBounds;

        public int NumValidPoints { get; private set; }

        public override TfFrame? Frame => node.Parent;

        public PointCloudConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                IntensityChannel = value.IntensityChannel;
                PointSize = value.PointSize;
                Colormap = value.Colormap;
                OverrideMinMax = value.OverrideMinMax;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
                PointCloudType = value.PointCloudType;
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                pointCloud.Visible = value;
                meshCloud.Visible = value;
            }
        }

        public string IntensityChannel
        {
            get => config.IntensityChannel;
            set
            {
                config.IntensityChannel = value;
                RehandleLastMessage();
            }
        }

        public float PointSize
        {
            get => config.PointSize;
            set
            {
                config.PointSize = value;
                UpdateSize();
            }
        }

        public ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                pointCloud.Colormap = value;
                meshCloud.Colormap = value;
            }
        }

        public bool OverrideMinMax
        {
            get => config.OverrideMinMax;
            set
            {
                config.OverrideMinMax = value;
                pointCloud.OverrideIntensityBounds = value;
                pointCloud.IntensityBounds = value
                    ? new Vector2(MinIntensity, MaxIntensity)
                    : MeasuredIntensityBounds;
                meshCloud.OverrideIntensityBounds = value;
                meshCloud.IntensityBounds = value
                    ? new Vector2(MinIntensity, MaxIntensity)
                    : MeasuredIntensityBounds;
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                pointCloud.FlipMinMax = value;
                meshCloud.FlipMinMax = value;
            }
        }


        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                if (config.OverrideMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    meshCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                if (config.OverrideMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    meshCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public PointCloudType PointCloudType
        {
            get => config.PointCloudType;
            set
            {
                if (config.PointCloudType == value)
                {
                    return;
                }

                config.PointCloudType = value;
                pointCloud.Reset();
                meshCloud.Reset();

                switch (value)
                {
                    case PointCloudType.Cubes:
                        meshCloud.MeshResource = Resource.Displays.Cube;
                        break;
                    case PointCloudType.Spheres:
                        meshCloud.MeshResource = Resource.Displays.Sphere;
                        break;
                }

                RehandleLastMessage();
            }
        }

        public bool IsIntensityUsed => pointCloud.UseColormap;

        public IEnumerable<string> FieldNames => fieldNames;

        public override IListener Listener { get; }

        public PointCloudListener(PointCloudConfiguration? config, string topic)
        {
            node = new FrameNode(nameof(PointCloudListener));
            pointCloud = ResourcePool.RentDisplay<PointListDisplay>(node.Transform);
            meshCloud = ResourcePool.RentDisplay<MeshListDisplay>(node.Transform);
            meshCloud.EnableShadows = false;

            Config = config ?? new PointCloudConfiguration
            {
                Topic = topic,
            };

            node.Name = Config.Topic;
            Listener = new Listener<PointCloud2>(Config.Topic, Handler);
        }

        static int FieldSizeFromType(int datatype)
        {
            return datatype switch
            {
                PointField.FLOAT64 => 8,
                PointField.FLOAT32 => 4,
                PointField.INT32 => 4,
                PointField.UINT32 => 4,
                PointField.INT16 => 2,
                PointField.UINT16 => 2,
                PointField.INT8 => 1,
                PointField.UINT8 => 1,
                _ => -1
            };
        }

        PointCloud2 LastMessage
        {
            set
            {
                lastMessage?.Dispose();
                lastMessage = value;
            }
        }

        bool Handler(PointCloud2 msg, IRosConnection? _)
        {
            if (IsProcessing)
            {
                return false;
            }

            IsProcessing = true;

            msg.IncreaseRefCount();
            Task.Run(() =>
            {
                try
                {
                    if (!ProcessMessage(msg))
                    {
                        IsProcessing = false;
                    }
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error handling point cloud", e);
                    IsProcessing = false;
                }
                finally
                {
                    LastMessage = msg;
                }
            });

            return true;
        }

        void RehandleLastMessage()
        {
            if (lastMessage is { } msg)
            {
                Handler(msg, null);
            }
        }

        void UpdateSize()
        {
            float value = PointSize;
            pointCloud.ElementScale = value;
            meshCloud.ElementScale = value;
        }

        bool ProcessMessage(PointCloud2 msg)
        {
            if (!node.IsAlive)
            {
                // we're dead
                return false;
            }

            if (!PointCloudHelper.FieldsEqual(fieldNames, msg.Fields))
            {
                fieldNames.Clear();
                foreach (var field in msg.Fields)
                {
                    fieldNames.Add(field.Name);
                }
            }

            /*
            checked
            {
                numPoints = (int)(msg.Width * msg.Height);

                if (msg.PointStep < 3 * sizeof(float))
                {
                    RosLogger.Error($"{this}: Invalid point step size!");
                    return false;
                }

                if (msg.RowStep < msg.PointStep * msg.Width)
                {
                    RosLogger.Error($"{this}: Row step size does not correspond to point step size and width!");
                    return false;
                }

                if (msg.Data.Length < msg.RowStep * msg.Height)
                {
                    RosLogger.Error($"{this}: Data length does not correspond to row step size and height!");
                    return false;
                }
            }

            if (msg.Data.Length > NativeList.MaxElements)
            {
                RosLogger.Error(
                    $"{this}: Number of elements is greater than maximum of {NativeList.MaxElements.ToString()}");
                return false;
            }


            if (!TryGetField(msg.Fields, "x", out var xField) || xField.Datatype != PointField.FLOAT32 ||
                !TryGetField(msg.Fields, "y", out var yField) || yField.Datatype != PointField.FLOAT32 ||
                !TryGetField(msg.Fields, "z", out var zField) || zField.Datatype != PointField.FLOAT32)
            {
                RosLogger.Error($"{this}: Unsupported point cloud! " +
                                "Expected three float data fields 'x', 'y', and 'z'.");
                return false;
            }

            checked
            {
                if (xField.Offset + sizeof(float) > msg.PointStep
                    || yField.Offset + sizeof(float) > msg.PointStep
                    || zField.Offset + sizeof(float) > msg.PointStep)
                {
                    RosLogger.Error($"{this}: Invalid position offsets");
                    return false;
                }
            }

            if (!TryGetField(msg.Fields, config.IntensityChannel, out PointField? iField))
            {
                return false;
            }

            int iFieldSize = FieldSizeFromType(iField.Datatype);
            if (iFieldSize < 0)
            {
                RosLogger.Error($"{this}: Invalid or unsupported intensity field type {iField.Datatype.ToString()}");
                return false;
            }

            checked
            {
                if (iField.Offset + iFieldSize > msg.PointStep)
                {
                    RosLogger.Error($"{this}: Invalid field properties iOffset={iField.Offset.ToString()} " +
                                    $"iFieldSize={iFieldSize.ToString()} dataType={iField.Datatype.ToString()}");
                    return false;
                }
            }

            if (xField.Count != 1 || yField.Count != 1 || zField.Count != 1 || iField.Count != 1)
            {
                RosLogger.Error($"{this}: Expected all point field counts to be 1");
                return false;
            }

            int xOffset = (int)xField.Offset;
            int yOffset = (int)yField.Offset;
            int zOffset = (int)zField.Offset;
            int iOffset = (int)iField.Offset;
            bool rgbaHint = iFieldSize == 4 && iField.Name is "rgb" or "rgba";

            if (pointBuffer.Length < numPoints)
            {
                pointBuffer = new float4[numPoints];
            }

            int pointBufferLength =
                PointCloudHelper.GeneratePointBuffer(pointBuffer, msg, xOffset, yOffset, zOffset, iOffset,
                    iField.Datatype, rgbaHint);
                    */

            Memory<float4> pointBufferToUse;
            bool useColormap;
            var header = msg.Header;

            try
            {
                if (PointCloudHelper.GeneratePointBuffer(ref pointBuffer, msg, config.IntensityChannel,
                        out bool rgbaHint)
                    is not { } pointBufferLength)
                {
                    return false; // unknown intensity channel
                }
                
                pointBufferToUse = new Memory<float4>(pointBuffer, 0, pointBufferLength);
                useColormap = !rgbaHint;
            }
            catch (PointCloudHelperException e)
            {
                RosLogger.Error($"{this}: {e.Message}");
                return false;
            }

            GameThread.PostInListenerQueue(() =>
            {
                try
                {
                    if (!node.IsAlive)
                    {
                        // we're dead
                        return;
                    }

                    node.AttachTo(header);

                    NumValidPoints = pointBufferToUse.Length;

                    if (pointBufferToUse.Length == 0)
                    {
                        pointCloud.Reset();
                        meshCloud.Reset();
                    }
                    else if (PointCloudType == PointCloudType.Points)
                    {
                        pointCloud.UseColormap = useColormap;
                        pointCloud.Set(pointBufferToUse.Span);
                        meshCloud.Reset();
                    }
                    else
                    {
                        meshCloud.UseColormap = useColormap;
                        meshCloud.Set(pointBufferToUse.Span);
                        pointCloud.Reset();
                    }
                }
                finally
                {
                    IsProcessing = false;
                }
            });

            return true;
        }

        public override void Dispose()
        {
            base.Dispose();

            lastMessage?.Dispose();

            pointCloud.ReturnToPool();
            meshCloud.ReturnToPool();

            node.Dispose();
            pointBuffer = Array.Empty<float4>();
        }
    }
}