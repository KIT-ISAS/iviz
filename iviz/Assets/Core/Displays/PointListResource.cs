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

        NativeArray<float4> pointBuffer;
        ComputeBuffer pointComputeBuffer;
        [SerializeField] int size;

        int Size
        {
            get => size;
            set
            {
                if (value == size)
                {
                    return;
                }

                size = value;
                Reserve(size * 11 / 10);
            }
        }

        void Reserve(int reqDataSize)
        {
            if (pointBuffer.Length >= reqDataSize)
            {
                return;
            }

            if (pointBuffer.Length != 0)
            {
                pointBuffer.Dispose();
            }

            pointBuffer = new NativeArray<float4>(reqDataSize, Allocator.Persistent);

            pointComputeBuffer?.Release();
            pointComputeBuffer = new ComputeBuffer(pointBuffer.Length, Marshal.SizeOf<PointWithColor>());
            Properties.SetBuffer(PointsID, pointComputeBuffer);
        }

        /// <summary>
        /// Sets the points with the given collection.
        /// </summary>
        public IReadOnlyCollection<PointWithColor> PointsWithColor
        {
            get => new PointGetHelper(pointBuffer);
            set => Set(value.Count, value);
        }

        static bool IsValid(PointWithColor t) => !t.HasNaN() && t.Position.sqrMagnitude < MaxPositionMagnitudeSq;

        /// <summary>
        /// Sets the list of points with the given enumerator.
        /// </summary>
        /// <param name="count">The number of points, or at least an upper bound.</param>
        /// <param name="points">The point enumerator.</param>
        public void Set(int count, IEnumerable<PointWithColor> points)
        {
            if (count < 0)
            {
                throw new ArgumentException("Invalid count " + count, nameof(count));
            }

            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            Size = count;

            int realSize = 0;
            foreach (PointWithColor t in points)
            {
                if (!IsValid(t)) { continue; }

                pointBuffer[realSize++] = t;
            }

            Size = realSize;
            UpdateBuffer();
        }

        void UpdateBuffer()
        {
            if (Size == 0)
            {
                return;
            }

            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(pointBuffer, Size, out Bounds bounds, out Vector2 span);
            BoxCollider.center = bounds.center;
            BoxCollider.size = bounds.size + ElementScale * Vector3.one;
            IntensityBounds = span;
        }

        protected override void Awake()
        {
            base.Awake();
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

            if (pointBuffer.Length > 0)
            {
                pointBuffer.Dispose();
            }
        }

        protected override void Rebuild()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(PointsID, null);
            }

            if (pointBuffer.Length != 0)
            {
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Length, Marshal.SizeOf<PointWithColor>());
                pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
                Properties.SetBuffer(PointsID, pointComputeBuffer);
            }

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
        }

        public override void Suspend()
        {
            base.Suspend();
            Size = 0;

            if (pointBuffer.Length > 0)
            {
                pointBuffer.Dispose();
            }

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