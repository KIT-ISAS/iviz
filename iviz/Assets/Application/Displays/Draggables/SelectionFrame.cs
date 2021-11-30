#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class SelectionFrame : MeshMarkerHolderResource, IRecyclable
    {
        float columnWidth = 0.005f;
        Vector3 size = Vector3.one;

        public Vector3 Size
        {
            get => size;
            set
            {
                if (Mathf.Approximately(value.x, size.x)
                    && Mathf.Approximately(value.y, size.y)
                    && Mathf.Approximately(value.z, size.z))
                {
                    return;
                }

                size = value.Abs();
                Collider.size = size;
                RebuildSize();
            }
        }
        
        public float ColumnWidth
        {
            get => columnWidth;
            set
            {
                if (Mathf.Approximately(value, columnWidth))
                {
                    return;
                }

                columnWidth = value;
                UpdateColumnWidth();
            }
        }        

        static MeshMarkerResource[] CreateObjects(Transform transform)
        {
            var objects = new MeshMarkerResource[12];
            foreach (ref var resource in objects.Ref())
            {
                resource = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, transform);
                resource.ShadowsEnabled = false;
                resource.ColliderEnabled = false;
            }

            return objects;
        }

        void RebuildSize()
        {
            if (children.Length == 0)
            {
                children = CreateObjects(Transform);
            }

            var (halfSizeX, halfSizeY, halfSizeZ) = size / 2;
            children[0].Transform.localPosition = new Vector3(halfSizeX, halfSizeY, 0);
            children[1].Transform.localPosition = new Vector3(halfSizeX, -halfSizeY, 0);
            children[2].Transform.localPosition = new Vector3(-halfSizeX, -halfSizeY, 0);
            children[3].Transform.localPosition = new Vector3(-halfSizeX, halfSizeY, 0);

            for (int i = 0; i < 4; i++)
            {
                children[i].Transform.localScale = new Vector3(ColumnWidth, ColumnWidth, size.z);
            }

            children[4].Transform.localPosition = new Vector3(0, halfSizeY, halfSizeZ);
            children[5].Transform.localPosition = new Vector3(0, -halfSizeY, halfSizeZ);
            children[6].Transform.localPosition = new Vector3(0, -halfSizeY, -halfSizeZ);
            children[7].Transform.localPosition = new Vector3(0, halfSizeY, -halfSizeZ);

            for (int i = 4; i < 8; i++)
            {
                children[i].Transform.localScale = new Vector3(size.x, ColumnWidth, ColumnWidth);
            }

            children[8].Transform.localPosition = new Vector3(halfSizeX, 0, halfSizeZ);
            children[9].Transform.localPosition = new Vector3(-halfSizeX, 0, halfSizeZ);
            children[10].Transform.localPosition = new Vector3(-halfSizeX, 0, -halfSizeZ);
            children[11].Transform.localPosition = new Vector3(halfSizeX, 0, -halfSizeZ);

            for (int i = 8; i < 12; i++)
            {
                children[i].Transform.localScale = new Vector3(ColumnWidth, size.y, ColumnWidth);
            }
        }

        void UpdateColumnWidth()
        {
            if (children.Length == 0)
            {
                children = CreateObjects(Transform);
            }

            for (int i = 0; i < 4; i++)
            {
                children[i].Transform.localScale = new Vector3(ColumnWidth, ColumnWidth, size.z);
            }

            for (int i = 4; i < 8; i++)
            {
                children[i].Transform.localScale = new Vector3(size.x, ColumnWidth, ColumnWidth);
            }

            for (int i = 8; i < 12; i++)
            {
                children[i].Transform.localScale = new Vector3(ColumnWidth, size.y, ColumnWidth);
            }            
        }

        public override void Suspend()
        {
            //OcclusionOnly = false;
            //ShadowsEnabled = false;
            //EmissiveColor = Color.black;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        void Awake()
        {
            Layer = LayerType.IgnoreRaycast;
            Collider.enabled = false;
            if (children.Length == 0)
            {
                RebuildSize();
            }
            
            ShadowsEnabled = false;
        }

        public void SetBounds(in Bounds bounds)
        {
            Size = bounds.size;
            Transform.localPosition = bounds.center;
        }

        public void SplitForRecycle()
        {
            foreach (var resource in children)
            {
                resource.ReturnToPool(Resource.Displays.Cube);
            }
        }
    }
}