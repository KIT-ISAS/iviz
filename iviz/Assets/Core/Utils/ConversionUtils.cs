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

namespace Iviz.Core
{
    public static class ConversionUtils
    {
        public static void CopyPixelsRgbToRgba(Span<byte> dst4, ReadOnlySpan<byte> src3, bool withBurst = true)
        {
            CopyPixelsRgbToRgba(dst4.Cast<uint>(), src3.Cast<Rgb>(), withBurst);
        }

        public static void CopyPixelsRgbToRgba(Span<uint> dst4, ReadOnlySpan<Rgb> src3, bool withBurst = true)
        {
            if (withBurst)
            {
                new CopyPixelsRgbToRgbaJob
                {
                    input = src3.CreateNativeArrayWrapper(),
                    output = dst4.CreateNativeArrayWrapper().Cast<uint, Rgba>()
                }.Schedule().Complete();
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

        [BurstCompile(CompileSynchronously = true)]
        struct CopyPixelsRgbToRgbaJob : IJob
        {
            [ReadOnly] public NativeArray<Rgb> input;
            [WriteOnly] public NativeArray<Rgba> output;

            public void Execute()
            {
                for (int i = 0; i < input.Length; i++)
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

        public static void CopyPixels565ToRgba(Span<uint> dst4, ReadOnlySpan<ushort> src2)
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

        public static int Convert565To888(int rgb565) => InternalConvert565To888(rgb565);

        public static void CopyPixelsR16ToR8(Span<byte> dst, ReadOnlySpan<byte> src, bool withBurst = true)
        {
            if (withBurst)
            {
                new CopyPixelsR16ToR8Job
                {
                    input = src.CreateNativeArrayWrapper().Cast<byte, R16>(),
                    output = dst.CreateNativeArrayWrapper()
                }.Schedule().Complete();
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

        [BurstCompile(CompileSynchronously = true)]
        struct CopyPixelsR16ToR8Job : IJob
        {
            [ReadOnly] public NativeArray<R16> input;
            [WriteOnly] public NativeArray<byte> output;

            public void Execute()
            {
                for (int i = 0; i < input.Length; i++)
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
            return new MirrorXFloatJob
            {
                width = width,
                height = height,
                input = src,
                output = dst.CreateNativeArrayWrapper().Cast<byte, float>()
            }.Schedule();
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MirrorXByteJob : IJob
        {
            [ReadOnly] public int width, height;
            [ReadOnly] public NativeArray<byte> input;
            public NativeArray<byte> output;

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
            return new MirrorXByteJob
            {
                width = width,
                height = height,
                input = src,
                output = dst.CreateNativeArrayWrapper()
            }.Schedule();
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