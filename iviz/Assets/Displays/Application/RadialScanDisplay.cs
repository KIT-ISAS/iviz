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

        readonly List<float4x2> lineBuffer = new(32);
        readonly List<float4> pointBuffer = new(32);
        Vector2[] cache = Array.Empty<Vector2>();

        LineDisplay? lines;
        PointListDisplay? pointCloud;
        (float angleMin, float angleIncrement, int length) cacheProperties;
        bool visible = true;
        int layer;

        LineDisplay Lines => ResourcePool.RentChecked(ref lines, transform);
        PointListDisplay PointCloud => ResourcePool.RentChecked(ref pointCloud, transform);

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
                }
                else
                {
                    PointCloud.Visible = Visible;
                    Lines.Visible = !Visible;
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
            lineBuffer.Clear();

            bool useIntensity = UseIntensityNotRange && intensities.Length != 0;
            var mCache = cache;

            if (!UseLines)
            {
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
                PointCloud.Set(pointBuffer.AsReadOnlySpan());
            }
            else
            {
                float4? prevF = null;
                float prevRange = 0; 

                foreach (int i in ..ranges.Length)
                {
                    float range = ranges[i];
                    if (range.IsInvalid() || range > rangeMax || range < rangeMin)
                    {
                        prevF = null;
                        continue;
                    }

                    var (x, z) = mCache[i];

                    float4 f1;
                    f1.x = x * range;
                    f1.y = 0;
                    f1.z = z * range;
                    f1.w = useIntensity ? intensities[i] : range;

                    if (prevF is { } f0)
                    {
                        float min = Mathf.Min(range, prevRange);
                        float max = Mathf.Max(range, prevRange);
                        const float k = 1.5f;
                        if (max < k * min)
                        {
                            float4x2 f;
                            f.c0 = f0;
                            f.c1 = f1;
                            lineBuffer.Add(f);
                        }
                    }

                    prevF = f1;
                    prevRange = range;
                }

                Size = lineBuffer.Count;
                Lines.Set(lineBuffer.AsReadOnlySpan(), false);
            }
        }
    }
}