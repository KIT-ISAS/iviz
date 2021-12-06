#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Iviz.Common;
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
        //readonly NativeList<float4> pointBuffer = new();

        float4[] pointBuffer = Array.Empty<float4>();
        int pointBufferLength = 0;


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


        public PointCloudListener(IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            FieldNames = fieldNames.AsReadOnly();
            node = FrameNode.Instantiate("[PointCloudNode]");
            pointCloud = ResourcePool.RentDisplay<PointListResource>(node.transform);
            meshCloud = ResourcePool.RentDisplay<MeshListResource>(node.transform);
            meshCloud.CastShadows = false;
            Config = new PointCloudConfiguration();
        }

        public override IModuleData ModuleData { get; }

        public Vector2 MeasuredIntensityBounds => pointCloud.MeasuredIntensityBounds;

        public int Size { get; private set; }

        public override TfFrame? Frame => node.Parent;

        public PointCloudConfiguration Config
        {
            get => config;
            set
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

        public int SizeMultiplierPow10
        {
            get => config.SizeMultiplier;
            set
            {
                config.SizeMultiplier = value;
                UpdateSize();
            }
        }

        void UpdateSize()
        {
            float value = PointSize * Mathf.Pow(10, SizeMultiplierPow10);
            pointCloud.ElementScale = value;
            meshCloud.ElementScale = value;
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

        public void StartListening()
        {
            Listener = new Listener<PointCloud2>(config.Topic, Handler);
            node.name = $"[{config.Topic}]";
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
                return false;
            }

            IsProcessing = true;

            Task.Run(() => ProcessMessage(msg));

            return true;
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

                GeneratePointBuffer(msg, msg.Data, xOffset, yOffset, zOffset, iOffset, iField.Datatype, rgbaHint);

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

                        Size = numPoints;

                        if (pointBufferLength == 0)
                        {
                            pointCloud.Reset();
                            meshCloud.Reset();
                        }
                        else if (PointCloudType == PointCloudType.Points)
                        {
                            pointCloud.UseColormap = !rgbaHint;
                            pointCloud.SetDirect(pointBuffer[..pointBufferLength]);
                            meshCloud.Reset();
                        }
                        else
                        {
                            meshCloud.UseColormap = !rgbaHint;
                            meshCloud.SetDirect(pointBuffer[..pointBufferLength]);
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
        }

        void GeneratePointBuffer(PointCloud2 msg, byte[] dataSrc, int xOffset, int yOffset,
            int zOffset, int iOffset,
            int iType, bool rgbaHint)
        {
            bool xyzAligned = xOffset == 0 && yOffset == 4 && zOffset == 8;
            if (xyzAligned)
            {
                GeneratePointBufferXYZ(msg, dataSrc, iOffset, rgbaHint ? PointField.FLOAT32 : iType);
            }
            else
            {
                GeneratePointBufferSlow(msg, dataSrc, xOffset, yOffset, zOffset, iOffset, iType, rgbaHint);
            }
        }

        void GeneratePointBufferSlow(PointCloud2 msg, byte[] dataSrc,
            int xOffset, int yOffset, int zOffset, int iOffset,
            int iType, bool rgbaHint)
        {
            int heightOffset = 0;
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;

            Func<byte[], int, float> intensityFn;
            if (rgbaHint)
            {
                intensityFn = BitConverter.ToSingle;
            }
            else
            {
                intensityFn = iType switch
                {
                    PointField.FLOAT32 => BitConverter.ToSingle,
                    PointField.FLOAT64 => (m, o) => (float)BitConverter.ToDouble(m, o),
                    PointField.INT8 => (m, o) => (sbyte)m[o],
                    PointField.UINT8 => (m, o) => m[o],
                    PointField.INT16 => (m, o) => BitConverter.ToInt16(m, o),
                    PointField.UINT16 => (m, o) => BitConverter.ToUInt16(m, o),
                    PointField.INT32 => (m, o) => BitConverter.ToInt32(m, o),
                    PointField.UINT32 => (m, o) => BitConverter.ToUInt32(m, o),
                    _ => (_, _) => 0
                };
            }

            pointBufferLength = 0;
            if (pointBuffer.Length < msg.Width * msg.Height)
            {
                pointBuffer = new float4[msg.Width * msg.Height];
            }

            int o = 0;
            for (int v = (int)msg.Height; v > 0; v--, heightOffset += rowStep)
            {
                int rowOffset = heightOffset;
                for (int u = (int)msg.Width; u > 0; u--, rowOffset += pointStep)
                {
                    Vector3 xyz = new(
                        BitConverter.ToSingle(dataSrc, rowOffset + xOffset),
                        BitConverter.ToSingle(dataSrc, rowOffset + yOffset),
                        BitConverter.ToSingle(dataSrc, rowOffset + zOffset)
                    );
                    pointBuffer[o++] = new float4(
                        -xyz.y, xyz.z, xyz.x,
                        intensityFn(dataSrc, rowOffset + iOffset)
                    );
                }
            }

            pointBufferLength = o;
        }

        void GeneratePointBufferXYZ(PointCloud2 msg, byte[] dataSrc, int iOffset, int iType)
        {
            const float maxPositionMagnitude = PointListResource.MaxPositionMagnitude;

            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            int height = (int)msg.Height;
            int width = (int)msg.Width;

            pointBufferLength = 0;
            if (pointBuffer.Length < msg.Width * msg.Height)
            {
                pointBuffer = new float4[msg.Width * msg.Height];
            }

            float4[] dstBuffer = pointBuffer;
            int dstOff = 0;

            void Set(float x, float y, float z, float w)
            {
                ref float4 point = ref dstBuffer[dstOff++];
                (point.x, point.y, point.z, point.w) = (x, y, z, w);
            }

            void TryAdd(in float3 data, float w)
            {
                if (!data.HasNaN() && data.MaxAbsCoeff() <= maxPositionMagnitude)
                {
                    Set(-data.y, data.z, data.x, w);
                }
            }

            ReadOnlySpan<byte> dataRowOff = dataSrc;

            switch (iType)
            {
                case PointField.FLOAT32 when iOffset == 12:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            float4 data = dataOff.Read<float4>();
                            if (!data.HasNaN() && !(data.MaxAbsCoeff3() > maxPositionMagnitude))
                            {
                                Set(-data.y, data.z, data.x, data.w);
                            }
                        }
                    }

                    break;
                case PointField.FLOAT32:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff.Read<float3>(), dataOff[iOffset..].Read<float>());
                        }
                    }

                    break;
                case PointField.FLOAT64:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff.Read<float3>(), (float)dataOff[iOffset..].Read<double>());
                        }
                    }

                    break;
                case PointField.INT8:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff.Read<float3>(), (sbyte)dataOff[iOffset]);
                        }
                    }
                    
                    break;
                case PointField.UINT8:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff.Read<float3>(), dataOff[iOffset]);
                        }
                    }

                    break;
                case PointField.INT16:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff.Read<float3>(), dataOff[iOffset..].Read<short>());
                        }
                    }

                    break;
                case PointField.UINT16:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff.Read<float3>(), dataOff[iOffset..].Read<ushort>());
                        }
                    }

                    break;
                case PointField.INT32:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff.Read<float3>(), dataOff[iOffset..].Read<int>());
                        }
                    }

                    break;
                case PointField.UINT32:
                    for (int v = height; v > 0; v--, dataRowOff = dataRowOff[rowStep..])
                    {
                        var dataOff = dataRowOff;
                        for (int u = width; u > 0; u--, dataOff = dataOff[pointStep..])
                        {
                            TryAdd(dataOff.Read<float3>(), dataOff[iOffset..].Read<uint>());
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