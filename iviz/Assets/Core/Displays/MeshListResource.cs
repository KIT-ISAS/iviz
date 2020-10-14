using System;
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
    /// Displays multiple copies of a given mesh. 
    /// </summary>
    public sealed class MeshListResource : MarkerResourceWithColormap, ISupportsAROcclusion
    {
        static readonly int BoundaryCenterID = Shader.PropertyToID("_BoundaryCenter");
        static readonly int PointsID = Shader.PropertyToID("_Points");
        static readonly int PropLocalScale = Shader.PropertyToID("_LocalScale");
        static readonly int PropLocalOffset = Shader.PropertyToID("_LocalOffset");

        [SerializeField] int size;
        [SerializeField] Vector3 elementScale3;
        [SerializeField] Vector3 preTranslation;
        [SerializeField] Mesh mesh;
        bool useIntensityForScaleY;


        readonly uint[] argsBuffer = {0, 0, 0, 0, 0};
        ComputeBuffer argsComputeBuffer;
        Info<GameObject> meshResource;
        NativeArray<float4> pointBuffer;
        ComputeBuffer pointComputeBuffer;

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

                argsBuffer[1] = (uint) size;
                argsComputeBuffer.SetData(argsBuffer);
            }
        }

        /// <summary>
        /// Whether to enable shadows. Displayed shadows can get bugged if the number of instances is too high.
        /// </summary>
        public bool CastShadows { get; set; } = true;

        /// <summary>
        /// Sets the instance positions and colors with the given collection.
        /// </summary>
        public IReadOnlyCollection<PointWithColor> PointsWithColor
        {
            get => new PointListResource.PointGetHelper(pointBuffer);
            set => Set(value.Count, value);
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
            Size = 0;
            OcclusionOnly = false;
            UseIntensityForScaleY = false;
            
            pointComputeBuffer?.Release();
            pointComputeBuffer = null;
            Properties.SetBuffer(PointsID, null);
        }

        static bool IsValid(PointWithColor t) => !t.HasNaN() && t.Position.sqrMagnitude < 1e9f;
        
        /// <summary>
        /// Sets the instance positions and colors with the given enumeration.
        /// </summary>
        /// <param name="count">The number of instances, or an upper bound.</param>
        /// <param name="points">The list of positions and colors.</param>
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

            var realSize = 0;
            foreach (var point in points)
            {
                if (!IsValid(point)) {continue;}
                pointBuffer[realSize] = point;
                realSize++;
            }

            Size = realSize;
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
            
            preTranslation = UseIntensityForScaleY ? ElementScale * ElementScale3.y * Vector3.up : Vector3.zero;
            Properties.SetVector(PropLocalOffset, preTranslation);
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

        void UpdateBuffer()
        {
            if (Size == 0)
            {
                return;
            }

            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(pointBuffer, Size, out Bounds pointBounds, out Vector2 span);

            Vector3 meshScale = ElementScale * ElementScale3;
            if (UseIntensityForScaleY)
            {
                meshScale.y *= Mathf.Max(Mathf.Abs(span.x), Mathf.Abs(span.y));
            }
            
            Bounds meshBounds = mesh.bounds;
            meshBounds.center =  Vector3.Scale(meshBounds.center + preTranslation, meshScale);
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
            }

            if (pointBuffer.Length != 0)
            {
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Length, Marshal.SizeOf<PointWithColor>());
                pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
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