#nullable enable

using System;
using Iviz.Core;
using Iviz.Tools;
using JetBrains.Annotations;
using Unity.Collections;
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
            if (mesh == null)
            {
                throw new ArgumentNullException(nameof(mesh));
            }

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
            using var indices = new Rent<int>(16 * 3 * lineBuffer.Length);
            int pOff = 0;
            int cOff = 0;
            int uvOff = 0;
            int iOff = 0;

            var pArray = points.Array;
            var cArray = colors.Array;
            var uArray = uvs.Array;
            int[] iArray = indices.Array;

            const float minMagnitude = 1e-5f;

            Vector3 a = default;
            Vector3 b = default;

            foreach (var line in lineBuffer)
            {
                (a.x, a.y, a.z) = (line.c0.x, line.c0.y, line.c0.z);
                (b.x, b.y, b.z) = (line.c1.x, line.c1.y, line.c1.z);

                Vector3 ab = b - a;
                Vector3 dirX, dirY, dirZ;

                if (ab.MagnitudeSq() < minMagnitude * minMagnitude)
                {
                    dirX = Vector3.zero;
                    dirY = Vector3.zero;
                    dirZ = Vector3.zero;
                }
                else
                {
                    dirX = ab.Normalized();
                    dirY = !Mathf.Approximately(Mathf.Abs(dirX.z), 1)
                        ? new Vector3(-dirX.y, dirX.x, 0).Normalized()
                        : new Vector3(-dirX.z, 0, dirX.x).Normalized();
                    dirZ = dirX.Cross(dirY).Normalized();
                }

                var halfDirX = halfScale * dirX;
                var halfSumYz = halfScale * (dirY + dirZ);
                var halfDiffYz = halfScale * (dirY - dirZ);

                int baseOff = pOff;

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

                var ca = UnityUtils.AsColor32(line.c0.w);
                var cb = UnityUtils.AsColor32(line.c1.w);

                var uv0 = new Vector2(line.c0.w, 0);
                var uv1 = new Vector2(line.c1.w, 0);

                /*
                for (int i = 0; i < 5; i++)
                {
                    colors[cOff++] = ca;
                    uvs[uvOff++] = uv0;
                }
                */
                {
                    cArray[cOff++] = ca;
                    cArray[cOff++] = ca;
                    cArray[cOff++] = ca;
                    cArray[cOff++] = ca;
                    cArray[cOff++] = ca;
                    uArray[uvOff++] = uv0;
                    uArray[uvOff++] = uv0;
                    uArray[uvOff++] = uv0;
                    uArray[uvOff++] = uv0;
                    uArray[uvOff++] = uv0;
                }

                /*
                for (int i = 5; i < 10; i++)
                {
                    colors[cOff++] = cb;
                    uvs[uvOff++] = uv1;
                }
                */
                {
                    cArray[cOff++] = cb;
                    cArray[cOff++] = cb;
                    cArray[cOff++] = cb;
                    cArray[cOff++] = cb;
                    cArray[cOff++] = cb;
                    uArray[uvOff++] = uv1;
                    uArray[uvOff++] = uv1;
                    uArray[uvOff++] = uv1;
                    uArray[uvOff++] = uv1;
                    uArray[uvOff++] = uv1;
                }

                foreach (int index in CapsuleIndices)
                {
                    iArray[iOff++] = baseOff + index;
                }
            }

            mesh.Clear();
            mesh.indexFormat = indices.Length <= UnityUtils.MeshUInt16Threshold
                ? IndexFormat.UInt16
                : IndexFormat.UInt32;

            mesh.SetVertices(points);
            mesh.SetTriangles(indices);
            mesh.SetColors(colors);
            mesh.SetUVs(uvs);
        }
    }
}