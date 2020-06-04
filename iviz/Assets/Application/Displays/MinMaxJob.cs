using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    [BurstCompile(CompileSynchronously = true)]
    public struct MinMaxJob : IJob
    {
        [ReadOnly]
        NativeArray<float4> Input;

        [WriteOnly]
        NativeArray<float4> Output;

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

        public static void CalculateBounds(
            in NativeArray<float4> pointBuffer,
            out Bounds bounds,
            out Vector2 intensitySpan)
        {
            NativeArray<float4> output = new NativeArray<float4>(2, Allocator.Persistent);

            MinMaxJob job = new MinMaxJob
            {
                Input = pointBuffer,
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