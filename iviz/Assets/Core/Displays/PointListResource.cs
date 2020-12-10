using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Sdf;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Material = UnityEngine.Material;

namespace Iviz.Displays
{
    /// <summary>
    /// Displays a list of points as squares.
    /// </summary>
    public sealed class PointListResource : MarkerResourceWithColormap
    {
        public const float MaxPositionMagnitudeSq = 1e9f;

        static readonly int PointsId = Shader.PropertyToID("_Points");
        static readonly int ScaleId = Shader.PropertyToID("_Scale");

        NativeList<float4> pointBuffer;
        [CanBeNull] ComputeBuffer pointComputeBuffer;
        bool isDirty;

        int Size => pointBuffer.Length;

        /// <summary>
        /// Sets the list of points.
        /// </summary>
        /// <param name="points">The list of points.</param>
        public void Set([NotNull] List<PointWithColor> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            pointBuffer.Capacity = Math.Max(pointBuffer.Capacity, points.Count);
            pointBuffer.Clear();
            foreach (PointWithColor t in points)
            {
                if (t.HasNaN() || t.Position.MagnitudeSq() > MaxPositionMagnitudeSq)
                {
                    continue;
                }

                pointBuffer.Add(t);
            }

            isDirty = true;
        }

        /// <summary>
        /// Sets the list of points to empty.
        /// </summary>
        public void Reset()
        {
            pointBuffer.Clear();
            isDirty = true;
        }


        /// <summary>
        /// Copies the array of points directly without checking.
        /// </summary>
        /// <param name="points">A native array with the points.</param>
        public void SetDirect(in NativeArray<float4> points)
        {
            pointBuffer.Clear();
            pointBuffer.AddRange(points);
            isDirty = true;
        }

        public static bool IsElementValid(PointWithColor t)
        {
            return !(t.Position.HasNaN() || t.Position.MagnitudeSq() > MaxPositionMagnitudeSq);
        }

        public delegate void DirectPointSetter(ref NativeList<float4> pointBuffer);

        public void SetDirect([NotNull] DirectPointSetter callback, int reserve = 0)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (reserve != 0)
            {
                pointBuffer.Capacity = Math.Max(pointBuffer.Capacity, reserve);
            }

            pointBuffer.Clear();
            callback(ref pointBuffer);
            isDirty = true;
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
                Properties.SetBuffer(PointsId, pointComputeBuffer);
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
            bool isCurrentlyVisible = GeometryUtility.TestPlanesAABB(UnityUtils.CalculateCameraPlanes(), WorldBounds);
            if (!isCurrentlyVisible)
            {
                return;
            }

            if (isDirty)
            {
                UpdateBuffer();
                isDirty = false;
            }

            if (Size == 0)
            {
                return;
            }

            UpdateTransform();
            Properties.SetFloat(ScaleId, ElementScale * transform.lossyScale.x);

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
                Properties.SetBuffer(PointsId, null);
            }

            pointBuffer.Dispose();
        }

        protected override void Rebuild()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(PointsId, null);
            }

            if (pointBuffer.Capacity != 0)
            {
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Capacity, Marshal.SizeOf<PointWithColor>());
                pointComputeBuffer.SetData(pointBuffer.AsArray(), 0, 0, Size);
                Properties.SetBuffer(PointsId, pointComputeBuffer);
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
            Properties.SetBuffer(PointsId, null);
        }
    }
}