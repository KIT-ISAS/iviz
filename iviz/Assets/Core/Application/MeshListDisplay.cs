#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace Iviz.Displays
{
    /// <summary>
    /// Displays multiple copies of a given mesh. 
    /// </summary>
    public sealed class MeshListDisplay : MarkerDisplayWithColormap, ISupportsAROcclusion, ISupportsShadows
    {
        [SerializeField] Vector3 elementScale3;
        [SerializeField] Vector3 preTranslation;
        [SerializeField] Mesh? mesh;

        readonly uint[] argsBuffer = { 0, 0, 0, 0, 0 };
        ComputeBuffer? argsComputeBuffer;
        ResourceKey<GameObject>? meshResource;

        readonly NativeList<float4> pointBuffer = new();

        ComputeBuffer? pointComputeBuffer;
        bool useIntensityForScaleY;
        bool useIntensityForAllScales;

        Mesh Mesh
        {
            get => mesh != null ? mesh : throw new NullReferenceException("Mesh has not been set!");
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                mesh = value;

                argsBuffer[0] = mesh.GetIndexCount(0);
                argsBuffer[2] = mesh.GetIndexStart(0);
                argsBuffer[3] = mesh.GetBaseVertex(0);
            }
        }

        /// <summary>
        /// The resource to be multiplied.
        /// </summary>
        public ResourceKey<GameObject>? MeshResource
        {
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));

                var meshContainer = value.Object.AssertHasComponent<MeshMarkerDisplay>(nameof(value.Object));

                Mesh = meshContainer.Mesh;
                meshResource = value;
            }
        }

        /// <summary>
        /// Whether the intensity should be used as a scale for the y-axis. Useful for bar graphs like occupancy grids.
        /// </summary>
        public bool UseIntensityForScaleY
        {
            get => useIntensityForScaleY;
            set
            {
                if (value)
                {
                    useIntensityForAllScales = false;
                }

                useIntensityForScaleY = value;
                UpdateScale();
            }
        }

        public bool UseIntensityForAllScales
        {
            get => useIntensityForAllScales;
            set
            {
                if (value)
                {
                    useIntensityForScaleY = false;
                }

                useIntensityForAllScales = value;
                UpdateScale();
            }
        }

        public int Size => pointBuffer.Length;

        /// <summary>
        /// Whether to enable shadows. Displayed shadows can get bugged if the number of instances is too high.
        /// </summary>
        public bool EnableShadows { get; set; } = true;

        public override float ElementScale
        {
            protected get => base.ElementScale;
            set
            {
                base.ElementScale = value;
                UpdateScale();
            }
        }

        /// <summary>
        /// Sets the scale of each instance for each axis. Gets multiplied by ElementScale.
        /// </summary>
        public Vector3 ElementScale3
        {
            get => elementScale3;
            set
            {
                elementScale3 = value;
                UpdateScale();
            }
        }

        public override Bounds? Bounds => Size == 0 ? null : base.Bounds;

        protected override void Awake()
        {
            base.Awake();

            UseColormap = true;
            MeshResource = Resource.Displays.Sphere;
            ElementScale3 = Vector3.one;
            Colormap = ColormapId.gray;

            argsComputeBuffer =
                new ComputeBuffer(1, argsBuffer.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
            argsComputeBuffer.SetData(argsBuffer);

            OcclusionOnly = false;
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            UpdateTransform();

            var worldBounds = Collider.bounds;
            Properties.SetVector(ShaderIds.BoundaryCenterId, worldBounds.center);

            var material = FindMaterial();

            Graphics.DrawMeshInstancedIndirect(mesh, 0, material, worldBounds, argsComputeBuffer,
                0, Properties, EnableShadows && !OcclusionOnly ? ShadowCastingMode.On : ShadowCastingMode.Off);
        }

        Material FindMaterial()
        {
            if (OcclusionOnly && UseIntensityForScaleY)
            {
                return Resource.Materials.MeshListOcclusionOnlyWithScaleY.Object;
            }

            if (OcclusionOnly && UseIntensityForAllScales)
            {
                return Resource.Materials.MeshListOcclusionOnlyWithScaleAll.Object;
            }

            if (OcclusionOnly)
            {
                return Resource.Materials.MeshListOcclusionOnly.Object;
            }

            if (UseIntensityForScaleY)
            {
                return Resource.Materials.MeshListWithColormapScaleY.Object;
            }

            if (UseIntensityForAllScales)
            {
                return Resource.Materials.MeshListWithColormapScaleAll.Object;
            }

            if (UseColormap)
            {
                return Resource.Materials.MeshListWithColormap.Object;
            }

            return Resource.Materials.MeshList.Object;
        }

        void OnDestroy()
        {
            pointComputeBuffer?.Release();
            argsComputeBuffer?.Release();
            pointBuffer.Dispose();
        }

        public bool OcclusionOnly { get; set; }

        public override void Suspend()
        {
            base.Suspend();
            OcclusionOnly = false;
            UseIntensityForScaleY = false;

            pointBuffer.Reset();

            pointComputeBuffer?.Release();
            pointComputeBuffer = null;
            Properties.SetBuffer(ShaderIds.PointsId, (ComputeBuffer?)null);
        }

        /// <summary>
        /// Sets the instance positions and colors with the given enumeration.
        /// </summary>
        /// <param name="points">The list of positions and colors.</param>
        public void Set(ReadOnlySpan<PointWithColor> points)
        {
            Set(MemoryMarshal.Cast<PointWithColor, float4>(points));
        }

        public void Reset()
        {
            pointBuffer.Clear();
            UpdateBuffer();
        }

        /// <summary>
        /// Copies the array directly without checking.
        /// </summary>
        /// <param name="points">A native array with the positions and colors.</param>        
        public void Set(ReadOnlySpan<float4> points)
        {
            pointBuffer.Clear();
            pointBuffer.AddRange(points);
            UpdateBuffer();
        }

        public void Set(Action<NativeList<float4>> callback, int reserve = 0)
        {
            ThrowHelper.ThrowIfNull(callback, nameof(callback));

            if (reserve != 0)
            {
                pointBuffer.EnsureCapacity(reserve);
            }

            pointBuffer.Clear();
            callback(pointBuffer);
            UpdateBuffer();
        }

        void UpdateScale()
        {
            var realScale = new Vector4(
                ElementScale * elementScale3.x,
                ElementScale * elementScale3.y,
                ElementScale * elementScale3.z,
                1);
            Properties.SetVector(ShaderIds.LocalScaleId, realScale);

            preTranslation = UseIntensityForScaleY ? (ElementScale * ElementScale3.y / 2) * Vector3.up : Vector3.zero;
            Properties.SetVector(ShaderIds.LocalOffsetId, preTranslation);
        }

        void UpdateBuffer()
        {
            if (Size == 0)
            {
                Collider.SetLocalBounds(default);
                MeasuredIntensityBounds = Vector2.zero;
                if (!OverrideIntensityBounds)
                {
                    IntensityBounds = Vector2.zero;
                }

                return;
            }

            if (pointComputeBuffer == null || pointComputeBuffer.count < pointBuffer.Capacity)
            {
                pointComputeBuffer?.Release();
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Capacity, Unsafe.SizeOf<float4>(),
                    ComputeBufferType.Default, ComputeBufferMode.Dynamic);
                Properties.SetBuffer(ShaderIds.PointsId, pointComputeBuffer);
            }

            pointComputeBuffer.SetData(pointBuffer.AsArray(), 0, 0, Size);
            MinMaxJobs.CalculateBounds(pointBuffer, out Bounds pointBounds, out Vector2 span);

            bool isSinglePassStereo = XRSettings.eyeTextureDesc.vrUsage == VRTextureUsage.TwoEyes;
            int instanceCount = isSinglePassStereo ? 2 * Size : Size;

            argsBuffer[1] = (uint)instanceCount;
            argsComputeBuffer?.SetData(argsBuffer);

            Vector3 meshScale = ElementScale * ElementScale3;
            if (UseIntensityForScaleY)
            {
                meshScale.y *= Mathf.Max(Mathf.Abs(span.x), Mathf.Abs(span.y));
            }

            var baseMeshBounds = Mesh.bounds;
            var meshBounds = new Bounds
            {
                center = meshScale.Mult(baseMeshBounds.center + preTranslation),
                size = meshScale.Mult(baseMeshBounds.size)
            };

            Collider.size = pointBounds.size + meshBounds.size;
            Collider.center = pointBounds.center + meshBounds.center;

            MeasuredIntensityBounds = span;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = span;
            }
        }

        protected override void UpdateProperties()
        {
        }

        public override string ToString() => $"[{nameof(MeshListDisplay)}]";

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

            if (argsComputeBuffer != null)
            {
                argsComputeBuffer.Release();
                argsComputeBuffer = null;

                argsComputeBuffer = new ComputeBuffer(1, argsBuffer.Length * sizeof(uint),
                    ComputeBufferType.IndirectArguments);
                argsComputeBuffer.SetData(argsBuffer);
            }

            IntensityBounds = IntensityBounds;
            ElementScale3 = ElementScale3;
            Colormap = Colormap;
        }
    }
}