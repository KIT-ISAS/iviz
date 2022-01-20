#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class SelectionFrame : MeshMarkerHolderDisplay, IRecyclable
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

        static MeshMarkerDisplay[] CreateObjects(Transform transform)
        {
            MeshMarkerDisplay CreateObject()
            {
                var resource = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, transform);
                resource.EnableShadows = false;
                resource.EnableCollider = false;
                return resource;
            }

            return (..12).Select(CreateObject).ToArray();
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

            foreach (var child in children.AsSpan(..4))
            {
                child.Transform.localScale = new Vector3(ColumnWidth, ColumnWidth, size.z);
            }

            children[4].Transform.localPosition = new Vector3(0, halfSizeY, halfSizeZ);
            children[5].Transform.localPosition = new Vector3(0, -halfSizeY, halfSizeZ);
            children[6].Transform.localPosition = new Vector3(0, -halfSizeY, -halfSizeZ);
            children[7].Transform.localPosition = new Vector3(0, halfSizeY, -halfSizeZ);

            foreach (var child in children.AsSpan(4..8))
            {
                child.Transform.localScale = new Vector3(size.x, ColumnWidth, ColumnWidth);
            }

            children[8].Transform.localPosition = new Vector3(halfSizeX, 0, halfSizeZ);
            children[9].Transform.localPosition = new Vector3(-halfSizeX, 0, halfSizeZ);
            children[10].Transform.localPosition = new Vector3(-halfSizeX, 0, -halfSizeZ);
            children[11].Transform.localPosition = new Vector3(halfSizeX, 0, -halfSizeZ);

            foreach (var child in children.AsSpan(8..12))
            {
                child.Transform.localScale = new Vector3(ColumnWidth, size.y, ColumnWidth);
            }
        }

        void UpdateColumnWidth()
        {
            if (children.Length == 0)
            {
                children = CreateObjects(Transform);
            }

            foreach (var child in children.AsSpan(..4))
            {
                child.Transform.localScale = new Vector3(ColumnWidth, ColumnWidth, size.z);
            }

            foreach (var child in children.AsSpan(4..8))
            {
                child.Transform.localScale = new Vector3(size.x, ColumnWidth, ColumnWidth);
            }

            foreach (var child in children.AsSpan(8..12))
            {
                child.Transform.localScale = new Vector3(ColumnWidth, size.y, ColumnWidth);
            }            
        }

        void Awake()
        {
            Layer = LayerType.IgnoreRaycast;
            Collider.enabled = false;
            if (children.Length == 0)
            {
                RebuildSize();
            }
            
            EnableShadows = false;
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