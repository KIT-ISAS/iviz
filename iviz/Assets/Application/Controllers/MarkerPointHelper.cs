using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Controllers
{
    internal class MarkerPointHelper
    {
        Point[] points;
        ColorRGBA[] colors;
        Color color;

        readonly PointListResource.DirectPointSetter singleColor;
        readonly PointListResource.DirectPointSetter noTint;
        readonly PointListResource.DirectPointSetter tintColor;

        public MarkerPointHelper()
        {
            singleColor = ProcessSingleColor;
            noTint = ProcessNoTint;
            tintColor = ProcessTintColor;
        }

        public PointListResource.DirectPointSetter GetPointSetter([NotNull] Marker msg)
        {
            points = msg.Points;
            colors = msg.Colors;
            color = msg.Color.ToUnityColor();

            if (msg.Colors.Length == 0)
            {
                return singleColor;
            }

            return color == Color.white ? noTint : tintColor;
        }

        void ProcessSingleColor(NativeList<float4> pointBuffer)
        {
            float colorAsFloat = PointWithColor.FloatFromColorBits(color);
            foreach (var rosPoint in points)
            {
                PointWithColor point = new PointWithColor(rosPoint.Ros2Unity(), colorAsFloat);
                if (PointListResource.IsElementValid(point))
                {
                    pointBuffer.Add(point.f);
                }
            }
        }

        void ProcessNoTint(NativeList<float4> pointBuffer)
        {
            for (int i = 0; i < points.Length; i++)
            {
                ColorRGBA c = colors[i];
                
                float4 rgba = new float4(c.R, c.G, c.B, c.A);
                if (rgba.HasNaN())
                {
                    continue;
                }

                int4 rgbaAsInt = math.min(new int4(rgba * 255), 255);

                PointWithColor point = new PointWithColor(
                    points[i].Ros2Unity(),
                    new Color32((byte) rgbaAsInt.x, (byte) rgbaAsInt.y, (byte) rgbaAsInt.z, (byte) rgbaAsInt.w)
                );
                if (PointListResource.IsElementValid(point))
                {
                    pointBuffer.Add(point.f);
                }
            }
        }

        void ProcessTintColor(NativeList<float4> pointBuffer)
        {
            float4 mRgba = new float4(color.r, color.g, color.b, color.a);
            for (int i = 0; i < points.Length; i++)
            {
                ColorRGBA c = colors[i];
                float4 rgba = new float4(c.R, c.G, c.B, c.A);
                if (rgba.HasNaN())
                {
                    continue;
                }

                int4 rgbaAsInt = math.min(new int4(rgba * mRgba * 255), 255);

                PointWithColor point = new PointWithColor(
                    points[i].Ros2Unity(),
                    new Color32((byte) rgbaAsInt.x, (byte) rgbaAsInt.y, (byte) rgbaAsInt.z, (byte) rgbaAsInt.w)
                );
                if (PointListResource.IsElementValid(point))
                {
                    pointBuffer.Add(point.f);
                }
            }
        }
    }
}