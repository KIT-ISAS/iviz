using UnityEngine;
using Iviz.Resources;
using System;
using System.Collections.Generic;
using Iviz.Controllers;

namespace Iviz.Displays
{
    public sealed class TrailResource : DisplayWrapperResource
    {
        LineResource lines;

        protected override IDisplay Display => lines;

        public Func<Vector3> DataSource { get; set; }

        readonly List<Vector3> measurements = new List<Vector3>();
        int startOffset = 0;

        int totalMeasurements = 10 * 2;

        const int MeasurementsPerSecond = 32;

        [SerializeField] int timeWindowInMs = 2000;

        public int TimeWindowInMs
        {
            get => timeWindowInMs;
            set
            {
                if (timeWindowInMs == value)
                {
                    return;
                }

                timeWindowInMs = value;
                totalMeasurements = timeWindowInMs * MeasurementsPerSecond / 1000;
                Reset();
                
            }
        }

        [SerializeField] Color color = UnityEngine.Color.red;

        public Color32 Color
        {
            get => color;
            set => color = value;
        }

        public float Scale
        {
            get => lines.ElementSize;
            set => lines.ElementSize = value;
        }

        void Awake()
        {
            lines = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, transform);
            lines.ElementSize = 0.01f;
            TimeWindowInMs = TimeWindowInMs;

            transform.parent = TFListener.UnityFrame?.transform;
        }

        public override void Suspend()
        {
            base.Suspend();
            measurements.Clear();
            DataSource = null;
        }

        public void Reset()
        {
            measurements.Clear();
            startOffset = 0;            
            lines?.Reserve(totalMeasurements);
        }

        float lastTick;
        void Update()
        {
            float tick = Time.time;
            if (tick - lastTick < 0.1f)
            {
                return;
            }
            lastTick = tick;

            if (DataSource == null)
            {
                return;
            }
            Vector3 newMeasurement = DataSource();
            //Debug.Log(newMeasurement);
            if (measurements.Count != totalMeasurements)
            {
                measurements.Add(newMeasurement);
            }
            else
            {
                measurements[startOffset] = newMeasurement;
                startOffset++;
                if (startOffset == measurements.Count)
                {
                    startOffset = 0;
                }
            }

            //Debug.Log("post: " + measurements.Count + " " + totalMeasurements);
            lines.Set(measurements.Count, lineColors =>
            {
                int count = measurements.Count;
                for (int i = 0; i < count - 1; i++)
                {
                    int indexA = (startOffset + i) % count;
                    int indexB = (indexA + 1) % count;
                    int alphaA = i * 255 / count;
                    int alphaB = (i + 1) * 255 / count;
                    lineColors[i] = new LineWithColor(
                        measurements[indexA],
                        new Color32(Color.r, Color.g, Color.b, (byte) alphaA),
                        measurements[indexB],
                        new Color32(Color.r, Color.g, Color.b, (byte) alphaB)
                    );
                    //Debug.Log(lineColors[i]);
                }
            });
        }
    }
}