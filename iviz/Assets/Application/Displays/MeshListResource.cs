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
        NativeArray<float4> pointBuffer = new NativeArray<float4>();
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

        [SerializeField] bool perVertexScale_;
        public bool UsePerVertexScale
        {
            get => perVertexScale_;
            set
            {
                if (perVertexScale_ == value)
                {
                    return;
                }
                perVertexScale_ = value;
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

        [SerializeField] int size_;
        public int Size
        {
            get => size_;
            private set
            {
                if (value == size_)
                {
                    return;
                }
                size_ = value;

                Reserve(size_ * 11 / 10);

                argsBuffer[1] = (uint)size_;
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

                if (pointComputeBuffer != null)
                {
                    pointComputeBuffer.Release();
                }
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
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].HasNaN)
                    {
                        continue;
                    }
                    pointBuffer[realSize] = value[i];
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
            Collider.size = bounds.size + Scale;
            Collider.center = new Vector3(bounds.center.x, bounds.center.y + Collider.size.y / 2, bounds.center.z);
            IntensityBounds = span;
        }


        static readonly int PropLocalScale = Shader.PropertyToID("_LocalScale");

        [SerializeField] Vector3 scale_;
        public Vector3 Scale
        {
            get => scale_;
            set
            {
                scale_ = value;
                material.SetVector(PropLocalScale, new Vector4(scale_.x, scale_.y, scale_.z, 1));
            }
        }

        static readonly int PropLocalOffset = Shader.PropertyToID("_LocalOffset");

        [SerializeField] Vector3 offset_;
        public Vector3 Offset
        {
            get => offset_;
            set
            {
                offset_ = value;
                material.SetVector(PropLocalOffset, offset_);
            }
        }

        [SerializeField] bool occlussionOnly_;
        public bool OcclusionOnly
        {
            get => occlussionOnly_;
            set
            {
                if (occlussionOnly_ == value)
                {
                    return;
                }
                occlussionOnly_ = value;

                if (value)
                {
                    if (occlusionMaterial == null)
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
            Scale = new Vector3(1, 1, 1);
            Offset = new Vector3(0, 0, 0);
            IntensityBounds = new Vector2(0, 1);
            Colormap = Resource.ColormapId.gray;

            argsComputeBuffer = new ComputeBuffer(1, argsBuffer.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
            argsComputeBuffer.SetData(argsBuffer);

            OcclusionOnly = true;
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }
            UpdateTransform();
            Bounds worldBounds = Collider.bounds;
            material.SetVector(PropBoundaryCenter, worldBounds.center);

            if (CastShadows && !OcclusionOnly)
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

            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
            }
            if (argsComputeBuffer != null)
            {
                argsComputeBuffer.Release();
            }
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
            Scale = Scale;
            Offset = Offset;
            Colormap = Colormap;
            UpdateMaterialKeywords();
        }

        public override void Stop()
        {
            base.Stop();
            Size = 0;
            OcclusionOnly = false;
        }
    }
}