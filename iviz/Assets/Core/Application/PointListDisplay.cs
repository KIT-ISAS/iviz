#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Common;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using Iviz.Tools;
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

        bool isDirty;

        Mesh? mesh;

        [SerializeField] MeshRenderer? meshRenderer;
        [SerializeField] MeshFilter? meshFilter;
        ComputeBuffer? pointComputeBuffer;

        Material? currentMaterial;

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
            if (!Settings.SupportsComputeBuffers)
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

            Properties.SetFloat(ShaderIds.ScaleId, ElementScale * transform.lossyScale.x);

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

            Bounds worldBounds = WorldBounds;
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
                ref float4 pPtr = ref pointBuffer.GetReference();
                ref Vector3 vPtr = ref vertices.Array[0];

                if (UseColormap)
                {
                    using var uvs = new Rent<Vector2>(pointsLength);
                    ref Vector2 uvsPtr = ref uvs.Array[0];
                    for (int i = 0; i < pointsLength; i++)
                    {
                        //ref readonly var p = ref points[i];
                        //ref var v = ref vArray[i];
                        vPtr.x = pPtr.x;
                        vPtr.y = pPtr.y;
                        vPtr.z = pPtr.z;
                        uvsPtr.x = pPtr.w;

                        pPtr = ref pPtr.Plus(1);
                        vPtr = ref vPtr.Plus(1);
                        uvsPtr = ref uvsPtr.Plus(1);
                    }

                    mesh.SetVertices(vertices);
                    mesh.SetUVs(uvs);
                }
                else
                {
                    using var colors = new Rent<Color32>(pointsLength);
                    ref float cPtr = ref Unsafe.As<Color32, float>(ref colors[0]);
                    for (int i = 0; i < pointsLength; i++)
                    {
                        //ref readonly var p = ref points[i];
                        //ref var v = ref vArray[i];
                        vPtr.x = pPtr.x;
                        vPtr.y = pPtr.y;
                        vPtr.z = pPtr.z;
                        cPtr = pPtr.w;
                        
                        pPtr = ref pPtr.Plus(1);
                        vPtr = ref vPtr.Plus(1);
                        cPtr = ref cPtr.Plus(1);
                    }

                    mesh.SetVertices(vertices);
                    mesh.SetColors(colors);
                }
            }

            using (var indices = new Rent<int>(pointsLength))
            {
                ref int iPtr = ref indices.Array[0];
                for (int i = 0; i < pointsLength; i++)
                {
                    iPtr = i;
                    iPtr = ref iPtr.Plus(1);
                    //iArray[i] = i;
                }

                mesh.SetIndices(indices, MeshTopology.Points, 0);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsElementValid(in Point t)
        {
            return !(t.IsInvalid() || t.MaxAbsCoeff() > MaxPositionMagnitude);
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
        }

        void CalculateBounds()
        {
            if (Size == 0)
            {
                CalculateBoundsEmpty();
                return;
            }

            MinMaxJob.CalculateBounds(pointBuffer.AsArray(), Size, out Bounds bounds, out Vector2 span);
            Collider.center = bounds.center;
            Collider.size = bounds.size + ElementScale * Vector3.one;
            MeasuredIntensityBounds = span;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = span;
            }

            BoundsChanged?.Invoke();
        }

        void CalculateBoundsEmpty()
        {
            Collider.SetLocalBounds(default);
            MeasuredIntensityBounds = Vector2.zero;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = Vector2.zero;
            }

            BoundsChanged?.Invoke();
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
            pointBuffer.Trim();

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