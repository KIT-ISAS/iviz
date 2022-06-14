#nullable enable

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Debug = UnityEngine.Debug;
using Vector3 = Iviz.Msgs.GeometryMsgs.Vector3;

namespace Iviz.Core
{
    public static class HashCalculator
    {
        public const uint DefaultSeed = 0;

        public static uint Compute<T>(T value, uint startHash = DefaultSeed) where T : unmanaged
        {
            return Compute(ref Unsafe.As<T, byte>(ref value), Unsafe.SizeOf<T>(), startHash);
        }

        public static uint Compute(in BuilderPool.BuilderRent value, uint startHash = DefaultSeed)
        {
            if (value.Length != value.Chunk.Length)
            {
                // shouldn't happen
                Debug.Log(nameof(HashCalculator) + ": StringBuilder does not have a cached chunk!");
            }

            return Compute(value.Chunk.Span, startHash);
        }

        public static uint Compute(string array, uint startHash = DefaultSeed)
        {
            return Compute(array.AsSpan(), startHash);
        }

        public static uint Compute<T>(T[] array, uint startHash = DefaultSeed) where T : unmanaged
        {
            return Compute((ReadOnlySpan<T>)array, startHash);
        }

        public static uint Compute<T>(ReadOnlySpan<T> array, uint startHash = DefaultSeed) where T : unmanaged
        {
            return Compute(MemoryMarshal.AsBytes(array), startHash);
        }

        static unsafe uint Compute(ReadOnlySpan<byte> span, uint startHash)
        {
            if (span.Length == 0)
            {
                return startHash;
            }

            fixed (byte* spanPtr = span)
            {
                return Xx32Hash.Hash(ref *spanPtr, span.Length, startHash);
            }
        }


        static uint Compute(ref byte value, int size, uint startHash)
        {
            return Xx32Hash.Hash(ref value, size, startHash);
        }

        /// Implementation of the xxHash32 algorithm, using Zhent_xxHash32 as the starting point.
        /// Stolen from https://github.com/Zhentar/xxHash3.NET
        static class Xx32Hash
        {
            const uint Prime32_1 = 2654435761U;
            const uint Prime32_2 = 2246822519U;
            const uint Prime32_3 = 3266489917U;
            const uint Prime32_4 = 668265263U;
            const uint Prime32_5 = 374761393U;

            const int MinForBurst = 8;

