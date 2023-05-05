#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Iviz.Core;
using Iviz.Msgs.SensorMsgs;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays.Helpers
{
    public static class PointCloudHelper
    {
        public static bool GeneratePointBuffer(ref float4[] pointBuffer, PointCloud2 msg, string intensityChannel,
            out bool rgbaHint,
            out int pointBufferLength)
        {
            int numPoints;
            uint pointStep = msg.PointStep;

            checked
            {
                numPoints = (int)(msg.Width * msg.Height);

                if (pointStep < 3 * sizeof(float))
                {
                    PointCloudHelperException.Throw("Invalid point step size!");
                }

                if (msg.RowStep < pointStep * msg.Width)
                {
                    PointCloudHelperException.Throw("Row step size does not correspond to point step size and width!");
                }

                if (msg.Data.Length < msg.RowStep * msg.Height)
                {
                    PointCloudHelperException.Throw("Data length does not correspond to row step size and height!");
                }
            }

            if (msg.Data.Length > NativeList.MaxElements)
            {
                PointCloudHelperException.Throw(
                    $"Number of elements is greater than maximum of {NativeList.MaxElements.ToString()}");
            }


            if (!TryGetField(msg.Fields, "x", out var xField) || xField.Datatype != PointField.FLOAT32 ||
                !TryGetField(msg.Fields, "y", out var yField) || yField.Datatype != PointField.FLOAT32 ||
                !TryGetField(msg.Fields, "z", out var zField) || zField.Datatype != PointField.FLOAT32)
            {
                PointCloudHelperException.Throw("Unsupported point cloud! " +
                                                "Expected three float data fields 'x', 'y', and 'z'.");
                rgbaHint = false; // unreachable
                pointBufferLength = 0;
                return false;
            }

            checked
            {
                if (xField.Offset + sizeof(float) > pointStep
                    || yField.Offset + sizeof(float) > pointStep
                    || zField.Offset + sizeof(float) > pointStep)
                {
                    PointCloudHelperException.Throw("Invalid position offsets");
                }
            }

            if (!TryGetField(msg.Fields, intensityChannel, out PointField? iField))
            {
                // nothing to do, unknown intensity field
                rgbaHint = false;
                pointBufferLength = 0;
                return false;
            }

            int iFieldSize = FieldSizeFromType(iField.Datatype);
            if (iFieldSize < 0)
            {
                PointCloudHelperException.Throw(
                    $"Invalid or unsupported intensity field type {iField.Datatype.ToString()}");
            }

            checked
            {
                if (iField.Offset + iFieldSize > pointStep)
                {
                    PointCloudHelperException.Throw($"Invalid field properties iOffset={iField.Offset.ToString()} " +
                                                    $"iFieldSize={iFieldSize.ToString()} dataType={iField.Datatype.ToString()}");
                }
            }

            if (xField.Count != 1 || yField.Count != 1 || zField.Count != 1 || iField.Count != 1)
            {
                PointCloudHelperException.Throw("Expected all point field counts to be 1");
            }

            int xOffset = (int)xField.Offset;
            int yOffset = (int)yField.Offset;
            int zOffset = (int)zField.Offset;
            int iOffset = (int)iField.Offset;
            rgbaHint = iFieldSize == 4 && iField.Name is "rgb" or "rgba";

            if (pointBuffer.Length < numPoints)
            {
                int desiredLength = Mathf.Max(pointBuffer.Length, 128);
                while (desiredLength < numPoints) desiredLength *= 2;
                pointBuffer = new float4[desiredLength];
            }

            pointBufferLength = GeneratePointBuffer(pointBuffer, msg, xOffset, yOffset, zOffset, iOffset,
                iField.Datatype, rgbaHint);
            return true;
        }

        static bool TryGetField(PointField[] fields, string name, [NotNullWhen(true)] out PointField? result)
        {
            foreach (var field in fields)
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

        public static bool FieldsEqual(List<string> fieldNames, PointField[] fields)
        {
            int length = fields.Length;

            if (fieldNames.Count != length)
            {
                return false;
            }

            for (int i = 0; i < length; i++)
            {
                if (fields[i].Name != fieldNames[i])
                {
                    return false;
                }
            }

            return true;
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
            int height = (int)msg.Height;
            int width = (int)msg.Width;
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            bool isDense = msg.IsDense;

            if (rowStep > width * pointStep) ThrowHelper.ThrowArgumentOutOfRange();

            if (rgbaHint)
            {
                return Process(new IFloatReader.FloatReader());
            }

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

            int Process<TFloatReader>(TFloatReader t) where TFloatReader : struct, IFloatReader
            {
                return isDense
                    ? ProcessImpl(t, new IBoolTrait.True())
                    : ProcessImpl(t, new IBoolTrait.False());
            }

            int ProcessImpl<TFloatReader, TBoolTrait>(TFloatReader t, TBoolTrait denseTrait)
                where TFloatReader : struct, IFloatReader where TBoolTrait : struct, IBoolTrait
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

                            if (denseTrait.Value() || IsPointValid(f)) dstOff++;
                        }

                        rowPtr += rowStep;
                    }

                    return dstOff;
                }
            }
        }

        static unsafe float ReadFloat(byte* ptr) => *(float*)ptr;

        static unsafe int GeneratePointBufferXYZ(float4[] dstBuffer, PointCloud2 msg, int iOffset, int iType)
        {
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            int height = (int)msg.Height;
            int width = (int)msg.Width;
            bool isDense = msg.IsDense;

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

            int ParseXyz() // xyz -> xyzz
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
                        dstOff += isDense
                            ? BurstUtilsDense.ParseFloat3(srcPtr, dstPtr, width)
                            : BurstUtilsNotDense.ParseFloat3(srcPtr, dstPtr, width);
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
                        dstOff += isDense
                            ? BurstUtilsDense.ParseFloat4(srcPtr, dstPtr, width)
                            : BurstUtilsNotDense.ParseFloat4(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int ParseXyzz() // xyzw -> xyzz
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
                        dstOff += isDense
                            ? BurstUtilsDense.ParseFloat4Z(srcPtr, dstPtr, width)
                            : BurstUtilsNotDense.ParseFloat4Z(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int Parse<T>(T t) where T : struct, IFloatReader
            {
                return isDense
                    ? ParseImpl(t, new IBoolTrait.True())
                    : ParseImpl(t, new IBoolTrait.False());
            }

            int ParseImpl<TFloatReader, TBoolTrait>(TFloatReader t, TBoolTrait denseTrait)
                where TFloatReader : struct, IFloatReader
                where TBoolTrait : struct, IBoolTrait
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
                            if (denseTrait.Value() || IsPointValid(point)) dstOff++;
                        }

                        rowPtr += rowStep;
                    }
                }

                return dstOff;
            }

            // ----------       
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

        interface IBoolTrait
        {
            bool Value();

            struct True : IBoolTrait
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool Value() => true;
            }

            struct False : IBoolTrait
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool Value() => false;
            }
        }

        [BurstCompile]
        static unsafe class BurstUtilsDense
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

        [BurstCompile]
        static unsafe class BurstUtilsNotDense
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

    internal class PointCloudHelperException : Exception
    {
        PointCloudHelperException(string msg, Exception? e) : base(msg, e)
        {
        }

        [DoesNotReturn]
        public static void Throw(string msg, Exception? e = null) => throw new PointCloudHelperException(msg, e);
    }
}