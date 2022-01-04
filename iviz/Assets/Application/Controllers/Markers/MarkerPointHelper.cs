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
            var f = new float4();
            f.w = PointWithColor.RecastToFloat(color32);

            pointBuffer.EnsureCapacity(points.Length);
            pointBuffer.Clear();

            for (int i = 0; i < points.Length; i++)
            {
                points[i].Ros2Unity(ref f);
                if (PointListResource.IsElementValid(f))
                {
                    pointBuffer.AddUnsafe(f);
                }
            }
        }

        void ProcessNoTint(NativeList<float4> pointBuffer)
        {
            var f = new float4();

            pointBuffer.EnsureCapacity(points.Length);
            pointBuffer.Clear();
            
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Ros2Unity(ref f);
                if (!PointListResource.IsElementValid(f))
                {
                    continue;
                }
                
                f.w = PointWithColor.RecastToFloat(colors[i].ToUnityColor32());
                pointBuffer.AddUnsafe(f);
            }
        }

        void ProcessTintColor(NativeList<float4> pointBuffer)
        {
            Color color = color32;
            var f = new float4();
            
            pointBuffer.EnsureCapacity(points.Length);
            pointBuffer.Clear();
            
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Ros2Unity(ref f);
                if (!PointListResource.IsElementValid(f))
                {
                    continue;
                }
                
                f.w = PointWithColor.RecastToFloat(color * colors[i].ToUnityColor());
                pointBuffer.AddUnsafe(f);
            }
        }
    }
}