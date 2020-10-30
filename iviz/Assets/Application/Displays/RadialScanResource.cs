using System;
using System.Collections.Generic;
using Iviz.Resources;
using JetBrains.Annotations;
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
        [SerializeField] Resource.ColormapId colormap;
        [SerializeField] bool useIntensityNotRange;
        [SerializeField] bool forceMinMax;
        [SerializeField] bool flipMinMax;
        [SerializeField] float minIntensity;
        [SerializeField] float maxIntensity;
        [SerializeField] bool useLines;
        [SerializeField] float maxLineDistance;

        readonly List<LineWithColor> lineBuffer = new List<LineWithColor>();
        readonly List<PointWithColor> pointBuffer = new List<PointWithColor>();

        (float angleMin, float angleIncrement, int length) cacheProperties;
        readonly List<Vector2> cache = new List<Vector2>();

        LineResource lines;
        PointListResource pointCloud;
        bool colliderEnabled;
        bool visible = true;
        int layer;

        public int Size
        {
            get => size;
            private set => size = value;
        }

        public Vector2 MeasuredIntensityBounds { get; private set; }

        public float PointSize
        {
            get => pointSize;
            set
            {
                pointSize = value;
                pointCloud.ElementScale = value;
                lines.ElementScale = value;
            }
        }

        public Resource.ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;
                pointCloud.Colormap = value;
                lines.Colormap = value;
            }
        }

        public bool UseIntensityNotRange
        {
            get => useIntensityNotRange;
            set => useIntensityNotRange = value;
        }

        public bool ForceMinMax
        {
            get => forceMinMax;
            set
            {
                forceMinMax = value;
                if (value)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
                else
                {
                    pointCloud.IntensityBounds = MeasuredIntensityBounds;
                    lines.IntensityBounds = MeasuredIntensityBounds;
                }
            }
        }

        public bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
                pointCloud.FlipMinMax = value;
                lines.FlipMinMax = value;
            }
        }

        public float MinIntensity
        {
            get => minIntensity;
            set
            {
                minIntensity = value;
                if (ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public float MaxIntensity
        {
            get => maxIntensity;
            set
            {
                maxIntensity = value;
                if (ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
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
                    lines.Visible = Visible;
                    pointCloud.Visible = !Visible;
                    SetLines();
                }
                else
                {
                    pointCloud.Visible = Visible;
                    lines.Visible = !Visible;
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

        void Awake()
        {
            pointCloud = ResourcePool.GetOrCreateDisplay<PointListResource>(transform);
            lines = ResourcePool.GetOrCreateDisplay<LineResource>(transform);

            pointCloud.UseColormap = true;
            lines.UseColormap = true;

            MinIntensity = 0;
            MaxIntensity = 1;
            UseLines = true;
            Colormap = Resource.ColormapId.hsv;
            PointSize = 0.01f;
            MaxLineDistance = 0.3f;
        }

        public string Name => "RadialScanResource";
        public Bounds Bounds => UseLines ? lines.Bounds : pointCloud.Bounds;
        public Bounds WorldBounds => UseLines ? lines.WorldBounds : pointCloud.WorldBounds;
        public Pose WorldPose => UseLines ? lines.WorldPose : pointCloud.WorldPose;
        public Vector3 WorldScale => UseLines ? lines.WorldScale : pointCloud.WorldScale;

        public int Layer
        {
            get => layer;
            set
            {
                layer = value;
                pointCloud.Layer = layer;
                lines.Layer = layer;
            }
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        public bool ColliderEnabled
        {
            get => colliderEnabled;
            set
            {
                colliderEnabled = value;
                lines.ColliderEnabled = value;
                pointCloud.ColliderEnabled = value;
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                pointCloud.Visible = value && !UseLines;
                lines.Visible = value && UseLines;
            }
        }

        public void Suspend()
        {
            pointCloud.Suspend();
            lines.Suspend();
        }

        public void SplitForRecycle()
        {
            ResourcePool.DisposeDisplay(pointCloud);
            ResourcePool.DisposeDisplay(lines);
        }

        public void Set(float angleMin, float angleIncrement, float rangeMin, float rangeMax, [NotNull] float[] ranges,
            [NotNull] float[] intensities)
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
                    Vector3 rosPos = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0);
                    Vector3 unityPos = rosPos.Ros2Unity();
                    cache.Add(new Vector2(unityPos.x, unityPos.z));
                }
            }

            pointBuffer.Clear();
            bool tmpUseIntensityNotRange = UseIntensityNotRange;
            for (int i = 0; i < ranges.Length; i++)
            {
                float range = ranges[i];
                if (float.IsNaN(range) || range > rangeMax || range < rangeMin)
                {
                    continue;
                }

                float x = cache[i].x;
                float z = cache[i].y;
                pointBuffer.Add(new PointWithColor(x * range, 0, z * range,
                    tmpUseIntensityNotRange ? intensities[i] : range));
            }

            Size = pointBuffer.Count;

            GameThread.PostImmediate(() =>
            {
                if (!UseLines)
                {
                    SetPoints();
                }
                else
                {
                    SetLines();
                }
            });
        }

        void SetPoints()
        {
            pointCloud.PointsWithColor = pointBuffer;
            MeasuredIntensityBounds = pointCloud.IntensityBounds;
            if (ForceMinMax)
            {
                pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
            }
        }

        void SetLines()
        {
            int n = pointBuffer.Count;
            float maxLineDistanceSq = maxLineDistance * maxLineDistance;

            lineBuffer.Clear();
            for (int i = 0; i < pointBuffer.Count; i++)
            {
                PointWithColor pA = pointBuffer[i];
                PointWithColor pB = pointBuffer[(i + 1) % n];
                if ((pB.Position - pA.Position).MagnitudeSq() < maxLineDistanceSq)
                {
                    lineBuffer.Add(new LineWithColor(pA, pB));
                }
            }

            lines.LinesWithColor = lineBuffer;
            MeasuredIntensityBounds = lines.IntensityBounds;
            if (ForceMinMax)
            {
                lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
            }
        }
    }
}