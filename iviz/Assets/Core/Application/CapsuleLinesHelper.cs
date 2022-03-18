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

        public static void CreateCapsulesFromSegments(ReadOnlySpan<float4x2> lineBuffer, float scale, Mesh mesh)
        {
            ThrowHelper.ThrowIfNull(mesh, nameof(mesh));

            if (lineBuffer.Length == 0)
            {
                mesh.Clear();
                return;
            }

            float halfScale = scale * 0.5f;
            int length = 10 * lineBuffer.Length;

            using var points = new Rent<Vector3>(length);
            using var colors = new Rent<Color32>(length);
            using var uvs = new Rent<Vector2>(length);
            int pOff = 0;
            int cOff = 0;
            int uvOff = 0;

            var pArray = points.AsSpan();
            var cArray = colors.AsSpan();
            var uArray = uvs.AsSpan();

            const float minMagnitude = 1e-5f;

            foreach (ref readonly var line in lineBuffer)
            {
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

                float abMagnitudeSq = ab.MagnitudeSq();
                if (abMagnitudeSq < minMagnitude * minMagnitude)
                {
                    dirX = Vector3.zero;
                    dirY = Vector3.zero;
                    dirZ = Vector3.zero;
                }
                else
                {
                    dirX = ab / Mathf.Sqrt(abMagnitudeSq);
                    var (x, y, z) = dirX;
                    dirY = (Math.Abs(z) - 1).ApproximatelyZero()
                        ? new Vector3(-z, 0, x) / Mathf.Sqrt(x * x + z * z)
                        : new Vector3(-y, x, 0) / Mathf.Sqrt(x * x + y * y);
                    dirZ = dirX.Cross(dirY);
                }

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


                /*
                for (int i = 0; i < 5; i++)
                {
                    colors[cOff++] = ca;
                    uvs[uvOff++] = uv0;
                }
                */
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

                /*
                for (int i = 5; i < 10; i++)
                {
                    colors[cOff++] = cb;
                    uvs[uvOff++] = uv1;
                }
                */

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
            }

            int indicesSize = 16 * 3 * lineBuffer.Length;
            mesh.Clear();
            mesh.indexFormat = indicesSize <= UnityUtils.MeshUInt16Threshold
                ? IndexFormat.UInt16
                : IndexFormat.UInt32;

            mesh.SetVertices(points);
            mesh.SetColors(colors);
            mesh.SetUVs(uvs);
            
            
            using var indices = new Rent<int>(indicesSize);
            var iArray = indices.AsSpan();
            int iOff = 0;

            foreach (int i in ..lineBuffer.Length)
            {
                int baseOff = i * 10;
                foreach (int index in CapsuleIndices)
                {
                    iArray[iOff++] = baseOff + index;
                }
            }

            mesh.SetTriangles(indices);
        }
    }
}