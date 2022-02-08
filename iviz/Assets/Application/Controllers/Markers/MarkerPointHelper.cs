#nullable enable

using System;
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
            color32 = msg.Color.ToUnityColor();

            if (msg.Colors.Length == 0)
            {
                return singleColor;
            }

            return color32 == Color.white ? noTint : tintColor;
        }

        void ProcessSingleColor(NativeList<float4> pointBuffer)
        {
            float w = UnityUtils.AsFloat(color32);

            pointBuffer.EnsureCapacity(points.Length);
            pointBuffer.Clear();

            foreach (ref readonly var point in points.AsSpan())
            {
                if (!PointListDisplay.IsElementValid(point))
                {
                    continue;
                }
                
                point.Ros2Unity(w, out var f);
                pointBuffer.AddUnsafe(f);
            }
        }

        void ProcessNoTint(NativeList<float4> pointBuffer)
        {
            pointBuffer.EnsureCapacity(points.Length);
            pointBuffer.Clear();
            
            for (int i = 0; i < points.Length; i++)
            {
                if (!PointListDisplay.IsElementValid(points[i]))
                {
                    continue;
                }
                
                float w = UnityUtils.AsFloat(colors[i].ToUnityColor32());
                points[i].Ros2Unity(w, out var f);
                pointBuffer.AddUnsafe(f);
            }
        }

        void ProcessTintColor(NativeList<float4> pointBuffer)
        {
            Color color = color32;
            pointBuffer.EnsureCapacity(points.Length);
            pointBuffer.Clear();
            
            for (int i = 0; i < points.Length; i++)
            {
                if (!PointListDisplay.IsElementValid(points[i]))
                {
                    continue;
                }
                
                float w = UnityUtils.AsFloat(color * colors[i].ToUnityColor());
                points[i].Ros2Unity(w, out var f);
                pointBuffer.AddUnsafe(f);
            }
        }
    }
}