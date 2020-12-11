using System;
using Iviz.Core;
using Iviz.Resources;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class OccupancyGridResource : DisplayWrapperResource, ISupportsTint
    {
        const int MaxSize = 10000;

        [SerializeField] int numCellsX;
        [SerializeField] int numCellsY;
        [SerializeField] float cellSize;

        NativeList<float4> pointBuffer;
        MeshListResource resource;

        protected override IDisplay Display => resource;

        public int NumCellsX
        {
            get => numCellsX;
            set
            {
                if (value == numCellsX)
                {
                    return;
                }

                if (value < 0 || value > MaxSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                numCellsX = value;
            }
        }

        public int NumCellsY
        {
            get => numCellsY;
            set
            {
                if (value == numCellsY)
                {
                    return;
                }

                if (value < 0 || value > MaxSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                numCellsY = value;
            }
        }

        public float CellSize
        {
            get => cellSize;
            set
            {
                if (Mathf.Approximately(value, cellSize))
                {
                    return;
                }

                cellSize = value;
                resource.ElementScale = value;
            }
        }

        public bool OcclusionOnly
        {
            get => resource.OcclusionOnly;
            set => resource.OcclusionOnly = value;
        }

        public Resource.ColormapId Colormap
        {
            get => resource.Colormap;
            set => resource.Colormap = value;
        }

        public bool FlipMinMax
        {
            get => resource.FlipMinMax;
            set => resource.FlipMinMax = value;
        }

        public bool IsProcessing { get; private set; }

        void Awake()
        {
            pointBuffer = new NativeList<float4>(Allocator.Persistent);
            resource = ResourcePool.GetOrCreateDisplay<MeshListResource>(transform);

            NumCellsX = 10;
            NumCellsY = 10;
            CellSize = 1.0f;

            Colormap = Resource.ColormapId.gray;

            resource.MeshResource = Resource.Displays.Cube;
            resource.UseColormap = true;
            resource.UseIntensityForScaleY = true;
            resource.CastShadows = false; // fix weird shadow bug

            Layer = LayerType.IgnoreRaycast;
        }

        public Color Tint
        {
            get => resource.Tint;
            set => resource.Tint = value;
        }

        public override void Suspend()
        {
            base.Suspend();
            pointBuffer.Clear();
        }

        public void SetOccupancy(sbyte[] values, Rect? tbounds = null)
        {
            IsProcessing = true;
            Rect bounds = tbounds ?? new Rect(0, numCellsX, 0, numCellsY);

            pointBuffer.Clear();

            float4 mul = new float4(cellSize, cellSize, 0, 0.01f);

            for (int v = bounds.Ymin; v < bounds.Ymax; v++)
            {
                int i = v * numCellsX + bounds.Xmin;
                for (int u = bounds.Xmin; u < bounds.Xmax; u++, i++)
                {
                    sbyte val = values[i];
                    if (val <= 0)
                    {
                        continue;
                    }

                    float4 p = new float4(u, v, 0, val) * mul;
                    pointBuffer.Add(p.Ros2Unity());
                }
            }

            GameThread.PostImmediate(() =>
            {
                resource.SetDirect(pointBuffer);
                resource.IntensityBounds = new Vector2(0, 1);
                IsProcessing = false;
            });
        }

        void OnDestroy()
        {
            pointBuffer.Dispose();
        }

        public readonly struct Rect
        {
            public int Xmin { get; }
            public int Xmax { get; }
            public int Ymin { get; }
            public int Ymax { get; }

            public Rect(int xmin, int xmax, int ymin, int ymax)
            {
                Xmin = xmin;
                Xmax = xmax;
                Ymin = ymin;
                Ymax = ymax;
            }
        }        
    }
}

