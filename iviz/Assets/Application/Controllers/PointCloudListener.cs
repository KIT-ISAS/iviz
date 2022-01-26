﻿#nullable enable

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
        readonly PointListDisplay pointCloud;
        readonly MeshListDisplay meshCloud;

        float4[] pointBuffer = Array.Empty<float4>();
        int pointBufferLength;
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

        public PointCloudListener(PointCloudConfiguration? config, string topic)
        {
            FieldNames = fieldNames.AsReadOnly();

            node = new FrameNode("[PointCloudListener]");
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

            Task.Run(() =>
            {
                ProcessMessage(msg);
                msg.Data.TryReturn();
            });

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

                if (msg.Data.Length > NativeList.MaxElements)
                {
                    RosLogger.Info(
                        $"{this}: Number of elements is greater than maximum of {NativeList.MaxElements.ToString()}");
                    IsProcessing = false;
                    return;
                }


                if (!FieldsEqual(msg.Fields))
                {
                    fieldNames.Clear();
                    foreach (var field in msg.Fields)
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
                int iFieldSize = FieldSizeFromType(iField.Datatype);
                if (iFieldSize == -1)
                {
                    RosLogger.Info($"{this}: Invalid or unsupported intensity field type {iField.Datatype.ToString()}");
                    IsProcessing = false;
                    return;
                }

                if (msg.PointStep < iOffset + iFieldSize)
                {
                    RosLogger.Info($"{this}: Invalid field properties iOffset={iOffset.ToString()} " +
                                   $"iFieldSize={iFieldSize.ToString()} dataType={iField.Datatype.ToString()}");
                    IsProcessing = false;
                    return;
                }


                bool rgbaHint = iFieldSize == 4 && iField.Name is "rgb" or "rgba";
                GeneratePointBuffer(msg, xOffset, yOffset, zOffset, iOffset, iField.Datatype, rgbaHint);
                {
                    bool useColormap = !rgbaHint;
                    var header = msg.Header;
                    int numPoints = (int)(msg.Width * msg.Height);
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
                                pointCloud.UseColormap = useColormap;
                                pointCloud.SetDirect(pointBuffer.AsReadOnlySpan(..pointBufferLength));
                                meshCloud.Reset();
                            }
                            else
                            {
                                meshCloud.UseColormap = useColormap;
                                meshCloud.SetDirect(pointBuffer.AsReadOnlySpan(..pointBufferLength));
                                pointCloud.Reset();
                            }
                        }
                        finally
                        {
                            IsProcessing = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error handling point cloud", e);
                IsProcessing = false;
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
            const float maxPositionMagnitude = PointListDisplay.MaxPositionMagnitude;

            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            int height = (int)msg.Height;
            int width = (int)msg.Width;

            //pointBufferLength = 0;
            if (pointBuffer.Length < msg.Width * msg.Height)
            {
                pointBuffer = new float4[msg.Width * msg.Height];
            }

            float4[] dstBuffer = pointBuffer;
            int dstOff = 0;

            switch (iType)
            {
                case PointField.FLOAT32 when iOffset == 12 && pointStep == 16:
                    ParseFloatAligned();
                    break;
                case PointField.FLOAT32:
                    ParseFloat();
                    break;
                case PointField.FLOAT64:
                    ParseDouble();
                    break;
                case PointField.INT8:
                    ParseInt8();
                    break;
                case PointField.UINT8:
                    ParseUint8();
                    break;
                case PointField.INT16:
                    ParseInt16();
                    break;
                case PointField.UINT16 when iOffset == 12 && pointStep == 14:
                    ParseUint16Aligned();
                    break;
                case PointField.UINT16:
                    ParseUint16();
                    break;
                case PointField.INT32:
                    ParseInt32();
                    break;
                case PointField.UINT32:
                    ParseUint32();
                    break;
            }

            pointBufferLength = dstOff;

            // ----------       
            
            void ParseFloatAligned()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow.Cast<float4>();
                    for (int u = 0; u < width; u++)
                    {
                        ref readonly var data = ref dataOff[u];
                        if (data.IsInvalid3() || data.MaxAbsCoeff3() > maxPositionMagnitude)
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
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void TryAdd(ReadOnlySpan<byte> span, float w)
            {
                var data = span.Read<float3>();
                if (data.IsInvalid() || data.MaxAbsCoeff() > maxPositionMagnitude)
                {
                    return;
                }

                ref float4 f = ref dstBuffer[dstOff++];
                f.x = -data.y;
                f.y = data.z;
                f.z = data.x;
                f.w = w;
            }            
            
            void ParseFloat()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow;
                    for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                    {
                        TryAdd(dataOff, dataOff[iOffset..].Read<float>());
                    }
                }
            }
            
            void ParseDouble()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow;
                    for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                    {
                        TryAdd(dataOff, (float)dataOff[iOffset..].Read<double>());
                    }
                }
            }
            
            void ParseInt8()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow;
                    for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                    {
                        TryAdd(dataOff, (sbyte)dataOff[iOffset]);
                    }
                }
            }

            void ParseUint8()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow;
                    for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                    {
                        TryAdd(dataOff, dataOff[iOffset]);
                    }
                }
            }

            void ParseInt16()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow;
                    for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                    {
                        TryAdd(dataOff, dataOff[iOffset..].Read<short>());
                    }
                }
            }
            
            void ParseUint16Aligned()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow.Cast<PointWithUshort>();
                    for (int u = 0; u < width; u++)
                    {
                        ref readonly var data = ref dataOff[u];
                        if (data.f.IsInvalid() || data.f.MaxAbsCoeff() > maxPositionMagnitude)
                        {
                            continue;
                        }

                        ref float4 f = ref dstBuffer[dstOff++];
                        f.x = -data.f.y;
                        f.y = data.f.z;
                        f.z = data.f.x;
                        f.w = data.w;
                    }
                }
            }

            void ParseUint16()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow;
                    for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                    {
                        TryAdd(dataOff, dataOff[iOffset..].Read<ushort>());
                    }
                }
            }

            void ParseInt32()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow;
                    for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                    {
                        TryAdd(dataOff, dataOff[iOffset..].Read<int>());
                    }
                }
            }

            void ParseUint32()
            {
                ReadOnlySpan<byte> dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow;
                    for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                    {
                        TryAdd(dataOff, dataOff[iOffset..].Read<uint>());
                    }
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            pointCloud.ReturnToPool();
            meshCloud.ReturnToPool();

            node.Dispose();
            pointBuffer = Array.Empty<float4>();
        }

        [StructLayout(LayoutKind.Sequential)]
        readonly struct PointWithUshort
        {
            public readonly float3 f;
            public readonly ushort w;
        }
    }
}