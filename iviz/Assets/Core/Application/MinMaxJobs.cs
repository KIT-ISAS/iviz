﻿using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public static class MinMaxJobs
    {
        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxFloat4 : IJob
        {
            [ReadOnly] public NativeArray<float4> input;
            [WriteOnly] public NativeArray<float4> output;

            public void Execute()
            {
                var min = new float4(float.MaxValue);
                var max = new float4(float.MinValue);
                for (int index = 0; index < input.Length; index++)
                {
                    float4 t = input[index];
                    min = math.min(min, t);
                    max = math.max(max, t);
                }

                output[0] = min;
                output[1] = max;
            }
        }

        public static void CalculateBounds(
            in NativeArray<float4> pointBuffer,
            out Bounds bounds,
            out Vector2 intensitySpan)
        {
            if (pointBuffer.Length == 0)
            {
                bounds = new Bounds();
                intensitySpan = Vector2.zero;
                return;
            }

            using var output = new NativeArray<float4>(2, Allocator.TempJob);

            var job = new MinMaxFloat4
            {
                input = pointBuffer,
                output = output
            };
            job.Schedule().Complete();

            Vector3 positionMin = output[0].xyz;
            Vector3 positionMax = output[1].xyz;

            bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
            intensitySpan = new Vector2(output[0].w, output[1].w);
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxFloat4x2 : IJob
        {
            [ReadOnly] public NativeArray<float4x2> input;
            [WriteOnly] public NativeArray<float4> output;

            public void Execute()
            {
                var min = new float4(float.MaxValue);
                var max = new float4(float.MinValue);
                for (int index = 0; index < input.Length; index++)
                {
                    float4x2 t = input[index];
                    min = math.min(min, t.c0);
                    max = math.max(max, t.c0);
                    min = math.min(min, t.c1);
                    max = math.max(max, t.c1);
                }

                output[0] = min;
                output[1] = max;
            }
        }

        public static void CalculateBounds(
            in NativeArray<float4x2> pointBuffer,
            out Bounds bounds,
            out Vector2 intensitySpan)
        {
            if (pointBuffer.Length == 0)
            {
                bounds = new Bounds();
                intensitySpan = Vector2.zero;
                return;
            }

            using var output = new NativeArray<float4>(2, Allocator.TempJob);

            var job = new MinMaxFloat4x2
            {
                input = pointBuffer,
                output = output
            };
            job.Schedule().Complete();

            Vector3 positionMin = output[0].xyz;
            Vector3 positionMax = output[1].xyz;

            bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
            intensitySpan = new Vector2(output[0].w, output[1].w);
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxFloat : IJob
        {
            [ReadOnly] public NativeArray<float> input;
            [WriteOnly] public NativeArray<float> output;

            public void Execute()
            {
                float min = float.MaxValue;
                float max = float.MinValue;
                for (int index = 0; index < input.Length; index++)
                {
                    float t = input[index];
                    min = Mathf.Min(min, t);
                    max = Mathf.Max(max, t);
                }

                output[0] = min;
                output[1] = max;
            }
        }

        public static (float, float) CalculateBounds(in NativeArray<float> pointBuffer)
        {
            if (pointBuffer.Length == 0)
            {
                return (0, 0);
            }
            
            using var output = new NativeArray<float>(2, Allocator.TempJob);

            var job = new MinMaxFloat
            {
                input = pointBuffer,
                output = output
            };
            job.Schedule().Complete();

            return (output[0], output[1]);
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxUshort : IJob
        {
            [ReadOnly] public NativeArray<ushort> input;
            [WriteOnly] public NativeArray<ushort> output;

            public void Execute()
            {
                ushort min = ushort.MinValue;
                ushort max = ushort.MaxValue;
                for (int index = 0; index < input.Length; index++)
                {
                    ushort t = input[index];
                    min = Math.Min(min, t);
                    max = Math.Max(max, t);
                }

                output[0] = min;
                output[1] = max;
            }
        }

        public static (ushort, ushort) CalculateBounds(in NativeArray<ushort> pointBuffer)
        {
            if (pointBuffer.Length == 0)
            {
                return (0, 0);
            }

            using var output = new NativeArray<ushort>(2, Allocator.TempJob);

            var job = new MinMaxUshort
            {
                input = pointBuffer,
                output = output
            };
            job.Schedule().Complete();

            return (output[0], output[1]);
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxByte : IJob
        {
            [ReadOnly] public NativeArray<byte> input;
            [WriteOnly] public NativeArray<byte> output;

            public void Execute()
            {
                byte min = byte.MinValue;
                byte max = byte.MaxValue;
                for (int index = 0; index < input.Length; index++)
                {
                    byte t = input[index];
                    min = Math.Min(min, t);
                    max = Math.Max(max, t);
                }

                output[0] = min;
                output[1] = max;
            }
        }

        public static (byte, byte) CalculateBounds(in NativeArray<byte> pointBuffer)
        {
            if (pointBuffer.Length == 0)
            {
                return (0, 0);
            }

            using var output = new NativeArray<byte>(2, Allocator.TempJob);

            var job = new MinMaxByte
            {
                input = pointBuffer,
                output = output
            };
            job.Schedule().Complete();

            return (output[0], output[1]);
        }
    }
}