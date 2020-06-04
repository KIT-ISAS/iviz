using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.App;
using Iviz.Resources;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public readonly struct PointWithColor
    {
        readonly float4 f;

        public float X => f.x;
        public float Y => f.y;
        public float Z => f.z;
        public Vector3 Position => new Vector3(f.x, f.y, f.z);
        public Color32 Color
        {
            get
            {
                unsafe
                {
                    float w = f.w;
                    return *(Color32*)&w;
                }
            }

        }
        public float Intensity => f.w;

        public PointWithColor(Vector3 position, Color32 color)
        {
            f.x = position.x;
            f.y = position.y;
            f.z = position.z;
            unsafe
            {
                f.w = *(float*)&color;
            }
        }

        public PointWithColor(Vector3 position, float intensity)
        {
            f.x = position.x;
            f.y = position.y;
            f.z = position.z;
            f.w = intensity;
        }

        public PointWithColor(float x, float y, float z, float w)
        {
            f.x = x;
            f.y = y;
            f.z = z;
            f.w = w;
        }

        public PointWithColor(float4 f)
        {
            this.f = f;
        }

        public bool HasNaN => math.any(math.isnan(f));

        public static implicit operator float4(PointWithColor c) => c.f;

        public override string ToString()
        {
            return $"[x={X} y={Y} z={Z} i={Intensity} c={Color}]";
        }
    };

    public class PointListResource : MarkerResource
    {
        Material material;

        //PointWithColor[] pointBuffer = new PointWithColor[0];
        NativeArray<float4> pointBuffer = new NativeArray<float4>();
        ComputeBuffer pointComputeBuffer;
        ComputeBuffer quadComputeBuffer;

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
                if (useIntensityTexture)
                {
                    material.EnableKeyword("USE_TEXTURE");
                }
                else
                {
                    material.DisableKeyword("USE_TEXTURE");
                }
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
                    material.SetFloat(PropIntensityCoeff, 1 / intensitySpan);
                    material.SetFloat(PropIntensityAdd, -intensityBounds.x / intensitySpan);
                }
            }
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
                int reqDataSize = size_ * 11 / 10;
                if (pointBuffer.Length < reqDataSize)
                {
                    //pointBuffer = new PointWithColor[reqDataSize];
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
                    pointBuffer[realSize++] = value[i];
                }
                Size = realSize;
                UpdateBuffer();
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

        void UpdateBuffer()
        {
            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(pointBuffer, out Bounds bounds, out Vector2 span);
            Collider.center = bounds.center;
            Collider.size = bounds.size;
            IntensityBounds = span;
        }

        Vector2 scale;
        public Vector2 Scale
        {
            get => scale;
            set
            {
                if (scale != value)
                {
                    scale = value;
                    UpdateQuadComputeBuffer();
                }
            }
        }

        public override string Name => "PointList";

        protected override void Awake()
        {
            base.Awake();

            material = Resource.Materials.PointCloud.Instantiate();
            material.DisableKeyword("USE_TEXTURE");

            UseIntensityTexture = false;
            IntensityBounds = new Vector2(0, 1);

        }

        static readonly int PropQuad = Shader.PropertyToID("_Quad");

        void UpdateQuadComputeBuffer()
        {
            Vector2[] quad = {
                    Vector2.Scale(new Vector2( 0.5f,  0.5f), Scale),
                    Vector2.Scale(new Vector2( 0.5f, -0.5f), Scale),
                    Vector2.Scale(new Vector2(-0.5f, -0.5f), Scale),
                    Vector2.Scale(new Vector2(-0.5f,  0.5f), Scale),
            };
            if (quadComputeBuffer == null)
            {
                quadComputeBuffer = new ComputeBuffer(4, Marshal.SizeOf<Vector2>());
                material.SetBuffer(PropQuad, quadComputeBuffer);
            }
            quadComputeBuffer.SetData(quad, 0, 0, 4);
        }


        static readonly int PropLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PropWorldToLocal = Shader.PropertyToID("_WorldToLocal");

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            material.SetMatrix(PropLocalToWorld, transform.localToWorldMatrix);
            material.SetMatrix(PropWorldToLocal, transform.worldToLocalMatrix);

            Bounds worldBounds = Collider.bounds;
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, Size);
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
                pointComputeBuffer = null;
            }
            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
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

            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }
            UpdateQuadComputeBuffer();

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;

            //Debug.Log("PointCloudListener: Rebuilding compute buffers");
        }

        public override void Stop()
        {
            base.Stop();
            Size = 0;
        }
    }

}