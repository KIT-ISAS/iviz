﻿using System.Runtime.CompilerServices;
using Iviz.Core;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Struct for position and color/intensity.
    /// The same field is read either as a Color32 or a float depending on whether UseColormap is enabled in the display.
    /// Internally a wrapper around <see cref="float4"/>. 
    /// </summary>
    public struct PointWithColor
    {
        public float4 f;
        
        readonly Color32 Color => UnityUtils.AsColor32(f.w);
        readonly float Intensity => f.w;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public PointWithColor(in Vector3 position, Color32 color) :
            this(position.x, position.y, position.z, UnityUtils.AsFloat(color))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public PointWithColor(in Vector3 position, float intensity) :
            this(position.x, position.y, position.z, intensity)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        PointWithColor(float x, float y, float z, float w)
        {
            f.x = x;
            f.y = y;
            f.z = z;
            f.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public readonly override string ToString()
        {
            return $"[x={f.x} y={f.y} z={f.z} i={Intensity} c={Color}]";
        }
    }
}