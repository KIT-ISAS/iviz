using UnityEngine;
using System.Collections;
using Iviz.App;
using Iviz.Resources;
using System;
using System.Collections.Generic;

namespace Iviz.Displays
{

    public class TrailResource : MonoBehaviour, IDisplay, IRecyclable
    {
        LineResource lines;
        bool keepGoing = true;

        public GameObject source;

        Func<Vector3> dataSource;
        public Func<Vector3> DataSource
        {
            get => dataSource;
            set
            {
                dataSource = value;
                Reset();
            }
        }

        readonly List<Vector3> measurements = new List<Vector3>();
        int startOffset = 0;

        int totalMeasurements = 10 * 2;

        /*
        int measurementsPerSecond = 10;
        public int MeasurementsPerSecond
        {
            get => measurementsPerSecond;
            set
            {
                measurementsPerSecond = value;
                Reset();
            }
        }
        */
        const int MeasurementsPerSecond = 16;

        int timeWindowInMs = 2000;
        public int TimeWindowInMs
        {
            get => timeWindowInMs;
            set
            {
                timeWindowInMs = value;
                Reset();
            }
        }

        public Color32 Color { get; set; } = UnityEngine.Color.red;

        public void Reset()
        {
            measurements.Clear();
            startOffset = 0;
            totalMeasurements = timeWindowInMs * MeasurementsPerSecond / 1000;
            lines.Reserve(totalMeasurements);
        }

        public string Name => "TrailResource";

        public Bounds Bounds => lines.Bounds;

        public Bounds WorldBounds => lines.WorldBounds;

        public bool ColliderEnabled
        {
            get => lines.ColliderEnabled;
            set => ColliderEnabled = value;
        }
        public Transform Parent
        {
            get => lines.Parent;
            set => lines.Parent = value;
        }
        public bool Visible
        {
            get => lines.Visible;
            set => lines.Visible = value;
        }
        public float Scale
        {
            get => lines.Scale;
            set => lines.Scale = value;
        }

        void Awake()
        {
            lines = ResourcePool.GetOrCreate<LineResource>(Resource.Markers.Line, transform);
            lines.Scale = 0.01f;
            StartCoroutine("GatherMeasurement");
        }

        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Markers.Line, lines.gameObject);
        }

        public void Stop()
        {
            lines.Stop();
            keepGoing = false;
        }

        IEnumerator GatherMeasurement()
        {
            while (true)
            {
                if (DataSource != null)
                {
                    Vector3 newMeasurement = DataSource();
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
                                new Color32(Color.r, Color.g, Color.b, (byte)alphaA),
                                measurements[indexB],
                                new Color32(Color.r, Color.g, Color.b, (byte)alphaB)
                                );
                        }
                    });
                }
                const float waitTime = 1.0f / MeasurementsPerSecond;
                yield return keepGoing ? new WaitForSeconds(waitTime) : null;
            }
        }

    }
}
