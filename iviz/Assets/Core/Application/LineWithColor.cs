#nullable enable

using System.Runtime.CompilerServices;
using UnityEngine;
using System.Runtime.InteropServices;
using Iviz.Core;
using Unity.Mathematics;

namespace Iviz.Displays
{
    /// <summary>
    /// Struct for a pair of <see cref="PointWithColor"/>.
    /// Internally a wrapper around <see cref="float4x2"/>. 
    /// </summary>    
    [StructLayout(LayoutKind.Sequential)]
    public struct LineWithColor
    {
        static float WhiteBits => UnityUtils.AsFloat(Color.white);

        public float4x2 f;

        readonly Color32 ColorA => UnityUtils.AsColor32(f.c0.w);
        readonly Color32 ColorB => UnityUtils.AsColor32(f.c1.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LineWithColor(in Vector3 start, Color32 colorA, in Vector3 end, Color32 colorB) :
            this(start, UnityUtils.AsFloat(colorA), end, UnityUtils.AsFloat(colorB))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LineWithColor(in Vector3 start, in Vector3 end, Color32 color) :
            this(start, UnityUtils.AsFloat(color), end, UnityUtils.AsFloat(color))
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

        public readonly override string ToString()
        {
            return $"[x={f.c0.x} y={f.c0.y} z={f.c0.z} ---- " +
                   $"x={f.c1.x} y={f.c1.y} z={f.c1.z}]";
        }
    };
}