﻿#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using Unity.Burst;
using Unity.Burst.CompilerServices;
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

        float4[] pointBuffer = Array.Empty<float4>();

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
                    ThrowHelper.ThrowArgumentOutOfRange(nameof(value));
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
                    ThrowHelper.ThrowArgumentOutOfRange(nameof(value));
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
            pointBuffer = Array.Empty<float4>();
        }

        public void SetOccupancy(sbyte[] values, RectInt? inBounds, Pose pose)
        {
            var bounds = inBounds ?? new RectInt(0, 0, numCellsX, numCellsY);

            int maxPointBufferLength = bounds.width * bounds.height;
            if (pointBuffer.Length < maxPointBufferLength)
            {
                int desiredLength = Mathf.Max(pointBuffer.Length, 128);
                while (desiredLength < maxPointBufferLength) desiredLength *= 2;
                pointBuffer = new float4[desiredLength];
            }

            /*
            float4[] mPointBuffer = pointBuffer;
            int pointBufferLength = 0;
            float size = cellSize;

            for (int v = bounds.y; v < bounds.yMax; v++)
            {
                var row = values[(v * numCellsX)..];
                float y = -v * size;

                for (int u = bounds.x; u < bounds.xMax; u++)
                {
                    sbyte val = row[u];
                    if (val <= 0)
                    {
                        continue;
                    }


                    float4 p;
                    p.x = y;
                    p.y = 0;
                    p.z = u * size;
                    p.w = val * 0.01f;

                    mPointBuffer[pointBufferLength++] = p;
                }
            }
            */

            int pointBufferLength;
            unsafe
            {
                fixed (sbyte* valuesPtr = values)
                fixed (float4* pointBufferPtr = pointBuffer)
                {
                    pointBufferLength = BurstUtils.AddToBuffer(valuesPtr, pointBufferPtr,
                        cellSize, bounds.y, bounds.yMax, bounds.x, bounds.xMax, numCellsX);
                }
            }

            var pointBufferToUse = new ReadOnlyMemory<float4>(pointBuffer, 0, pointBufferLength);
            
            GameThread.PostImmediate(() =>
            {
                Resource.Transform.SetLocalPose(pose);
                Resource.Set(pointBufferToUse.Span);
            });
        }

        [BurstCompile]
        static unsafe class BurstUtils
        {
            [BurstCompile(CompileSynchronously = true)]
            public static int AddToBuffer([NoAlias] sbyte* input, [NoAlias] float4* output,
                float size, int vMin, int vMax, int uMin, int uMax, int stride)
            {
                int length = 0;

                for (int v = vMin; v < vMax; v++)
                {
                    sbyte* row = input + v * stride;
                    float y = -v * size;

                    for (int u = uMin; u < uMax; u++)
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

                        output[length++] = p;
                    }
                }

                return length;
            }
        }


        public void SplitForRecycle()
        {
            resource.ReturnToPool();
        }
    }
}