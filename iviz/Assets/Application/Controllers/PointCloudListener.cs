#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
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
        static readonly PointField EmptyPointField = new();

        readonly PointCloudConfiguration config = new();
        readonly List<string> fieldNames = new() { "x", "y", "z" };
        readonly FrameNode node;
        readonly PointListResource pointCloud;
        readonly MeshListResource meshCloud;

        float4[] pointBuffer = Array.Empty<float4>();
        int pointBufferLength;
        bool isProcessing;

        bool IsProcessing
        {
            get => isProcessing;
            set
            {
                isProcessing = value;
                Listener?.SetPause(value);
            }
        }

        public override IModuleData ModuleData { get; }

        public Vector2 MeasuredIntensityBounds =>
            PointCloudType == PointCloudType.Points
                ? pointCloud.MeasuredIntensityBounds
                : meshCloud.MeasuredIntensityBounds;

        public int NumPoints { get; private set; }
        public int NumValidPoints => pointBufferLength;

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
            set => config.IntensityChannel = value;
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
            }
        }

        public bool IsIntensityUsed => pointCloud != null && pointCloud.UseColormap;

        public ReadOnlyCollection<string> FieldNames { get; }

        public override IListener Listener { get; }

        public PointCloudListener(IModuleData moduleData, PointCloudConfiguration? config, string topic)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            FieldNames = fieldNames.AsReadOnly();
            node = FrameNode.Instantiate("[PointCloudNode]");
            pointCloud = ResourcePool.RentDisplay<PointListResource>(node.transform);
            meshCloud = ResourcePool.RentDisplay<MeshListResource>(node.transform);
            meshCloud.ShadowsEnabled = false;

            Config = config ?? new PointCloudConfiguration
            {
                Topic = topic,
            };

            Listener = new Listener<PointCloud2>(Config.Topic, Handler);
            node.name = $"[{Config.Topic}]";
        }

        static int FieldSizeFromType(int datatype)
        {
            switch (datatype)
            {
                case PointField.FLOAT64:
                    return 8;
                case PointField.FLOAT32:
                case PointField.INT32:
                case PointField.UINT32:
                    return 4;
                case PointField.INT16:
                case PointField.UINT16:
                    return 2;
                case PointField.INT8:
                case PointField.UINT8:
                    return 1;
                default:
                    return -1;
            }
        }

        bool Handler(PointCloud2 msg)
        {
            if (IsProcessing)
            {
                msg.Data.TryReturn();
                return false;
            }

            IsProcessing = true;

            Task.Run(() => ProcessMessage(msg));

            return true;
        }

        void UpdateSize()
        {
            float value = PointSize;
            pointCloud.ElementScale = value;
            meshCloud.ElementScale = value;
        }

        static bool TryGetField(PointField[] fields, string name, [NotNullWhen(true)] out PointField? result)
        {
            foreach (PointField field in fields)
            {
                if (field.Name != name)
                {
                    continue;
                }

                result = field;
                return true;
            }

            result = null;
            return false;
        }

        bool FieldsEqual(PointField[] fields)
        {
            if (fieldNames.Count != fields.Length)
            {
                return false;
            }

            foreach (var (field, fieldName) in fields.Zip(fieldNames))
            {
                if (field.Name != fieldName)
                {
                    return false;
                }
            }

            return true;
        }

        void ProcessMessage(PointCloud2 msg)
        {
            try
            {
                if (!node.IsAlive)
                {
                    // we're dead
                    IsProcessing = false;
                    return;
                }

                if (msg.PointStep < 3 * sizeof(float) ||
                    msg.RowStep < msg.PointStep * msg.Width ||
                    msg.Data.Length < msg.RowStep * msg.Height)
                {
                    RosLogger.Info($"{this}: Invalid point cloud dimensions!");
                    IsProcessing = false;
                    return;
                }

                if (!FieldsEqual(msg.Fields))
                {
                    fieldNames.Clear();
                    foreach (PointField field in msg.Fields)
                    {
                        fieldNames.Add(field.Name);
                    }
                }

                if (!TryGetField(msg.Fields, "x", out var xField) || xField.Datatype != PointField.FLOAT32 ||
                    !TryGetField(msg.Fields, "y", out var yField) || yField.Datatype != PointField.FLOAT32 ||
                    !TryGetField(msg.Fields, "z", out var zField) || zField.Datatype != PointField.FLOAT32)
                {
                    RosLogger.Info($"{this}: Unsupported point cloud! Expected XYZ as floats.");
                    IsProcessing = false;
                    return;
                }

                int xOffset = (int)xField.Offset;
                int yOffset = (int)yField.Offset;
                int zOffset = (int)zField.Offset;

                var iField = TryGetField(msg.Fields, config.IntensityChannel, out PointField? outField)
                    ? outField
                    : EmptyPointField;

                int iOffset = (int)iField.Offset;
                int iSize = FieldSizeFromType(iField.Datatype);
                if (iSize == -1 || msg.PointStep < iOffset + iSize)
                {
                    RosLogger.Info($"{this}: Invalid or unsupported intensity field type!");
                    IsProcessing = false;
                    return;
                }

                bool rgbaHint = iSize == 4 && iField.Name is "rgb" or "rgba";
                var header = msg.Header;
                int numPoints = (int)(msg.Width * msg.Height);

                GeneratePointBuffer(msg, xOffset, yOffset, zOffset, iOffset, iField.Datatype, rgbaHint);

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

                        NumPoints = numPoints;

                        if (pointBufferLength == 0)
                        {
                            pointCloud.Reset();
                            meshCloud.Reset();
                        }
                        else if (PointCloudType == PointCloudType.Points)
                        {
                            pointCloud.UseColormap = !rgbaHint;
                            pointCloud.SetDirect(pointBuffer.AsSpan(..pointBufferLength));
                            meshCloud.Reset();
                        }
                        else
                        {
                            meshCloud.UseColormap = !rgbaHint;
                            meshCloud.SetDirect(pointBuffer.AsSpan(..pointBufferLength));
                            pointCloud.Reset();
                        }
                    }
                    finally
                    {
                        IsProcessing = false;
                    }
                });
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error handling point cloud", e);
                IsProcessing = false;
            }
            finally
            {
                msg.Data.TryReturn();
            }
        }

        void GeneratePointBuffer(PointCloud2 msg, int xOffset, int yOffset,
            int zOffset, int iOffset,
            int iType, bool rgbaHint)
        {
            bool xyzAligned = xOffset == 0 && yOffset == 4 && zOffset == 8;
            if (xyzAligned)
            {
                GeneratePointBufferXYZ(msg, iOffset, rgbaHint ? PointField.FLOAT32 : iType);
            }
            else
            {
                GeneratePointBufferSlow(msg, xOffset, yOffset, zOffset, iOffset, iType, rgbaHint);
            }
        }

        delegate float IntensityFnDelegate(ReadOnlySpan<byte> span);

        void GeneratePointBufferSlow(PointCloud2 msg,
            int xOffset, int yOffset, int zOffset, int iOffset,
            int iType, bool rgbaHint)
        {
            int height = (int)msg.Height;
            int width = (int)msg.Width;
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;

            IntensityFnDelegate intensityFn;
            if (rgbaHint)
            {
                intensityFn = m => m.Read<float>();
            }
            else
            {
                intensityFn = iType switch
                {
                    PointField.FLOAT32 => m => m.Read<float>(),
                    PointField.FLOAT64 => m => (float)m.Read<double>(),
                    PointField.INT8 => m => (sbyte)m[0],
                    PointField.UINT8 => m => m[0],
                    PointField.INT16 => m => m.Read<short>(),
                    PointField.UINT16 => m => m.Read<ushort>(),
                    PointField.INT32 => m => m.Read<int>(),
                    PointField.UINT32 => m => m.Read<uint>(),
                    _ => _ => 0
                };
            }

            //pointBufferLength = 0;
            if (pointBuffer.Length < msg.Width * msg.Height)
            {
                pointBuffer = new float4[msg.Width * msg.Height];
            }

            ReadOnlySpan<byte> dataSrc = msg.Data.AsSpan();

            int heightOff = 0;
            int dstOff = 0;
            foreach (int _ in ..height)
            {
                var dataOff = dataSrc[heightOff..];
                heightOff += rowStep;

                foreach (int __ in ..width)
                {
                    float x = dataOff[xOffset..].Read<float>();
                    float y = dataOff[yOffset..].Read<float>();
                    float z = dataOff[zOffset..].Read<float>();

                    ref var f = ref pointBuffer[dstOff++];
                    (f.x, f.y, f.z, f.w) = (-y, z, x, intensityFn(dataOff[iOffset..]));
                    dataOff = dataOff[pointStep..];
                }
            }

            pointBufferLength = dstOff;
        }

        void GeneratePointBufferXYZ(PointCloud2 msg, int iOffset, int iType)
        {
            const float maxPositionMagnitude = PointListResource.MaxPositionMagnitude;

            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            int height = (int)msg.Height;
            int width = (int)msg.Width;

            //pointBufferLength = 0;
            if (pointBuffer.Length < msg.Width * msg.Height)
            {
                pointBuffer = new float4[msg.Width * msg.Height];
            }

            var dstBuffer = pointBuffer;
            int dstOff = 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void TryAdd(ReadOnlySpan<byte> span, float w)
            {
                var data = span.Read<float3>();
                if (data.HasNaN() || data.MaxAbsCoeff() > maxPositionMagnitude)
                {
                    return;
                }
                
                ref float4 f = ref dstBuffer[dstOff++];
                f.x = -data.y;
                f.y = data.z;
                f.z = data.x;
                f.w = w;
            }

            ReadOnlySpan<byte> dataRowOff = msg.Data.AsSpan();

            switch (iType)
            {
                case PointField.FLOAT32 when iOffset == 12:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            var data = dataOff.Read<float4>();
                            if (data.HasNaN() || data.MaxAbsCoeff3() > maxPositionMagnitude)
                            {
                                continue;
                            }
                            
                            ref float4 f = ref dstBuffer[dstOff++];
                            f.x = -data.y;
                            f.y = data.z;
                            f.z = data.x;
                            f.w = data.w;
                            //(f.x, f.y, f.z, f.w) = (-data.y, data.z, data.x, data.w);
                        }
                    }

                    break;
                case PointField.FLOAT32:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff, dataOff[iOffset..].Read<float>());
                        }
                    }

                    break;
                case PointField.FLOAT64:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff, (float)dataOff[iOffset..].Read<double>());
                        }
                    }

                    break;
                case PointField.INT8:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff, (sbyte)dataOff[iOffset]);
                        }
                    }

                    break;
                case PointField.UINT8:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff, dataOff[iOffset]);
                        }
                    }

                    break;
                case PointField.INT16:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff, dataOff[iOffset..].Read<short>());
                        }
                    }

                    break;
                case PointField.UINT16:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff, dataOff[iOffset..].Read<ushort>());
                        }
                    }

                    break;
                case PointField.INT32:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff, dataOff[iOffset..].Read<int>());
                        }
                    }

                    break;
                case PointField.UINT32:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff, dataOff[iOffset..].Read<uint>());
                        }
                    }

                    break;
            }

            pointBufferLength = dstOff;
        }

        public override void Dispose()
        {
            base.Dispose();

            pointCloud.ReturnToPool();
            meshCloud.ReturnToPool();

            node.DestroySelf();
            pointBuffer = Array.Empty<float4>();
        }
    }
}