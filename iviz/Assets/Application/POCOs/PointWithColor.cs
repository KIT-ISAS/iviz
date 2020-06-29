using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public readonly struct PointWithColor
    {
        public static unsafe float WhiteAsIntensity()
        {
            Color tmp = UnityEngine.Color.white;
            return *(float*)&tmp;
        }
        static readonly float White = WhiteAsIntensity();

        readonly float4 f;

        public float X => f.x;
        public float Y => f.y;
        public float Z => f.z;
        public Vector3 Position => new Vector3(f.x, f.y, f.z);
        public Color32 Color
        {
            get
            {
                unsafe
                {
                    float w = f.w;
                    return *(Color32*)&w;
                }
            }

        }
        public float Intensity => f.w;

        public PointWithColor(in Vector3 position, Color32 color)
        {
            f.x = position.x;
            f.y = position.y;
            f.z = position.z;
            unsafe
            {
                f.w = *(float*)&color;
            }
        }

        public PointWithColor(in Vector3 position)
        {
            f.x = position.x;
            f.y = position.y;
            f.z = position.z;
            f.w = White;
        }

        public PointWithColor(in Vector3 position, float intensity)
        {
            f.x = position.x;
            f.y = position.y;
            f.z = position.z;
            f.w = intensity;
        }

        public PointWithColor(float x, float y, float z, float w)
        {
            f.x = x;
            f.y = y;
            f.z = z;
            f.w = w;
        }

        public PointWithColor(float x, float y, float z)
        {
            f.x = x;
            f.y = y;
            f.z = z;
            f.w = White;
        }

        public PointWithColor(in float4 f)
        {
            this.f = f;
        }

        public bool HasNaN => f.HasNaN();

        public static implicit operator float4(in PointWithColor c) => c.f;

        public override string ToString()
        {
            return $"[x={X} y={Y} z={Z} i={Intensity} c={Color}]";
        }
    };

}