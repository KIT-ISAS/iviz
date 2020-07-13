using UnityEngine;
using System.Collections;
using Iviz.App;
using Iviz.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using System.Threading.Tasks;

namespace Iviz.Displays
{
    public sealed class OccupancyGridResource : WrapperResource, IRecyclable, ISupportsTint
    {
        const int MaxSize = 10000;

        MeshListResource resource;
        readonly List<PointWithColor> pointBuffer = new List<PointWithColor>();

        protected override IDisplay Display => resource;

        [SerializeField] int numCellsX;

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
                UpdateSize();
            }
        }

        [SerializeField] int numCellsY;

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
                UpdateSize();
            }
        }

        [SerializeField] float cellSize;

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
                resource.Scale = value * Vector3.one;
                resource.Offset = new Vector3(0, cellSize / 2, 0);
                UpdateSize();
            }
        }

        public Color Tint
        {
            get => resource.Tint;
            set => resource.Tint = value;
        }

        public bool OcclusionOnly
        {
            get => resource.OcclusionOnly;
            set => resource.OcclusionOnly = value;
        }

        void UpdateSize()
        {
            float totalSizeX = NumCellsX * CellSize;
            float totalSizeY = NumCellsY * CellSize;
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


        void Awake()
        {
            resource = ResourcePool.GetOrCreate<MeshListResource>(Resource.Displays.MeshList, transform);

            NumCellsX = 10;
            NumCellsY = 10;
            CellSize = 1.0f;

            UpdateSize();

            Colormap = Resource.ColormapId.gray;

            GameObject cubeObject = Resource.Displays.Cube.Object;
            resource.Mesh = cubeObject.GetComponent<MeshFilter>().sharedMesh;
            resource.UseIntensityTexture = true;
            resource.UsePerVertexScale = true;
        }

        public override void Stop()
        {
            base.Stop();
            pointBuffer.Clear();
        }

        public void SplitForRecycle()
        {
            ResourcePool.Dispose(Resource.Displays.MeshList, resource.gameObject);
        }

        public readonly struct Rect
        {
            public readonly int xmin;
            public readonly int xmax;
            public readonly int ymin;
            public readonly int ymax;

            public Rect(int xmin, int xmax, int ymin, int ymax)
            {
                this.xmin = xmin;
                this.xmax = xmax;
                this.ymin = ymin;
                this.ymax = ymax;
            }
        }

        volatile bool isProcessing;

        public void SetOccupancy(sbyte[] values, Rect? tbounds = null)
        {
            if (isProcessing)
            {
                return;
            }
            isProcessing = true;
            Task.Run(() =>
                {
                    Rect bounds = tbounds ?? new Rect(0, numCellsX, 0, numCellsY);

                    pointBuffer.Clear();

                    float4 mul = new float4(cellSize, cellSize, 0, 0.01f);

                    for (int v = bounds.ymin; v < bounds.ymax; v++)
                    {
                        int i = v * numCellsX + bounds.xmin;
                        for (int u = bounds.xmin; u < bounds.xmax; u++, i++)
                        {
                            sbyte val = values[i];
                            if (val <= 0)
                            {
                                continue;
                            }

                            float4 p = new float4(u, v, 0, val);
                            float4 pc = p * mul;
                            pointBuffer.Add(new PointWithColor(pc.Ros2Unity()));
                        }
                    }
                    GameThread.RunOnce(() =>
                    {
                        resource.PointsWithColor = pointBuffer;
                        resource.IntensityBounds = new Vector2(0, 1);
                        isProcessing = false;
                    });
                }
            );
        }
    }

//}
}