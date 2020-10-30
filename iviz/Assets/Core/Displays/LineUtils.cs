using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    internal static class LineUtils
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

        public static void AddLineStipple([NotNull] List<LineWithColor> lines, in Vector3 a, in Vector3 b, Color color,
            float stippleLength = 0.1f)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            float remainingLength = (b - a).Magnitude();
            Vector3 direction = (b - a) / remainingLength;
            Vector3 advance = direction * stippleLength;
            Vector3 position = a;

            while (true)
            {
                if (remainingLength < 0)
                {
                    break;
                }

                if (remainingLength < stippleLength)
                {
                    lines.Add(new LineWithColor(position, b, color));
                    break;
                }

                lines.Add(new LineWithColor(position, position + advance, color));
                position += 2 * advance;
                remainingLength -= 2 * stippleLength;
            }
        }

        public static void AddCircleStipple(List<LineWithColor> lines, in Vector3 c, float radius, in Vector3 axis,
            Color color,
            int numStipples = 10)
        {
            Vector3 notAxis = Vector3.forward;
            if (Mathf.Approximately(notAxis.Cross(axis).MagnitudeSq(), 0))
            {
                notAxis = Vector3.right;
            }

            Vector3 diry = notAxis.Cross(axis).Normalized();
            Vector3 dirx = axis.Cross(diry).Normalized();
            dirx *= radius;
            diry *= radius;

            float coeff = Mathf.PI / numStipples;
            for (int i = 1; i <= 2 * numStipples + 1; i += 2)
            {
                float a, ax, ay;

                a = i * coeff;
                ax = Mathf.Cos(a);
                ay = Mathf.Sin(a);
                Vector3 v0 = ax * dirx + ay * diry;

                a += coeff;
                ax = Mathf.Cos(a);
                ay = Mathf.Sin(a);
                Vector3 v1 = ax * dirx + ay * diry;

                lines.Add(new LineWithColor(c + v0, c + v1, color));
            }
        }


        public static (Vector3[], Color32[], int[], Vector2[]) CreateCapsulesFromSegments(
            in NativeList<float4x2> lineBuffer, float scale)
        {
            int length = lineBuffer.Length;
            Vector3[] points = new Vector3[10 * length];
            Color32[] colors = new Color32[10 * length];
            int[] indices = new int[48 * length];
            Vector2[] coords = new Vector2[10 * length];

            Vector3 dirx, diry, dirz;

            Vector3 Transform(in Vector3 p) => p.x * dirx + p.y * diry + p.z * dirz;

            int poff = 0, ioff = 0;
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

                points[poff + 0] = Transform(CapsuleLines[0]) + a;
                points[poff + 1] = Transform(CapsuleLines[1]) + a;
                points[poff + 2] = Transform(CapsuleLines[2]) + a;
                points[poff + 3] = Transform(CapsuleLines[3]) + a;
                points[poff + 4] = Transform(CapsuleLines[4]) + a;

                points[poff + 5] = Transform(CapsuleLines[1]) + b;
                points[poff + 6] = Transform(CapsuleLines[2]) + b;
                points[poff + 7] = Transform(CapsuleLines[3]) + b;
                points[poff + 8] = Transform(CapsuleLines[4]) + b;
                points[poff + 9] = Transform(CapsuleLines[5]) + b;

                Color32 ca = PointWithColor.ColorFromFloatBits(line.c0.w);
                Color32 cb = PointWithColor.ColorFromFloatBits(line.c1.w);

                for (int i = 0; i < 5; i++)
                {
                    colors[poff + i] = ca;
                    coords[poff + i] = new Vector2(line.c0.w, 0);
                }

                for (int i = 5; i < 10; i++)
                {
                    colors[poff + i] = cb;
                    coords[poff + i] = new Vector2(line.c1.w, 0);
                }

                foreach (int index in CapsuleIndices)
                {
                    indices[ioff++] = poff + index;
                }

                poff += 10;
            }

            return (points, colors, indices, coords);
        }
    }
}