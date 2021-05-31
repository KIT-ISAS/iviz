using System;
using Iviz.Core;
using Iviz.Msgs;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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

                const float minMagnitude = 1e-8f;
                
                foreach (ref float4x2 line in lineBuffer.Ref())
                {
                    Vector3 a = line.c0.xyz;
                    Vector3 b = line.c1.xyz;
                    
                    Vector3 dirX = b - a;
                    Vector3 dirY, dirZ;

                    float dirXMagnitude = dirX.Magnitude();
                    if (dirXMagnitude < minMagnitude)
                    {
                        dirX = Vector3.zero;
                        dirY = Vector3.zero;
                        dirZ = Vector3.zero;
                    }
                    else
                    {
                        dirX /= dirX.Magnitude();

                        dirY = new Vector3(-dirX.y, dirX.x, 0);
                        if (Mathf.Abs(dirY.MaxAbsCoeff()) < minMagnitude)
                        {
                            dirY = Vector3.up.Cross(dirX);
                        }

                        dirX *= scale;
                        dirY *= scale / dirY.Magnitude();

                        dirZ = dirX.Cross(dirY);
                        dirZ *= scale / dirZ.Magnitude();
                    }
                    

                    Vector3 halfDirX = 0.5f * dirX;
                    Vector3 halfSumYz = 0.5f * (dirY + dirZ);
                    Vector3 halfDiffYz = 0.5f * (dirY - dirZ);

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