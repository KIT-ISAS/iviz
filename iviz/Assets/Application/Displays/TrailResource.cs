using UnityEngine;
using System.Collections;
using Iviz.App;
using Iviz.Resources;
using System;
using System.Collections.Generic;
using Iviz.App.Listeners;

namespace Iviz.Displays
{
    public sealed class TrailResource : MonoBehaviour, IDisplay, IRecyclable
    {
        LineResource lines;

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
                ResetMeasurements();
            }
        }

        [SerializeField] Color color = UnityEngine.Color.red;

        public Color32 Color
        {
            get => color;
            set => color = value;
        }

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public Bounds Bounds => lines.Bounds;

        public Bounds WorldBounds => lines.WorldBounds;

        public bool ColliderEnabled
        {
            get => lines.ColliderEnabled;
            set => lines.ColliderEnabled = value;
        }

        public Transform Parent
        {
            get => lines.Parent;
            set => lines.Parent = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public float Scale
        {
            get => lines.LineScale;
            set => lines.LineScale = value;
        }

        void Awake()
        {
            lines = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, transform);
            lines.LineScale = 0.01f;
            TimeWindowInMs = TimeWindowInMs;

            transform.parent = TFListener.UnityFrame?.transform;
        }

        void ResetMeasurements()
        {
            measurements.Clear();
            startOffset = 0;
            totalMeasurements = timeWindowInMs * MeasurementsPerSecond / 1000;
            lines?.Reserve(totalMeasurements);
        }
        
        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Displays.Line, lines.gameObject);
        }

        public void Stop()
        {
            lines.Stop();
            measurements.Clear();
            DataSource = null;
        }

        float lastTick = 0;
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