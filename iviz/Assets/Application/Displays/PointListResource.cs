using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.Resources;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    class NativeArrayList<T> where T : struct
    {
        NativeArray<T> pointBuffer = new NativeArray<T>();
        int size;

        public bool Reserve (int reserved)
        {
            if (pointBuffer.Length >= reserved)
            {
                return false;
            }
            if (pointBuffer.Length != 0)
            {
                pointBuffer.Dispose();
            }
            pointBuffer = new NativeArray<T>(reserved, Allocator.Persistent);
            return true;
        }

        public int Count => size;

        public void Add(in T t)
        {
            pointBuffer[size++] = t;
        }

        public void Clear()
        {
            size = 0;
        }

        public NativeArray<T> Buffer => pointBuffer;
    }

    public sealed class PointListResource : MarkerResourceWithColormap
    {
        NativeArray<float4> pointBuffer;
        ComputeBuffer pointComputeBuffer;
        ComputeBuffer quadComputeBuffer;

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
            }
        }

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
                foreach (var t in value)
                {
                    if (t.HasNaN)
                    {
                        continue;
                    }
                    pointBuffer[realSize++] = t;
                }
                Size = realSize;
                UpdateBuffer();
            }
        }

        /*
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
        */

        void UpdateBuffer()
        {
            if (Size == 0)
            {
                return;
            }
            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(pointBuffer, Size, out Bounds bounds, out Vector2 span);
            Collider.center = bounds.center;
            Collider.size = bounds.size + Scale.x * Vector3.one;
            IntensityBounds = span;
        }

        [SerializeField] Vector2 scale;
        public Vector2 Scale
        {
            get => scale;
            set
            {
                if (scale == value)
                {
                    return;
                }
                scale = value;
                UpdateQuadComputeBuffer();
            }
        }

        protected override void Awake()
        {
            material = Resource.Materials.PointCloud.Instantiate();
            material.DisableKeyword("USE_TEXTURE");

            base.Awake();

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


        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            UpdateTransform();

            Bounds worldBounds = Collider.bounds;
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, Size);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
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

            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }
            UpdateQuadComputeBuffer();

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
        }

        public override void Stop()
        {
            base.Stop();
            Size = 0;
        }
    }

}