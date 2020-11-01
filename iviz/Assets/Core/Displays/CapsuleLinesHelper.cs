using System;
using System.Collections.Generic;
using Iviz.Core;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    internal class CapsuleLinesHelper
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
    } 
}