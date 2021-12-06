#nullable enable

using System;
using Iviz.Core;
using Iviz.Tools;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
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

            int length = 10 * lineBuffer.Length;
            using var points = new Rent<Vector3>(length);
            using var colors = new Rent<Color32>(length);
            using var uvs = new Rent<Vector2>(length);
            using var indices = new Rent<int>(16 * 3 * lineBuffer.Length);
            int baseOff = 0;
            int pOff = 0;
            int cOff = 0;
            int uvOff = 0;
            int iOff = 0;

            var pArray = points.Array;

            const float minMagnitude = 1e-8f;
                
            for (int l = 0; l < lineBuffer.Length; l++)
            {
                ref readonly float4x2 line = ref lineBuffer[l];
                Vector3 a = line.c0.xyz;
                Vector3 b = line.c1.xyz;
                    
                Vector3 dirX = b - a;
                Vector3 dirY, dirZ;

                float dirXMagnitudeSq = dirX.MagnitudeSq();
                if (dirXMagnitudeSq < minMagnitude * minMagnitude)
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
                    

                var halfDirX = 0.5f * dirX;
                var halfSumYz = 0.5f * (dirY + dirZ);
                var halfDiffYz = 0.5f * (dirY - dirZ);

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

                var ca = PointWithColor.RecastToColor32(line.c0.w);
                var cb = PointWithColor.RecastToColor32(line.c1.w);

                var uv0 = new Vector2(line.c0.w, 0);
                for (int i = 0; i < 5; i++)
                {
                    colors.Array[cOff++] = ca;
                    uvs.Array[uvOff++] = uv0;
                }

                var uv1 = new Vector2(line.c1.w, 0);
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
            mesh.SetUVs(uvs);
        }
    }
}