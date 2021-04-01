using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace Iviz.Displays
{
    /// <summary>
    /// Displays multiple copies of a given mesh. 
    /// </summary>
    public sealed class MeshListResource : MarkerResourceWithColormap, ISupportsAROcclusion
    {
        const float MaxPositionMagnitude = 1e3f;

        static readonly int BoundaryCenterID = Shader.PropertyToID("_BoundaryCenter");
        static readonly int PointsID = Shader.PropertyToID("_Points");
        static readonly int PropLocalScale = Shader.PropertyToID("_LocalScale");
        static readonly int PropLocalOffset = Shader.PropertyToID("_LocalOffset");

        [SerializeField] Vector3 elementScale3;
        [SerializeField] Vector3 preTranslation;
        [SerializeField] Mesh mesh;

        readonly uint[] argsBuffer = {0, 0, 0, 0, 0};
        [CanBeNull] ComputeBuffer argsComputeBuffer;
        [CanBeNull] Info<GameObject> meshResource;

        NativeList<float4> pointBuffer;
        
        [CanBeNull] ComputeBuffer pointComputeBuffer;
        bool useIntensityForScaleY;
        bool useIntensityForAllScales;

        [NotNull]
        Mesh Mesh
        {
            get => mesh != null ? mesh : throw new NullReferenceException("Mesh has not been set!");
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Cannot set a null mesh!");
                }

                mesh = value;

                argsBuffer[0] = mesh.GetIndexCount(0);
                argsBuffer[2] = mesh.GetIndexStart(0);
                argsBuffer[3] = mesh.GetBaseVertex(0);
            }
        }

        /// <summary>
        /// The resource to be multiplied.
        /// </summary>
        [CanBeNull]
        public Info<GameObject> MeshResource
        {
            get => meshResource;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                var meshContainer = value.Object.GetComponent<MeshMarkerResource>();

                Mesh tmpMesh;
                if (meshContainer == null || (tmpMesh = meshContainer.Mesh) == null)
                {
                    throw new ArgumentException("Object does not contain a mesh!");
                }

                meshResource = value;
                Mesh = tmpMesh;
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
        public bool CastShadows { get; set; } = true;

        public override float ElementScale
        {
            get => base.ElementScale;
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

        protected override void Awake()
        {
            base.Awake();

            pointBuffer = new NativeList<float4>(Allocator.Persistent);

            UseColormap = true;
            MeshResource = Resource.Displays.Sphere;
            ElementScale3 = Vector3.one;
            IntensityBounds = new Vector2(0, 1);
            Colormap = Resource.ColormapId.gray;

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

            Bounds worldBounds = BoxCollider.bounds;
            Properties.SetVector(BoundaryCenterID, worldBounds.center);

            Material material = FindMaterial();

            Graphics.DrawMeshInstancedIndirect(mesh, 0, material, worldBounds, argsComputeBuffer,
                0, Properties, CastShadows && !OcclusionOnly ? ShadowCastingMode.On : ShadowCastingMode.Off);
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

            pointBuffer.Clear();

            pointComputeBuffer?.Release();
            pointComputeBuffer = null;
            Properties.SetBuffer(PointsID, (ComputeBuffer) null);
        }

        /// <summary>
        /// Sets the instance positions and colors with the given enumeration.
        /// </summary>
        /// <param name="points">The list of positions and colors.</param>
        public void Set([NotNull] List<PointWithColor> points)
        {
            pointBuffer.Capacity = Math.Max(pointBuffer.Capacity, points.Count);

            pointBuffer.Clear();
            foreach (PointWithColor t in points)
            {
                if (t.HasNaN() || t.Position.MaxAbsCoeff() > MaxPositionMagnitude)
                {
                    continue;
                }

                pointBuffer.Add(t);
            }

            UpdateBuffer();
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
        public void SetDirect(in NativeArray<float4> points)
        {
            pointBuffer.Clear();
            pointBuffer.AddRange(points);
            UpdateBuffer();
        }

        public void SetDirect([NotNull] PointListResource.DirectPointSetter callback, int reserve = 0)
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
            UpdateBuffer();
        }

        void UpdateScale()
        {
            var realScale = new Vector4(
                ElementScale * elementScale3.x,
                ElementScale * elementScale3.y,
                ElementScale * elementScale3.z,
                1);
            Properties.SetVector(PropLocalScale, realScale);

            preTranslation = UseIntensityForScaleY ? (ElementScale * ElementScale3.y / 2) * Vector3.up : Vector3.zero;
            Properties.SetVector(PropLocalOffset, preTranslation);
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
            MinMaxJob.CalculateBounds(pointBuffer, Size, out Bounds pointBounds, out Vector2 span);

            bool isSinglePassStereo = XRSettings.eyeTextureDesc.vrUsage == VRTextureUsage.TwoEyes;
            int instanceCount = isSinglePassStereo ? 2 * Size : Size;

            argsBuffer[1] = (uint) instanceCount;
            argsComputeBuffer?.SetData(argsBuffer);

            Vector3 meshScale = ElementScale * ElementScale3;
            if (UseIntensityForScaleY)
            {
                meshScale.y *= Mathf.Max(Mathf.Abs(span.x), Mathf.Abs(span.y));
            }

            Bounds meshBounds = mesh.bounds;
            meshBounds.center = Vector3.Scale(meshBounds.center + preTranslation, meshScale);
            meshBounds.size = Vector3.Scale(meshBounds.size, meshScale);

            BoxCollider.size = pointBounds.size + meshBounds.size;
            BoxCollider.center = pointBounds.center + meshBounds.center;
            IntensityBounds = span;
        }

        protected override void Rebuild()
        {
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
                Properties.SetBuffer(PointsID, (ComputeBuffer) null);
            }

            if (pointBuffer.Capacity != 0)
            {
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Capacity, Marshal.SizeOf<PointWithColor>());
                pointComputeBuffer.SetData(pointBuffer.AsArray(), 0, 0, Size);
                Properties.SetBuffer(PointsID, pointComputeBuffer);
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