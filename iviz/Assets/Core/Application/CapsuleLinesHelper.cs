#nullable enable

using System;
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
            mesh.indexFormat = indicesSize <= UnityUtils.MeshUInt16Threshold
                ? IndexFormat.UInt16
                : IndexFormat.UInt32;

            var indices = new Rent<int>(indicesSize);

            try
            {
                CreateCapsulesFromSegments(ref lineBuffer.GetReference(), bufferLength, scale,
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

        static void CreateCapsulesFromSegments(ref float4x2 linePtr, int numSegments, float scale,
            Vector3[] pArray, Color32[] cArray, Vector2[] uArray, int[] iArray)
        {
            const float minMagnitude = 1e-5f;

            float halfScale = scale * 0.5f;

            foreach (int segment in ..numSegments)
            {
                ref readonly var line = ref linePtr.Plus(segment);

                //(a.x, a.y, a.z) = (line.c0.x, line.c0.y, line.c0.z);
                //(b.x, b.y, b.z) = (line.c1.x, line.c1.y, line.c1.z);

                Vector3 a;
                a.x = line.c0.x;
                a.y = line.c0.y;
                a.z = line.c0.z;

                Vector3 b;
                b.x = line.c1.x;
                b.y = line.c1.y;
                b.z = line.c1.z;

                Vector3 ab = b - a;
                Vector3 dirX, dirY, dirZ;

                float abMagnitudeSq = ab.sqrMagnitude;
                if (abMagnitudeSq < minMagnitude * minMagnitude)
                {
                    dirX.x = dirX.y = dirX.z = 0;
                    dirY.x = dirY.y = dirY.z = 0;
                    dirZ.x = dirZ.y = dirZ.z = 0;
                }
                else
                {
                    dirX = ab / Mathf.Sqrt(abMagnitudeSq);
                    var (x, y, z) = dirX;
                    dirY = (Mathf.Abs(z) - 1).ApproximatelyZero()
                        ? new Vector3(-z, 0, x) / Mathf.Sqrt(x * x + z * z)
                        : new Vector3(-y, x, 0) / Mathf.Sqrt(x * x + y * y);
                    dirZ = dirX.Cross(dirY);
                }

                /*
                var halfDirX = halfScale * dirX;
                var halfSumYz = halfScale * (dirY + dirZ);
                var halfDiffYz = halfScale * (dirY - dirZ);

                pArray[pOff++] = a - halfDirX;
                pArray[pOff++] = a + halfSumYz;
                pArray[pOff++] = a + halfDiffYz;
                pArray[pOff++] = a - halfSumYz;
                pArray[pOff++] = a - halfDiffYz;

                pArray[pOff++] = b + halfSumYz;
                pArray[pOff++] = b + halfDiffYz;
                pArray[pOff++] = b - halfSumYz;
                pArray[pOff++] = b - halfDiffYz;
                pArray[pOff++] = b + halfDirX;
                */

                int offset = segment * 10;

                {
                    ref var pPtr = ref pArray[offset];

                    var halfDirX = halfScale * dirX;
                    var halfSumYz = halfScale * (dirY + dirZ);
                    var halfDiffYz = halfScale * (dirY - dirZ);

                    pPtr = a - halfDirX;
                    pPtr.Plus(9) = b + halfDirX;

                    pPtr.Plus(1) = a + halfSumYz;
                    pPtr.Plus(3) = a - halfSumYz;
                    pPtr.Plus(5) = b + halfSumYz;
                    pPtr.Plus(7) = b - halfSumYz;

                    pPtr.Plus(2) = a + halfDiffYz;
                    pPtr.Plus(4) = a - halfDiffYz;
                    pPtr.Plus(6) = b + halfDiffYz;
                    pPtr.Plus(8) = b - halfDiffYz;
                }


                /*
                for (int i = 0; i < 5; i++)
                {
                    colors[cOff++] = ca;
                    uvs[uvOff++] = uv0;
                }
                */
                /*
                {
                    var ca = UnityUtils.AsColor32(line.c0.w);
                    cArray[cOff++] = ca;
                    cArray[cOff++] = ca;
                    cArray[cOff++] = ca;
                    cArray[cOff++] = ca;
                    cArray[cOff++] = ca;

                    var cb = UnityUtils.AsColor32(line.c1.w);
                    cArray[cOff++] = cb;
                    cArray[cOff++] = cb;
                    cArray[cOff++] = cb;
                    cArray[cOff++] = cb;
                    cArray[cOff++] = cb;
                }
                */

                {
                    ref var cPtr = ref cArray[offset];

                    var ca = UnityUtils.AsColor32(line.c0.w);
                    cPtr = ca;
                    cPtr.Plus(1) = ca;
                    cPtr.Plus(2) = ca;
                    cPtr.Plus(3) = ca;
                    cPtr.Plus(4) = ca;

                    var cb = UnityUtils.AsColor32(line.c1.w);
                    cPtr.Plus(5) = cb;
                    cPtr.Plus(6) = cb;
                    cPtr.Plus(7) = cb;
                    cPtr.Plus(8) = cb;
                    cPtr.Plus(9) = cb;
                }

                /*
                for (int i = 5; i < 10; i++)
                {
                    colors[cOff++] = cb;
                    uvs[uvOff++] = uv1;
                }
                */

                /*
                {
                    Vector2 uv0;
                    uv0.x = line.c0.w;
                    uv0.y = 0;

                    uArray[uvOff++] = uv0;
                    uArray[uvOff++] = uv0;
                    uArray[uvOff++] = uv0;
                    uArray[uvOff++] = uv0;
                    uArray[uvOff++] = uv0;

                    Vector2 uv1;
                    uv1.x = line.c1.w;
                    uv1.y = 0;

                    uArray[uvOff++] = uv1;
                    uArray[uvOff++] = uv1;
                    uArray[uvOff++] = uv1;
                    uArray[uvOff++] = uv1;
                    uArray[uvOff++] = uv1;
                }
                */
                {
                    ref var uvPtr = ref uArray[offset];

                    Vector2 uv0;
                    uv0.x = line.c0.w;
                    uv0.y = 0;

                    uvPtr = uv0;
                    uvPtr.Plus(1) = uv0;
                    uvPtr.Plus(2) = uv0;
                    uvPtr.Plus(3) = uv0;
                    uvPtr.Plus(4) = uv0;

                    Vector2 uv1;
                    uv1.x = line.c1.w;
                    uv1.y = 0;

                    uvPtr.Plus(5) = uv1;
                    uvPtr.Plus(6) = uv1;
                    uvPtr.Plus(7) = uv1;
                    uvPtr.Plus(8) = uv1;
                    uvPtr.Plus(9) = uv1;
                }
            }

            ref int capsulePtr = ref CapsuleIndices[0];
            ref int iPtr = ref iArray[0];

            /*
            foreach (int i in ..lineBuffer.Length)
            {
                int baseOff = i * 10;
                foreach (int index in cIndices)
                {
                    iArray[iOff++] = baseOff + index;
                }
            }
            */

            foreach (int segment in ..numSegments)
            {
                int vertexOff = segment * 10;
                foreach (int capsuleIndex in ..48)
                {
                    iPtr = vertexOff + capsulePtr.Plus(capsuleIndex);
                    iPtr = ref iPtr.Plus(1);
                }
            }
        }
    }
}