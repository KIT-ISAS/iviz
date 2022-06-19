#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
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
using Unity.Burst;
using Unity.Burst.CompilerServices;
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

        public int NumPoints { get; private set; }
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

        public bool IsIntensityUsed => pointCloud.UseColormap;

        public ReadOnlyCollection<string> FieldNames { get; }

        public override IListener Listener { get; }

        public PointCloudListener(PointCloudConfiguration? config, string topic)
        {
            FieldNames = fieldNames.AsReadOnly();

            node = new FrameNode("PointCloudListener");
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
                return false;
            }

            IsProcessing = true;

            var shared = msg.Data.Share();
            Task.Run(() =>
            {
                try
                {
                    ProcessMessage(msg);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error handling point cloud", e);
                    IsProcessing = false;
                }
                finally
                {
                    shared.TryReturn();
                }
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
            if (!node.IsAlive)
            {
                // we're dead
                IsProcessing = false;
                return;
            }

            int numPoints;

            checked
            {
                numPoints = (int)(msg.Width * msg.Height);

                if (msg.PointStep < 3 * sizeof(float) ||
                    msg.RowStep < msg.PointStep * msg.Width ||
                    msg.Data.Length < msg.RowStep * msg.Height)
                {
                    RosLogger.Error($"{this}: Invalid point cloud dimensions!");
                    IsProcessing = false;
                    return;
                }
            }

            if (msg.Data.Length > NativeList.MaxElements)
            {
                RosLogger.Error(
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
                RosLogger.Error($"{this}: Unsupported point cloud! Expected XYZ as floats.");
                IsProcessing = false;
                return;
            }

            checked
            {
                if (xField.Offset + sizeof(float) > msg.PointStep
                    || yField.Offset + sizeof(float) > msg.PointStep
                    || zField.Offset + sizeof(float) > msg.PointStep)
                {
                    RosLogger.Error($"{this}: Invalid position offsets");
                    IsProcessing = false;
                }
            }

            if (!TryGetField(msg.Fields, config.IntensityChannel, out PointField? iField))
            {
                IsProcessing = false;
                return;
            }

            int iFieldSize = FieldSizeFromType(iField.Datatype);
            if (iFieldSize < 0)
            {
                RosLogger.Error($"{this}: Invalid or unsupported intensity field type {iField.Datatype.ToString()}");
                IsProcessing = false;
                return;
            }

            checked
            {
                if (iField.Offset + iFieldSize > msg.PointStep)
                {
                    RosLogger.Error($"{this}: Invalid field properties iOffset={iField.Offset.ToString()} " +
                                    $"iFieldSize={iFieldSize.ToString()} dataType={iField.Datatype.ToString()}");
                    IsProcessing = false;
                    return;
                }
            }

            if (xField.Count != 1 || yField.Count != 1 || zField.Count != 1 || iField.Count != 1)
            {
                RosLogger.Error($"{this}: Expected all point field counts to be 1");
                IsProcessing = false;
                return;
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
                GeneratePointBuffer(pointBuffer, msg, xOffset, yOffset, zOffset, iOffset, iField.Datatype, rgbaHint);
            var pointBufferToUse = new Memory<float4>(pointBuffer, 0, pointBufferLength);

            bool useColormap = !rgbaHint;
            var header = msg.Header;
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
        }

        static int GeneratePointBuffer(float4[] pointBuffer, PointCloud2 msg, int xOffset, int yOffset,
            int zOffset, int iOffset, int iType, bool rgbaHint)
        {
            bool xyzAligned = xOffset == 0 && yOffset == 4 && zOffset == 8;
            return xyzAligned
                ? GeneratePointBufferXYZ(pointBuffer, msg, iOffset, rgbaHint ? PointField.FLOAT32 : iType)
                : GeneratePointBufferSlow(pointBuffer, msg, xOffset, yOffset, zOffset, iOffset, iType, rgbaHint);
        }

        delegate float IntensityFnDelegate(ReadOnlySpan<byte> span);

        static int GeneratePointBufferSlow(float4[] pointBuffer, PointCloud2 msg, int xOffset, int yOffset,
            int zOffset, int iOffset, int iType, bool rgbaHint)
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

            ReadOnlySpan<byte> dataSrc = msg.Data.AsSpan();

            int heightOff = 0;
            int dstOff = 0;
            for (int j = 0; j < height; j++)
            {
                var dataOff = dataSrc[heightOff..];
                heightOff += rowStep;

                for (int u = 0; u < width; u++)
                {
                    float x = dataOff[xOffset..].Read<float>();
                    float y = dataOff[yOffset..].Read<float>();
                    float z = dataOff[zOffset..].Read<float>();

                    if (IsInvalid(x) || IsInvalid(y) || IsInvalid(z))
                    {
                        dataOff = dataOff[pointStep..]; 
                        continue;
                    }
                    
                    ref var f = ref pointBuffer[dstOff++];
                    (f.x, f.y, f.z, f.w) = (-y, z, x, intensityFn(dataOff[iOffset..]));
                    dataOff = dataOff[pointStep..]; 
                }
            }

            return dstOff;
        }

        static unsafe int GeneratePointBufferXYZ(float4[] pointBuffer, PointCloud2 msg, int iOffset, int iType)
        {
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            int height = (int)msg.Height;
            int width = (int)msg.Width;

            float4[] dstBuffer = pointBuffer;
            int dstOff = 0;

            switch (iType)
            {
                case PointField.FLOAT32 when iOffset == 8 && pointStep == 12: // xyz
                    ParseFloatAligned3();
                    break;
                case PointField.FLOAT32 when iOffset == 12 && pointStep == 16: // xyzw
                    ParseFloatAligned();
                    break;
                case PointField.FLOAT32 when iOffset == 8 && pointStep == 16: // xyzz
                    ParseFloatAlignedZ();
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

            return dstOff;

            // ----------       

            void ParseFloatAligned()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var src = dataRow.Cast<float4>()[..width];
                    var dst = dstBuffer.AsSpan(dstOff, width);
                    fixed (float4* srcPtr = &src[0])
                    fixed (float4* dstPtr = &dst[0])
                    {
                        dstOff += Utils.ParseFloat4(srcPtr, dstPtr, width);
                    }
                }
            }
            
            void ParseFloatAlignedZ()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var src = dataRow.Cast<float4>()[..width];
                    var dst = dstBuffer.AsSpan(dstOff, width);
                    fixed (float4* srcPtr = &src[0])
                    fixed (float4* dstPtr = &dst[0])
                    {
                        dstOff += Utils.ParseFloat4Z(srcPtr, dstPtr, width);
                    }
                }
            }
            
            void ParseFloatAligned3()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var src = dataRow.Cast<float3>()[..width];
                    var dst = dstBuffer.AsSpan(dstOff, width);
                    fixed (float3* srcPtr = &src[0])
                    fixed (float4* dstPtr = &dst[0])
                    {
                        dstOff += Utils.ParseFloat3(srcPtr, dstPtr, width);
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void TryAdd(byte* dataPtr, float w)
            {
                ref readonly var point = ref *(float3*)dataPtr;
                if (IsPointInvalid3(point)) return;
                ref float4 f = ref dstBuffer[dstOff];
                f.z = point.x;
                f.x = -point.y;
                f.y = point.z;
                f.w = w;
                dstOff++;
            }

            void ParseFloat()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow[..(pointStep * width)];
                    fixed (byte* dataPtr0 = &dataOff[0])
                    {
                        byte* dataPtr = dataPtr0;
                        for (int u = width; u > 0; u--)
                        {
                            float value = *(float*)(dataPtr + iOffset);
                            TryAdd(dataPtr, value);
                            dataPtr += pointStep;
                        }
                    }
                }
            }

            void ParseDouble()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow[..(pointStep * width)];
                    fixed (byte* dataPtr0 = &dataOff[0])
                    {
                        byte* dataPtr = dataPtr0;
                        for (int u = width; u > 0; u--)
                        {
                            double value = *(double*)(dataPtr + iOffset);
                            TryAdd(dataPtr, (float)value);
                            dataPtr += pointStep;
                        }
                    }
                }
            }

            void ParseInt8()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow[..(pointStep * width)];
                    fixed (byte* dataPtr0 = &dataOff[0])
                    {
                        byte* dataPtr = dataPtr0;
                        for (int u = width; u > 0; u--)
                        {
                            sbyte value = *(sbyte*)(dataPtr + iOffset);
                            TryAdd(dataPtr, value);
                            dataPtr += pointStep;
                        }
                    }
                }
            }

            void ParseUint8()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow[..(pointStep * width)];
                    fixed (byte* dataPtr0 = &dataOff[0])
                    {
                        byte* dataPtr = dataPtr0;
                        for (int u = width; u > 0; u--)
                        {
                            byte value = *(dataPtr + iOffset);
                            TryAdd(dataPtr, value);
                            dataPtr += pointStep;
                        }
                    }
                }
            }

            void ParseInt16()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow[..(pointStep * width)];
                    fixed (byte* dataPtr0 = &dataOff[0])
                    {
                        byte* dataPtr = dataPtr0;
                        for (int u = width; u > 0; u--)
                        {
                            short value = *(short*)(dataPtr + iOffset);
                            TryAdd(dataPtr, value);
                            dataPtr += pointStep;
                        }
                    }
                }
            }

            void ParseUint16()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow[..(pointStep * width)];
                    fixed (byte* dataPtr0 = &dataOff[0])
                    {
                        byte* dataPtr = dataPtr0;
                        for (int u = width; u > 0; u--)
                        {
                            short value = *(short*)(dataPtr + iOffset);
                            TryAdd(dataPtr, value);
                            dataPtr += pointStep;
                        }
                    }
                }
            }

            void ParseInt32()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow[..(pointStep * width)];
                    fixed (byte* dataPtr0 = &dataOff[0])
                    {
                        byte* dataPtr = dataPtr0;
                        for (int u = width; u > 0; u--)
                        {
                            int value = *(int*)(dataPtr + iOffset);
                            TryAdd(dataPtr, value);
                            dataPtr += pointStep;
                        }
                    }
                }
            }

            void ParseUint32()
            {
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var dataOff = dataRow[..(pointStep * width)];
                    fixed (byte* dataPtr0 = &dataOff[0])
                    {
                        byte* dataPtr = dataPtr0;
                        for (int u = width; u > 0; u--)
                        {
                            uint value = *(uint*)(dataPtr + iOffset);
                            TryAdd(dataPtr, value);
                            dataPtr += pointStep;
                        }
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

        const float MaxPositionMagnitude = PointListDisplay.MaxPositionMagnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static unsafe int4 AsInt4(float4 point) => *(int4*)&point;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsPointInvalid4(in float4 point) =>
            math.any(math.abs(point).xyz > MaxPositionMagnitude) || math.any((AsInt4(point).xyz & 0x7FFFFFFF) == 0x7F800000);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsPointInvalid3(in float3 point) =>
            IsInvalid(point.x) || IsInvalid(point.y) || IsInvalid(point.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsInvalid(float f) => f.IsInvalid() || Mathf.Abs(f) > MaxPositionMagnitude;

        [BurstCompile]
        static unsafe class Utils
        {
            [BurstCompile(CompileSynchronously = true)]
            public static int ParseFloat3([NoAlias] float3* input, [NoAlias] float4* output, int inputLength)
            {
                int o = 0;
                for (int i = 0; i < inputLength; i++)
                {
                    float4 point;
                    point.x = input[i].x;
                    point.y = input[i].y;
                    point.z = input[i].z;
                    point.w = input[i].z;

                    if (Hint.Unlikely(IsPointInvalid4(point)))
                    {
                        continue;
                    }

                    float4 f;
                    f.z = point.x;
                    f.x = -point.y;
                    f.y = point.z;
                    f.w = point.w;
                    output[o++] = f;
                }

                return o;
            }

            [BurstCompile(CompileSynchronously = true)]
            public static int ParseFloat4([NoAlias] float4* input, [NoAlias] float4* output, int inputLength)
            {
                int o = 0;
                for (int i = 0; i < inputLength; i++)
                {
                    float4 point = input[i];

                    if (Hint.Unlikely(IsPointInvalid4(point)))
                    {
                        continue;
                    }

                    float4 f;
                    f.z = point.x;
                    f.x = -point.y;
                    f.y = point.z;
                    f.w = point.w;
                    output[o++] = f;
                }

                return o;
            }
            
            [BurstCompile(CompileSynchronously = true)]
            public static int ParseFloat4Z([NoAlias] float4* input, [NoAlias] float4* output, int inputLength)
            {
                int o = 0;
                for (int i = 0; i < inputLength; i++)
                {
                    float4 point = input[i];

                    if (Hint.Unlikely(IsPointInvalid4(point)))
                    {
                        continue;
                    }

                    float4 f;
                    f.z = point.x;
                    f.x = -point.y;
                    f.y = point.z;
                    f.w = point.z;
                    output[o++] = f;
                }

                return o;
            }
        }
    }
}