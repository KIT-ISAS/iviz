using System;
using System.Threading.Tasks;
using Iviz.App.Displays;
using Iviz.Msgs.sensor_msgs;
using RosSharp;
using UnityEngine;

namespace Iviz.App
{
    public class LaserScanListener : TopicListener, IRecyclable
    {
        PointListResource pointCloud;
        DisplayNode node;

        public float MinIntensity { get; private set; }
        public float MaxIntensity { get; private set; }
        public int Size { get; private set; }

        public bool CalculateMinMax { get; private set; } = true;

        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.LaserScan;
            public string topic = "";
            public float pointSize = 0.03f;
            public Resource.ColormapId colormap = Resource.ColormapId.hsv;
            public bool ignoreIntensity;
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                config.topic = value.topic;
                PointSize = value.pointSize;
                Colormap = value.colormap;
                IgnoreIntensity = value.ignoreIntensity;
            }
        }

        public float PointSize
        {
            get => config.pointSize;
            set
            {
                config.pointSize = value;
                pointCloud.Scale = value * Vector2.one;
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.colormap;
            set
            {
                config.colormap = value;
                pointCloud.Colormap = value;
            }
        }

        public bool IgnoreIntensity
        {
            get => config.ignoreIntensity;
            set
            {
                config.ignoreIntensity = value;
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

        void Awake()
        {
            transform.parent = TFListener.ListenersFrame.transform;

            node = SimpleDisplayNode.Instantiate("LaserScanNode", transform);
            pointCloud = ResourcePool.GetOrCreate(Resource.Markers.PointList, node.transform).GetComponent<PointListResource>();
            Config = new Configuration();
        }

        public override void StartListening()
        {
            base.StartListening();
            Listener = new RosListener<LaserScan>(config.topic, Handler);
            name = "LaserScan:" + config.topic;
            node.name = "LaserScanNode:" + config.topic;
        }

        void Handler(LaserScan msg)
        {
            int newSize = msg.ranges.Length;
            if (newSize > pointBuffer.Length)
            {
                pointBuffer = new PointWithColor[newSize * 11 / 10];
            }

            if (msg.angle_min >= msg.angle_max || msg.range_min >= msg.range_max)
            {
                Logger.Info("LaserScanListener: Invalid angle or range dimensions!");
                return;
            }


            Task.Run(() =>
            {
                bool useIntensity = (!IgnoreIntensity && msg.ranges.Length == msg.intensities.Length && msg.range_min < msg.range_max);

                float x = Mathf.Cos(msg.angle_min);
                float y = Mathf.Sin(msg.angle_min);

                float dx = Mathf.Cos(msg.angle_increment);
                float dy = Mathf.Sin(msg.angle_increment);

                Vector2 intensityBounds;
                if (!useIntensity)
                {
                    for (int i = 0; i < msg.ranges.Length; i++)
                    {
                        float range = msg.ranges[i];
                        if (range > msg.range_max || range < msg.range_min)
                        {
                            range = float.NaN;
                        }
                        pointBuffer[i].position = new Vector3(x * range, y * range, 0).Ros2Unity();
                        pointBuffer[i].intensity = range;
                        x = dx * x - dy * y;
                        y = dy * x + dx * y;
                    }
                    intensityBounds = new Vector2(msg.range_min, msg.range_max);
                }
                else
                {
                    for (int i = 0; i < msg.ranges.Length; i++)
                    {
                        float range = msg.ranges[i];
                        if (range > msg.range_max || range < msg.range_min)
                        {
                            range = float.NaN;
                        }
                        pointBuffer[i].position = new Vector3(x * range, y * range, 0).Ros2Unity();
                        pointBuffer[i].intensity = msg.intensities[i];
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
                    pointCloud.Set(pointBuffer, newSize);
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
            pointCloud.Parent = node.transform;
        }

        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Markers.PointList, pointCloud.gameObject);
            pointCloud = null;
        }
    }
}
