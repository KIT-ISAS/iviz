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
    public sealed class OccupancyGridResource : MonoBehaviour, IDisplay, IRecyclable, ISupportsTint
    {
        const int MaxSize = 10000;

        MeshListResource resource;
        MeshMarkerResource plane;
        readonly List<PointWithColor> pointBuffer = new List<PointWithColor>();

        public string Name => "Occupancy Grid";
        public Bounds Bounds => resource.Bounds;
        public Bounds WorldBounds => resource.WorldBounds;
        public Vector3 WorldScale => resource.Scale;
        public Pose WorldPose => resource.WorldPose;

        int layer_;
        public int Layer
        {
            get => layer_;
            set
            {
                layer_ = value;
                resource.Layer = layer_;
                plane.Layer = layer_;
            }
        }

        bool visible_;
        public bool Visible
        {
            get => visible_;
            set
            {
                visible_ = value;
                resource.Visible = value;
                plane.Visible = value && interiorVisible_;
            }
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        bool colliderEnabled_;
        public bool ColliderEnabled
        {
            get => colliderEnabled_;
            set
            {
                colliderEnabled_ = value;
                resource.ColliderEnabled = value;
                plane.ColliderEnabled = value;
            }
        }

        [SerializeField] bool interiorVisible_;
        public bool InteriorVisible
        {
            get => interiorVisible_;
            set
            {
                interiorVisible_ = value;
                plane.Visible = value && Visible;
            }
        }

        [SerializeField] int numCellsX_;
        public int NumCellsX
        {
            get => numCellsX_;
            set
            {
                if (value == numCellsX_)
                {
                    return;
                }
                if (value < 0 || value > MaxSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                numCellsX_ = value;
                UpdateSize();
            }
        }

        [SerializeField] int numCellsY_;
        public int NumCellsY
        {
            get => numCellsY_;
            set
            {
                if (value == numCellsY_)
                {
                    return;
                }
                if (value < 0 || value > MaxSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                numCellsY_ = value;
                UpdateSize();
            }
        }

        [SerializeField] float cellSize_;
        public float CellSize
        {
            get => cellSize_;
            set
            {
                if (value == cellSize_)
                {
                    return;
                }
                cellSize_ = value;
                resource.Scale = value * Vector3.one;
                resource.Offset = new Vector3(0, cellSize_ / 2, 0);
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
            plane.transform.localScale = new Vector3(totalSizeX, 0.001f, totalSizeY);
        }

        public Resource.ColormapId Colormap
        {
            get => resource.Colormap;
            set
            {
                resource.Colormap = value;
                UpdatePlaneColor();
            }
        }

        public Vector2 IntensityBounds
        {
            get => resource.IntensityBounds;
            set
            {
                resource.IntensityBounds = value;
                UpdatePlaneColor();
            }
        }

        void UpdatePlaneColor()
        {
            Texture2D texture = Resource.Colormaps.Textures[Colormap];
            plane.Color = texture.GetPixel((int)(IntensityBounds.x * (texture.width - 1)), 0);
        }

        void Awake()
        {
            resource = ResourcePool.GetOrCreate<MeshListResource>(Resource.Displays.MeshList, transform);
            plane = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Cube, transform);

            NumCellsX = 10;
            NumCellsY = 10;
            CellSize = 1.0f;

            UpdateSize();

            InteriorVisible = false;

            Colormap = Resource.ColormapId.gray;
            IntensityBounds = new Vector2(0, 1);

            resource.Mesh = plane.GetComponent<MeshFilter>().sharedMesh;
            resource.UseIntensityTexture = true;
            resource.UsePerVertexScale = true;
        }

        public void Stop()
        {
            resource.Stop();
            plane.Stop();
            pointBuffer.Clear();
        }

        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Displays.MeshList, resource.gameObject);
            ResourcePool.Dispose(Resource.Displays.Cube, plane.gameObject);
        }

        volatile bool isProcessing;
        public void SetOccupancy(sbyte[] values)
        {
            if (isProcessing)
            {
                return;
            }
            isProcessing = true;
            Task.Run(() =>
            {
                pointBuffer.Clear();

                int i = 0;
                float offsetX = (numCellsX_ - 1) / 2f;
                float offsetY = (numCellsY_ - 1) / 2f;

                float4 add = new float4(-offsetX, -offsetY, 0, 0);
                float4 mul = new float4(cellSize_, cellSize_, 0, 0.01f);

                float4 addmul = add * mul;

                for (int v = 0; v < numCellsY_; v++)
                {
                    for (int u = 0; u < numCellsX_; u++, i++)
                    {
                        sbyte val = values[i];
                        if (val <= 0)
                        {
                            continue;
                        }
                        float4 p = new float4(u, v, 0, val);
                        float4 pc = p * mul + addmul;
                        pointBuffer.Add(new PointWithColor(pc.Ros2Unity()));
                    }
                }
                isProcessing = false;

                GameThread.RunOnce(() =>
                {
                    resource.PointsWithColor = pointBuffer;
                    resource.IntensityBounds = new Vector2(0, 1);
                });
            });
        }
    }

}