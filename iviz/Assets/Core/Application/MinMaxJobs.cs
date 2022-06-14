using System;
using Iviz.Core;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public static unsafe class MinMaxJobs
    {
        [BurstCompile]
        static class Impl
        {
            [BurstCompile(CompileSynchronously = true)]
            public static void MinMaxFloat4([NoAlias] float4* input, [NoAlias] float4* output, int inputLength)
            {
                var min = new float4(float.MaxValue);
                var max = new float4(float.MinValue);
                for (int index = 0; index < inputLength; index++)
                {
                    float4 t = input[index];
                    min = math.min(min, t);
                    max = math.max(max, t);
                }

                output[0] = min;
                output[1] = max;
            }

            public static void MinMaxFloat4x2([NoAlias] float4x2* input, [NoAlias] float4* output, int inputLength)
            {
                var min = new float4(float.MaxValue);
                var max = new float4(float.MinValue);
                for (int index = 0; index < inputLength; index++)
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

            [BurstCompile(CompileSynchronously = true)]
            public static void MinMaxFloat([NoAlias] float* input, [NoAlias] float* output, int inputLength)
            {
                float min = float.MaxValue;
                float max = float.MinValue;
                for (int index = 0; index < inputLength; index++)
                {
                    float t = input[index];
                    min = Mathf.Min(min, t);
                    max = Mathf.Max(max, t);
                }

                output[0] = min;
                output[1] = max;
            }

            [BurstCompile(CompileSynchronously = true)]
            public static void MinMaxUshort([NoAlias] ushort* input, [NoAlias] ushort* output, int inputLength)
            {
                ushort min = ushort.MaxValue;
                ushort max = ushort.MinValue;
                for (int index = 0; index < inputLength; index++)
                {
                    ushort t = input[index];
                    min = Math.Min(min, t);
                    max = Math.Max(max, t);
                }

                output[0] = min;
                output[1] = max;
            }

            [BurstCompile(CompileSynchronously = true)]
            public static void MinMaxByte([NoAlias] byte* input, [NoAlias] byte* output, int inputLength)
            {
                byte min = byte.MaxValue;
                byte max = byte.MinValue;
                for (int index = 0; index < inputLength; index++)
                {
                    byte t = input[index];
                    min = Math.Min(min, t);
                    max = Math.Max(max, t);
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

            float4* output = stackalloc float4[2];
            Impl.MinMaxFloat4(pointBuffer.GetUnsafePtr(), output, pointBuffer.Length);

            Vector3 positionMin = output[0].xyz;
            Vector3 positionMax = output[1].xyz;

            bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
            intensitySpan = new Vector2(output[0].w, output[1].w);
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

            float4* output = stackalloc float4[2];
            Impl.MinMaxFloat4x2(pointBuffer.GetUnsafePtr(), output, pointBuffer.Length);

            Vector3 positionMin = output[0].xyz;
            Vector3 positionMax = output[1].xyz;

            bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
            intensitySpan = new Vector2(output[0].w, output[1].w);
        }

        public static (float, float) CalculateBounds(ReadOnlySpan<float> pointBuffer)
        {
            if (pointBuffer.Length == 0)
            {
                return (0, 0);
            }

            float* output = stackalloc float[2];
            fixed (float* pointBufferPtr = pointBuffer)
            {
                Impl.MinMaxFloat(pointBufferPtr, output, pointBuffer.Length);
            }

            return (output[0], output[1]);
        }

        public static (float, float) CalculateBounds(in NativeArray<float> pointBuffer)
        {
            if (pointBuffer.Length == 0)
            {
                return (0, 0);
            }

            float* output = stackalloc float[2];
            Impl.MinMaxFloat(pointBuffer.GetUnsafePtr(), output, pointBuffer.Length);

            return (output[0], output[1]);
        }

        public static (ushort, ushort) CalculateBounds(in NativeArray<ushort> pointBuffer)
        {
            if (pointBuffer.Length == 0)
            {
                return (0, 0);
            }

            ushort* output = stackalloc ushort[2];
            Impl.MinMaxUshort(pointBuffer.GetUnsafePtr(), output, pointBuffer.Length);

            return (output[0], output[1]);
        }

        public static (byte, byte) CalculateBounds(in NativeArray<byte> pointBuffer)
        {
            if (pointBuffer.Length == 0)
            {
                return (0, 0);
            }

            byte* output = stackalloc byte[2];
            Impl.MinMaxByte(pointBuffer.GetUnsafePtr(), output, pointBuffer.Length);

            return (output[0], output[1]);
        }
    }
}