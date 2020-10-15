using System.Runtime.CompilerServices;
using UnityEngine;
using System.Runtime.InteropServices;
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
        
        readonly float4x2 f;

        public Vector3 A => f.c0.xyz;

        public Color32 ColorA => PointWithColor.ColorFromFloatBits(f.c0.w);

        public Vector3 B => f.c1.xyz;

        public Color32 ColorB => PointWithColor.ColorFromFloatBits(f.c1.w);

        public float4 PA => f.c0;

        public float4 PB => f.c1;

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
            f.c0 = start;
            f.c1 = end;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public LineWithColor(in float4x2 f)
        {
            this.f = f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static implicit operator float4x2(in LineWithColor c) => c.f;

        /// <summary>
        /// Do the positions have a Nan? (ignores intensity) 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public bool HasNaN() => f.c0.HasNaN() || f.c1.HasNaN();

        public override string ToString()
        {
            return $"[x={PA.x} y={PA.y} z={PA.y} i={PA.w} c={ColorA} ---- x={PB.x} y={PB.y} z={PB.y} i={PB.w} c={ColorB}]";
        }
    };
}