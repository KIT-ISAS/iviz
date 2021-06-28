using System;
using Iviz.Core;
using Iviz.Msgs.IvizCommonMsgs;
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

        readonly NativeList<float4> pointBuffer = new NativeList<float4>();
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

        public ColormapId Colormap
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

        public int NumValidValues => resource != null ? resource.Size : 0;

        void Awake()
        {
            resource = ResourcePool.RentDisplay<MeshListResource>(transform);

            resource.OverrideIntensityBounds = true;
            resource.IntensityBounds = new Vector2(0, 1);

            NumCellsX = 10;
            NumCellsY = 10;
            CellSize = 1.0f;

            Colormap = ColormapId.gray;

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

        public void SetOccupancy(sbyte[] values, Rect? inBounds, Pose pose)
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
                resource.transform.SetLocalPose(pose);
                resource.SetDirect(pointBuffer);
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

