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
    internal class MarkerLineHelper
    {
        Point[] points = Array.Empty<Point>();
        ColorRGBA[] colors = Array.Empty<ColorRGBA>();
        Color32 color32;

        readonly Func<NativeList<float4x2>, bool?> lineStripSingleColor;
        readonly Func<NativeList<float4x2>, bool?> lineStripNoTint;
        readonly Func<NativeList<float4x2>, bool?> lineStripTintColor;
        readonly Func<NativeList<float4x2>, bool?> lineListSingleColor;
        readonly Func<NativeList<float4x2>, bool?> lineListNoTint;
        readonly Func<NativeList<float4x2>, bool?> lineListTintColor;

        public MarkerLineHelper()
        {
            lineStripSingleColor = ProcessLineStripSingleColor;
            lineStripNoTint = ProcessLineStripNoTint;
            lineStripTintColor = ProcessLineStripTintColor;
            lineListSingleColor = ProcessLineListSingleColor;
            lineListNoTint = ProcessLineListNoTint;
            lineListTintColor = ProcessLineListTintColor;
        }

        public Func<NativeList<float4x2>, bool?> GetLineSetterForStrip(Marker msg)
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
            var lPoints = points;

            if (lPoints.Length == 0)
            {
                return color32.a < 255;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float colorAsFloat = UnityUtils.AsFloat(color32);

            lPoints[0].Ros2Unity(colorAsFloat, out c1);

            lineBuffer.EnsureCapacity(lPoints.Length - 1);
            lineBuffer.Clear();

            for (int i = 1; i < lPoints.Length; i++)
            {
                c0 = c1;
                lPoints[i].Ros2Unity(colorAsFloat, out c1);
                if (LineResource.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color32.a < 255;
        }

        bool? ProcessLineStripNoTint(NativeList<float4x2> lineBuffer)
        {
            var lPoints = points;
            var lColors = colors;

            if (lPoints.Length == 0 || lPoints.Length != lColors.Length)
            {
                return null;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float w0 = UnityUtils.AsFloat(lColors[0].ToUnityColor32());
            lPoints[0].Ros2Unity(w0, out c1);

            lineBuffer.EnsureCapacity(lPoints.Length - 1);
            lineBuffer.Clear();

            for (int i = 1; i < lPoints.Length; i++)
            {
                c0 = c1;

                float w = UnityUtils.AsFloat(lColors[i].ToUnityColor32());
                lPoints[i].Ros2Unity(w, out c1);

                if (LineResource.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return null;
        }

        bool? ProcessLineStripTintColor(NativeList<float4x2> lineBuffer)
        {
            var lPoints = points;
            var lColors = colors;
            Color color = color32;

            if (lPoints.Length == 0)
            {
                return color.a < 1 ? true : null;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float w0 = UnityUtils.AsFloat(color * lColors[0].ToUnityColor());
            lPoints[0].Ros2Unity(w0, out c1);

            lineBuffer.EnsureCapacity(lPoints.Length - 1);
            lineBuffer.Clear();

            for (int i = 1; i < lPoints.Length; i++)
            {
                c0 = c1;

                float w = UnityUtils.AsFloat(color * lColors[i].ToUnityColor());
                lPoints[i].Ros2Unity(w, out c1);

                if (LineResource.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color.a < 1 ? true : null;
        }

        public Func<NativeList<float4x2>, bool?> GetLineSetterForList(Marker msg)
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
            var lPoints = points;

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float colorAsFloat = UnityUtils.AsFloat(color32);
            c0.w = colorAsFloat;
            c1.w = colorAsFloat;

            lineBuffer.EnsureCapacity(lPoints.Length / 2);
            lineBuffer.Clear();

            for (int i = 0; i < lPoints.Length; i += 2)
            {
                lPoints[i + 0].Ros2Unity(colorAsFloat, out c0);
                lPoints[i + 1].Ros2Unity(colorAsFloat, out c1);
                if (LineResource.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color32.a < 255;
        }

        bool? ProcessLineListNoTint(NativeList<float4x2> lineBuffer)
        {
            var lPoints = points;
            var lColors = colors;

            var f = new float4x2();
            ref var c0 = ref f.c0;
            ref var c1 = ref f.c1;

            lineBuffer.EnsureCapacity(lPoints.Length / 2);
            lineBuffer.Clear();

            for (int i = 0; i < lPoints.Length; i += 2)
            {
                float w0 = UnityUtils.AsFloat(lColors[i + 0].ToUnityColor32());
                lPoints[i + 0].Ros2Unity(w0, out c0);
                float w1 = UnityUtils.AsFloat(lColors[i + 1].ToUnityColor32());
                lPoints[i + 1].Ros2Unity(w1, out c1);

                if (LineResource.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return null;
        }

        bool? ProcessLineListTintColor(NativeList<float4x2> lineBuffer)
        {
            var lPoints = points;
            var lColors = colors;
            Color color = color32;

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            lineBuffer.EnsureCapacity(lPoints.Length / 2);
            lineBuffer.Clear();

            for (int i = 0; i < lPoints.Length; i += 2)
            {
                float w0 = UnityUtils.AsFloat(color * lColors[i + 0].ToUnityColor());
                lPoints[i + 0].Ros2Unity(w0, out c0);
                float w1 = UnityUtils.AsFloat(color * lColors[i + 1].ToUnityColor());
                lPoints[i + 1].Ros2Unity(w1, out c1);

                if (LineResource.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color.a < 1 ? true : null;
        }
    }
}