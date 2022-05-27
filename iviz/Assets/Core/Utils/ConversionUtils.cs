using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Urdf;
using JetBrains.Annotations;

namespace Iviz.Core
{
    public static class ConversionUtils
    {
        public static void CopyPixelsRgbToRgba(Span<byte> dst4, ReadOnlySpan<byte> src3)
        {
            CopyPixelsRgbToRgba(dst4.Cast<uint>(), src3.Cast<Rgb>());
        }

        public static void CopyPixelsRgbToRgba(Span<uint> dst4, ReadOnlySpan<Rgb> src3)
        {
            int sizeToWrite = src3.Length;
            AssertSize(dst4, sizeToWrite);

            var srcI4 = MemoryMarshal.Cast<Rgb, uint>(src3);

            ref uint dstIPtr = ref dst4.GetReference();
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

            for (int x = sizeToWrite; x > 0; x--)
            {
                dstPtr.r = srcPtr.r; // dstPtr->rgb = *srcPtr;
                srcPtr = ref Unsafe.Add(ref srcPtr, 1); // srcPtr++;
                dstPtr = ref Unsafe.Add(ref dstPtr, 1); // dstPtr++;
            }
        }

        public static void CopyPixels565ToRgba(Span<uint> dst4, ReadOnlySpan<ushort> src2)
        {
            int sizeToWrite = src2.Length;
            AssertSize(dst4, sizeToWrite);

            ref int dstPtr = ref Unsafe.As<uint, int>(ref dst4.GetReference());
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

        public static void CopyPixelsR16ToR8(Span<byte> dst, ReadOnlySpan<byte> src)
        {
            var dst4 = dst.Cast<uint>();
            var src4 = src.Cast<uint>();

            ref uint dstIPtr = ref dst4.GetReference();
            ref uint srcIPtr = ref src4.GetReference();

            int sizeToWrite = src.Length / 2;
            AssertSize(dst, sizeToWrite);

            while (sizeToWrite >= 4)
            {
                uint src0 = srcIPtr;
                uint a = src0 & 0xff;
                uint b = (src0 >> 8) & 0xff00;

                uint src1 = srcIPtr.Plus(1);
                uint c = (src1 << 16) & 0xff0000;
                uint d = (src1 << 8) & 0xff000000;

                dstIPtr = a + b + c + d;

                sizeToWrite -= 4;
                srcIPtr = ref srcIPtr.Plus(2);
                dstIPtr = ref dstIPtr.Plus(1);
            }

            ref R16 srcPtr = ref Unsafe.As<uint, R16>(ref srcIPtr);
            ref byte dstPtr = ref Unsafe.As<uint, byte>(ref dstIPtr);
            for (int i = 0; i < sizeToWrite; i++)
            {
                dstPtr = srcPtr.low;

                srcPtr = ref srcPtr.Plus(1);
                dstPtr = ref dstPtr.Plus(1);
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
        
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Rgba
    {
        public byte r, g, b;
        public readonly byte a;

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