#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public interface ITrailDataSource
    {
        public Vector3 TrailPosition { get; }
    }

    public sealed class TrailDisplay : DisplayWrapper, IRecyclable
    {
        const int MeasurementsPerSecond = 32;

        readonly Queue<(Vector3 A, Vector3 B)> measurements = new(5 * MeasurementsPerSecond);
        readonly Func<NativeList<float4x2>, bool?> lineSetterDelegate;

        ITrailDataSource? dataSource;
        int timeWindowInMs = 5000;
        int maxMeasurements = 160;
        Color color = UnityEngine.Color.red;
        Vector3? lastMeasurement;
        LineDisplay? resource;

        LineDisplay Resource => resource != null ? resource : (resource = CreateLineResource(Transform));
        protected override IDisplay Display => Resource;

        static LineDisplay CreateLineResource(Transform transform)
        {
            var resource = ResourcePool.RentDisplay<LineDisplay>(transform);
            resource.gameObject.name = "[Line for Trail]";
            resource.ElementScale = 0.01f;
            return resource;
        }

        public ITrailDataSource? DataSource
        {
            set
            {
                if (dataSource == null && value != null)
                {
                    GameThread.EveryTenthOfASecond += UpdateMagnitude;
                }
                else if (dataSource != null && value == null)
                {
                    GameThread.EveryTenthOfASecond -= UpdateMagnitude;
                }

                dataSource = value;
                if (value != null)
                {
                    UpdateMagnitude();
                }
            }
        }

        public TrailDisplay()
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
            private get => color;
            set => color = value;
        }

        public float ElementScale
        {
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
            Resource.Reset();
        }

        void UpdateMagnitude()
        {
            if (dataSource == null)
            {
                return;
            }

            var newMeasurement = dataSource.TrailPosition;
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
            var colorA = Color.WithAlpha(0);
            float scale = 255f / measurements.Count;

            foreach (var (a, b) in measurements)
            {
                var colorB = colorA;
                colorB.a = (byte)(i * scale);
                
                var line = new LineWithColor(a, colorA, b, colorB);
                if (LineDisplay.IsElementValid(line.f))
                {
                    lineBuffer.AddUnsafe(line.f);
                }

                colorA = colorB;
                i++;
            }

            return true;
        }

        public void SplitForRecycle()
        {
            DataSource = null;
            resource.ReturnToPool();
        }

        public override void Suspend()
        {
            Reset();
            DataSource = null;
        }
    }
}