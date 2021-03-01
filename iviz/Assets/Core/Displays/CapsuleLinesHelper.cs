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

        /*
        readonly List<Vector3> points = new List<Vector3>();
        readonly List<Color32> colors = new List<Color32>();
        readonly List<int> indices = new List<int>();
        readonly List<Vector2> uvs = new List<Vector2>();

        public void CreateCapsulesFromSegments(in NativeList<float4x2> lineBuffer, float scale)
        {
            points.Clear();
            colors.Clear();
            indices.Clear();
            uvs.Clear();
            
            Vector3 dirx, diry, dirz;

            Vector3 Transform(in Vector3 p) => p.x * dirx + p.y * diry + p.z * dirz;

            int poff = 0;
            foreach (float4x2 line in lineBuffer)
            {
                Vector3 a = line.c0.xyz;
                Vector3 b = line.c1.xyz;
                dirx = b - a;
                dirx /= dirx.Magnitude();

                diry = Vector3.forward.Cross(dirx);
                if (Mathf.Approximately(diry.MagnitudeSq(), 0))
                {
                    diry = Vector3.up.Cross(dirx);
                }

                dirx *= scale;
                diry *= scale / diry.Magnitude();
                dirz = dirx.Cross(diry);
                dirz *= scale / dirz.Magnitude();

                points.Add(Transform(CapsuleLines[0]) + a);
                points.Add(Transform(CapsuleLines[1]) + a);
                points.Add(Transform(CapsuleLines[2]) + a);
                points.Add(Transform(CapsuleLines[3]) + a);
                points.Add(Transform(CapsuleLines[4]) + a);

                points.Add(Transform(CapsuleLines[1]) + b);
                points.Add(Transform(CapsuleLines[2]) + b);
                points.Add(Transform(CapsuleLines[3]) + b);
                points.Add(Transform(CapsuleLines[4]) + b);
                points.Add(Transform(CapsuleLines[5]) + b);

                Color32 ca = PointWithColor.ColorFromFloatBits(line.c0.w);
                Color32 cb = PointWithColor.ColorFromFloatBits(line.c1.w);

                for (int i = 0; i < 5; i++)
                {
                    colors.Add(ca);
                    uvs.Add(new Vector2(line.c0.w, 0));
                }

                for (int i = 5; i < 10; i++)
                {
                    colors.Add(cb);
                    uvs.Add(new Vector2(line.c1.w, 0));
                }

                foreach (int index in CapsuleIndices)
                {
                    indices.Add(poff + index);
                }

                poff += 10;
            }
        }

        public void UpdateMesh([NotNull] Mesh mesh)
        {
            if (mesh == null)
            {
                throw new ArgumentNullException(nameof(mesh));
            }

            mesh.Clear();
            mesh.SetVertices(points);
            mesh.SetTriangles(indices, 0);
            mesh.SetColors(colors);
            mesh.SetUVs(0, uvs);
        }
        */

        public static void CreateCapsulesFromSegments(in NativeList<float4x2> lineBuffer, float scale, [NotNull] Mesh mesh)
        {
            if (mesh == null)
            {
                throw new ArgumentNullException(nameof(mesh));
            }

            //Vector3 Transform(in Vector3 p) => p.x * dirx + p.y * diry + p.z * dirz;

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
                
                foreach (float4x2 line in lineBuffer)
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