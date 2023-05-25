#nullable enable

using System;
using System.Buffers;
using System.Runtime.InteropServices;
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
            int mPointsLength = mPoints.Length;
            
            if (mPointsLength == 0)
            {
                return color32.a < 255;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float colorAsFloat = UnityUtils.AsFloat(color32);

            lineBuffer.EnsureCapacity(mPointsLength - 1);
            lineBuffer.Clear();

            mPoints[0].Ros2Unity(colorAsFloat, out c1);

            for (int i = 1; i < mPointsLength; i++)
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
            int mPointsLength = mPoints.Length;
            
            if (mPointsLength == 0 || mPointsLength != mColors.Length)
            {
                return null;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float w0 = UnityUtils.AsFloat(mColors[0].ToUnityColor32());
            mPoints[0].Ros2Unity(w0, out c1);

            lineBuffer.EnsureCapacity(mPointsLength - 1);
            lineBuffer.Clear();

            for (int i = 1; i < mPointsLength; i++)
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
            int mPointsLength = mPoints.Length;
            
            if (mPointsLength == 0)
            {
                return color.a < 1 ? true : null;
            }

            var f = new float4x2();
            ref float4 c0 = ref f.c0;
            ref float4 c1 = ref f.c1;

            float w0 = UnityUtils.AsFloat(color * mColors[0].ToUnity());
            mPoints[0].Ros2Unity(w0, out c1);

            lineBuffer.EnsureCapacity(mPointsLength - 1);
            lineBuffer.Clear();

            for (int i = 1; i < mPointsLength; i++)
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
            var lines = MemoryMarshal.Cast<Point, double3x2>(points);

            float colorAsFloat = UnityUtils.AsFloat(color32);

            lineBuffer.EnsureCapacity(lines.Length);
            lineBuffer.Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                float4x2 f;
                lines[i].c0.Ros2Unity(colorAsFloat, out f.c0);
                lines[i].c1.Ros2Unity(colorAsFloat, out f.c1);
                if (LineDisplay.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color32.a < 255;
        }

        bool? ProcessLineListNoTint(NativeList<float4x2> lineBuffer)
        {
            var lines = MemoryMarshal.Cast<Point, double3x2>(points);
            var colors2 = MemoryMarshal.Cast<ColorRGBA, ColorRGBA2>(colors);

            lineBuffer.EnsureCapacity(colors2.Length);
            lineBuffer.Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                float4x2 f;
                float w0 = UnityUtils.AsFloat(colors2[i].c0.ToUnityColor32());
                lines[i].c0.Ros2Unity(w0, out f.c0);
                float w1 = UnityUtils.AsFloat(colors2[i].c1.ToUnityColor32());
                lines[i].c1.Ros2Unity(w1, out f.c1);

                if (LineDisplay.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return null;
        }

        bool? ProcessLineListTintColor(NativeList<float4x2> lineBuffer)
        {
            var lines = MemoryMarshal.Cast<Point, double3x2>(points);
            var colors2 = MemoryMarshal.Cast<ColorRGBA, ColorRGBA2>(colors);
            Color color = color32;

            lineBuffer.EnsureCapacity(points.Length);
            lineBuffer.Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                float4x2 f;

                Color mixedColor0 = color * colors2[i].c0.ToUnity();
                float w0 = UnityUtils.AsFloat(mixedColor0);
                lines[i].c0.Ros2Unity(w0, out f.c0);

                Color mixedColor1 = color * colors2[i].c1.ToUnity();
                float w1 = UnityUtils.AsFloat(mixedColor1);
                lines[i].c1.Ros2Unity(w1, out f.c1);

                if (LineDisplay.IsElementValid(f))
                {
                    lineBuffer.AddUnsafe(f);
                }
            }

            return color.a < 1 ? true : null;
        }

        struct ColorRGBA2
        {
            public ColorRGBA c0;
            public ColorRGBA c1;
        }
    }
}