using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using RosSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class LaserScanConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.LaserScan;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public float PointSize { get; set; } = 0.03f;
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.hsv;
        [DataMember] public bool UseIntensity { get; set; } = false;
        [DataMember] public bool UseLines { get; set; } = false;

        [DataMember] public bool ForceMinMax { get; set; } = false;
        [DataMember] public float MinIntensity { get; set; } = 0;
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; } = false;
    }

    public class LaserScanListener : TopicListener
    {
        const float MaxDistance = 0.3f;
        PointListResource pointCloud;
        LineResource lines;
        DisplayNode node;

        public override DisplayData DisplayData { get; set; }

        public Vector2 LastIntensityBounds { get; private set; }

        public int Size { get; private set; }
        public bool CalculateMinMax { get; private set; } = true;

        readonly LaserScanConfiguration config = new LaserScanConfiguration();
        public LaserScanConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                PointSize = value.PointSize;
                Colormap = value.Colormap;
                UseIntensity = value.UseIntensity;
                ForceMinMax = value.ForceMinMax;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
                UseLines = value.UseLines;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                pointCloud.Visible = value;
            }
        }

        public float PointSize
        {
            get => config.PointSize;
            set
            {
                config.PointSize = value;
                pointCloud.Scale = value * Vector2.one;
                lines.Scale = value;
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                pointCloud.Colormap = value;
                lines.Colormap = value;
            }
        }

        public bool UseIntensity
        {
            get => config.UseIntensity;
            set
            {
                config.UseIntensity = value;
            }
        }

        public bool ForceMinMax
        {
            get => config.ForceMinMax;
            set
            {
                config.ForceMinMax = value;
                if (config.ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                pointCloud.FlipMinMax = value;
                lines.FlipMinMax = value;
            }
        }


        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                if (config.ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                if (config.ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public bool UseLines
        {
            get => config.UseLines;
            set
            {
                config.UseLines = value;
                lines.Visible = value;
                pointCloud.Visible = !value;
            }
        }

        readonly List<PointWithColor> pointBuffer = new List<PointWithColor>();
        readonly List<LineWithColor> lineBuffer = new List<LineWithColor>();

        void Awake()
        {
            transform.parent = TFListener.ListenersFrame.transform;

            node = SimpleDisplayNode.Instantiate("LaserScanNode", transform);
            pointCloud = ResourcePool.GetOrCreate<PointListResource>(Resource.Markers.PointList, node.transform);
            lines = ResourcePool.GetOrCreate<LineResource>(Resource.Markers.Line, node.transform);
            Config = new LaserScanConfiguration();

            pointCloud.UseIntensityTexture = true;
            lines.UseIntensityTexture = true;
        }

        public override void StartListening()
        {
            base.StartListening();
            Listener = new RosListener<LaserScan>(config.Topic, Handler);
            name = "[" + config.Topic + "]";
            node.name = "[" + config.Topic + "]";
        }

        void Handler(LaserScan msg)
        {
            node.SetParent(msg.Header.FrameId);

            if (msg.AngleMin >= msg.AngleMax || msg.RangeMin >= msg.RangeMax)
            {
                Logger.Info("LaserScanListener: Invalid angle or range dimensions!");
                return;
            }

            bool useIntensity = (UseIntensity && msg.Ranges.Length == msg.Intensities.Length && msg.RangeMin < msg.RangeMax);

            float x = Mathf.Cos(msg.AngleMin);
            float y = Mathf.Sin(msg.AngleMin);

            float dx = Mathf.Cos(msg.AngleIncrement);
            float dy = Mathf.Sin(msg.AngleIncrement);

            pointBuffer.Clear();

            if (!useIntensity)
            {
                for (int i = 0; i < msg.Ranges.Length; i++)
                {
                    float range = msg.Ranges[i];
                    if (range > msg.RangeMax || range < msg.RangeMin)
                    {
                        range = float.NaN;
                    }
                    pointBuffer.Add(new PointWithColor(new Unity.Mathematics.float4(-y, 0, x, 1) * range));
                    x = dx * x - dy * y;
                    y = dy * x + dx * y;
                }
            }
            else
            {
                for (int i = 0; i < msg.Ranges.Length; i++)
                {
                    float range = msg.Ranges[i];
                    if (range > msg.RangeMax || range < msg.RangeMin)
                    {
                        range = float.NaN;
                    }
                    pointBuffer.Add(new PointWithColor(
                        new Vector3(x * range, y * range, 0).Ros2Unity(),
                        msg.Intensities[i]
                    ));
                    x = dx * x - dy * y;
                    y = dy * x + dx * y;
                }
            }

            Size = pointBuffer.Count;

            if (!UseLines)
            {
                pointCloud.PointsWithColor = pointBuffer;
                LastIntensityBounds = pointCloud.IntensityBounds;
                if (ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
            else
            {
                int n = pointBuffer.Count;

                lineBuffer.Clear();
                for (int i = 0; i < pointBuffer.Count; i++)
                {
                    PointWithColor pA = pointBuffer[i];
                    PointWithColor pB = pointBuffer[(i + 1) % n];
                    if (!pB.HasNaN && (pB.Position - pA.Position).sqrMagnitude < MaxDistance * MaxDistance)
                    {
                        lineBuffer.Add(new LineWithColor(pA, pB));
                    }
                    else if (!pA.HasNaN)
                    {
                        lineBuffer.Add(new LineWithColor(pA, pA));
                    }
                }
                lines.LinesWithColor = lineBuffer;
                LastIntensityBounds = lines.IntensityBounds;
                if (ForceMinMax)
                {
                    lines.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public override void Stop()
        {
            base.Stop();

            pointCloud.Stop();
            ResourcePool.Dispose(Resource.Markers.PointList, pointCloud.gameObject);

            lines.Stop();
            ResourcePool.Dispose(Resource.Markers.Line, lines.gameObject);

            node.Stop();
            Destroy(node);
        }
    }
}
