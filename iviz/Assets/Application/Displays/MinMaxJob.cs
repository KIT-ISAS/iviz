using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public class MinMaxJob
    {
        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxJobPoints : IJob
        {
            [ReadOnly]
            public NativeSlice<float4> Input;

            [WriteOnly]
            public NativeArray<float4> Output;

            public void Execute()
            {
                float4 Min = new float4(float.MaxValue);
                float4 Max = new float4(float.MinValue);
                for (int i = 0; i < Input.Length; i++)
                {
                    Min = math.min(Min, Input[i]);
                    Max = math.max(Max, Input[i]);
                }
                Output[0] = Min;
                Output[1] = Max;
            }
        }

        public static void CalculateBounds(
            in NativeArray<float4> pointBuffer,
            int size,
            out Bounds bounds,
            out Vector2 intensitySpan)
        {
            NativeArray<float4> output = new NativeArray<float4>(2, Allocator.Persistent);

            MinMaxJobPoints job = new MinMaxJobPoints
            {
                Input = pointBuffer.Slice(0, size),
                Output = output
            };
            job.Schedule().Complete();

            Vector3 positionMin = new Vector3(output[0].x, output[0].y, output[0].z);
            Vector3 positionMax = new Vector3(output[1].x, output[1].y, output[1].z);

            bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
            intensitySpan = new Vector2(output[0].w, output[1].w);

            output.Dispose();
        }

        [BurstCompile(CompileSynchronously = true)]
        struct MinMaxJobLines : IJob
        {
            [ReadOnly]
            public NativeSlice<float4x2> Input;

            [WriteOnly]
            public NativeArray<float4> Output;

            public void Execute()
            {
                float4 Min = new float4(float.MaxValue);
                float4 Max = new float4(float.MinValue);
                for (int i = 0; i < Input.Length; i++)
                {
                    Min = math.min(Min, Input[i].c0);
                    Max = math.max(Max, Input[i].c0);
                    Min = math.min(Min, Input[i].c1);
                    Max = math.max(Max, Input[i].c1);
                }
                Output[0] = Min;
                Output[1] = Max;
            }
        }

        public static void CalculateBounds(
            in NativeArray<float4x2> pointBuffer,
            int size,
            out Bounds bounds,
            out Vector2 intensitySpan)
        {
            NativeArray<float4> output = new NativeArray<float4>(2, Allocator.Persistent);

            MinMaxJobLines job = new MinMaxJobLines
            {
                Input = pointBuffer.Slice(0, size),
                Output = output
            };
            job.Schedule().Complete();

            Vector3 positionMin = new Vector3(output[0].x, output[0].y, output[0].z);
            Vector3 positionMax = new Vector3(output[1].x, output[1].y, output[1].z);

            bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
            intensitySpan = new Vector2(output[0].w, output[1].w);

            output.Dispose();
        }
    }

}