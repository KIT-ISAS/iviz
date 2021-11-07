#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Controllers
{
    internal class MarkerPointHelper
    {
        Point[] points = Array.Empty<Point>();
        ColorRGBA[] colors = Array.Empty<ColorRGBA>();
        Color32 color32;

        readonly PointListResource.DirectPointSetter singleColor;
        readonly PointListResource.DirectPointSetter noTint;
        readonly PointListResource.DirectPointSetter tintColor;

        public MarkerPointHelper()
        {
            singleColor = ProcessSingleColor;
            noTint = ProcessNoTint;
            tintColor = ProcessTintColor;
        }

        public PointListResource.DirectPointSetter GetPointSetter(Marker msg)
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

            foreach (var rosPoint in points)
            {
                (f.x, f.y, f.z) = rosPoint.Ros2Unity();
                if (PointListResource.IsElementValid(f))
                {
                    pointBuffer.Add(f);
                }
            }
        }

        void ProcessNoTint(NativeList<float4> pointBuffer)
        {
            var f = new float4();

            for (int i = 0; i < points.Length; i++)
            {
                (f.x, f.y, f.z) = points[i].Ros2Unity();
                if (PointListResource.IsElementValid(f))
                {
                    f.w = PointWithColor.RecastToFloat(colors[i].ToUnityColor32());
                    pointBuffer.Add(f);
                }
            }
        }

        void ProcessTintColor(NativeList<float4> pointBuffer)
        {
            Color color = color32;
            var f = new float4();
            
            for (int i = 0; i < points.Length; i++)
            {
                (f.x, f.y, f.z) = points[i].Ros2Unity();
                if (PointListResource.IsElementValid(f))
                {
                    f.w = PointWithColor.RecastToFloat(color * colors[i].ToUnityColor());
                    pointBuffer.Add(f);
                }
            }
        }
    }
}