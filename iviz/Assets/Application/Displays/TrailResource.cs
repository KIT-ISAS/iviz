using System;
using System.Collections.Generic;
using Iviz.Controllers;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class TrailResource : DisplayWrapperResource
    {
        const int MeasurementsPerSecond = 32;

        [SerializeField] int timeWindowInMs = 5000;
        [SerializeField] Color color = UnityEngine.Color.red;

        readonly Queue<LineWithColor> measurements = new Queue<LineWithColor>(5 * MeasurementsPerSecond);
        Vector3? lastMeasurement;
        float lastTick;
        int maxMeasurements = 160;
        
        LineResource resource;

        protected override IDisplay Display => resource;

        public Func<Vector3> DataSource { get; set; }


        public int TimeWindowInMs
        {
            get => timeWindowInMs;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Invalid value " + value, nameof(value));    
                }
                
                if (timeWindowInMs == value)
                {
                    return;
                }

                timeWindowInMs = value;
                maxMeasurements = timeWindowInMs * MeasurementsPerSecond / 1000;
                Reset();
            }
        }

        public Color32 Color
        {
            get => color;
            set => color = value;
        }

        public float ElementScale
        {
            get => resource.ElementScale;
            set => resource.ElementScale = value;
        }

        void Awake()
        {
            resource = ResourcePool.GetOrCreateDisplay<LineResource>(transform);
            resource.Name = "[Line for Trail]";
            resource.ElementScale = 0.01f;
            TimeWindowInMs = TimeWindowInMs;

            transform.parent = TfListener.UnityFrame?.transform;
        }

        public void Reset()
        {
            measurements.Clear();
            lastMeasurement = null;
        }

        void Update()
        {
            if (DataSource == null)
            {
                return;
            }

            var tick = Time.time;
            if (tick - lastTick < 0.1f)
            {
                return;
            }

            lastTick = tick;

            var newMeasurement = DataSource();
            if (lastMeasurement == null)
            {
                lastMeasurement = newMeasurement;
                return;
            }

            measurements.Enqueue(new LineWithColor(lastMeasurement.Value, newMeasurement));
            if (measurements.Count > maxMeasurements)
            {
                measurements.Dequeue();
            }

            lastMeasurement = newMeasurement;

            IEnumerable<LineWithColor> LineEnumerator()
            {
                int i = 1;
                Color32 colorA = new Color32(Color.r, Color.g, Color.b, 0);
                float scale = 255f / measurements.Count;

                foreach (LineWithColor line in measurements)
                {
                    Color32 colorB = new Color32(Color.r, Color.g, Color.b, (byte) (i * scale));
                    yield return new LineWithColor(line.A, colorA, line.B, colorB);
                    colorA = colorB;
                    i++;
                }
            }

            resource.Set(LineEnumerator(), measurements.Count);
        }

        public override void Suspend()
        {
            base.Suspend();
            measurements.Clear();
            DataSource = null;
        }
    }
}