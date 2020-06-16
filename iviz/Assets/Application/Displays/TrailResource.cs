using UnityEngine;
using System.Collections;
using Iviz.App;
using Iviz.Resources;
using System;
using System.Collections.Generic;

namespace Iviz.Displays
{
    public sealed class TrailResource : MonoBehaviour, IDisplay, IRecyclable
    {
        LineResource lines;
        bool keepGoing = true;

        public Func<Vector3> DataSource { get; set; }

        public Vector3 WorldScale => lines.WorldScale;
        public Pose WorldPose => lines.WorldPose;

        public int Layer
        {
            get => lines.Layer;
            set => lines.Layer = value;
        }

        readonly List<Vector3> measurements = new List<Vector3>();
        int startOffset = 0;

        int totalMeasurements = 10 * 2;

        const int MeasurementsPerSecond = 32;

        [SerializeField] int timeWindowInMs_ = 2000;
        public int TimeWindowInMs
        {
            get => timeWindowInMs_;
            set
            {
                if (timeWindowInMs_ != value)
                {
                    timeWindowInMs_ = value;
                    Reset();
                }
            }
        }

        [SerializeField] Color color_ = UnityEngine.Color.red;
        public Color32 Color
        {
            get => color_;
            set => color_ = value;
        }

        public void Reset()
        {
            measurements.Clear();
            startOffset = 0;
            totalMeasurements = timeWindowInMs_ * MeasurementsPerSecond / 1000;
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
            TimeWindowInMs = 2000;
            StartCoroutine("GatherMeasurement");
        }

        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Markers.Line, lines.gameObject);
        }

        public void Stop()
        {
            lines.Stop();
            measurements.Clear();
            keepGoing = false;
        }

        IEnumerator GatherMeasurement()
        {
            while (true)
            {
                if (keepGoing && DataSource != null)
                {
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
                                new Color32(Color.r, Color.g, Color.b, (byte)alphaA),
                                measurements[indexB],
                                new Color32(Color.r, Color.g, Color.b, (byte)alphaB)
                                );
                            //Debug.Log(lineColors[i]);
                        }
                    });
                }
                const float waitTime = 1.0f / MeasurementsPerSecond;
                yield return keepGoing ? new WaitForSeconds(waitTime) : null;
            }
        }

    }
}
