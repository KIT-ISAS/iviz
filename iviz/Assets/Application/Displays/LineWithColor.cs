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
                    return *(Color32*)&w;
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
                    return *(Color32*)&w;
                }
            }
        }

        public float4 PA => f.c0;

        public float4 PB => f.c1;

        public LineWithColor(in Vector3 a, Color32 colorA, in Vector3 b, Color32 colorB)
        {
            f.c0.x = a.x;
            f.c0.y = a.y;
            f.c0.z = a.z;

            f.c1.x = b.x;
            f.c1.y = b.y;
            f.c1.z = b.z;

            unsafe
            {
                f.c0.w = *(float*)&colorA;
                f.c1.w = *(float*)&colorB;
            }
        }

        public LineWithColor(in Vector3 a, in Vector3 b)
        {
            f.c0.x = a.x;
            f.c0.y = a.y;
            f.c0.z = a.z;
            f.c0.w = white;

            f.c1.x = b.x;
            f.c1.y = b.y;
            f.c1.z = b.z;
            f.c1.w = white;
        }

        public LineWithColor(in PointWithColor A, in PointWithColor B)
        {
            f.c0 = A;
            f.c1 = B;
        }

        public static implicit operator float4x2(in LineWithColor c) => c.f;

        public bool HasNaN => math.any(math.isnan(f.c0)) || math.any(math.isnan(f.c1));
    };
}