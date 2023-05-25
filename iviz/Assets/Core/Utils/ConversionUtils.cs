#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Tools;
using Iviz.Urdf;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Core
{
    public static unsafe class ConversionUtils
    {
        public static void CopyPixelsRgbToRgba(Span<byte> dst4, ReadOnlySpan<byte> src3)
        {
            CopyPixelsRgbToRgba(dst4.Cast<uint>(), src3.Cast<Rgb>());
        }

        public static void CopyPixelsRgbToRgba(Span<uint> dst4, ReadOnlySpan<Rgb> src3)
        {
            fixed (Rgb* src3Ptr = src3)
            fixed (uint* dst4Ptr = dst4)
            {
                CopyPixelsRgbToRgbaJob.Execute(src3Ptr, (Rgba*)dst4Ptr, src3.Length);
            }
        }

        [BurstCompile]
        static class CopyPixelsRgbToRgbaJob
        {
            [BurstCompile(CompileSynchronously = true)]
            public static void Execute([NoAlias] Rgb* input, [NoAlias] Rgba* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    var valueIn = input[i];
                    output[i] = new Rgba
                    {
                        r = valueIn.r,
                        g = valueIn.g,
                        b = valueIn.b,
                        a = valueIn.b, // unused
                    };
                }
            }

            /*
            [BurstCompile(CompileSynchronously = true)]
            public static void Palette([NoAlias] byte* input, [NoAlias] float* palette, [NoAlias] float* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    output[i] = palette[input[i]];
                }
            }
            */
        }

        public static void CopyPixels565ToRgba(Span<uint> dst4, ReadOnlySpan<ushort> src2)
        {
            fixed (ushort* src2Ptr = src2)
            fixed (uint* dst4Ptr = dst4)
            {
                Convert565To888Job.Execute(src2Ptr, (int*)dst4Ptr, src2.Length);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int InternalConvert565To888(int rgb565)
        {
            int r5 = (rgb565 & 0xf800) >> 11;
            int r8 = (r5 * 527 + 23) >> 6;
            int rgba = r8;

            int g6 = (rgb565 & 0x07e0) >> 5;
            int g8 = (g6 * 259 + 33) >> 6;
            rgba |= g8 << 8;

            int b5 = rgb565 & 0x001f;
            int b8 = (b5 * 527 + 23) >> 6;
            rgba |= b8 << 16;

            return rgba;
        }

        [BurstCompile]
        static class Convert565To888Job
        {
            [BurstCompile(CompileSynchronously = true)]
            public static void Execute([NoAlias] ushort* input, [NoAlias] int* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    output[i] = InternalConvert565To888(input[i]);
                }
            }
        }

        public static int Convert565To888(int rgb565) => InternalConvert565To888(rgb565);

        public static void CopyPixelsR16ToR8(Span<byte> dst, ReadOnlySpan<byte> src)
        {
            fixed (byte* srcPtr = src)
            fixed (byte* dstPtr = dst)
            {
                CopyPixelsR16ToR8Job.Execute((R16*)srcPtr, dstPtr, dst.Length);
            }
        }

        [BurstCompile]
        static class CopyPixelsR16ToR8Job
        {
            [BurstCompile(CompileSynchronously = true)]
            public static void Execute([NoAlias] R16* input, [NoAlias] byte* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    output[i] = input[i].low;
                }
            }
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MirrorXFloatJob : IJob
        {
            public int width, height;
            [ReadOnly] public NativeArray<float> input;
            [WriteOnly] public NativeArray<float> output;

            public void Execute()
            {
                for (int v = 0; v < height; v++)
                {
                    int l = v * width;
                    int r = l + width - 1;
                    for (int i = 0; i < width; i++)
                    {
                        output[r - i] = input[l + i];
                    }
                }
            }
        }

        public static Task MirrorXf(int width, int height, NativeArray<float> src, NativeArray<byte> dst)
        {
            int minSize = width * height;
            if (src.Length < minSize || dst.Length < minSize * sizeof(float))
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(src));
            }

            return new MirrorXFloatJob
            {
                width = width,
                height = height,
                input = src,
                output = dst.Cast<byte, float>()
            }.Schedule().AsTask();
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MirrorXByteJob : IJob
        {
            [ReadOnly] public int width, height;
            [ReadOnly] public NativeArray<byte> input;
            [WriteOnly] public NativeArray<byte> output;

            public void Execute()
            {
                ExecuteScale();
                ExecuteFlip();
            }

            void ExecuteFlip()
            {
                for (int v = 0; v < height; v++)
                {
                    int l = v * width;
                    int r = l + width - 1;
                    for (int i = 0; i < width; i++)
                    {
                        output[r - i] = input[l + i];
                    }
                }
            }

            void ExecuteScale()
            {
                var inputInt = input.Cast<byte, uint>();
                for (int i = 0; i < inputInt.Length; i++)
                {
                    // 4 independent bytes, values are either 0, 1, or 2
                    // 0 -> 0
                    // 1 -> 128
                    // 2 -> 255
                    inputInt[i] = inputInt[i] * 127 + 0x01010101u;
                }
            }
        }

        public static Task MirrorXb(int width, int height, NativeArray<byte> src, NativeArray<byte> dst)
        {
            int minSize = width * height;
            if (src.Length < minSize || dst.Length < minSize)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(src));
            }

            return new MirrorXByteJob
            {
                width = width,
                height = height,
                input = src,
                output = dst
            }.Schedule().AsTask();
        }
    }

    public static unsafe class IndicesUtils
    {
        [BurstCompile]
        static class Impl
        {
            [BurstCompile(CompileSynchronously = true)]
            public static void FillIndices([NoAlias] int* input, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    input[i] = i;
                }
            }

            [BurstCompile(CompileSynchronously = true)]
            public static void FillFlipped([NoAlias] int3* input, int inputLength)
            {
                int j = 0;
                for (int i = 0; i < inputLength; i++)
                {
                    int3 triangle;
                    triangle.x = j;
                    triangle.y = j + 2;
                    triangle.z = j + 1;
                    input[i] = triangle;
                    j += 3;
                }
            }

            [BurstCompile(CompileSynchronously = true)]
            public static void ToVector3([NoAlias] float4* input, [NoAlias] float3* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    output[i] = input[i].xyz;
                }
            }

            [BurstCompile(CompileSynchronously = true)]
            public static void ToUV([NoAlias] float4* input, [NoAlias] float2* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    output[i] = input[i].ww;
                }
            }

            [BurstCompile(CompileSynchronously = true)]
            public static void ToColor([NoAlias] float4* input, [NoAlias] float* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    output[i] = input[i].w;
                }
            }
        }

        public static void FillIndices(Rent<int> input)
        {
            fixed (int* inputPtr = input.Array)
            {
                Impl.FillIndices(inputPtr, input.Length);
            }
        }

        public static void FillIndicesFlipped(Rent<int> input)
        {
            var input3 = MemoryMarshal.Cast<int, int3>(input);
            fixed (int3* input3Ptr = input3)
            {
                Impl.FillFlipped(input3Ptr, input3.Length);
            }
        }

        public static void FillVector3(NativeArray<float4> input, Rent<Vector3> output)
        {
            if (input.Length != output.Length)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(input));
            }

            fixed (Vector3* outputPtr = output.Array)
            {
                Impl.ToVector3(input.GetUnsafePtr(), (float3*)outputPtr, input.Length);
            }
        }

        public static void FillUV(NativeArray<float4> input, Rent<Vector2> output)
        {
            if (input.Length != output.Length)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(input));
            }

            fixed (Vector2* outputPtr = output.Array)
            {
                Impl.ToUV(input.GetUnsafePtr(), (float2*)outputPtr, input.Length);
            }
        }

        public static void FillColor(NativeArray<float4> input, Rent<Color32> output)
        {
            if (input.Length != output.Length)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(input));
            }

            fixed (Color32* outputPtr = output.Array)
            {
                Impl.ToColor(input.GetUnsafePtr(), (float*)outputPtr, input.Length);
            }
        }
    }

    public static unsafe class OccupancyGridUtils
    {
        [BurstCompile]
        static class Impl
        {
            [BurstCompile(CompileSynchronously = true)]
            public static void Fill([NoAlias] SByte2* row0, [NoAlias] SByte2* row1, [NoAlias] sbyte* output,
                int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    output[i] = (sbyte)Fuse(row0[i].a, row0[i].b, row1[i].a, row1[i].b);
                }
            }

            static int Fuse(int a, int b, int c, int d)
            {
                int signA = ~a >> 8; // a >= 0 ? -1 : 0
                int valueA = a & signA; // a >= 0 ? a : 0
                int numValid = -signA;
                int sum = valueA;

                int signB = ~b >> 8; // b >= 0 ? -1 : 0
                int valueB = b & signB; // b >= 0 ? b : 0
                numValid -= signB;
                sum += valueB;

                int signC = ~c >> 8; // c >= 0 ? -1 : 0
                int valueC = c & signC; // c >= 0 ? c : 0
                numValid -= signC;
                sum += valueC;

                int signD = ~d >> 8; // d >= 0 ? -1 : 0
                int valueD = d & signD; // d >= 0 ? d : 0
                numValid -= signD;
                sum += valueD;

                return numValid switch
                {
                    0 or 1 => -1,
                    2 => sum >> 1, // sum / 2
                    3 => (sum * (65536/3)) >> 16,
                    4 => sum >> 2,
                    _ => 0
                };
            }

            [BurstCompile(CompileSynchronously = true)]
            public static int CountValid([NoAlias] sbyte* input, int inputLength)
            {
                int numValidValues = 0;
                for (int i = 0; i < inputLength; i++)
                {
                    numValidValues += (input[i] >> 8) + 1;
                }

                return numValidValues;
            }

            public struct SByte2
            {
                public sbyte a, b;
            }
        }

        public static void ReduceRows(ReadOnlySpan<sbyte> row0, ReadOnlySpan<sbyte> row1, Span<sbyte> output)
        {
            var row02 = MemoryMarshal.Cast<sbyte, Impl.SByte2>(row0);
            var row12 = MemoryMarshal.Cast<sbyte, Impl.SByte2>(row1);

            if (row02.Length != output.Length || row12.Length != output.Length)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(output));
            }

            fixed (Impl.SByte2* row02Ptr = row02)
            fixed (Impl.SByte2* row12Ptr = row12)
            fixed (sbyte* outputPtr = output)
            {
                Impl.Fill(row02Ptr, row12Ptr, outputPtr, output.Length);
            }
        }

        public static int CountValidValues(ReadOnlySpan<sbyte> row0)
        {
            fixed (sbyte* row0Ptr = row0)
            {
                return Impl.CountValid(row0Ptr, row0.Length);
            }
        }
    }

    public static unsafe class MarkerObjectUtils
    {
        [BurstCompile]
        static class Impl
        {
            [BurstCompile(CompileSynchronously = true)]
            public static bool CopyPoints(int pointsLength, [NoAlias] double3* srcPtr, [NoAlias] float3* dstPtr)
            {
                bool allIndicesValid = true;
                for (int i = 0; i < pointsLength; i++)
                {
                    double3 srcPtrI = srcPtr[i];

                    float3 srcPtrF;
                    srcPtrF.x = (float)srcPtrI.x;
                    srcPtrF.y = (float)srcPtrI.y;
                    srcPtrF.z = (float)srcPtrI.z;

                    float3 dstPtrI;
                    dstPtrI.x = -srcPtrF.y;
                    dstPtrI.y = srcPtrF.z;
                    dstPtrI.z = srcPtrF.x;

                    dstPtr[i] = dstPtrI;

                    allIndicesValid &= math.all(math.isfinite(srcPtrF));
                }

                return allIndicesValid;
            }
        }

        public static bool CopyPoints(Msgs.GeometryMsgs.Point[] src, Vector3[] dst)
        {
            if (src.Length > dst.Length)
            {
                ThrowHelper.ThrowArgument("Sizes do not match!", nameof(src));
            }

            var src3 = MemoryMarshal.Cast<Msgs.GeometryMsgs.Point, double3>(src);
            var dst3 = MemoryMarshal.Cast<Vector3, float3>(dst);

            fixed (double3* srcPtr = src3)
            fixed (float3* dstPtr = dst3)
            {
                return Impl.CopyPoints(src3.Length, srcPtr, dstPtr);
            }
        }
    }

    [BurstCompile]
    internal static unsafe class LineDisplayUtils
    {
        [BurstCompile(CompileSynchronously = true)]
        public static bool CheckIfAlphaNeeded([NoAlias] float4x2* lineSpan, int size)
        {
            uint4x2* lineSpanInt = (uint4x2*)lineSpan;
            for (int i = 0; i < size; i++)
            {
                var line = lineSpanInt[i];
                uint cA = line.c0.w;
                uint cB = line.c1.w;

                if (cA >> 24 < 255 || cB >> 24 < 255)
                {
                    return true;
                }
            }

            return false;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rgba
    {
        public byte r, g, b, a;

        public bool IsGray => r == g && g == b;

        public Rgb Rgb
        {
            set => (r, g, b) = (value.r, value.g, value.b);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Rgb
    {
        public readonly byte r, g, b;
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct R16
    {
        readonly byte high;
        public readonly byte low;
    }

    [BurstCompile]
    public static unsafe class MeshBurstUtils
    {
        [BurstCompile(CompileSynchronously = true)]
        static void ToPoint4([NoAlias] float3x4* input, [NoAlias] double3x4* output, int inputLength)
        {
            for (int i = 0; i < inputLength; i++)
            {
                float3x4 input3 = input[i];

                double3x4 output3;
                output3.c0.x = input3.c0.z;
                output3.c0.y = -input3.c0.x;
                output3.c0.z = input3.c0.y;

                output3.c1.x = input3.c1.z;
                output3.c1.y = -input3.c1.x;
                output3.c1.z = input3.c1.y;

                output3.c2.x = input3.c2.z;
                output3.c2.y = -input3.c2.x;
                output3.c2.z = input3.c2.y;

                output3.c3.x = input3.c3.z;
                output3.c3.y = -input3.c3.x;
                output3.c3.z = input3.c3.y;

                output[i] = output3;
            }
        }

        [BurstCompile(CompileSynchronously = true)]
        static void ToPoint([NoAlias] float3* input, [NoAlias] double3* output, int inputLength)
        {
            for (int i = 0; i < inputLength; i++)
            {
                float3 input3 = input[i];

                double3 output3;
                output3.x = input3.z;
                output3.y = -input3.x;
                output3.z = input3.y;

                output[i] = output3;
            }
        }

        public static void ToPoint(Span<Vector3> input, Point[] output)
        {
            fixed (Vector3* inputPtr = input)
            fixed (Point* outputPtr = output)
            {
                int length = input.Length;
                int length4 = length / 4;
                ToPoint4((float3x4*)inputPtr, (double3x4*)outputPtr, length4);

                int offset = length4 * 4;
                ToPoint((float3*)(inputPtr + offset), (double3*)(outputPtr + offset), length - offset);
            }
        }
    }
}