using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    /// <summary>
    /// Displays multiple copies of a given mesh. 
    /// </summary>
    public sealed class MeshListResource : MarkerResourceWithColormap, ISupportsAROcclusion
    {
        const float MaxPositionMagnitudeSq = 1e9f;

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

        Mesh Mesh
        {
            get => mesh;
            set
            {
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
                useIntensityForScaleY = value;
                UpdateScale();
            }
        }

        int Size => pointBuffer.Length;

        /// <summary>
        /// Whether to enable shadows. Displayed shadows can get bugged if the number of instances is too high.
        /// </summary>
        public bool CastShadows { get; set; } = true;

        /// <summary>
        /// Sets the instance positions and colors with the given collection.
        /// </summary>
        [NotNull]
        public IReadOnlyCollection<PointWithColor> PointsWithColor
        {
            get => pointBuffer.Select(f => new PointWithColor(f)).ToArray();
            set => Set(value, value.Count);
        }

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

            Material material;
            if (OcclusionOnly && UseIntensityForScaleY)
            {
                material = Resource.Materials.MeshListOcclusionOnlyWithScaleY.Object;
            }
            else if (OcclusionOnly)
            {
                material = Resource.Materials.MeshListOcclusionOnly.Object;
            }
            else if (UseIntensityForScaleY)
            {
                material = Resource.Materials.MeshListWithColormapScaleY.Object;
            }
            else if (UseColormap)
            {
                material = Resource.Materials.MeshListWithColormap.Object;
            }
            else
            {
                material = Resource.Materials.MeshList.Object;
            }

            Graphics.DrawMeshInstancedIndirect(mesh, 0, material, worldBounds, argsComputeBuffer,
                0, Properties, CastShadows && !OcclusionOnly ? ShadowCastingMode.On : ShadowCastingMode.Off);
        }

        void OnDestroy()
        {
            pointComputeBuffer?.Release();
            argsComputeBuffer?.Release();
            if (pointBuffer.Length > 0)
            {
                pointBuffer.Dispose();
            }
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
            Properties.SetBuffer(PointsID, null);
        }

        /// <summary>
        /// Sets the instance positions and colors with the given enumeration.
        /// </summary>
        /// <param name="points">The list of positions and colors.</param>
        /// <param name="reserve">The number of points to reserve, or 0 if unknown.</param>
        public void Set([NotNull] IEnumerable<PointWithColor> points, int reserve = 0)
        {
            if (reserve < 0)
            {
                throw new ArgumentException($"Invalid reserve {reserve}", nameof(reserve));
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
                if (t.HasNaN() || t.Position.MagnitudeSq() > MaxPositionMagnitudeSq)
                {
                    continue;
                }

                pointBuffer.Add(t);
            }

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
            argsBuffer[1] = (uint) Size;

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
                Properties.SetBuffer(PointsID, null);
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