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
    internal class MarkerLineHelper
    {
        Point[] points;
        ColorRGBA[] colors;
        Color32 color32;

        readonly LineResource.DirectLineSetter lineStripSingleColor;
        readonly LineResource.DirectLineSetter lineStripNoTint;
        readonly LineResource.DirectLineSetter lineStripTintColor;
        readonly LineResource.DirectLineSetter lineListSingleColor;
        readonly LineResource.DirectLineSetter lineListNoTint;
        readonly LineResource.DirectLineSetter lineListTintColor;

        public MarkerLineHelper()
        {
            lineStripSingleColor = ProcessLineStripSingleColor;
            lineStripNoTint = ProcessLineStripNoTint;
            lineStripTintColor = ProcessLineStripTintColor;
            lineListSingleColor = ProcessLineListSingleColor;
            lineListNoTint = ProcessLineListNoTint;
            lineListTintColor = ProcessLineListTintColor;
        }

        public LineResource.DirectLineSetter GetLineSetterForStrip([NotNull] Marker msg)
        {
            points = msg.Points;
            colors = msg.Colors;
            color32 = msg.Color.ToUnityColor32();

            if (msg.Colors.Length == 0)
            {
                return lineStripSingleColor;
            }

            return color32 == Color.white 
                ? lineStripNoTint 
                : lineStripTintColor;
        }

        bool? ProcessLineStripSingleColor(NativeList<float4x2> lineBuffer)
        {
            float colorAsFloat = PointWithColor.FloatFromColorBits(color32);
            for (int i = 0; i < points.Length - 1; i++)
            {
                LineWithColor line = new LineWithColor(
                    points[i].Ros2Unity(), colorAsFloat,
                    points[i + 1].Ros2Unity(), colorAsFloat
                );
                if (LineResource.IsElementValid(line))
                {
                    lineBuffer.Add(line.f);
                }
            }

            return color32.a < 255;
        }

        bool? ProcessLineStripNoTint(NativeList<float4x2> lineBuffer)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                LineWithColor line = new LineWithColor(
                    points[i].Ros2Unity(), colors[i].ToUnityColor32(),
                    points[i + 1].Ros2Unity(), colors[i + 1].ToUnityColor32()
                );
                if (LineResource.IsElementValid(line))
                {
                    lineBuffer.Add(line.f);
                }
            }

            return null;
        }

        bool? ProcessLineStripTintColor(NativeList<float4x2> lineBuffer)
        {
            Color color = color32;
            for (int i = 0; i < points.Length - 1; i++)
            {
                LineWithColor line = new LineWithColor(
                    points[i].Ros2Unity(), color * colors[i].ToUnityColor(),
                    points[i + 1].Ros2Unity(), color * colors[i + 1].ToUnityColor()
                );
                if (LineResource.IsElementValid(line))
                {
                    lineBuffer.Add(line.f);
                }
            }

            return color.a < 1 ? (bool?) true : null;
        }

        public LineResource.DirectLineSetter GetLineSetterForList([NotNull] Marker msg)
        {
            points = msg.Points;
            colors = msg.Colors;
            color32 = msg.Color.ToUnityColor32();

            if (msg.Colors.Length == 0)
            {
                return lineListSingleColor;
            }

            return color32 == Color.white 
                ? lineListNoTint 
                : lineListTintColor;
        }

        bool? ProcessLineListSingleColor(NativeList<float4x2> lineBuffer)
        {
            float colorAsFloat = PointWithColor.FloatFromColorBits(color32);
            for (int i = 0; i < points.Length / 2; i++)
            {
                LineWithColor line = new LineWithColor(
                    points[2 * i + 0].Ros2Unity(), colorAsFloat,
                    points[2 * i + 1].Ros2Unity(), colorAsFloat
                );
                if (LineResource.IsElementValid(line))
                {
                    lineBuffer.Add(line.f);
                }
            }

            return color32.a < 255;
        }

        bool? ProcessLineListNoTint(NativeList<float4x2> lineBuffer)
        {
            for (int i = 0; i < points.Length / 2; i++)
            {
                LineWithColor line = new LineWithColor(
                    points[2 * i + 0].Ros2Unity(), colors[2 * i + 0].ToUnityColor32(),
                    points[2 * i + 1].Ros2Unity(), colors[2 * i + 1].ToUnityColor32()
                );
                if (LineResource.IsElementValid(line))
                {
                    lineBuffer.Add(line.f);
                }
            }

            return null;
        }

        bool? ProcessLineListTintColor(NativeList<float4x2> lineBuffer)
        {
            Color color = color32;
            for (int i = 0; i < points.Length / 2; i++)
            {
                LineWithColor line = new LineWithColor(
                    points[2 * i + 0].Ros2Unity(), color * colors[2 * i + 0].ToUnityColor(),
                    points[2 * i + 1].Ros2Unity(), color * colors[2 * i + 1].ToUnityColor()
                );
                if (LineResource.IsElementValid(line))
                {
                    lineBuffer.Add(line.f);
                }
            }

            return color.a < 1 ? (bool?) true : null;
        }
    }
}