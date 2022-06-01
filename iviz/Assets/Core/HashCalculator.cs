#nullable enable

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using Debug = UnityEngine.Debug;
using Vector3 = Iviz.Msgs.GeometryMsgs.Vector3;

namespace Iviz.Core
{
    public static class HashCalculator
    {
        public const uint DefaultSeed = 0;

        public static uint Compute(int value, uint startHash = DefaultSeed)
        {
            return Compute(ref Unsafe.As<int, byte>(ref value), sizeof(int), startHash);
        }

        public static uint Compute(Vector3 value, uint startHash = DefaultSeed)
        {
            return Compute(ref Unsafe.As<Vector3, byte>(ref value), Unsafe.SizeOf<Vector3>(), startHash);
        }

        public static uint Compute(ColorRGBA value, uint startHash = DefaultSeed)
        {
            return Compute(ref Unsafe.As<ColorRGBA, byte>(ref value), Unsafe.SizeOf<ColorRGBA>(), startHash);
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

        public static uint Compute(Point[] array, uint startHash = DefaultSeed)
        {
            int length = array.Length;
            return length == 0
                ? startHash
                : Compute(ref Unsafe.As<Point, byte>(ref array[0]), length * Unsafe.SizeOf<Point>(), startHash);
        }

        public static uint Compute(ColorRGBA[] array, uint startHash = DefaultSeed)
        {
            int length = array.Length;
            return length == 0
                ? startHash
                : Compute(ref Unsafe.As<ColorRGBA, byte>(ref array[0]), length * Unsafe.SizeOf<ColorRGBA>(), startHash);
        }

        public static uint Compute(string array, uint startHash = DefaultSeed)
        {
            return Compute(array.AsSpan(), startHash);
        }

        static uint Compute(ReadOnlySpan<char> array, uint startHash = DefaultSeed)
        {
            int length = array.Length;
            return length == 0
                ? startHash
                : Compute(ref Unsafe.As<char, byte>(ref array.GetReference()), length * sizeof(char), startHash);
        }

        public static uint Compute(ReadOnlySpan<sbyte> array, uint startHash = DefaultSeed)
        {
            int length = array.Length;
            return length == 0
                ? startHash
                : Compute(ref Unsafe.As<sbyte, byte>(ref array.GetReference()), length * sizeof(sbyte), startHash);
        }

        static uint ComputeCore(ref byte value, int size, uint startHash)
        {
            return Xx32Hash.Hash(ref value, size, startHash);
        }

        static uint Compute(ref byte value, int size, uint startHash)
        {
            return ComputeCore(ref value, size, startHash);
        }

        /// Implementation of the xxHash32 algorithm, using Zhent_xxHash32 as the starting point 
        /// Stolen from https://github.com/Zhentar/xxHash3.NET
        static class Xx32Hash
        {
            const uint Prime32_1 = 2654435761U;
            const uint Prime32_2 = 2246822519U;
            const uint Prime32_3 = 3266489917U;
            const uint Prime32_4 = 668265263U;
            const uint Prime32_5 = 374761393U;

            public static uint Hash(ref byte data, int length, uint seed)
            {
                unchecked
                {
                    if (length < 0) return 0;

                    int lengthInBulk = length / 16 * 16;

                    uint h32 = lengthInBulk switch
                    {
                        0 => Prime32_5,
                        < 8192 => ExecuteDirect(seed, ref data, lengthInBulk / 16),
                        _ => ExecuteAsJob(seed, ref data, lengthInBulk / 16)
                    };

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

                    switch (remainingLength % sizeof(uint))
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
            }

            static uint ExecuteDirect(uint seed, ref byte data, int length)
            {
                unchecked
                {
                    uint v1 = seed + Prime32_1 + Prime32_2;
                    uint v2 = seed + Prime32_2;
                    uint v3 = seed;
                    uint v4 = seed - Prime32_1;

                    ref uint4 val = ref Unsafe.As<byte, uint4>(ref data);
                    for (int i = 0; i < length; i++)
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
            }

            static NativeArray<uint>? temp;

            static uint ExecuteAsJob(uint seed, ref byte data, int lengthInBulk)
            {
                var input = NativeArrayUtils.CreateNativeArrayWrapper(ref Unsafe.As<byte, uint4>(ref data),
                    lengthInBulk);
                using var output = NativeArrayUtils.TempArrayFromValue(uint4.zero);
                new Hash32Job { seed = seed, input = input, output = output }.Schedule().Complete();
                return MergeValues(output[0].x, output[0].y, output[0].z, output[0].w);
            }

            [BurstCompile(CompileSynchronously = true)]
            struct Hash32Job : IJob
            {
                [ReadOnly] public uint seed;
                [ReadOnly] public NativeArray<uint4> input;
                [WriteOnly] public NativeArray<uint4> output;

                public void Execute()
                {
                    uint4 v;
                    unchecked
                    {
                        v = new uint4(Prime32_1 + Prime32_2, Prime32_2, 0, 0 - Prime32_1) + seed;
                    }

                    for (int i = 0; i < input.Length; i++)
                    {
                        v += input[i] * Prime32_2;
                        v = (v << 13) | (v >> (32 - 13));
                        v *= Prime32_1;
                    }

                    output[0] = v;
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static uint RotateLeft(uint val, int bits) => (val << bits) | (val >> (32 - bits));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static ulong RotateLeft(ulong val, int bits) => (val << bits) | (val >> (64 - bits));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static uint MergeValues(uint v1, uint v2, uint v3, uint v4) =>
                RotateLeft(v1, 1) + RotateLeft(v2, 7) + RotateLeft(v3, 12) + RotateLeft(v4, 18);
        }
    }
}