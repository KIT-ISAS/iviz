using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Iviz.Resources;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    /// <summary>
    /// Displays a list of points as squares.
    /// </summary>
    public sealed class PointListResource : MarkerResourceWithColormap
    {
        const float MaxPositionMagnitudeSq = 1e9f;

        static readonly int PointsID = Shader.PropertyToID("_Points");
        static readonly int ScaleID = Shader.PropertyToID("_Scale");

        NativeList<float4> pointBuffer;
        ComputeBuffer pointComputeBuffer;

        int Size => pointBuffer.Length;

        /// <summary>
        /// Sets the points with the given collection.
        /// </summary>
        public IReadOnlyCollection<PointWithColor> PointsWithColor
        {
            get => new PointGetHelper(pointBuffer);
            set => Set(value, value.Count);
        }

        static bool IsValid(in PointWithColor t) => !t.HasNaN() && t.Position.MagnitudeSq() < MaxPositionMagnitudeSq;

        /// <summary>
        /// Sets the list of points with the given enumerator.
        /// </summary>
        /// <param name="points">The point enumerator.</param>
        /// <param name="reserve">The number of points to reserve, or 0 if unknown.</param>
        public void Set(IEnumerable<PointWithColor> points, int reserve = 0)
        {
            if (reserve < 0)
            {
                throw new ArgumentException("Invalid reserve " + reserve, nameof(reserve));
            }

            if (reserve > 0)
            {
                pointBuffer.Capacity = Math.Max(pointBuffer.Capacity, reserve);
            }

            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            pointBuffer.Clear();
            foreach (PointWithColor t in points)
            {
                if (!IsValid(t))
                {
                    continue;
                }

                pointBuffer.Add(t);
            }

            UpdateBuffer();
        }

        void UpdateBuffer()
        {
            if (Size == 0)
            {
                return;
            }

            if (pointComputeBuffer == null || pointComputeBuffer.count < pointBuffer.Capacity)
            {
                pointComputeBuffer?.Release();
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Capacity, Marshal.SizeOf<PointWithColor>());
                Properties.SetBuffer(PointsID, pointComputeBuffer);
            }

            pointComputeBuffer.SetData(pointBuffer.AsArray(), 0, 0, Size);
            MinMaxJob.CalculateBounds(pointBuffer, Size, out Bounds bounds, out Vector2 span);
            BoxCollider.center = bounds.center;
            BoxCollider.size = bounds.size + ElementScale * Vector3.one;
            IntensityBounds = span;
        }

        protected override void Awake()
        {
            base.Awake();
            pointBuffer = new NativeList<float4>(Allocator.Persistent);
            ElementScale = 0.1f;
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            UpdateTransform();
            Properties.SetFloat(ScaleID, ElementScale * transform.lossyScale.x);

            Material material = UseColormap
                ? Resource.Materials.PointCloudWithColormap.Object
                : Resource.Materials.PointCloud.Object;

            Bounds worldBounds = BoxCollider.bounds;
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, Size, null, Properties,
                ShadowCastingMode.Off, false, gameObject.layer);
        }

        void OnDestroy()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(PointsID, null);
            }

            pointBuffer.Dispose();
        }

        protected override void Rebuild()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(PointsID, null);
            }

            if (pointBuffer.Capacity != 0)
            {
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Capacity, Marshal.SizeOf<PointWithColor>());
                pointComputeBuffer.SetData(pointBuffer.AsArray(), 0, 0, Size);
                Properties.SetBuffer(PointsID, pointComputeBuffer);
            }

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
        }

        public override void Suspend()
        {
            base.Suspend();

            pointBuffer.Clear();

            pointComputeBuffer?.Release();
            pointComputeBuffer = null;
            Properties.SetBuffer(PointsID, null);
        }

        internal class PointGetHelper : IReadOnlyCollection<PointWithColor>
        {
            readonly NativeArray<float4> nArray;
            public PointGetHelper(in NativeArray<float4> array) => nArray = array;
            public int Count => nArray.Length;

            public IEnumerator<PointWithColor> GetEnumerator() =>
                nArray.Select(f => new PointWithColor(f)).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() =>
                nArray.Select(f => new PointWithColor(f)).GetEnumerator();
        }
    }
}