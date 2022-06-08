#nullable enable

using Iviz.Core;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Iviz.Displays
{
    internal static class CapsuleLinesHelper
    {
        public static void CreateCapsulesFromSegments(NativeArray<float4x2> lineBuffer, float scale, Mesh mesh)
        {
            ThrowHelper.ThrowIfNull(mesh, nameof(mesh));

            mesh.Clear();

            int bufferLength = lineBuffer.Length;
            if (bufferLength == 0)
            {
                return;
            }

            int length = 10 * bufferLength;
            using var points = new NativeArray<float3>(length, Allocator.TempJob,
                NativeArrayOptions.UninitializedMemory);
            using var colors = new NativeArray<float>(length, Allocator.TempJob,
                NativeArrayOptions.UninitializedMemory);
            using var uvs = new NativeArray<float2>(length, Allocator.TempJob,
                NativeArrayOptions.UninitializedMemory);
            using var indices = new NativeArray<int>(16 * 3 * bufferLength, Allocator.TempJob,
                NativeArrayOptions.UninitializedMemory);

            var pHandle =
                new CapsuleCreatePositions { input = lineBuffer, scale = scale, output = points }.Schedule();
            var cHandle = new CapsuleCreateColors { input = lineBuffer, output = colors }.Schedule();
            var uvHandle = new CapsuleCreateUVs { input = lineBuffer, output = uvs }.Schedule();
            var iHandle = new CapsuleCreateIndices { output = indices }.Schedule();

            JobHandle.ScheduleBatchedJobs();

            pHandle.Complete();
            cHandle.Complete();
            uvHandle.Complete();
            iHandle.Complete();

            mesh.SetVertices(points);
            mesh.SetColors(colors.Reinterpret<Color32>());
            mesh.SetUVs(uvs.Reinterpret<Vector2>());
            mesh.SetTriangles(indices);
        }


        [BurstCompile(CompileSynchronously = true)]
        struct CapsuleCreatePositions : IJob
        {
            [ReadOnly] public NativeArray<float4x2> input;
            [ReadOnly] public float scale;
            [WriteOnly] public NativeArray<float3> output;

            public void Execute()
            {
                const float minMagnitude = 1e-5f;
                float halfScale = scale * 0.5f;

                var batchOutput = output.Cast<float3, Batch>();

                for (int index = 0; index < input.Length; index++)
                {
                    var line = input[index];
                    var a = line.c0.xyz;
                    var b = line.c1.xyz;

                    var ab = b - a;
                    float3 dirX, dirY;

                    float abMagnitudeSq = math.lengthsq(ab);
                    if (Hint.Unlikely(abMagnitudeSq < minMagnitude * minMagnitude))
                    {
                        dirX.x = dirX.y = dirX.z = 0;
                        dirY.x = dirY.y = dirY.z = 0;
                    }
                    else
                    {
                        dirX = ab / Mathf.Sqrt(abMagnitudeSq);
                        var (x, y, z) = dirX;
                        if (Hint.Unlikely((Mathf.Abs(z) - 1).ApproximatelyZero()))
                        {
                            float den = Mathf.Sqrt(x * x + z * z);
                            dirY.x = -z / den;
                            dirY.y = 0;
                            dirY.z = x / den;
                            //? new Vector3(-z, 0, x) / Mathf.Sqrt(x * x + z * z)
                        }
                        else
                        {
                            float den = Mathf.Sqrt(x * x + y * y);
                            dirY.x = -y / den;
                            dirY.y = x / den;
                            dirY.z = 0;
                            //: new Vector3(-y, x, 0) / Mathf.Sqrt(x * x + y * y);
                        }
                    }

                    var dirZ = math.cross(dirX, dirY);

                    var halfDirX = halfScale * dirX;
                    var halfSumYz = halfScale * (dirY + dirZ);
                    var halfDiffYz = halfScale * (dirY - dirZ);

                    batchOutput[index] = new Batch
                    {
                        a = a - halfDirX,
                        b = a + halfSumYz,
                        c = a + halfDiffYz,
                        d = a - halfSumYz,
                        e = a - halfDiffYz,

                        f = b + halfSumYz,
                        g = b + halfDiffYz,
                        h = b - halfSumYz,
                        i = b - halfDiffYz,
                        j = b + halfDirX
                    };   
                }
            }
            
            struct Batch
            {
                public float3 a, b, c, d, e;
                public float3 f, g, h, i, j;
            }
        }

        [BurstCompile(CompileSynchronously = true)]
        struct CapsuleCreateColors : IJob
        {
            [ReadOnly] public NativeArray<float4x2> input;
            [WriteOnly] public NativeArray<float> output;

            public void Execute()
            {
                var batchOutput = output.Cast<float, Batch>();

                for (int index = 0; index < input.Length; index++)
                {
                    var line = input[index];
                    float ca = line.c0.w;
                    float cb = line.c1.w;

                    batchOutput[index] = new Batch
                    {
                        a = ca,
                        b = ca,
                        c = ca,
                        d = ca,
                        e = ca,

                        f = cb,
                        g = cb,
                        h = cb,
                        i = cb,
                        j = cb
                    };                    
                }
            }
            
            struct Batch
            {
                public float a, b, c, d, e;
                public float f, g, h, i, j;
            }
        }

        [BurstCompile(CompileSynchronously = true)]
        struct CapsuleCreateUVs : IJob
        {
            [ReadOnly] public NativeArray<float4x2> input;
            [WriteOnly] public NativeArray<float2> output;

            public void Execute()
            {
                var batchOutput = output.Cast<float2, Batch>();
                
                for (int index = 0; index < input.Length; index++)
                {
                    var line = input[index];
                    float2 uv0;
                    uv0.x = line.c0.w;
                    uv0.y = line.c0.w; // unused

                    float2 uv1;
                    uv1.x = line.c1.w;
                    uv1.y = line.c1.w; // unused

                    batchOutput[index] = new Batch
                    {
                        a = uv0,
                        b = uv0,
                        c = uv0,
                        d = uv0,
                        e = uv0,

                        f = uv1,
                        g = uv1,
                        h = uv1,
                        i = uv1,
                        j = uv1
                    };
                }
            }

            struct Batch
            {
                public float2 a, b, c, d, e;
                public float2 f, g, h, i, j;
            }
        }


        [BurstCompile(CompileSynchronously = true)]
        struct CapsuleCreateIndices : IJob
        {
            static readonly int[] CapsuleIndices =
            {
                0, 1, 2,
                0, 2, 3,
                0, 3, 4,
                0, 4, 1,

                1, 5, 6,
                1, 6, 2,

                2, 6, 7,
                2, 7, 3,

                3, 7, 8,
                3, 8, 4,

                4, 8, 5,
                4, 5, 1,

                9, 6, 5,
                9, 7, 6,
                9, 8, 7,
                9, 5, 8
            };

            [WriteOnly] public NativeArray<int> output;

            public void Execute()
            {
                int offset = 0;
                int index = 0;
                while (index != output.Length)
                {
                    for (int i = 0; i < 48; i++)
                    {
                        output[index++] = offset + CapsuleIndices[i];
                    }

                    offset += 10;
                }
            }
        }
    }
}