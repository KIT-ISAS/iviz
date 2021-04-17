using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Msgs;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    internal static class CapsuleLinesHelper
    {
        static readonly Vector3[] CapsuleLines =
        {
            new Vector3(-0.5f, 0, 0),
            new Vector3(0, 0.5f, 0.5f),
            new Vector3(0, 0.5f, -0.5f),
            new Vector3(0, -0.5f, -0.5f),
            new Vector3(0, -0.5f, 0.5f),
            new Vector3(0.5f, 0, 0)
        };

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

        public static void CreateCapsulesFromSegments([NotNull] NativeList<float4x2> lineBuffer, float scale,
            [NotNull] Mesh mesh)
        {
            if (lineBuffer == null)
            {
                throw new ArgumentNullException(nameof(lineBuffer));
            }

            if (mesh == null)
            {
                throw new ArgumentNullException(nameof(mesh));
            }

            int length = 10 * lineBuffer.Length;
            using (var points = new Rent<Vector3>(length))
            using (var colors = new Rent<Color32>(length))
            using (var uvs = new Rent<Vector2>(length))
            using (var indices = new Rent<int>(CapsuleIndices.Length * lineBuffer.Length))
            {
                int baseOff = 0;
                int pOff = 0;
                int cOff = 0;
                int uvOff = 0;
                int iOff = 0;

                foreach (ref float4x2 line in lineBuffer.Ref())
                {
                    Vector3 a = line.c0.xyz;
                    Vector3 b = line.c1.xyz;
                    var dirx = b - a;
                    dirx /= dirx.Magnitude();

                    //Vector3 diry = Vector3.forward.Cross(dirx);
                    Vector3 diry = new Vector3(-dirx.y, dirx.x, 0);
                    if (Mathf.Approximately(diry.MaxAbsCoeff(), 0))
                    {
                        diry = Vector3.up.Cross(dirx);
                    }

                    dirx *= scale;
                    diry *= scale / diry.Magnitude();

                    Vector3 dirz = dirx.Cross(diry);
                    dirz *= scale / dirz.Magnitude();

                    Vector3 halfDirX = 0.5f * dirx;
                    Vector3 halfSumYz = 0.5f * (diry + dirz);
                    Vector3 halfDiffYz = 0.5f * (diry - dirz);

                    points.Array[pOff++] = a - halfDirX;
                    points.Array[pOff++] = a + halfSumYz;
                    points.Array[pOff++] = a + halfDiffYz;
                    points.Array[pOff++] = a - halfSumYz;
                    points.Array[pOff++] = a - halfDiffYz;

                    points.Array[pOff++] = b + halfSumYz;
                    points.Array[pOff++] = b + halfDiffYz;
                    points.Array[pOff++] = b - halfSumYz;
                    points.Array[pOff++] = b - halfDiffYz;
                    points.Array[pOff++] = b + halfDirX;

                    Color32 ca = PointWithColor.ColorFromFloatBits(line.c0.w);
                    Color32 cb = PointWithColor.ColorFromFloatBits(line.c1.w);

                    Vector2 uv0 = new Vector2(line.c0.w, 0);
                    for (int i = 0; i < 5; i++)
                    {
                        colors.Array[cOff++] = ca;
                        uvs.Array[uvOff++] = uv0;
                    }

                    Vector2 uv1 = new Vector2(line.c1.w, 0);
                    for (int i = 5; i < 10; i++)
                    {
                        colors.Array[cOff++] = cb;
                        uvs.Array[uvOff++] = uv1;
                    }

                    foreach (int index in CapsuleIndices)
                    {
                        indices.Array[iOff++] = baseOff + index;
                    }

                    baseOff += 10;
                }

                mesh.Clear();
                mesh.SetVertices(points);
                mesh.SetTriangles(indices);
                mesh.SetColors(colors);
                mesh.SetUVs(0, uvs);
            }
        }
    }
}