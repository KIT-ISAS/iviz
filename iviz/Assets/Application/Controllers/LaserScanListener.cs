using System;
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
        [DataMember] public float MaxDistance { get; set; } = 1.0f;
    }

    public class LaserScanListener : TopicListener
    {
        PointListResource pointCloud;
        DisplayNode node;

        public float MinIntensity { get; private set; }
        public float MaxIntensity { get; private set; }
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
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                pointCloud.Colormap = value;
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

        /*
        public Color Color
        {
            get => config.color;
            set
            {
                config.color = value;
            }
        }
        */

        PointWithColor[] pointBuffer = new PointWithColor[0];
        LineWithColor[] lineBuffer = new LineWithColor[0];

        void Awake()
        {
            transform.parent = TFListener.ListenersFrame.transform;

            node = SimpleDisplayNode.Instantiate("LaserScanNode", transform);
            pointCloud = ResourcePool.GetOrCreate<PointListResource>(Resource.Markers.PointList, node.transform);
            Config = new LaserScanConfiguration();
        }

        public override void StartListening()
        {
            base.StartListening();
            Listener = new RosListener<LaserScan>(config.Topic, Handler);
            name = "LaserScan:" + config.Topic;
            node.name = "LaserScanNode:" + config.Topic;
        }

        void Handler(LaserScan msg)
        {
            if (msg.AngleMin >= msg.AngleMax || msg.RangeMin >= msg.RangeMax)
            {
                Logger.Info("LaserScanListener: Invalid angle or range dimensions!");
                return;
            }
            int newSize = msg.Ranges.Length;
            if (newSize > pointBuffer.Length)
            {
                pointBuffer = new PointWithColor[newSize * 11 / 10];
            }
            if (newSize > lineBuffer.Length)
            {
                lineBuffer = new LineWithColor[newSize * 11 / 10];
            }

            Task.Run(() =>
            {
                bool useIntensity = (UseIntensity && msg.Ranges.Length == msg.Intensities.Length && msg.RangeMin < msg.RangeMax);

                float x = Mathf.Cos(msg.AngleMin);
                float y = Mathf.Sin(msg.AngleMin);

                float dx = Mathf.Cos(msg.AngleIncrement);
                float dy = Mathf.Sin(msg.AngleIncrement);

                Vector2 intensityBounds;
                if (!useIntensity)
                {
                    for (int i = 0; i < msg.Ranges.Length; i++)
                    {
                        float range = msg.Ranges[i];
                        if (range > msg.RangeMax || range < msg.RangeMin)
                        {
                            range = float.NaN;
                        }
                        pointBuffer[i].position = new Vector3(x * range, y * range, 0).Ros2Unity();
                        pointBuffer[i].intensity = range;
                        x = dx * x - dy * y;
                        y = dy * x + dx * y;
                    }
                    intensityBounds = new Vector2(msg.RangeMin, msg.RangeMax);
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
                        pointBuffer[i].position = new Vector3(x * range, y * range, 0).Ros2Unity();
                        pointBuffer[i].intensity = msg.Intensities[i];
                        x = dx * x - dy * y;
                        y = dy * x + dx * y;
                    }
                    intensityBounds = CalculateBounds(newSize);
                }

                GameThread.RunOnce(() =>
                {
                    Size = newSize;
                    pointCloud.IntensityBounds = intensityBounds;
                    pointCloud.UseIntensityTexture = true;
                    pointCloud.PointsWithColor = new ArraySegment<PointWithColor>(pointBuffer, 0, newSize);
                });
            });
        }

        Vector2 CalculateBounds(int size)
        {
            float intensityMin = float.MaxValue, intensityMax = float.MinValue;
            for (int i = 0; i < size; i++)
            {
                Vector3 position = pointBuffer[i].position;
                float intensity = pointBuffer[i].intensity;
                if (float.IsNaN(position.x) ||
                    float.IsNaN(position.y) ||
                    float.IsNaN(position.z) ||
                    float.IsNaN(intensity))
                {
                    continue;
                }

                intensityMin = Mathf.Min(intensityMin, intensity);
                intensityMax = Mathf.Max(intensityMax, intensity);
            }

            return new Vector2(intensityMin, intensityMax);
        }

        public override void Stop()
        {
            base.Stop();

            ResourcePool.Dispose(Resource.Markers.PointList, pointCloud.gameObject);
            pointCloud = null;

            node.Stop();
            Destroy(node);
        }
    }
}
