#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Urdf;
using JetBrains.Annotations;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Core
{
    public static unsafe class ConversionUtils
    {
        public static void CopyPixelsRgbToRgba(Span<byte> dst4, ReadOnlySpan<byte> src3, bool withBurst = true)
        {
            CopyPixelsRgbToRgba(dst4.Cast<uint>(), src3.Cast<Rgb>(), withBurst);
        }

        public static void CopyPixelsRgbToRgba(Span<uint> dst4, ReadOnlySpan<Rgb> src3, bool withBurst = true)
        {
            if (withBurst)
            {
                CopyPixelsRgbToRgbaJob.Execute(src3.GetPointer(), (Rgba*)dst4.GetPointer(), src3.Length);
                return;
            }

            CopyPixelsRgbToRgbaNoBurst(dst4, src3);
        }

        static void CopyPixelsRgbToRgbaNoBurst(Span<uint> dst4, ReadOnlySpan<Rgb> src3)
        {
            int sizeToWrite = src3.Length;
            AssertSize(dst4, sizeToWrite);

            var srcI4 = MemoryMarshal.Cast<Rgb, uint>(src3);

            ref uint dstIPtr = ref dst4[0];
            ref uint srcIPtr = ref srcI4.GetReference();

            while (sizeToWrite >= 8)
            {
                // stolen from https://stackoverflow.com/questions/2973708/fast-24-bit-array-32-bit-array-conversion

                uint sa = srcIPtr;
                dstIPtr = sa;

                uint sb = srcIPtr.Plus(1);
                dstIPtr.Plus(1) = (sa >> 24) | (sb << 8);

                uint sc = srcIPtr.Plus(2);
                dstIPtr.Plus(2) = (sb >> 16) | (sc << 16);
                dstIPtr.Plus(3) = sc >> 8;

                // but twice!

                uint sd = srcIPtr.Plus(3);
                dstIPtr.Plus(4) = sd;

                uint se = srcIPtr.Plus(4);
                dstIPtr.Plus(5) = (sd >> 24) | (se << 8);

                uint sf = srcIPtr.Plus(5);
                dstIPtr.Plus(6) = (se >> 16) | (sf << 16);
                dstIPtr.Plus(7) = sf >> 8;


                sizeToWrite -= 8;
                srcIPtr = ref srcIPtr.Plus(6);
                dstIPtr = ref dstIPtr.Plus(8);
            }

            ref var srcPtr = ref Unsafe.As<uint, Rgb>(ref srcIPtr);
            ref var dstPtr = ref Unsafe.As<uint, Rgba>(ref dstIPtr);

            for (int i = sizeToWrite; i > 0; i--)
            {
                dstPtr.r = srcPtr.r; // dstPtr->rgb = *srcPtr;
                srcPtr = ref Unsafe.Add(ref srcPtr, 1); // srcPtr++;
                dstPtr = ref Unsafe.Add(ref dstPtr, 1); // dstPtr++;
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
        }

        public static void CopyPixels565ToRgba(Span<uint> dst4, ReadOnlySpan<ushort> src2, bool withBurst = true)
        {
            if (withBurst)
            {
                Convert565To888Job.Execute(src2.GetPointer(), (int*)dst4.GetPointer(), src2.Length);
                return;
            }

            CopyPixels565ToRgbaNoBurst(dst4, src2);
        }


        static void CopyPixels565ToRgbaNoBurst(Span<uint> dst4, ReadOnlySpan<ushort> src2)
        {
            int sizeToWrite = src2.Length;
            AssertSize(dst4, sizeToWrite);

            ref int dstPtr = ref Unsafe.As<uint, int>(ref dst4[0]);
            ref ushort srcPtr = ref src2.GetReference();

            for (int x = sizeToWrite; x > 0; x--)
            {
                // stolen from https://stackoverflow.com/questions/2442576/how-does-one-convert-16-bit-rgb565-to-24-bit-rgb888

                int rgb565 = srcPtr;
                srcPtr = ref srcPtr.Plus(1); // srcPtr++;

                int rgba = InternalConvert565To888(rgb565);

                dstPtr = rgba;
                dstPtr = ref dstPtr.Plus(1); // dstPtr++;
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

        public static void CopyPixelsR16ToR8(Span<byte> dst, ReadOnlySpan<byte> src, bool withBurst = true)
        {
            if (withBurst)
            {
                CopyPixelsR16ToR8Job.Execute(src.Cast<R16>().GetPointer(), dst.GetPointer(), dst.Length);
                return;
            }

            CopyPixelsR16ToR8NoBurst(dst, src);
        }

        static void CopyPixelsR16ToR8NoBurst(Span<byte> dst, ReadOnlySpan<byte> src)
        {
            var dst4 = dst.Cast<uint>();
            var src4 = src.Cast<uint>();

            ref uint dstIPtr = ref dst4[0];
            ref uint srcIPtr = ref src4.GetReference();

            int sizeToWrite = src.Length / 2;
            AssertSize(dst, sizeToWrite);

            while (sizeToWrite >= 4)
            {
                uint src0 = srcIPtr;
                uint a = (src0 >> 8) & 0xff;
                uint b = (src0 >> 16) & 0xff00;

                uint src1 = srcIPtr.Plus(1);
                uint c = (src1 << 8) & 0xff0000;
                uint d = (src1 << 16) & 0xff000000;

                dstIPtr = a + b + c + d;

                sizeToWrite -= 4;
                srcIPtr = ref srcIPtr.Plus(2);
                dstIPtr = ref dstIPtr.Plus(1);
            }

            ref R16 srcPtr = ref Unsafe.As<uint, R16>(ref srcIPtr);
            ref byte dstPtr = ref Unsafe.As<uint, byte>(ref dstIPtr);

            for (int i = sizeToWrite; i > 0; i--)
            {
                dstPtr = srcPtr.low;
                srcPtr = ref srcPtr.Plus(1);
                dstPtr = ref dstPtr.Plus(1);
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

        [AssertionMethod]
        static void AssertSize(in Span<uint> span, int size)
        {
            if (span.Length < size)
                ThrowHelper.ThrowIndexOutOfRange("Span array is too short for the given operation");
        }

        [AssertionMethod]
        static void AssertSize(in Span<byte> span, int size)
        {
            if (span.Length < size)
                ThrowHelper.ThrowIndexOutOfRange("Span array is too short for the given operation");
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

        public static JobHandle MirrorXfFast(int width, int height, NativeArray<float> src, byte[] dst)
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
                output = dst.AsNativeArray().Cast<byte, float>()
            }.Schedule();
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
                    // 8 independent bytes, values are either 0, 1, or 2
                    // 0 -> 0
                    // 1 -> 128
                    // 2 -> 255
                    inputInt[i] = inputInt[i] * 127 + 0x01010101u;
                }
            }
        }

        public static JobHandle MirrorXbFast(int width, int height, NativeArray<byte> src, byte[] dst)
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
                output = dst.AsNativeArray()
            }.Schedule();
        }
    }

    [BurstCompile]
    public static unsafe class IndicesUtils
    {
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

        public static void FillIndices(Span<int> input)
        {
            Impl.FillIndices(input.GetPointer(), input.Length);
        }

        public static void FillIndicesFlipped(Span<int> input)
        {
            var input3 = MemoryMarshal.Cast<int, int3>(input);
            Impl.FillFlipped(input3.GetPointer(), input3.Length);
        }

        public static void FillVector3(NativeArray<float4> input, Span<Vector3> output)
        {
            if (input.Length != output.Length)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(input));
            }

            Impl.ToVector3(input.GetUnsafePtr(), (float3*)output.GetPointer(), input.Length);
        }

        public static void FillUV(NativeArray<float4> input, Span<Vector2> output)
        {
            if (input.Length != output.Length)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(input));
            }

            Impl.ToUV(input.GetUnsafePtr(), (float2*)output.GetPointer(), input.Length);
        }

        public static void FillColor(NativeArray<float4> input, Span<Color32> output)
        {
            if (input.Length != output.Length)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(input));
            }

            Impl.ToColor(input.GetUnsafePtr(), (float*)output.GetPointer(), input.Length);
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
                    < 2 => -1,
                    2 => sum >> 1, // sum / 2
                    3 => (sum * 21845) >> 16, // sum * (65536/3) / 65536
                    _ => sum >> 2
                };
            }

            [BurstCompile(CompileSynchronously = true)]
            public static void CountValid([NoAlias] sbyte* input, int inputLength, out int numValidValues)
            {
                numValidValues = 0;
                for (int i = 0; i < inputLength; i++)
                {
                    numValidValues += (input[i] >> 8) + 1;
                }
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

            Impl.Fill(row02.GetPointer(), row12.GetPointer(), output.GetPointer(), output.Length);
        }

        public static int CountValidValues(ReadOnlySpan<sbyte> row0)
        {
            /*
            var row02 = MemoryMarshal.Cast<sbyte, MipmapUtils.SByte2>(row0);
            var row12 = MemoryMarshal.Cast<sbyte, MipmapUtils.SByte2>(row1);
            
            if (row02.Length != output.Length || row12.Length != output.Length)
            {
                ThrowHelper.ThrowArgument("Size does not match!", nameof(output));
            }

            MipmapUtils.Fill(row02.GetPointer(), row12.GetPointer(), output.GetPointer(), output.Length);
            */
            Impl.CountValid(row0.GetPointer(), row0.Length, out int numValidValues);
            return numValidValues;
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
}