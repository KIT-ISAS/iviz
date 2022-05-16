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
            var mPoints = points;

            if (mPoints.Length == 0)
            {
                return color32.a < 255;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float colorAsFloat = UnityUtils.AsFloat(color32);
            
            lineBuffer.EnsureCapacity(mPoints.Length - 1);
            lineBuffer.Clear();

            mPoints[0].Ros2Unity(colorAsFloat, out c1);
            
            for (int i = 1; i < mPoints.Length; i++)
            {
                c0 = c1;
                mPoints[i].Ros2Unity(colorAsFloat, out c1);
                if (LineDisplay.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color32.a < 255;
        }

        bool? ProcessLineStripNoTint(NativeList<float4x2> lineBuffer)
        {
            var mPoints = points;
            var mColors = colors;

            if (mPoints.Length == 0 || mPoints.Length != mColors.Length)
            {
                return null;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float w0 = UnityUtils.AsFloat(mColors[0].ToUnityColor32());
            mPoints[0].Ros2Unity(w0, out c1);

            lineBuffer.EnsureCapacity(mPoints.Length - 1);
            lineBuffer.Clear();

            for (int i = 1; i < mPoints.Length; i++)
            {
                c0 = c1;

                float w = UnityUtils.AsFloat(mColors[i].ToUnityColor32());
                mPoints[i].Ros2Unity(w, out c1);

                if (LineDisplay.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return null;
        }

        bool? ProcessLineStripTintColor(NativeList<float4x2> lineBuffer)
        {
            var mPoints = points;
            var mColors = colors;
            Color color = color32;

            if (mPoints.Length == 0)
            {
                return color.a < 1 ? true : null;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float w0 = UnityUtils.AsFloat(color * mColors[0].ToUnity());
            mPoints[0].Ros2Unity(w0, out c1);

            lineBuffer.EnsureCapacity(mPoints.Length - 1);
            lineBuffer.Clear();

            for (int i = 1; i < mPoints.Length; i++)
            {
                c0 = c1;

                float w = UnityUtils.AsFloat(color * mColors[i].ToUnity());
                mPoints[i].Ros2Unity(w, out c1);

                if (LineDisplay.IsElementValid(f))
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
            var mPoints = points;

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float colorAsFloat = UnityUtils.AsFloat(color32);
            c0.w = colorAsFloat;
            c1.w = colorAsFloat;

            lineBuffer.EnsureCapacity(mPoints.Length / 2);
            lineBuffer.Clear();

            for (int i = 0; i < mPoints.Length; i += 2)
            {
                mPoints[i + 0].Ros2Unity(colorAsFloat, out c0);
                mPoints[i + 1].Ros2Unity(colorAsFloat, out c1);
                if (LineDisplay.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color32.a < 255;
        }

        bool? ProcessLineListNoTint(NativeList<float4x2> lineBuffer)
        {
            var mPoints = points;
            var mColors = colors;

            var f = new float4x2();
            ref var c0 = ref f.c0;
            ref var c1 = ref f.c1;

            lineBuffer.EnsureCapacity(mPoints.Length / 2);
            lineBuffer.Clear();

            for (int i = 0; i < mPoints.Length; i += 2)
            {
                float w0 = UnityUtils.AsFloat(mColors[i + 0].ToUnityColor32());
                mPoints[i + 0].Ros2Unity(w0, out c0);
                float w1 = UnityUtils.AsFloat(mColors[i + 1].ToUnityColor32());
                mPoints[i + 1].Ros2Unity(w1, out c1);

                if (LineDisplay.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return null;
        }

        bool? ProcessLineListTintColor(NativeList<float4x2> lineBuffer)
        {
            var mPoints = points;
            var mColors = colors;
            Color color = color32;

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            lineBuffer.EnsureCapacity(mPoints.Length / 2);
            lineBuffer.Clear();

            for (int i = 0; i < mPoints.Length; i += 2)
            {
                float w0 = UnityUtils.AsFloat(color * mColors[i + 0].ToUnity());
                mPoints[i + 0].Ros2Unity(w0, out c0);
                float w1 = UnityUtils.AsFloat(color * mColors[i + 1].ToUnity());
                mPoints[i + 1].Ros2Unity(w1, out c1);

                if (LineDisplay.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color.a < 1 ? true : null;
        }
    }
}