using UnityEngine;
using Iviz.Resources;
using System.Collections.Generic;
using System;

namespace Iviz.Displays
{
    public sealed class RadialScanResource : MonoBehaviour, IDisplay, IRecyclable
    {
        PointListResource pointCloud;
        LineResource lines;

        readonly List<PointWithColor> pointBuffer = new List<PointWithColor>();
        readonly List<LineWithColor> lineBuffer = new List<LineWithColor>();

        public string Name => "RadialScanResource";
        public Bounds Bounds => UseLines ? lines.Bounds : pointCloud.Bounds;
        public Bounds WorldBounds => UseLines ? lines.WorldBounds : pointCloud.WorldBounds;
        public Pose WorldPose => UseLines ? lines.WorldPose : pointCloud.WorldPose;
        public Vector3 WorldScale => UseLines ? lines.WorldScale : pointCloud.WorldScale;

        int layer;
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

        bool colliderEnabled;
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

        [SerializeField] int size;
        public int Size
        {
            get => size;
            private set => size = value;
        }

        public Vector2 MeasuredIntensityBounds { get; private set; }

        bool visible = true;
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

        [SerializeField] float pointSize;
        public float PointSize
        {
            get => pointSize;
            set
            {
                pointSize = value;
                pointCloud.ElementSize = value;
                lines.ElementSize = value;
            }
        }

        [SerializeField] Resource.ColormapId colormap;
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

        [SerializeField] bool useIntensityNotRange;
        public bool UseIntensityNotRange
        {
            get => useIntensityNotRange;
            set => useIntensityNotRange = value;
        }

        [SerializeField] bool forceMinMax;
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

        [SerializeField] bool flipMinMax_;
        public bool FlipMinMax
        {
            get => flipMinMax_;
            set
            {
                flipMinMax_ = value;
                pointCloud.FlipMinMax = value;
                lines.FlipMinMax = value;
            }
        }

        [SerializeField] float minIntensity_;
        public float MinIntensity
        {
            get => minIntensity_;
            set
            {
                minIntensity_ = value;
                if (ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        [SerializeField] float maxIntensity_;
        public float MaxIntensity
        {
            get => maxIntensity_;
            set
            {
                maxIntensity_ = value;
                if (ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        [SerializeField] bool useLines_;
        public bool UseLines
        {
            get => useLines_;
            set
            {
                useLines_ = value;
                if (useLines_)
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

        [SerializeField] float maxLineDistance;
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
            pointCloud = ResourcePool.GetOrCreate<PointListResource>(Resource.Displays.PointList, transform);
            lines = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, transform);

            pointCloud.UseColormap = true;
            lines.UseColormap = true;

            MinIntensity = 0;
            MaxIntensity = 1;
            UseLines = true;
            Colormap = Resource.ColormapId.hsv;
            PointSize = 0.01f;
            MaxLineDistance = 0.3f;
        }

        public void Set(float angleMin, float angleIncrement, float rangeMin, float rangeMax, float[] ranges, float[] intensities)
        {
            if (rangeMin > rangeMax)
            {
                throw new ArgumentException(nameof(rangeMin));
            }
            if (intensities.Length != 0 && intensities.Length != ranges.Length)
            {
                throw new ArgumentException(nameof(ranges));
            }

            pointBuffer.Clear();
            if (!UseIntensityNotRange)
            {
                for (int i = 0; i < ranges.Length; i++)
                {
                    float range = ranges[i];
                    if (float.IsNaN(range) || range > rangeMax || range < rangeMin)
                    {
                        continue;
                    }

                    float a = angleMin + angleIncrement * i;
                    float x = Mathf.Cos(a); 
                    float y = Mathf.Sin(a); 
                    pointBuffer.Add(new PointWithColor(new Unity.Mathematics.float4(-y, 0, x, 1) * range));
                    //x = dx * x - dy * y;
                    //y = dy * x + dx * y;
                }
            }
            else
            {
                for (int i = 0; i < ranges.Length; i++)
                {
                    float range = ranges[i];
                    if (float.IsNaN(range) || range > rangeMax || range < rangeMin)
                    {
                        continue;
                    }
                    
                    float a = angleMin + angleIncrement * i;
                    float x = Mathf.Cos(a); 
                    float y = Mathf.Sin(a); 
                    pointBuffer.Add(new PointWithColor(-y * range, 0, x * range, intensities[i]));
                    //x = dx * x - dy * y;
                    //y = dy * x + dx * y;
                }
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
                if ((pB.Position - pA.Position).sqrMagnitude < maxLineDistanceSq)
                {
                    lineBuffer.Add(new LineWithColor(pA, pB));
                }
                else
                {
                    lineBuffer.Add(new LineWithColor(pA, pA));
                }
            }
            lines.LinesWithColor = lineBuffer;
            MeasuredIntensityBounds = lines.IntensityBounds;
            if (ForceMinMax)
            {
                lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
            }
        }

        public void Suspend()
        {
            pointCloud.Suspend();
            lines.Suspend();
        }

        public void SplitForRecycle()
        {
            ResourcePool.Dispose(Resource.Displays.PointList, pointCloud.gameObject);
            ResourcePool.Dispose(Resource.Displays.Line, lines.gameObject);
        }
    }
}