            public static uint Hash(ref byte data, int length, uint seed)
            {
                if (length <= 0)
                {
                    return seed;
                }

                int lengthInBulk = length / 16 * 16;

                uint h32 = lengthInBulk switch
                {
                    0 => seed + Prime32_5,
                    < MinForBurst * 16 => ExecuteDirect(seed, ref data, lengthInBulk / 16),
                    _ => ExecuteBurst(seed, ref data, lengthInBulk / 16)
                };

                //Profile(seed, ref data, lengthInBulk);

                h32 += (uint)length;

                ref uint remainingInt = ref Unsafe.As<byte, uint>(ref data.Plus(lengthInBulk));
                int remainingLength = length - lengthInBulk;

                switch (remainingLength >> 2)
                {
                    case 3:
                        h32 = RotateLeft(h32 + remainingInt * Prime32_3, 17) * Prime32_4;
                        remainingInt = ref remainingInt.Plus(1);
                        goto case 2;
                    case 2:
                        h32 = RotateLeft(h32 + remainingInt * Prime32_3, 17) * Prime32_4;
                        remainingInt = ref remainingInt.Plus(1);
                        goto case 1;
                    case 1:
                        h32 = RotateLeft(h32 + remainingInt * Prime32_3, 17) * Prime32_4;
                        remainingInt = ref remainingInt.Plus(1);
                        break;
                }

                ref byte remaining = ref Unsafe.As<uint, byte>(ref remainingInt);

                switch (remainingLength % 4)
                {
                    case 3:
                        h32 = RotateLeft(h32 + remaining * Prime32_5, 11) * Prime32_1;
                        remaining = ref remaining.Plus(1);
                        goto case 2;
                    case 2:
                        h32 = RotateLeft(h32 + remaining * Prime32_5, 11) * Prime32_1;
                        remaining = ref remaining.Plus(1);
                        goto case 1;
                    case 1:
                        h32 = RotateLeft(h32 + remaining * Prime32_5, 11) * Prime32_1;
                        break;
                }

                h32 ^= h32 >> 15;
                h32 *= Prime32_2;
                h32 ^= h32 >> 13;
                h32 *= Prime32_3;
                h32 ^= h32 >> 16;

                return h32;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static uint ExecuteDirect(uint seed, ref byte data, int length)
            {
                uint v1 = seed + Prime32_1 + Prime32_2;
                uint v2 = seed + Prime32_2;
                uint v3 = seed;
                uint v4 = seed - Prime32_1;

                ref uint4 val = ref Unsafe.As<byte, uint4>(ref data);
                for (int i = length; i > 0; i--)
                {
                    v1 += val.x * Prime32_2;
                    v2 += val.y * Prime32_2;
                    v3 += val.z * Prime32_2;
                    v4 += val.w * Prime32_2;

                    v1 = RotateLeft(v1, 13);
                    v2 = RotateLeft(v2, 13);
                    v3 = RotateLeft(v3, 13);
                    v4 = RotateLeft(v4, 13);

                    v1 *= Prime32_1;
                    v2 *= Prime32_1;
                    v3 *= Prime32_1;
                    v4 *= Prime32_1;

                    val = ref val.Plus(1);
                }

                return MergeValues(v1, v2, v3, v4);
            }

            static unsafe uint ExecuteBurst(uint seed, ref byte data, int lengthInBulk)
            {
                uint4 output;
                uint4* pointer = (uint4*)Unsafe.AsPointer(ref data);
                Hash32Job.Execute(seed, pointer, &output, lengthInBulk);
                return MergeValues(output.x, output.y, output.z, output.w);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static uint RotateLeft(uint val, int bits) => (val << bits) | (val >> (32 - bits));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static uint MergeValues(uint v1, uint v2, uint v3, uint v4) =>
                RotateLeft(v1, 1) + RotateLeft(v2, 7) + RotateLeft(v3, 12) + RotateLeft(v4, 18);

            [BurstCompile]
            static unsafe class Hash32Job
            {
                [BurstCompile(CompileSynchronously = true)]
                public static void Execute(uint seed, [NoAlias] uint4* input, [NoAlias] uint4* output, int length)
                {
                    uint4 v;
                    unchecked
                    {
                        v = new uint4(Prime32_1 + Prime32_2, Prime32_2, 0, 0 - Prime32_1) + seed;
                    }

                    for (int i = 0; i < MinForBurst; i++)
                    {
                        v += input[i] * Prime32_2;
                        v = (v << 13) | (v >> (32 - 13));
                        v *= Prime32_1;
                    }

                    for (int i = MinForBurst; i < length; i++)
                    {
                        v += input[i] * Prime32_2;
                        v = (v << 13) | (v >> (32 - 13));
                        v *= Prime32_1;
                    }

                    *output = v;
                }
            }

            static void Profile(uint seed, ref byte data, int lengthInBulk)
            {
                var stopWatch = new Stopwatch();

                stopWatch.Restart();
                for (int i = 0; i < 128; i++)
                {
                    ExecuteDirect(seed, ref data, lengthInBulk / 16);
                }

                stopWatch.Stop();
                double d = stopWatch.Elapsed.TotalMilliseconds / 128;
                stopWatch.Restart();

                for (int i = 0; i < 128; i++)
                {
                    ExecuteBurst(seed, ref data, lengthInBulk / 16);
                }

                stopWatch.Stop();
                double f = stopWatch.Elapsed.TotalMilliseconds / 128;

                Debug.Log("Length: " + lengthInBulk + " Direct: " + d + " Pointer: " + f);
                //Debug.Log(kk + " -- " + kkk);
            }
        }
    }
}