#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class OccupancyGridDisplay : DisplayWrapper, ISupportsTint, IRecyclable
    {
        const int MaxSize = 10000;

        [SerializeField] int numCellsX;
        [SerializeField] int numCellsY;
        [SerializeField] float cellSize;

        readonly List<float4> pointBuffer = new();
        MeshListDisplay? resource;

        MeshListDisplay Resource => ResourcePool.RentChecked(ref resource, Transform);

        protected override IDisplay Display => Resource;

        public int NumCellsX
        {
            get => numCellsX;
            set
            {
                if (value == numCellsX)
                {
                    return;
                }

                if (value is < 0 or > MaxSize)
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

                if (value is < 0 or > MaxSize)
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
                Resource.ElementScale = value;
            }
        }

        public bool OcclusionOnly
        {
            get => Resource.OcclusionOnly;
            set => Resource.OcclusionOnly = value;
        }

        public ColormapId Colormap
        {
            get => Resource.Colormap;
            set => Resource.Colormap = value;
        }

        public bool FlipMinMax
        {
            get => Resource.FlipMinMax;
            set => Resource.FlipMinMax = value;
        }

        public bool IsProcessing { get; private set; }

        public int NumValidValues => Resource != null ? Resource.Size : 0;

        void Awake()
        {
            Resource.OverrideIntensityBounds = true;
            Resource.IntensityBounds = new Vector2(0, 1);

            NumCellsX = 10;
            NumCellsY = 10;
            CellSize = 1.0f;

            Colormap = ColormapId.gray;

            Resource.MeshResource = Resources.Resource.Displays.Cube;
            Resource.UseColormap = true;
            Resource.UseIntensityForScaleY = true;
            Resource.EnableShadows = false; // fix weird shadow bug

            Layer = LayerType.IgnoreRaycast;
        }

        public Color Tint
        {
            get => Resource.Tint;
            set => Resource.Tint = value;
        }

        public override void Suspend()
        {
            pointBuffer.Clear();
        }

        public void SetOccupancy(ReadOnlySpan<sbyte> values, RectInt? inBounds, Pose pose)
        {
            IsProcessing = true;
            
            var bounds = inBounds ?? new RectInt(0,  0, numCellsX, numCellsY);

            pointBuffer.Clear();

            float size = cellSize;

            foreach (int v in bounds.y..bounds.yMax)
            {
                var row = values[(v * numCellsX)..];
                float y = -v * size;

                foreach (int u in bounds.x..bounds.xMax)
                {
                    sbyte val = row[u];
                    if (val <= 0)
                    {
                        continue;
                    }

                    
                    float4 p;
                    /*
                    p.x = u * size;
                    p.y = y;
                    p.z = 0;
                    */
                    p.x = y;
                    p.y = 0;
                    p.z = u * size;
                    p.w = val * 0.01f;
                    pointBuffer.Add(p);
                }
            }

            GameThread.PostImmediate(() =>
            {
                Resource.Transform.SetLocalPose(pose);
                Resource.Set(pointBuffer.AsReadOnlySpan());
                IsProcessing = false;
            });
        }

        public void SplitForRecycle()
        {
            resource.ReturnToPool();
        }
    }
}