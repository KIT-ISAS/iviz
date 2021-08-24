using System.Runtime.CompilerServices;
using UnityEngine;
using System.Runtime.InteropServices;
using Iviz.Core;
using JetBrains.Annotations;
using Unity.Mathematics;

namespace Iviz.Displays
{
    /// <summary>
    /// Struct for a pair of <see cref="PointWithColor"/>.
    /// Internally a wrapper around <see cref="float4x2"/>. 
    /// </summary>    
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct LineWithColor
    {
        static readonly float WhiteBits = PointWithColor.FloatFromColorBits(Color.white);

        public readonly float4x2 f;

        public float3 A => f.c0.xyz;

        Color32 ColorA => PointWithColor.ColorFromFloatBits(f.c0.w);

        public float3 B => f.c1.xyz;

        Color32 ColorB => PointWithColor.ColorFromFloatBits(f.c1.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LineWithColor(in Vector3 start, Color32 colorA, in Vector3 end, Color32 colorB) :
            this(start, PointWithColor.FloatFromColorBits(colorA), end, PointWithColor.FloatFromColorBits(colorB))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LineWithColor(in Vector3 start, in Vector3 end, Color32 color) :
            this(start, PointWithColor.FloatFromColorBits(color), end, PointWithColor.FloatFromColorBits(color))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LineWithColor(in Vector3 start, in Vector3 end) :
            this(start, WhiteBits, end, WhiteBits)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LineWithColor(in Vector3 start, float intensityA, in Vector3 end, float intensityB)
        {
            f.c0.x = start.x;
            f.c0.y = start.y;
            f.c0.z = start.z;
            f.c0.w = intensityA;

            f.c1.x = end.x;
            f.c1.y = end.y;
            f.c1.z = end.z;
            f.c1.w = intensityB;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LineWithColor(in PointWithColor start, in PointWithColor end)
        {
            f.c0 = start.f;
            f.c1 = end.f;
        }

        /// <summary>
        /// Do the positions have a Nan? (ignores intensity) 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasNaN() => f.c0.HasNaN() || f.c1.HasNaN();

        [NotNull]
        public override string ToString()
        {
            return
                $"[x={f.c0.x} y={f.c0.y} z={f.c0.z} i={f.c0.w} c={ColorA} ---- x={f.c1.x} y={f.c1.y} z={f.c1.z} i={f.c1.w} c={ColorB}]";
        }
    };
}