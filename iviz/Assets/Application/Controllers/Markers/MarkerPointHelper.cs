#nullable enable

using System;
using System.Runtime.CompilerServices;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Controllers.Markers
{
    internal class MarkerPointHelper
    {
        Point[] points = Array.Empty<Point>();
        ColorRGBA[] colors = Array.Empty<ColorRGBA>();
        Color32 color32;

        readonly Action<NativeList<float4>> singleColor;
        readonly Action<NativeList<float4>> noTint;
        readonly Action<NativeList<float4>> tintColor;

        public MarkerPointHelper()
        {
            singleColor = ProcessSingleColor;
            noTint = ProcessNoTint;
            tintColor = ProcessTintColor;
        }

        public Action<NativeList<float4>> GetPointSetter(Marker msg)
        {
            points = msg.Points;
            colors = msg.Colors;
            color32 = msg.Color.ToUnity();

            if (msg.Colors.Length == 0)
            {
                return singleColor;
            }

            return color32 == Color.white ? noTint : tintColor;
        }

        void ProcessSingleColor(NativeList<float4> pointBuffer)
        {
            float w = UnityUtils.AsFloat(color32);
            var mPoints = points;
            
            pointBuffer.EnsureCapacity(mPoints.Length);
            pointBuffer.Clear();

            for (int i = 0; i < mPoints.Length; i++)
            {
                if (IsInvalid(mPoints[i]))
                {
                    continue;
                }
                
                mPoints[i].Ros2Unity(w, out var f);
                pointBuffer.AddUnsafe(f);
            }
        }

        void ProcessNoTint(NativeList<float4> pointBuffer)
        {
            var mPoints = points;
            var mColors = colors;

            pointBuffer.EnsureCapacity(mPoints.Length);
            pointBuffer.Clear();
            
            for (int i = 0; i < mPoints.Length; i++)
            {
                if (IsInvalid(mPoints[i]))
                {
                    continue;
                }

                float w = UnityUtils.AsFloat(mColors[i].ToUnityColor32());
                mPoints[i].Ros2Unity(w, out var f);
                pointBuffer.AddUnsafe(f);
            }
        }

        void ProcessTintColor(NativeList<float4> pointBuffer)
        {
            Color color = color32;
            var mPoints = points;
            var mColors = colors;

            pointBuffer.EnsureCapacity(mPoints.Length);
            pointBuffer.Clear();
            
            for (int i = 0; i < mPoints.Length; i++)
            {
                if (IsInvalid(mPoints[i]))
                {
                    continue;
                }

                float w = UnityUtils.AsFloat(color * mColors[i].ToUnity());
                mPoints[i].Ros2Unity(w, out var f);
                pointBuffer.AddUnsafe(f);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsInvalid(in Point t)
        {
            return t.IsInvalid() || t.MaxAbsCoeff() > PointListDisplay.MaxPositionMagnitude;
        }
    }
}