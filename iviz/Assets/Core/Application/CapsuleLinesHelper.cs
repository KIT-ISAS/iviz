#nullable enable

using System;
using System.Numerics;
using Iviz.Core;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Displays
{
    internal static class CapsuleLinesHelper
    {
        static int[]? capsuleIndices;

        static int[] CapsuleIndices => capsuleIndices ??= new[]
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

        public static void CreateCapsulesFromSegments(ReadOnlySpan<float4x2> lineBuffer, float scale, Mesh mesh)
        {
            ThrowHelper.ThrowIfNull(mesh, nameof(mesh));

            int bufferLength = lineBuffer.Length;
            if (bufferLength == 0)
            {
                mesh.Clear();
                return;
            }

            int length = 10 * bufferLength;
            var points = new Rent<Vector3>(length);
            var colors = new Rent<Color32>(length);
            var uvs = new Rent<Vector2>(length);

            int indicesSize = 16 * 3 * bufferLength;
            mesh.Clear();
            var indices = new Rent<int>(indicesSize);

            try
            {
                CreateCapsulesFromSegments(lineBuffer, bufferLength, scale,
                    points.Array, colors.Array, uvs.Array, indices.Array);

                mesh.SetVertices(points);
                mesh.SetColors(colors);
                mesh.SetUVs(uvs);
                mesh.SetTriangles(indices);
            }
            finally
            {
                points.Dispose();
                colors.Dispose();
                uvs.Dispose();
                indices.Dispose();
            }
        }

        static unsafe void CreateCapsulesFromSegments(ReadOnlySpan<float4x2> lines, int numSegments, float scale,
            Vector3[] pArray, Color32[] cArray, Vector2[] uArray, int[] iArray)
        {
            const float minMagnitude = 1e-5f;

            float halfScale = scale * 0.5f;

            for (int segment = 0; segment < numSegments; segment++)
            {
                ref readonly var line = ref lines[segment];

                Vector3 a;
                a.x = line.c0.x;
                a.y = line.c0.y;
                a.z = line.c0.z;

                Vector3 b;
                b.x = line.c1.x;
                b.y = line.c1.y;
                b.z = line.c1.z;

                Vector3 ab = b - a;
                Vector3 dirX, dirY;

                float abMagnitudeSq = ab.sqrMagnitude;
                if (abMagnitudeSq < minMagnitude * minMagnitude)
                {
                    dirX.x = dirX.y = dirX.z = 0;
                    dirY.x = dirY.y = dirY.z = 0;
                }
                else
                {
                    dirX = ab / Mathf.Sqrt(abMagnitudeSq);
                    var (x, y, z) = dirX;
                    if ((Mathf.Abs(z) - 1).ApproximatelyZero())
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

                var dirZ = dirX.Cross(dirY);

                int offset = segment * 10;

                var halfDirX = halfScale * dirX;
                var halfSumYz = halfScale * (dirY + dirZ);
                var halfDiffYz = halfScale * (dirY - dirZ);

                // note: it's ok to pin the array in every loop, in il2cpp it's a free operation
                // it's probably not a good idea to do this in normal C#
                fixed (Vector3* pPtr = &pArray[offset]) 
                {
                    pPtr[0] = a - halfDirX;
                    pPtr[9] = b + halfDirX;

                    pPtr[1] = a + halfSumYz;
                    pPtr[3] = a - halfSumYz;
                    pPtr[5] = b + halfSumYz;
                    pPtr[7] = b - halfSumYz;

                    pPtr[2] = a + halfDiffYz;
                    pPtr[4] = a - halfDiffYz;
                    pPtr[6] = b + halfDiffYz;
                    pPtr[8] = b - halfDiffYz;
                }
                
            }

            for (int segment = 0; segment < numSegments; segment++)
            {
                ref readonly var line = ref lines[segment];
                int offset = segment * 10;

                var ca = UnityUtils.AsColor32(line.c0.w);
                var cb = UnityUtils.AsColor32(line.c1.w);

                fixed (Color32* cPtr = &cArray[offset])
                {
                    cPtr[0] = ca;
                    cPtr[1] = ca;
                    cPtr[2] = ca;
                    cPtr[3] = ca;
                    cPtr[4] = ca;

                    cPtr[5] = cb;
                    cPtr[6] = cb;
                    cPtr[7] = cb;
                    cPtr[8] = cb;
                    cPtr[9] = cb;
                }
            }

            for (int segment = 0; segment < numSegments; segment++)
            {
                ref readonly var line = ref lines[segment];

                int offset = segment * 10;

                Vector2 uv0;
                uv0.x = line.c0.w;
                uv0.y = 0;

                Vector2 uv1;
                uv1.x = line.c1.w;
                uv1.y = 0;

                fixed (Vector2* uvPtr = &uArray[offset])
                {
                    uvPtr[0] = uv0;
                    uvPtr[1] = uv0;
                    uvPtr[2] = uv0;
                    uvPtr[3] = uv0;
                    uvPtr[4] = uv0;

                    uvPtr[5] = uv1;
                    uvPtr[6] = uv1;
                    uvPtr[7] = uv1;
                    uvPtr[8] = uv1;
                    uvPtr[9] = uv1;
                }
            }

            
            

            /*
            int[] capsules = CapsuleIndices;
            int wordSize = Vector<int>.Count;
            int numWords = 48 / wordSize;
            
            for (int segment = numSegments; segment > 0; segment--)
            {
                var delta = new Vector<int>(segment * 10);
                for (int j = 0; j < numWords; j++)
                {
                    var capsulesSpan = new Vector<int>(capsules, j * wordSize);
                    var indicesSpan = capsulesSpan + delta;
                    (indicesSpan + delta).CopyTo(capsules, 48 * segment + j * wordSize);
                }
            }
            */

            /*
            foreach (int segment in ..numSegments)
            {
                int vertexOff = segment * 10;
                foreach (int capsuleIndex in ..48)
                {
                    iPtr = vertexOff + capsulePtr.Plus(capsuleIndex);
                    iPtr = ref iPtr.Plus(1);
                }
            }
            */

            /*
            int[] capsules = CapsuleIndices;
            ref int indexPtr = ref iArray[0];

            int vertexOff = 0;
            for (int segment = numSegments; segment > 0; segment--)
            {
                ref int capsulePtr = ref capsules[0];

                for (int capsuleIndex = 48; capsuleIndex > 0; capsuleIndex--)
                {
                    indexPtr = vertexOff + capsulePtr;

                    indexPtr = ref indexPtr.Plus(1);
                    capsulePtr = ref capsulePtr.Plus(1);
                }

                vertexOff += 10;
            }
            */
            fixed (int* iPtr0 = iArray, capsulePtr = CapsuleIndices)
            {
                int* iPtr = iPtr0;
                int vertexOff = 0;

                for (int segment = 0; segment < numSegments; segment++)
                {
                    for (int i = 0; i < 48; i++)
                    {
                        iPtr[i] = vertexOff + capsulePtr[i];
                    }

                    vertexOff += 10;
                    iPtr += 48;
                }            
            }
        }
    }
}