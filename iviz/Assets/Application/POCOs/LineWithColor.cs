using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace Iviz.Displays
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct LineWithColor
    {
        static readonly float white = PointWithColor.WhiteAsIntensity();

        readonly float4x2 f;

        public Vector3 A => new Vector3(f.c0.x, f.c0.y, f.c0.z);

        public Color32 ColorA
        {
            get
            {
                unsafe
                {
                    float w = f.c0.w;
                    return *(Color32*) &w;
                }
            }
        }

        public Vector3 B => new Vector3(f.c1.x, f.c1.y, f.c1.z);

        public Color32 ColorB
        {
            get
            {
                unsafe
                {
                    float w = f.c1.w;
                    return *(Color32*) &w;
                }
            }
        }

        public float4 PA => f.c0;

        public float4 PB => f.c1;

        public LineWithColor(in Vector3 start, Color32 colorA, in Vector3 end, Color32 colorB)
        {
            f.c0.x = start.x;
            f.c0.y = start.y;
            f.c0.z = start.z;

            f.c1.x = end.x;
            f.c1.y = end.y;
            f.c1.z = end.z;

            unsafe
            {
                f.c0.w = *(float*) &colorA;
                f.c1.w = *(float*) &colorB;
            }
        }

        public LineWithColor(in Vector3 start, in Vector3 end, Color32 color) : this(start, color, end, color)
        {
        }

        public LineWithColor(in Vector3 start, in Vector3 end)
        {
            f.c0.x = start.x;
            f.c0.y = start.y;
            f.c0.z = start.z;
            f.c0.w = white;

            f.c1.x = end.x;
            f.c1.y = end.y;
            f.c1.z = end.z;
            f.c1.w = white;
        }

        public LineWithColor(in PointWithColor start, in PointWithColor end)
        {
            f.c0 = start;
            f.c1 = end;
        }

        public static implicit operator float4x2(in LineWithColor c) => c.f;

        public bool HasNaN => f.c0.HasNaN() || f.c1.HasNaN();
    };

    static class LineUtils
    {
        public static void AddLineStipple(List<LineWithColor> lines, in Vector3 a, in Vector3 b, Color color,
            float stippleLength = 0.1f)
        {
            float remainingLength = (b - a).magnitude;
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
            Vector3 x = Vector3.forward;
            if (x == axis)
            {
                x = Vector3.right;
            }

            Vector3 diry = Vector3.Cross(x, axis).normalized;
            Vector3 dirx = Vector3.Cross(axis, diry).normalized;
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
    }
}