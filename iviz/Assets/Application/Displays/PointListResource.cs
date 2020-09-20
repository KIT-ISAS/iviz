using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.Resources;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class PointListResource : MarkerResourceWithColormap
    {
        NativeArray<float4> pointBuffer;
        ComputeBuffer pointComputeBuffer;

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
            }
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
            material.SetBuffer(PointsID, pointComputeBuffer);
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

        void UpdateBuffer()
        {
            if (Size == 0)
            {
                return;
            }

            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(pointBuffer, Size, out Bounds bounds, out Vector2 span);
            boxCollider.center = bounds.center;
            boxCollider.size = bounds.size + ElementSize * Vector3.one;
            IntensityBounds = span;
        }

        protected override void Awake()
        {
            material = Resource.Materials.PointCloud.Instantiate();
            material.DisableKeyword("USE_TEXTURE");

            base.Awake();

            UseIntensityTexture = false;
            IntensityBounds = new Vector2(0, 1);
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            UpdateTransform();

            Bounds worldBounds = boxCollider.bounds;
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
                material.SetBuffer(PointsID, pointComputeBuffer);
            }

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
        }

        public override void Suspend()
        {
            base.Suspend();
            Size = 0;
        }
    }
}