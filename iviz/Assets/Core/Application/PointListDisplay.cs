#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Common;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using Iviz.Tools;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Displays
{
    /// <summary>
    ///     Displays a list of points as squares.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class PointListDisplay : MarkerDisplayWithColormap, ISupportsDynamicBounds
    {
        public const float MaxPositionMagnitude = 1e3f;
        
        readonly NativeList<float4> pointBuffer = new();

        [SerializeField] MeshRenderer? meshRenderer;
        [SerializeField] MeshFilter? meshFilter;
        ComputeBuffer? pointComputeBuffer;
        Mesh? mesh;
        Material? currentMaterial;
        bool isDirty;

        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));
        MeshFilter MeshFilter => meshFilter.AssertNotNull(nameof(meshFilter));

        int Size => pointBuffer.Length;

        public event Action? BoundsChanged;

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

        public override Bounds? Bounds => Size == 0 ? null : base.Bounds;

        protected override void Awake()
        {
            if (Settings.SupportsComputeBuffers)
            {
                pointComputeBuffer = new ComputeBuffer(1, Unsafe.SizeOf<float4>());
                Properties.SetBuffer(ShaderIds.PointsId, pointComputeBuffer);
            }
            else
            {
                RosLogger.Info($"{this}: Device does not support compute buffers in vertices. " +
                               "Point clouds may not appear correctly.");
                mesh = new Mesh { name = "PointCloud Mesh" };
                mesh.MarkDynamic();
                MeshFilter.mesh = mesh;
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
            if (Size == 0 && !isDirty)
            {
                return;
            }

            // ReSharper disable once RedundantBaseQualifier
            float scaleX = base.ElementScale * Transform.lossyScale.x;
            Properties.SetFloat(ShaderIds.ScaleId, scaleX);

            if (Settings.SupportsComputeBuffers)
            {
                UpdateWithComputeBuffers();
            }
            else
            {
                UpdateWithMesh();
            }
        }

        void UpdateWithComputeBuffers()
        {
            if (isDirty)
            {
                UpdateBuffer();
                CalculateBounds();
                isDirty = false;
            }

            var worldBounds = WorldBounds;
            if (!worldBounds.IsVisibleFromMainCamera())
            {
                return;
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

            if (mesh == null)
            {
                throw new InvalidOperationException("Cannot update empty mesh!");
            }

            mesh.Clear();

            if (Size == 0)
            {
                CalculateBounds();
                isDirty = false;
                return;
            }

            int pointsLength = pointBuffer.Length;
            
            using (var vertices = new Rent<Vector3>(pointsLength))
            {
                IndicesUtils.FillVector3(pointBuffer, vertices);
                mesh.SetVertices(vertices);

                if (UseColormap)
                {
                    using var uvs = new Rent<Vector2>(pointsLength);
                    IndicesUtils.FillUV(pointBuffer, uvs);
                    mesh.SetUVs(uvs);
                }
                else
                {
                    using var colors = new Rent<Color32>(pointsLength);
                    IndicesUtils.FillColor(pointBuffer, colors);
                    mesh.SetColors(colors);
                }
            }

            using (var indices = new Rent<int>(pointsLength))
            {
                IndicesUtils.FillIndices(indices);
                mesh.SetIndices(indices, MeshTopology.Points);
            }

            isDirty = false;
            CalculateBounds();
        }

        void OnDestroy()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(ShaderIds.PointsId, (ComputeBuffer?)null);
            }

            pointBuffer.Dispose();
        }

        public override string ToString() => $"[{nameof(PointListDisplay)}]";

        /// <summary>
        ///     Copies the list of points directly without checking.
        /// </summary>
        /// <param name="points">The list of points.</param>
        public void Set(ReadOnlySpan<PointWithColor> points)
        {
            Set(MemoryMarshal.Cast<PointWithColor, float4>(points));
        }


        /// <summary>
        ///     Copies the list of points directly without checking.
        /// </summary>
        /// <param name="points">A native list with the points.</param>
        public void Set(ReadOnlySpan<float4> points)
        {
            pointBuffer.Clear();
            pointBuffer.AddRange(points);
            isDirty = true;
        }

        public void Set(Action<NativeList<float4>> callback, int reserve)
        {
            ThrowHelper.ThrowIfNull(callback, nameof(callback));
            pointBuffer.EnsureCapacity(reserve);
            pointBuffer.Clear();

            callback(pointBuffer);
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
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Capacity, Unsafe.SizeOf<float4>());
                Properties.SetBuffer(ShaderIds.PointsId, pointComputeBuffer);
            }

            pointComputeBuffer.SetData(pointBuffer.AsArray(), 0, 0, Size);
            //pointBuffer.AsArray().AsSpan().Fill(default);
        }

        void CalculateBounds()
        {
            if (Size == 0)
            {
                CalculateBoundsEmpty();
                return;
            }

            MinMaxJobs.CalculateBounds(pointBuffer, out var bounds, out var span);
            Collider.center = bounds.center;
            Collider.size = bounds.size + ElementScale * Vector3.one;
            MeasuredIntensityBounds = span;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = span;
            }

            RaiseBoundsChanged();
        }

        void CalculateBoundsEmpty()
        {
            Collider.SetLocalBounds(default);
            MeasuredIntensityBounds = Vector2.zero;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = Vector2.zero;
            }

            RaiseBoundsChanged();
        }
        
        void RaiseBoundsChanged()
        {
            BoundsChanged.TryRaise(this);
        }        

        protected override void Rebuild()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(ShaderIds.PointsId, (ComputeBuffer?)null);
            }

            if (pointBuffer.Capacity != 0)
            {
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Capacity, Unsafe.SizeOf<float4>());
                pointComputeBuffer.SetData(pointBuffer.AsArray(), 0, 0, Size);
                Properties.SetBuffer(ShaderIds.PointsId, pointComputeBuffer);
            }

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
        }

        public override void Suspend()
        {
            base.Suspend();

            pointBuffer.Clear();
            pointBuffer.Reset();

            pointComputeBuffer?.Release();
            pointComputeBuffer = null;
            Properties.SetBuffer(ShaderIds.PointsId, (ComputeBuffer?)null);
            BoundsChanged = null;
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