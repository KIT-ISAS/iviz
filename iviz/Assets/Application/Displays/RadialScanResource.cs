#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Visualization of a radial/laser scan.
    /// </summary>
    public sealed class RadialScanResource : MonoBehaviour, IDisplay, IRecyclable
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
        readonly List<PointWithColor> pointBuffer = new();
        readonly List<Vector2> cache = new();

        LineResource? lines;
        PointListResource? pointCloud;
        (float angleMin, float angleIncrement, int length) cacheProperties;
        bool colliderEnabled;
        bool visible = true;
        int layer;
        
        LineResource Lines => lines != null
            ? lines
            : (lines = ResourcePool.RentDisplay<LineResource>(transform));

        PointListResource PointCloud => pointCloud != null
            ? pointCloud
            : (pointCloud = ResourcePool.RentDisplay<PointListResource>(transform));

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
        
        public Bounds? Bounds => UseLines ? Lines.Bounds : PointCloud.Bounds;

        public int Layer
        {
            set
            {
                layer = value;
                PointCloud.Layer = layer;
                Lines.Layer = layer;
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
        }

        public void SplitForRecycle()
        {
            PointCloud.ReturnToPool();
            Lines.ReturnToPool();
        }

        public void Set(float angleMin, float angleIncrement, float rangeMin, float rangeMax,
            float[] ranges, float[] intensities)
        {
            if (ranges == null)
            {
                throw new ArgumentNullException(nameof(ranges));
            }

            if (intensities == null)
            {
                throw new ArgumentNullException(nameof(intensities));
            }

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
                cache.Clear();
                cacheProperties = (angleMin, angleIncrement, intensities.Length);
                for (int i = 0; i < ranges.Length; i++)
                {
                    float a = angleMin + angleIncrement * i;
                    var rosPos = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0);
                    var (x, _, z) = rosPos.Ros2Unity();
                    cache.Add(new Vector2(x, z));
                }
            }

            pointBuffer.Clear();

            bool useIntensity = UseIntensityNotRange && intensities.Length != 0;
            for (int i = 0; i < ranges.Length; i++)
            {
                float range = ranges[i];
                if (float.IsNaN(range) || range > rangeMax || range < rangeMin)
                {
                    continue;
                }

                var (x, z) = cache[i];
                pointBuffer.Add(new PointWithColor(
                    x * range, 0, z * range,
                    useIntensity ? intensities[i] : range));
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
            int numPoints = points.Length;
            lineBuffer.Clear();

            var line = new LineWithColor();
            ref var pA = ref line.f.c0;
            ref var pB = ref line.f.c1;
            
            for (int i = 0; i < points.Length - 1; i++)
            {
                pA = points[i].f;
                pB = points[i + 1].f;
                if ((pB - pA).MaxAbsCoeff3() < maxLineDistance)
                {
                    lineBuffer.Add(line);
                }
            }

            if (numPoints != 0)
            {
                pA = points[^1].f;
                pB = points[0].f;
                if ((pB - pA).MaxAbsCoeff3() < maxLineDistance)
                {
                    lineBuffer.Add(line);
                }
            }

            Lines.Set(lineBuffer.AsReadOnlySpan(), false);
        }
    }
}