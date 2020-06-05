using UnityEngine;
using System.Collections;
using Iviz.App;
using Iviz.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iviz.Displays
{

    public sealed class OccupancyGridResource : MonoBehaviour, IDisplay, IRecyclable
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

        int layer;
        public int Layer
        {
            get => layer;
            set
            {
                layer = value;
                resource.Layer = layer;
                plane.Layer = layer;
            }
        }

        bool visible;
        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                resource.Visible = value;
                plane.Visible = value && interiorVisible;
            }
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        bool colliderEnabled;
        public bool ColliderEnabled
        {
            get => colliderEnabled;
            set
            {
                colliderEnabled = value;
                resource.ColliderEnabled = value;
                plane.ColliderEnabled = value;
            }
        }

        bool interiorVisible;
        public bool InteriorVisible
        {
            get => interiorVisible;
            set
            {
                interiorVisible = value;
                plane.Visible = value && Visible;
            }
        }

        int numCellsX;
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

        int numCellsY;
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

        float cellSize;
        public float CellSize
        {
            get => cellSize;
            set
            {
                if (value == cellSize)
                {
                    return;
                }
                cellSize = value;
                resource.Scale = value * Vector3.one;
                resource.Offset = new Vector3(0, cellSize / 2, 0);
                UpdateSize();
            }
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
            resource = ResourcePool.GetOrCreate<MeshListResource>(Resource.Markers.MeshList, transform);
            plane = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Markers.Cube, transform);

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
            ResourcePool.Dispose(Resource.Markers.MeshList, resource.gameObject);
            ResourcePool.Dispose(Resource.Markers.Cube, plane.gameObject);
        }

        public void SetOccupancy(IList<sbyte> values)
        {
            pointBuffer.Clear();
            int i = 0;
            float offsetX = (NumCellsX - 1) / 2f;
            float offsetY = (NumCellsY - 1) / 2f;
            for (int v = 0; v < NumCellsY; v++)
            {
                for (int u = 0; u < NumCellsX; u++, i++)
                {
                    sbyte val = values[i];
                    if (val <= 0)
                    {
                        continue;
                    }
                    Vector3 pos = new Vector3(
                        (u - offsetX) * CellSize,
                        (v - offsetY) * CellSize,
                        0
                        );

                    pointBuffer.Add(new PointWithColor(
                        pos.Ros2Unity(),
                        val * 0.01f
                        ));
                }
            }
            resource.PointsWithColor = pointBuffer;
        }
    }

}