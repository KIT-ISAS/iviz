#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Iviz.Core;
using Iviz.Displays.Helpers;
using Iviz.Msgs.SensorMsgs;
using JetBrains.Annotations;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays.PointCloudHelpers
{
    public static class PointCloudHelper
    {
        public static bool GeneratePointBuffer(SelfClearingBuffer pointBuffer, PointCloud2 msg,
            string intensityChannel,
            out bool rgbaHint,
            out ReadOnlyMemory<float4> points)
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

                uint expectedRowStep = pointStep * msg.Width;
                if (msg.RowStep < expectedRowStep)
                {
                    if (msg.Data.Length == numPoints * pointStep)
                    {
                        // broken row_step, but everything else looks fine
                        // fix it and move on
                        msg.RowStep = expectedRowStep;
                    }
                    else
                    {
                        PointCloudHelperException.Throw(
                            "Row step size does not correspond to point step size and width!");
                    }
                }

                if (msg.Data.Length < msg.RowStep * msg.Height)
                {
                    PointCloudHelperException.Throw("Data length does not correspond to width, height, and pointStep!");
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
                PointCloudHelperException.Throw(
                    "Unsupported point cloud! " +
                    "Expected three float data fields 'x', 'y', and 'z'.");
                rgbaHint = false; // unreachable
                points = default;
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
                points = default;
                return false;
            }

            int iFieldSize = FieldSizeFromType(iField.Datatype);
            if (iFieldSize < 0)
            {
                PointCloudHelperException.Throw(
                    $"Invalid or unsupported intensity field " +
                    $"type {iField.Datatype.ToString()}");
            }

            checked
            {
                if (iField.Offset + iFieldSize > pointStep)
                {
                    PointCloudHelperException.Throw(
                        $"Invalid field properties iOffset={iField.Offset.ToString()} " +
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

            var pointArray = pointBuffer.EnsureCapacity(numPoints);
            int numUsedPoints = GeneratePointBuffer(pointArray, msg, xOffset, yOffset, zOffset, iOffset,
                iField.Datatype, rgbaHint);
            points = new ReadOnlyMemory<float4>(pointArray, 0, numUsedPoints);
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
                ? GeneratePointBufferFast(pointBuffer, msg, iOffset, rgbaHint ? PointField.FLOAT32 : iType)
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
            byte[] array = msg.Data.Array;

            if (rowStep > width * pointStep) ThrowHelper.ThrowArgumentOutOfRange();

            if (rgbaHint)
            {
                return ProcessSlow(new IFloatReader.FloatReader());
            }

            return iType switch
            {
                PointField.FLOAT32 => ProcessSlow(new IFloatReader.FloatReader()),
                PointField.FLOAT64 => ProcessSlow(new IFloatReader.DoubleReader()),
                PointField.INT8 => ProcessSlow(new IFloatReader.SbyteReader()),
                PointField.UINT8 => ProcessSlow(new IFloatReader.ByteReader()),
                PointField.INT16 => ProcessSlow(new IFloatReader.ShortReader()),
                PointField.UINT16 => ProcessSlow(new IFloatReader.UshortReader()),
                PointField.INT32 => ProcessSlow(new IFloatReader.IntReader()),
                PointField.UINT32 => ProcessSlow(new IFloatReader.UintReader()),
                _ => ProcessSlow(new IFloatReader.NullReader()),
            };

            int ProcessSlow<TFloatReader>(TFloatReader t) where TFloatReader : struct, IFloatReader
            {
                return isDense
                    ? ProcessSlowImpl(t, new IBoolTrait.True())
                    : ProcessSlowImpl(t, new IBoolTrait.False());
            }

            int ProcessSlowImpl<TFloatReader, TBoolTrait>(TFloatReader t, TBoolTrait denseTrait)
                where TFloatReader : struct, IFloatReader where TBoolTrait : struct, IBoolTrait
            {
                int dstOff = 0;
                fixed (byte* dataPtr = array)
                {
                    byte* rowPtr = dataPtr;
                    for (int v = 0; v < height; v++)
                    {
                        byte* pointPtr = rowPtr;
                        for (int u = 0; u < width; u++, pointPtr += pointStep)
                        {
                            ref var f = ref pointBuffer[dstOff];
                            f.x = Unsafe.ReadUnaligned<float>(pointPtr + xOffset);
                            f.y = Unsafe.ReadUnaligned<float>(pointPtr + yOffset);
                            f.z = Unsafe.ReadUnaligned<float>(pointPtr + zOffset);
                            f.w = t.Read(pointPtr + iOffset);

                            if (denseTrait.Value() || IsPointValid(f)) dstOff++;
                        }

                        rowPtr += rowStep;
                    }

                    return dstOff;
                }
            }
        }

        static unsafe int GeneratePointBufferFast(float4[] dstBuffer, PointCloud2 msg, int iOffset, int iType)
        {
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            int height = (int)msg.Height;
            int width = (int)msg.Width;
            bool isDense = msg.IsDense;

            return iType switch
            {
                PointField.FLOAT32 when iOffset == 8 && pointStep == 12 => ProcessXyzToXyzz(), // xyz
                PointField.FLOAT32 when iOffset == 12 && pointStep == 16 => ProcessXyzw(), // xyzw
                PointField.FLOAT32 when iOffset == 8 && pointStep == 16 => ProcessXyzz(), // xyzz
                PointField.FLOAT32 when iOffset == 12 && pointStep == 20 => ProcessXyzwtToXyzw(), // xyzz
                PointField.FLOAT32 => ProcessGeneric(new IFloatReader.FloatReader()),
                PointField.FLOAT64 => ProcessGeneric(new IFloatReader.DoubleReader()),
                PointField.INT8 => ProcessGeneric(new IFloatReader.SbyteReader()),
                PointField.UINT8 => ProcessGeneric(new IFloatReader.ByteReader()),
                PointField.INT16 => ProcessGeneric(new IFloatReader.ShortReader()),
                PointField.UINT16 => ProcessGeneric(new IFloatReader.UshortReader()),
                PointField.INT32 => ProcessGeneric(new IFloatReader.IntReader()),
                PointField.UINT32 => ProcessGeneric(new IFloatReader.UintReader()),
                _ => throw new IndexOutOfRangeException()
            };

            // ----------       

            int ProcessXyzToXyzz() // xyz -> xyzz
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
                            ? BurstUtilsDense.ProcessXyzToXyzz(srcPtr, dstPtr, width)
                            : BurstUtilsNotDense.ProcessXyzToXyzz(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int ProcessXyzw() // xyzw
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
                            ? BurstUtilsDense.ProcessXyzw(srcPtr, dstPtr, width)
                            : BurstUtilsNotDense.ProcessXyzw(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int ProcessXyzz() // xyzw -> xyzz
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
                            ? BurstUtilsDense.ProcessXyzz(srcPtr, dstPtr, width)
                            : BurstUtilsNotDense.ProcessXyzz(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int ProcessXyzwtToXyzw() // xyz -> xyzz
            {
                int dstOff = 0;
                var dataRow = msg.Data.AsSpan();
                for (int v = height; v > 0; v--, dataRow = dataRow[rowStep..])
                {
                    var src = dataRow.Cast<float5>()[..width];
                    var dst = dstBuffer.AsSpan(dstOff, width);
                    fixed (float5* srcPtr = &src[0])
                    fixed (float4* dstPtr = &dst[0])
                    {
                        dstOff += isDense
                            ? BurstUtilsDense.ProcessXyzwtToXyzw(srcPtr, dstPtr, width)
                            : BurstUtilsNotDense.ProcessXyzwtToXyzw(srcPtr, dstPtr, width);
                    }
                }

                return dstOff;
            }

            int ProcessGeneric<T>(T t) where T : struct, IFloatReader
            {
                return isDense
                    ? ProcessGenericImpl(t, new IBoolTrait.True())
                    : ProcessGenericImpl(t, new IBoolTrait.False());
            }

            int ProcessGenericImpl<TFloatReader, TBoolTrait>(TFloatReader t, TBoolTrait denseTrait)
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
                            f.x = point.x;
                            f.y = point.y;
                            f.z = point.z;
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

        internal const float MaxPositionMagnitude = PointListDisplay.MaxPositionMagnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsPointValid(in float3 point) => IsValid(point.x) && IsValid(point.y) && IsValid(point.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsPointValid(in float4 point) => IsValid(point.x) && IsValid(point.y) && IsValid(point.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsValid(float f) => f.IsValid() && Mathf.Abs(f) < MaxPositionMagnitude;
    }

    internal unsafe interface IFloatReader
    {
        float Read(byte* b);

        struct FloatReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* b) => Unsafe.ReadUnaligned<float>(b);
        }

        struct DoubleReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* b) => (float)Unsafe.ReadUnaligned<double>(b);
        }

        struct SbyteReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* b) => (sbyte)*b;
        }

        struct ByteReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* b) => *b;
        }

        struct IntReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* b) => Unsafe.ReadUnaligned<int>(b);
        }

        struct UintReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* b) => Unsafe.ReadUnaligned<uint>(b);
        }

        struct ShortReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* b) => Unsafe.ReadUnaligned<short>(b);
        }

        struct UshortReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* b) => Unsafe.ReadUnaligned<ushort>(b);
        }

        struct NullReader : IFloatReader
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Read(byte* _) => 0;
        }
    }

    internal interface IBoolTrait
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
    internal static unsafe class BurstUtilsDense
    {
        [BurstCompile(CompileSynchronously = true)]
        public static int ProcessXyzToXyzz([NoAlias] float3* input, [NoAlias] float4* output, int inputLength)
        {
            int o = 0;
            for (int i = 0; i < inputLength; i++)
            {
                float3 point = input[i];

                float4 f;
                f.x = point.x;
                f.y = point.y;
                f.z = point.z;
                f.w = point.z;

                output[o++] = f;
            }

            return o;
        }

        [BurstCompile(CompileSynchronously = true)]
        public static int ProcessXyzw([NoAlias] float4* input, [NoAlias] float4* output, int inputLength)
        {
            Buffer.MemoryCopy(input, output, inputLength * 16, inputLength * 16);

            return inputLength;
        }

        [BurstCompile(CompileSynchronously = true)]
        public static int ProcessXyzz([NoAlias] float4* input, [NoAlias] float4* output, int inputLength)
        {
            int o = 0;
            for (int i = 0; i < inputLength; i++)
            {
                float4 point = input[i];

                float4 f;
                f.x = point.x;
                f.y = point.y;
                f.z = point.z;
                f.w = point.z;

                output[o++] = f;
            }

            return o;
        }

        [BurstCompile(CompileSynchronously = true)]
        public static int ProcessXyzwtToXyzw([NoAlias] float5* input, [NoAlias] float4* output, int inputLength)
        {
            int o = 0;
            for (int i = 0; i < inputLength; i++)
            {
                float5 point = input[i];

                float4 f;
                f.x = point.x;
                f.y = point.y;
                f.z = point.z;
                f.w = point.w;

                output[o++] = f;
            }

            return o;
        }
    }

    [BurstCompile]
    static unsafe class BurstUtilsNotDense
    {
        [BurstCompile(CompileSynchronously = true)]
        public static int ProcessXyzToXyzz([NoAlias] float3* input, [NoAlias] float4* output, int inputLength)
        {
            int o = 0;
            for (int i = 0; i < inputLength; i++)
            {
                float3 point = input[i];

                float4 f;
                f.x = point.x;
                f.y = point.y;
                f.z = point.z;
                f.w = point.z;

                output[o] = f;

                if (Hint.Likely(IsPointValid4(f))) o++;
            }

            return o;
        }

        [BurstCompile(CompileSynchronously = true)]
        public static int ProcessXyzw([NoAlias] float4* input, [NoAlias] float4* output, int inputLength)
        {
            int o = 0;
            for (int i = 0; i < inputLength; i++)
            {
                float4 point = input[i];

                float4 f;
                f.x = point.x;
                f.y = point.y;
                f.z = point.z;
                f.w = point.w;

                output[o] = f;

                if (Hint.Likely(IsPointValid3(point))) o++;
            }

            return o;
        }

        [BurstCompile(CompileSynchronously = true)]
        public static int ProcessXyzz([NoAlias] float4* input, [NoAlias] float4* output, int inputLength)
        {
            int o = 0;
            for (int i = 0; i < inputLength; i++)
            {
                float4 point = input[i];

                float4 f;
                f.x = point.x;
                f.y = point.y;
                f.z = point.z;
                f.w = point.z;

                output[o] = f;

                if (Hint.Likely(IsPointValid4(f))) o++;
            }

            return o;
        }

        [BurstCompile(CompileSynchronously = true)]
        public static int ProcessXyzwtToXyzw([NoAlias] float5* input, [NoAlias] float4* output, int inputLength)
        {
            int o = 0;
            for (int i = 0; i < inputLength; i++)
            {
                float5 point = input[i];

                float4 f;
                f.x = point.x;
                f.y = point.y;
                f.z = point.z;
                f.w = point.w;

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
            return math.all(math.abs(point) < PointCloudHelper.MaxPositionMagnitude)
                   && math.all(math.isfinite(point));
        }
    }

    [UsedImplicitly]
    internal readonly struct float5
    {
        public readonly float x, y, z, w, t;
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