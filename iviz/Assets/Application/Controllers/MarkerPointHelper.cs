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
        UniqueRef<Point> points;
        UniqueRef<ColorRGBA> colors;
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

        void ProcessSingleColor(ref NativeList<float4> pointBuffer)
        {
            float colorAsFloat = PointWithColor.FloatFromColorBits(color);
            foreach (var rosPoint in points)
            {
                PointWithColor point = new PointWithColor(rosPoint.Ros2Unity(), colorAsFloat);
                if (PointListResource.IsElementValid(point))
                {
                    pointBuffer.Add(point);
                }
            }
        }

        void ProcessNoTint(ref NativeList<float4> pointBuffer)
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
                    pointBuffer.Add(point);
                }
            }
        }

        void ProcessTintColor(ref NativeList<float4> pointBuffer)
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
                    pointBuffer.Add(point);
                }
            }
        }
    }

    /*
    [BurstCompile]
    struct MyJob : IJobParallelFor, IDisposable
    {
        [ReadOnly] NativeArray<float3> points;
        [ReadOnly] NativeArray<float4> colors;
        [ReadOnly] readonly float4 mRgba;
        [WriteOnly] NativeList<float4> pointBuffer;

        public MyJob(Point[] points, ColorRGBA[] colors, Color color, NativeList<float4> pointBuffer)
        {
            this.points = new NativeArray<float3>(points.Length, Allocator.Temp);
            this.points.Reinterpret<Point>().CopyFrom(points);
            this.colors = new NativeArray<float4>(colors.Length, Allocator.Temp);
            this.colors.Reinterpret<ColorRGBA>().CopyFrom(colors);
            mRgba = new float4(color.r, color.g, color.b, color.a);
            this.pointBuffer = pointBuffer;
        }

        public void Dispose()
        {
            points.Dispose();
            colors.Dispose();
        }

        public void Execute(int i)
        {
                float4 rgba = colors[i];
                int4 rgbaAsInt = math.min(new int4(rgba * mRgba * 255), 255);
                int4 rgbaTmp = new int4(rgbaAsInt.x << 24, rgbaAsInt.y << 16, rgbaAsInt.z << 8, rgbaAsInt.w) & 0xff;
                int rgba32 = rgbaTmp.x + rgbaTmp.y + rgbaTmp.z + rgbaTmp.w;
                float rgbaAsFloat;
                unsafe
                {
                    rgbaAsFloat = *(float*) &rgba32;
                }

                float4 point = new float4(points[i].Ros2Unity(), rgbaAsFloat);
                if (!(float.IsNaN(point.x) || float.IsNaN(point.y) || float.IsNaN(point.z)))
                {
                    pointBuffer.Add(point);
                }
        }
    }
    */
}