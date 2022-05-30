using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public static class MinMaxJob
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
                var min = new float4(float.MaxValue);
                var max = new float4(float.MinValue);
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
            using var output = new NativeArray<float4>(2, Allocator.Persistent);
            var job = new MinMaxJobPoints
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

        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxJobLines : IJob
        {
            [ReadOnly]
            public NativeSlice<float4x2> input;

            [WriteOnly]
            public NativeArray<float4> output;

            public void Execute()
            {
                var min = new float4(float.MaxValue);
                var max = new float4(float.MinValue);
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
            using var output = new NativeArray<float4>(2, Allocator.Persistent);
            var job = new MinMaxJobLines
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
        
        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxJobFloat : IJob
        {
            [ReadOnly]
            public NativeArray<float> input;

            [WriteOnly]
            public float outputMin, outputMax;

            public void Execute()
            {
                float min = float.MinValue;
                float max = float.MaxValue;
                foreach (float t in input)
                {
                    min = math.min(min, t);
                    max = math.max(max, t);
                }
                outputMin = min;
                outputMax = max;
            }
        }
        
        public static (float, float) CalculateBounds(in NativeArray<float> pointBuffer)
        {
            var job = new MinMaxJobFloat
            {
                input = pointBuffer,
            };
            job.Schedule().Complete();

            return (job.outputMin, job.outputMax);
        }
    }

}