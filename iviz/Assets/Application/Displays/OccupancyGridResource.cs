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

        public void SetOccupancy(sbyte[] values, Rect? inBounds = null)
        {
            IsProcessing = true;
            Rect bounds = inBounds ?? new Rect(0, numCellsX, 0, numCellsY);

            pointBuffer.Clear();

            float4 mul = new float4(cellSize, cellSize, 0, 0.01f);

            for (int v = bounds.YMin; v < bounds.YMax; v++)
            {
                int i = v * numCellsX + bounds.XMin;
                for (int u = bounds.XMin; u < bounds.XMax; u++, i++)
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
            public int XMin { get; }
            public int XMax { get; }
            public int YMin { get; }
            public int YMax { get; }

            public int Width => XMax - XMin;
            public int Height => YMax - YMin;

            public Rect(int xMin, int xMax, int yMin, int yMax)
            {
                XMin = xMin;
                XMax = xMax;
                YMin = yMin;
                YMax = yMax;
            }
        }        
    }
}

