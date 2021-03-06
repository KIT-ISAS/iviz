﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Logger = Iviz.Core.Logger;

namespace Iviz.Displays
{
    /// <summary>
    ///     Displays a list of points as squares.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class PointListResource : MarkerResourceWithColormap
    {
        public delegate void DirectPointSetter(NativeList<float4> pointBuffer);

        public const float MaxPositionMagnitude = 1e3f;

        static readonly int PointsId = Shader.PropertyToID("_Points");
        static readonly int ScaleId = Shader.PropertyToID("_Scale");

        readonly NativeList<float4> pointBuffer = new NativeList<float4>();

        bool isDirty;
        Mesh mesh;
        //bool processing;
        [CanBeNull] MeshRenderer meshRenderer;
        [CanBeNull] ComputeBuffer pointComputeBuffer;

        Material currentMaterial;

        [NotNull]
        MeshRenderer MeshRenderer => meshRenderer != null ? meshRenderer : meshRenderer = GetComponent<MeshRenderer>();

        int Size => pointBuffer.Length;

        public override bool UseColormap
        {
            get => base.UseColormap;
            set
            {
                base.UseColormap = value;
                if (Settings.SupportsComputeBuffers)
                {
                    currentMaterial = UseColormap
                        ? Resource.Materials.PointCloudWithColormap.Object
                        : Resource.Materials.PointCloud.Object;
                }
                else
                {
                    currentMaterial = UseColormap
                        ? Resource.Materials.PointCloudDirectWithColormap.Object
                        : Resource.Materials.PointCloudDirect.Object;
                    MeshRenderer.material = currentMaterial;
                }
            }
        }

        protected override void Awake()
        {
            if (!Settings.SupportsComputeBuffers)
            {
                Logger.Info($"{this}: Device does not support compute buffers in vertices. " +
                            "Point clouds may not appear correctly.");
                mesh = new Mesh {name = "PointCloud Mesh"};
                mesh.MarkDynamic();
                GetComponent<MeshFilter>().mesh = mesh;
                MeshRenderer.enabled = true;
            }

            if (currentMaterial == null)
            {
                UseColormap = false;
            }

            base.Awake();
            ElementScale = 0.1f;
        }

        /// <summary>
        ///     Sets the list of points to empty.
        /// </summary>
        public void Reset()
        {
            pointBuffer.Clear();
            isDirty = true;
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            //bool isProcessing = processing;

            Properties.SetFloat(ScaleId, ElementScale * transform.lossyScale.x);

            if (Settings.SupportsComputeBuffers)
            {
                UpdateWithComputeBuffers();
            }
            else
            {
                UpdateWithMesh();
            }

            /*
            if (isProcessing)
            {
                processing = false;
            }
            */
        }

        void UpdateWithComputeBuffers()
        {
            Bounds worldBounds = WorldBounds;
            if (!worldBounds.IsVisibleFromMainCamera())
            {
                return;
            }

            if (isDirty)
            {
                UpdateBuffer();
                UpdateStats();
                isDirty = false;
            }

            UpdateTransform();

            Graphics.DrawProcedural(currentMaterial, worldBounds, MeshTopology.Quads, 4, Size, null, Properties,
                ShadowCastingMode.Off, false, gameObject.layer);
        }

        void UpdateWithMesh()
        {
            if (!isDirty)
            {
                return;
            }

            using (var points = new Rent<Vector3>(pointBuffer.Length))
            using (var indices = new Rent<int>(pointBuffer.Length))
            {
                if (UseColormap)
                {
                    using (var uvs = new Rent<Vector2>(pointBuffer.Length))
                    {
                        for (int i = 0; i < pointBuffer.Length; i++)
                        {
                            points.Array[i] = pointBuffer[i].xyz;
                            indices.Array[i] = i;
                            uvs.Array[i].x = pointBuffer[i].w;
                        }

                        mesh.Clear();
                        mesh.SetVertices(points);
                        mesh.SetUVs(0, uvs);
                        mesh.SetIndices(indices, MeshTopology.Points, 0);
                    }
                }
                else
                {
                    using (var colors = new Rent<Color32>(pointBuffer.Length))
                    {
                        for (int i = 0; i < pointBuffer.Length; i++)
                        {
                            points.Array[i] = pointBuffer[i].xyz;
                            indices.Array[i] = i;
                            colors.Array[i] = PointWithColor.ColorFromFloatBits(pointBuffer[i].w);
                        }

                        mesh.Clear();
                        mesh.SetVertices(points);
                        mesh.SetColors(colors);
                        mesh.SetIndices(indices, MeshTopology.Points, 0);
                    }
                }
            }

            isDirty = false;
            UpdateStats();
        }

        void OnDestroy()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(PointsId, (ComputeBuffer) null);
            }

            pointBuffer.Dispose();
        }

        [NotNull]
        public override string ToString()
        {
            return "[PointListResource '" + Name + "']";
        }

        /// <summary>
        ///     Sets the list of points.
        /// </summary>
        /// <param name="points">The list of points.</param>
        public void Set([NotNull] NativeList<PointWithColor> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            /*
            if (processing)
            {
                Logger.Debug($"{this}: Missed a Set!");
                return;
            }
            */

            pointBuffer.EnsureCapacity(points.Length);
            pointBuffer.Clear();
            foreach (ref PointWithColor t in points.Ref())
            {
                if (t.HasNaN() || t.Position.MaxAbsCoeff() > MaxPositionMagnitude)
                {
                    continue;
                }

                pointBuffer.Add(t.f);
            }

            isDirty = true;
        }


        /// <summary>
        ///     Copies the array of points directly without checking.
        /// </summary>
        /// <param name="points">A native array with the points.</param>
        public void SetDirect(in NativeArray<float4> points)
        {
            /*
            if (processing)
            {
                Logger.Debug($"{this}: Missed a SetDirect!");
                return;
            }
            */

            pointBuffer.Clear();
            pointBuffer.AddRange(points);
            //processing = false;
            isDirty = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsElementValid(in PointWithColor t)
        {
            return !(t.Position.HasNaN() || t.Position.MaxAbsCoeff() > MaxPositionMagnitude);
        }

        public void SetDirect([NotNull] DirectPointSetter callback, int reserve = 0)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            /*
            if (processing)
            {
                Logger.Debug($"{this}: Missed a SetDirect!");
                return;
            }
            */

            //processing = true;
            if (reserve != 0)
            {
                pointBuffer.EnsureCapacity(reserve);
            }

            pointBuffer.Clear();
            callback(pointBuffer);
            isDirty = true;
            //processing = false;
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
        }

        void UpdateStats()
        {
            MinMaxJob.CalculateBounds(pointBuffer.AsArray(), Size, out Bounds bounds, out Vector2 span);
            BoxCollider.center = bounds.center;
            BoxCollider.size = bounds.size + ElementScale * Vector3.one;
            MeasuredIntensityBounds = span;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = span;
            }
        }

        protected override void Rebuild()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(PointsId, (ComputeBuffer) null);
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
            Properties.SetBuffer(PointsId, (ComputeBuffer) null);

            //processing = false;
        }

        protected override void UpdateProperties()
        {
            if (!Settings.SupportsComputeBuffers)
            {
                MeshRenderer.SetPropertyBlock(Properties);
            }
        }
    }
}