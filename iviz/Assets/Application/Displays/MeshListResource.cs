using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.Resources;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    public sealed class MeshListResource : MarkerResourceWithColormap, ISupportsAROcclusion
    {
        NativeArray<float4> pointBuffer;
        ComputeBuffer pointComputeBuffer;

        readonly uint[] argsBuffer = new uint[5] { 0, 0, 0, 0, 0 };
        ComputeBuffer argsComputeBuffer;

        Material normalMaterial;
        Material occlusionMaterial;

        Mesh mesh;
        public Mesh Mesh
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

        [SerializeField] bool perVertexScale;
        public bool UsePerVertexScale
        {
            get => perVertexScale;
            set
            {
                if (perVertexScale == value)
                {
                    return;
                }
                perVertexScale = value;
                UpdateMaterialKeywords();
            }
        }

        protected override void UpdateMaterialKeywords()
        {
            if (UsePerVertexScale && UseIntensityTexture)
            {
                material.DisableKeyword("USE_TEXTURE");
                material.EnableKeyword("USE_TEXTURE_SCALE");
            }
            else if (UseIntensityTexture)
            {
                material.DisableKeyword("USE_TEXTURE_SCALE");
                material.EnableKeyword("USE_TEXTURE");
            }
            else
            {
                material.DisableKeyword("USE_TEXTURE_SCALE");
                material.DisableKeyword("USE_TEXTURE");
            }
        }

        [SerializeField] int size;
        public int Size
        {
            get => size;
            private set
            {
                if (value == size)
                {
                    return;
                }
                size = value;

                Reserve(size * 11 / 10);

                argsBuffer[1] = (uint)size;
                argsComputeBuffer.SetData(argsBuffer);
            }
        }

        public bool CastShadows { get; set; } = true;        

        public void Reserve(int reqDataSize)
        {
            if (pointBuffer.Length < reqDataSize)
            {
                if (pointBuffer.Length != 0)
                {
                    pointBuffer.Dispose();
                }
                pointBuffer = new NativeArray<float4>(reqDataSize, Allocator.Persistent);

                pointComputeBuffer?.Release();
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Length, Marshal.SizeOf<PointWithColor>());
                material.SetBuffer(PropPoints, pointComputeBuffer);
            }
        }

        public IList<PointWithColor> PointsWithColor
        {
            set
            {
                Size = value.Count;

                int realSize = 0;
                foreach (var point in value)
                {
                    if (point.HasNaN)
                    {
                        continue;
                    }
                    pointBuffer[realSize] = point;
                    realSize++;
                }
                Size = realSize;
                UpdateBuffer();
            }
        }

        void UpdateBuffer()
        {
            if (Size == 0)
            {
                return;
            }
            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(pointBuffer, Size, out Bounds bounds, out Vector2 span);
            boxCollider.size = bounds.size + Scale3;
            boxCollider.center = new Vector3(bounds.center.x, bounds.center.y + boxCollider.size.y / 2, bounds.center.z);
            IntensityBounds = span;
        }


        static readonly int PropLocalScale = Shader.PropertyToID("_LocalScale");

        [SerializeField] Vector3 scale3;
        public Vector3 Scale3
        {
            get => scale3;
            set
            {
                scale3 = value;
                material.SetVector(PropLocalScale, new Vector4(scale3.x, scale3.y, scale3.z, 1));
            }
        }

        static readonly int PropLocalOffset = Shader.PropertyToID("_LocalOffset");

        [SerializeField] Vector3 offset;
        public Vector3 Offset
        {
            get => offset;
            set
            {
                offset = value;
                material.SetVector(PropLocalOffset, offset);
            }
        }

        [SerializeField] bool occlusionOnly;
        public bool OcclusionOnlyActive
        {
            get => occlusionOnly;
            set
            {
                if (occlusionOnly == value)
                {
                    return;
                }
                occlusionOnly = value;

                if (value)
                {
                    if (occlusionMaterial is null)
                    {
                        occlusionMaterial = Resource.Materials.MeshListOcclusionOnly.Instantiate();
                        occlusionMaterial.enableInstancing = true;
                    }
                    material = occlusionMaterial;
                }
                else
                {
                    material = normalMaterial;
                }
                Rebuild();
            }
        }

        protected override void Awake()
        {
            normalMaterial = Resource.Materials.MeshList.Instantiate();
            normalMaterial.enableInstancing = true;
            material = normalMaterial;

            base.Awake();

            UseIntensityTexture = true;
            UpdateMaterialKeywords();

            Mesh = Resource.Displays.SphereSimple.Object.GetComponent<MeshFilter>().sharedMesh;
            Scale3 = Vector3.one;
            Offset = Vector3.zero;
            IntensityBounds = new Vector2(0, 1);
            Colormap = Resource.ColormapId.gray;

            argsComputeBuffer = new ComputeBuffer(1, argsBuffer.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
            argsComputeBuffer.SetData(argsBuffer);

            OcclusionOnlyActive = false;
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }
            UpdateTransform();
            Bounds worldBounds = boxCollider.bounds;
            material.SetVector(PropBoundaryCenter, worldBounds.center);

            if (CastShadows && !OcclusionOnlyActive)
            {
                Graphics.DrawMeshInstancedIndirect(mesh, 0, material, worldBounds, argsComputeBuffer);
            }
            else
            {
                Graphics.DrawMeshInstancedIndirect(mesh, 0, material, worldBounds, argsComputeBuffer, 
                    0, null, ShadowCastingMode.Off);
            }
        }

        protected override void OnDestroy()
        {
            material = null;
            base.OnDestroy();

            pointComputeBuffer?.Release();
            argsComputeBuffer?.Release();
            if (pointBuffer.Length > 0)
            {
                pointBuffer.Dispose();
            }
            if (occlusionMaterial != null)
            {
                Destroy(occlusionMaterial);
            }
            if (normalMaterial != null)
            {
                Destroy(normalMaterial);
            }
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
                material.SetBuffer(PropPoints, pointComputeBuffer);
            }
            if (argsComputeBuffer != null)
            {
                argsComputeBuffer.Release();
                argsComputeBuffer = null;

                argsComputeBuffer = new ComputeBuffer(1, argsBuffer.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
                argsComputeBuffer.SetData(argsBuffer);

                //Debug.Log(string.Join(",", pointBuffer));
            }
            IntensityBounds = IntensityBounds;
            Scale3 = Scale3;
            Offset = Offset;
            Colormap = Colormap;
            UpdateMaterialKeywords();
        }

        public override void Suspend()
        {
            base.Suspend();
            Size = 0;
            OcclusionOnlyActive = false;
        }
    }
}