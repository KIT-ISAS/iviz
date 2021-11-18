#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class TrailResource : DisplayWrapperResource, IRecyclable
    {
        const int MeasurementsPerSecond = 32;

        [SerializeField] int timeWindowInMs = 5000;
        [SerializeField] Color color = UnityEngine.Color.red;

        readonly Queue<(Vector3 A, Vector3 B)> measurements = new(5 * MeasurementsPerSecond);
        readonly Func<NativeList<float4x2>, bool?> lineSetterDelegate;

        Vector3? lastMeasurement;
        float lastTick;
        int maxMeasurements = 160;

        LineResource? resource;
        LineResource Resource => resource != null ? resource : (resource = CreateLineResource(Transform));

        protected override IDisplay Display => Resource;
        public Func<Vector3>? DataSource { get; set; }


        static LineResource CreateLineResource(Transform transform)
        {
            var resource = ResourcePool.RentDisplay<LineResource>(transform);
            resource.Name = "[Line for Trail]";
            resource.ElementScale = 0.01f;
            return resource;
        }
        
        public TrailResource()
        {
            lineSetterDelegate = LineSetter;
        }

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
            get => Resource.ElementScale;
            set => Resource.ElementScale = value;
        }

        void Awake()
        {
            Transform.SetPose(Pose.identity);
            TimeWindowInMs = TimeWindowInMs;
            Layer = LayerType.IgnoreRaycast;
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

            float tick = Time.time;
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

            measurements.Enqueue((lastMeasurement.Value, newMeasurement));
            if (measurements.Count > maxMeasurements)
            {
                measurements.Dequeue();
            }

            lastMeasurement = newMeasurement;

            Resource.SetDirect(lineSetterDelegate, measurements.Count);
        }

        bool? LineSetter(NativeList<float4x2> lineBuffer)
        {
            int i = 1;
            Color32 colorA = Color.WithAlpha(0);
            float scale = 255f / measurements.Count;

            foreach ((Vector3 a, Vector3 b) in measurements)
            {
                Color32 colorB = colorA.WithAlpha((byte) (i * scale));
                LineWithColor line = new(a, colorA, b, colorB);
                if (LineResource.IsElementValid(line))
                {
                    lineBuffer.Add(line.f);
                }

                colorA = colorB;
                i++;
            }

            return true;
        }
        
        public void SplitForRecycle()
        {
            resource.ReturnToPool();
        }        

        public override void Suspend()
        {
            base.Suspend();
            measurements.Clear();
            DataSource = null;
        }
    }
}