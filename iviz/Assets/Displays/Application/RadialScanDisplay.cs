#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Core;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Visualization of a radial/laser scan.
    /// </summary>
    public sealed class RadialScanDisplay : MonoBehaviour, IDisplay, IRecyclable
    {
        [SerializeField] int size;
        [SerializeField] float pointSize;
        [SerializeField] ColormapId colormap;
        [SerializeField] bool useIntensityNotRange;
        [SerializeField] bool overrideMinMax;
        [SerializeField] bool flipMinMax;
        [SerializeField] float minIntensity;
        [SerializeField] float maxIntensity;
        [SerializeField] bool useLines;
        [SerializeField] float maxLineDistance;

        readonly List<LineWithColor> lineBuffer = new();
        readonly List<float4> pointBuffer = new();
        Vector2[] cache = Array.Empty<Vector2>();

        LineDisplay? lines;
        PointListDisplay? pointCloud;
        (float angleMin, float angleIncrement, int length) cacheProperties;
        bool visible = true;
        int layer;

        LineDisplay Lines => lines != null
            ? lines
            : (lines = ResourcePool.RentDisplay<LineDisplay>(transform));

        PointListDisplay PointCloud => pointCloud != null
            ? pointCloud
            : (pointCloud = ResourcePool.RentDisplay<PointListDisplay>(transform));

        public int Size
        {
            get => size;
            private set => size = value;
        }

        public Vector2 MeasuredIntensityBounds => useLines
            ? Lines.MeasuredIntensityBounds
            : PointCloud.MeasuredIntensityBounds;

        public float PointSize
        {
            get => pointSize;
            set
            {
                pointSize = value;
                PointCloud.ElementScale = value;
                Lines.ElementScale = value;
            }
        }

        public ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;
                PointCloud.Colormap = value;
                Lines.Colormap = value;
            }
        }

        public bool UseIntensityNotRange
        {
            get => useIntensityNotRange;
            set => useIntensityNotRange = value;
        }

        public bool OverrideMinMax
        {
            get => overrideMinMax;
            set
            {
                overrideMinMax = value;
                PointCloud.OverrideIntensityBounds = value;
                Lines.OverrideIntensityBounds = value;
                if (value)
                {
                    PointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    Lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
                else
                {
                    PointCloud.IntensityBounds = MeasuredIntensityBounds;
                    Lines.IntensityBounds = MeasuredIntensityBounds;
                }
            }
        }

        public bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
                PointCloud.FlipMinMax = value;
                Lines.FlipMinMax = value;
            }
        }

        public float MinIntensity
        {
            get => minIntensity;
            set
            {
                minIntensity = value;
                if (OverrideMinMax)
                {
                    PointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    Lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public float MaxIntensity
        {
            get => maxIntensity;
            set
            {
                maxIntensity = value;
                if (OverrideMinMax)
                {
                    PointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    Lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public bool UseLines
        {
            get => useLines;
            set
            {
                useLines = value;
                if (useLines)
                {
                    Lines.Visible = Visible;
                    PointCloud.Visible = !Visible;
                    SetLines();
                }
                else
                {
                    PointCloud.Visible = Visible;
                    Lines.Visible = !Visible;
                    SetPoints();
                }
            }
        }

        public float MaxLineDistance
        {
            get => maxLineDistance;
            set
            {
                bool changed = !Mathf.Approximately(value, maxLineDistance);
                maxLineDistance = value;
                if (changed)
                {
                    SetLines();
                }
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                PointCloud.Visible = value && !UseLines;
                Lines.Visible = value && UseLines;
            }
        }

        void Awake()
        {
            PointCloud.UseColormap = true;
            Lines.UseColormap = true;

            MinIntensity = 0;
            MaxIntensity = 1;
            UseLines = true;
            Colormap = ColormapId.hsv;
            PointSize = 0.01f;
            MaxLineDistance = 0.3f;
        }

        void IDisplay.Suspend()
        {
            PointCloud.Suspend();
            Lines.Suspend();
            cache = Array.Empty<Vector2>();
            cacheProperties = default;
        }

        public void SplitForRecycle()
        {
            PointCloud.ReturnToPool();
            Lines.ReturnToPool();
        }

        public void Set(float angleMin, float angleIncrement, float rangeMin, float rangeMax,
            ReadOnlySpan<float> ranges, ReadOnlySpan<float> intensities)
        {
            if (float.IsNaN(rangeMin) || rangeMin > rangeMax)
            {
                throw new ArgumentException("rangeMin is nan or invalid!", nameof(rangeMin));
            }

            if (float.IsNaN(rangeMax))
            {
                throw new ArgumentException("rangeMax is nan!", nameof(rangeMax));
            }

            if (intensities.Length != 0 && intensities.Length != ranges.Length)
            {
                throw new ArgumentException("Invalid intensity size", nameof(ranges));
            }

            if (!Mathf.Approximately(angleMin, cacheProperties.angleMin) ||
                !Mathf.Approximately(angleIncrement, cacheProperties.angleIncrement) ||
                intensities.Length != cacheProperties.length)
            {
                if (cache.Length < ranges.Length)
                {
                    cache = new Vector2[Math.Max(2 * cache.Length, ranges.Length)];
                }

                cacheProperties = (angleMin, angleIncrement, intensities.Length);

                foreach (int i in ..ranges.Length)
                {
                    float a = angleMin + angleIncrement * i;
                    //(xz.x, _, xz.y) = ( Mathf.Cos(a),  Mathf.Sin(a), 0 ).Ros2Unity();

                    Vector2 xz;
                    xz.x = -Mathf.Sin(a);
                    xz.y = Mathf.Cos(a);
                    cache[i] = xz;
                }
            }

            pointBuffer.Clear();
            bool useIntensity = UseIntensityNotRange && intensities.Length != 0;
            var mCache = cache;
            foreach (int i in ..ranges.Length)
            {
                float range = ranges[i];
                if (range.IsInvalid() || range > rangeMax || range < rangeMin)
                {
                    continue;
                }

                var (x, z) = mCache[i];

                float4 f;
                f.x = x * range;
                f.y = 0;
                f.z = z * range;
                f.w = useIntensity ? intensities[i] : range;
                pointBuffer.Add(f);
            }

            Size = pointBuffer.Count;

            if (!UseLines)
            {
                SetPoints();
            }
            else
            {
                SetLines();
            }
        }

        void SetPoints()
        {
            PointCloud.Set(pointBuffer.AsReadOnlySpan());
        }

        void SetLines()
        {
            var points = pointBuffer.AsReadOnlySpan();
            lineBuffer.Clear();

            var line = new LineWithColor();
            ref var pA = ref line.f.c0;
            ref var pB = ref line.f.c1;

            for (int i = 0; i < points.Length - 1; i++)
            {
                pA = points[i];
                pB = points[i + 1];
                if ((pB - pA).MaxAbsCoeff3() < maxLineDistance)
                {
                    lineBuffer.Add(line);
                }
            }

            if (points.Length != 0)
            {
                pA = points[^1];
                pB = points[0];
                if ((pB - pA).MaxAbsCoeff3() < maxLineDistance)
                {
                    lineBuffer.Add(line);
                }
            }

            Lines.Set(lineBuffer.AsReadOnlySpan(), false);
        }
    }
}