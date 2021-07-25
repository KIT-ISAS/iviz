using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    internal static class MinMaxJob
    {
        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxJobPoints : IJob
        {
            [ReadOnly]
            public NativeSlice<float4> input;

            [WriteOnly]
            public NativeArray<float4> output;

            public void Execute()
            {
                float4 min = new float4(float.MaxValue);
                float4 max = new float4(float.MinValue);
                for (int i = 0; i < input.Length; i++)
                {
                    min = math.min(min, input[i]);
                    max = math.max(max, input[i]);
                }
                output[0] = min;
                output[1] = max;
            }
        }

        public static void CalculateBounds(
            in NativeArray<float4> pointBuffer,
            int size,
            out Bounds bounds,
            out Vector2 intensitySpan)
        {
            using (var output = new NativeArray<float4>(2, Allocator.Persistent))
            {
                MinMaxJobPoints job = new MinMaxJobPoints
                {
                    input = pointBuffer.Slice(0, size),
                    output = output
                };
                job.Schedule().Complete();

                Vector3 positionMin = output[0].xyz;
                Vector3 positionMax = output[1].xyz;

                bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
                intensitySpan = new Vector2(output[0].w, output[1].w);
            }
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxJobLines : IJob
        {
            [ReadOnly]
            public NativeSlice<float4x2> input;

            [WriteOnly]
            public NativeArray<float4> output;

            public void Execute()
            {
                float4 min = new float4(float.MaxValue);
                float4 max = new float4(float.MinValue);
                for (int i = 0; i < input.Length; i++)
                {
                    min = math.min(min, input[i].c0);
                    max = math.max(max, input[i].c0);
                    min = math.min(min, input[i].c1);
                    max = math.max(max, input[i].c1);
                }
                output[0] = min;
                output[1] = max;
            }
        }

        public static void CalculateBounds(
            in NativeArray<float4x2> pointBuffer,
            int size,
            out Bounds bounds,
            out Vector2 intensitySpan)
        {
            using (var output = new NativeArray<float4>(2, Allocator.Persistent))
            {
                MinMaxJobLines job = new MinMaxJobLines
                {
                    input = pointBuffer.Slice(0, size),
                    output = output
                };
                job.Schedule().Complete();

                Vector3 positionMin = output[0].xyz;
                Vector3 positionMax = output[1].xyz;

                bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
                intensitySpan = new Vector2(output[0].w, output[1].w);
            }
        }
    }

}