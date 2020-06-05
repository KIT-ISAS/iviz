using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Iviz.Resources;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class MeshListResource : MarkerResource
    {
        Material material;

        NativeArray<float4> pointBuffer = new NativeArray<float4>();
        ComputeBuffer pointComputeBuffer;

        readonly uint[] argsBuffer = new uint[5] { 0, 0, 0, 0, 0 };
        ComputeBuffer argsComputeBuffer;

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

        public Color Color { get; set; } = Color.white;

        /*
        void SetCapacity(int reqDataSize)
        {
            if (pointBuffer == null || pointBuffer.Length < reqDataSize)
            {
                pointBuffer = new PointWithColor[reqDataSize];

                if (pointComputeBuffer != null)
                {
                    pointComputeBuffer.Release();
                }
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Length, Marshal.SizeOf<PointWithColor>());
                material.SetBuffer(PropPoints, pointComputeBuffer);
            }
        }
        */

        bool useIntensityTexture;
        public bool UseIntensityTexture
        {
            get => useIntensityTexture;
            set
            {
                if (useIntensityTexture == value)
                {
                    return;
                }
                useIntensityTexture = value;
                UpdateMaterialKeywords();
            }
        }

        bool perVertexScale;
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

        void UpdateMaterialKeywords()
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

        static readonly int PropIntensity = Shader.PropertyToID("_IntensityTexture");

        Resource.ColormapId colormap;
        public Resource.ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;

                Texture2D texture = Resource.Colormaps.Textures[Colormap];
                material.SetTexture(PropIntensity, texture);
            }
        }

        static readonly int PropIntensityCoeff = Shader.PropertyToID("_IntensityCoeff");
        static readonly int PropIntensityAdd = Shader.PropertyToID("_IntensityAdd");

        Vector2 intensityBounds;
        public Vector2 IntensityBounds
        {
            get => intensityBounds;
            set
            {
                intensityBounds = value;
                float intensitySpan = intensityBounds.y - intensityBounds.x;

                if (intensitySpan == 0)
                {
                    material.SetFloat(PropIntensityCoeff, 1);
                    material.SetFloat(PropIntensityAdd, 0);
                }
                else
                {
                    if (FlipMinMax)
                    {
                        material.SetFloat(PropIntensityCoeff, 1 / intensitySpan);
                        material.SetFloat(PropIntensityAdd, -intensityBounds.x / intensitySpan);
                    }
                    else
                    {
                        material.SetFloat(PropIntensityCoeff, -1 / intensitySpan);
                        material.SetFloat(PropIntensityAdd, intensityBounds.y / intensitySpan);
                    }
                }
            }
        }

        bool flipMinMax;
        public bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
                IntensityBounds = IntensityBounds;
            }
        }

        public void Set(IList<Vector3> points, IList<Color> colors = null, Color? color = null)
        {
            Size = points.Count;
            int realSize = 0;
            if (colors == null || points.Count != colors.Count)
            {
                float intensity = new PointWithColor(Vector3.zero, color ?? Color.white).Intensity;
                for (int i = 0; i < points.Count; i++)
                {
                    if (float.IsNaN(points[i].x) ||
                        float.IsNaN(points[i].y) ||
                        float.IsNaN(points[i].z))
                    {
                        continue;
                    }
                    pointBuffer[realSize++] = new PointWithColor(points[i], intensity);
                }
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (float.IsNaN(points[i].x) ||
                        float.IsNaN(points[i].y) ||
                        float.IsNaN(points[i].z))
                    {
                        continue;
                    }
                    pointBuffer[realSize++] = new PointWithColor(points[i], colors[i]);
                }
            }
            Size = realSize;
            UpdateBuffer();
        }

        static readonly int PropPoints = Shader.PropertyToID("_Points");

        int size_;
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

                argsBuffer[1] = (uint)size_;
                argsComputeBuffer.SetData(argsBuffer);

                int reqDataSize = size_ * 11 / 10;
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
            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(pointBuffer, Size, out Bounds bounds, out Vector2 _);
            Collider.center = bounds.center;
            Collider.size = bounds.size + Scale;
        }


        static readonly int PropLocalScale = Shader.PropertyToID("_LocalScale");

        Vector3 scale;
        public Vector3 Scale
        {
            get => scale;
            set
            {
                scale = value;
                material.SetVector(PropLocalScale, new Vector4(scale.x, scale.y, scale.z, 1));
            }
        }

        static readonly int PropLocalOffset = Shader.PropertyToID("_LocalOffset");

        Vector3 offset;
        public Vector3 Offset
        {
            get => offset;
            set
            {
                offset = value;
                material.SetVector(PropLocalOffset, new Vector4(offset.x, offset.y, offset.z, 0));
            }
        }

        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                if (value)
                {
                    OnApplicationFocus(true);
                }
            }
        }

        public override string Name => "MeshListResource";

        protected override void Awake()
        {
            base.Awake();

            material = Resource.Materials.MeshList.Instantiate();
            material.enableInstancing = true;
            UpdateMaterialKeywords();

            Mesh = Resource.Markers.SphereSimple.Object.GetComponent<MeshFilter>().sharedMesh;
            Scale = new Vector3(1, 1, 1);
            Offset = new Vector3(0, 0, 0);
            IntensityBounds = new Vector2(0, 1);
            Colormap = Resource.ColormapId.gray;

            argsComputeBuffer = new ComputeBuffer(1, argsBuffer.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
            argsComputeBuffer.SetData(argsBuffer);
        }

        static readonly int PropLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PropBoundaryCenter = Shader.PropertyToID("_BoundaryCenter");

        void Update()
        {
            if (Size == 0)
            {
                return;
            }
            material.SetMatrix(PropLocalToWorld, transform.localToWorldMatrix);
            Bounds worldBounds = Collider.bounds;
            material.SetVector(PropBoundaryCenter, worldBounds.center);
            Graphics.DrawMeshInstancedIndirect(mesh, 0, material, worldBounds, argsComputeBuffer);
        }

        void OnDestroy()
        {
            if (material != null)
            {
                Destroy(material);
            }
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
        }



        void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }
            // unity bug causes all compute buffers to disappear when focus is lost
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
        }
    }
}