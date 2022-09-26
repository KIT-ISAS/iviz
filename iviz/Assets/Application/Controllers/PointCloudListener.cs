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

        public IEnumerable<string> FieldNames => fieldNames;

        public override IListener Listener { get; }

        public PointCloudListener(PointCloudConfiguration? config, string topic)
        {
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

        bool Handler(PointCloud2 msg, IRosConnection _)
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
            if (msg.Width == 0 || msg.Height == 0)
            {
                return 0;
            }

            bool xyzAligned = xOffset == 0 && yOffset == 4 && zOffset == 8;
            return xyzAligned
                ? GeneratePointBufferXYZ(pointBuffer, msg, iOffset, rgbaHint ? PointField.FLOAT32 : iType)
                : GeneratePointBufferSlow(pointBuffer, msg, xOffset, yOffset, zOffset, iOffset, iType, rgbaHint);
        }
        
        static unsafe int GeneratePointBufferSlow(float4[] pointBuffer, PointCloud2 msg, int xOffset, int yOffset,
            int zOffset, int iOffset, int iType, bool rgbaHint)
        {
            static float ReadFloat(byte* ptr) => *(float*)ptr;

            int height = (int)msg.Height;
            int width = (int)msg.Width;
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;

            if (rowStep > width * pointStep) ThrowHelper.ThrowArgumentOutOfRange();

            if (rgbaHint) Process(new IFloatReader.FloatReader());

            return iType switch
            {
                PointField.FLOAT32 => Process(new IFloatReader.FloatReader()),
                PointField.FLOAT64 => Process(new IFloatReader.DoubleReader()),
                PointField.INT8 => Process(new IFloatReader.SbyteReader()),
                PointField.UINT8 => Process(new IFloatReader.ByteReader()),
                PointField.INT16 => Process(new IFloatReader.ShortReader()),
                PointField.UINT16 => Process(new IFloatReader.UshortReader()),
                PointField.INT32 => Process(new IFloatReader.IntReader()),
                PointField.UINT32 => Process(new IFloatReader.UintReader()),
                _ => Process(new IFloatReader.NullReader()),
            };

            int Process<T>(T t) where T : struct, IFloatReader
            {
                int dstOff = 0;
                fixed (byte* dataPtr = msg.Data.Array)
                {
                    byte* rowPtr = dataPtr;
                    for (int v = 0; v < height; v++)
                    {
                        byte* pointPtr = rowPtr;

                        for (int u = 0; u < width; u++, pointPtr += pointStep)
                        {
                            float x = ReadFloat(pointPtr + xOffset);
                            float y = ReadFloat(pointPtr + yOffset);
                            float z = ReadFloat(pointPtr + zOffset);

                            ref var f = ref pointBuffer[dstOff];
                            f.x = -y;
                            f.y = z;
                            f.z = x;
                            f.w = t.Read(pointPtr + iOffset);

                            if (IsPointValid(f)) dstOff++;
                        }

                        rowPtr += rowStep;
                    }

                    return dstOff;
                }
            }
        }

        static unsafe int GeneratePointBufferXYZ(float4[] dstBuffer, PointCloud2 msg, int iOffset, int iType)
        {
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            int height = (int)msg.Height;
            int width = (int)msg.Width;

            return iType switch
            {
                PointField.FLOAT32 when iOffset == 8 && pointStep == 12 => ParseXyz(), // xyz
                PointField.FLOAT32 when iOffset == 12 && pointStep == 16 => ParseXyzw(), // xyzw
                PointField.FLOAT32 when iOffset == 8 && pointStep == 16 => ParseXyzz(), // xyzz
                PointField.FLOAT32 => Parse(new IFloatReader.FloatReader()),
                PointField.FLOAT64 => Parse(new IFloatReader.DoubleReader()),
                PointField.INT8 => Parse(new IFloatReader.SbyteReader()),
                PointField.UINT8 => Parse(new IFloatReader.ByteReader()),
                PointField.INT16 => Parse(new IFloatReader.ShortReader()),
                PointField.UINT16 => Parse(new IFloatReader.UshortReader()),
                PointField.INT32 => Parse(new IFloatReader.IntReader()),
                PointField.UINT32 => Parse(new IFloatReader.UintReader()),
                _ => throw new IndexOutOfRangeException()
            };

            // ----------       

            int ParseXyz() // xyz
            {
                int dstOff = 0;
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var src = dataRow.Cast<float3>()[..width];
                    var dst = dstBuffer.AsSpan(dstOff, width);
                    fixed (float3* srcPtr = &src[0])
                    fixed (float4* dstPtr = &dst[0])
                    {
                        dstOff += BurstUtils.ParseFloat3(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int ParseXyzw() // xyzw
            {
                int dstOff = 0;
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var src = dataRow.Cast<float4>()[..width];
                    var dst = dstBuffer.AsSpan(dstOff, width);
                    fixed (float4* srcPtr = &src[0])
                    fixed (float4* dstPtr = &dst[0])
                    {
                        dstOff += BurstUtils.ParseFloat4(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int ParseXyzz() // xyzz
            {
                int dstOff = 0;
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var src = dataRow.Cast<float4>()[..width];
                    var dst = dstBuffer.AsSpan(dstOff, width);
                    fixed (float4* srcPtr = &src[0])
                    fixed (float4* dstPtr = &dst[0])
                    {
                        dstOff += BurstUtils.ParseFloat4Z(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int Parse<T>(T t) where T : struct, IFloatReader
            {
                int dstOff = 0;
                fixed (byte* dataPtr = msg.Data.Array)
                {
                    byte* rowPtr = dataPtr;
                    for (int v = 0; v < height; v++)
                    {
                        byte* pointPtr = rowPtr;

                        for (int u = 0; u < width; u++, pointPtr += pointStep)
                        {
                            ref var point = ref *(float3*)pointPtr;
                            
                            float4 f;
                            f.z = point.x;
                            f.x = -point.y;
                            f.y = point.z;
                            f.w = t.Read(pointPtr + iOffset);

                            dstBuffer[dstOff] = f;
                            if (IsPointValid(point)) dstOff++;
                        }

                        rowPtr += rowStep;
                    }
                }

                return dstOff;
            }
            
            // ----------       
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
        static bool IsPointValid(in float3 point) => IsValid(point.x) && IsValid(point.y) && IsValid(point.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsPointValid(in float4 point) => IsValid(point.x) && IsValid(point.y) && IsValid(point.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsValid(float f) => f.IsValid() && Mathf.Abs(f) < MaxPositionMagnitude;

        unsafe interface IFloatReader
        {
            float Read(byte* b);

            struct FloatReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => *(float*)b;
            }

            struct DoubleReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => (float)*(double*)b;
            }

            struct SbyteReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => *(sbyte*)b;
            }

            struct ByteReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => *b;
            }

            struct IntReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => *(int*)b;
            }

            struct UintReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => *(uint*)b;
            }

            struct ShortReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => *(short*)b;
            }

            struct UshortReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => *(ushort*)b;
            }

            struct NullReader : IFloatReader
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public float Read(byte* b) => 0;
            }
        }

        [BurstCompile]
        static unsafe class BurstUtils
        {
            [BurstCompile(CompileSynchronously = true)]
            public static int ParseFloat3([NoAlias] float3* input, [NoAlias] float4* output, int inputLength)
            {
                int o = 0;
                for (int i = 0; i < inputLength; i++)
                {
                    float3 point3 = input[i];

                    float4 point;
                    point.x = point3.x;
                    point.y = point3.y;
                    point.z = point3.z;
                    point.w = point3.z;

                    float4 f;
                    f.z = point.x;
                    f.x = -point.y;
                    f.y = point.z;
                    f.w = point.w;

                    output[o] = f;

                    if (Hint.Likely(IsPointValid4(f))) o++;
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

                    float4 f;
                    f.z = point.x;
                    f.x = -point.y;
                    f.y = point.z;
                    f.w = point.w;

                    output[o] = f;

                    if (Hint.Likely(IsPointValid3(point))) o++;
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

                    float4 f;
                    f.z = point.x;
                    f.x = -point.y;
                    f.y = point.z;
                    f.w = point.z;

                    output[o] = f;

                    if (Hint.Likely(IsPointValid4(f))) o++;
                }

                return o;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static bool IsPointValid3(in float4 point)
            {
                float4 p;
                p.x = point.x;
                p.y = point.y;
                p.z = point.z;
                p.w = point.z;
                return IsPointValid4(p);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static bool IsPointValid4(in float4 point)
            {
                return math.all(math.abs(point) < MaxPositionMagnitude) 
                       && math.all(math.isfinite(point));
            }
        }
    }
}