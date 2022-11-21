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
        float columnWidth = 0.002f;
        Vector3 size = Vector3.one;
        float? lastAdjustedColumnWidth;

        public Vector3 Size
        {
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
            set
            {
                columnWidth = value;
                RebuildColumnWidth();
            }
        }

        float AdjustedColumnWidth => columnWidth / Mathf.Clamp(Transform.lossyScale.MaxAbsCoeff(), 0.1f, 0.5f);

        static MeshMarkerDisplay[] CreateObjects(Transform transform)
        {
            var array = new MeshMarkerDisplay[12];
            foreach (ref var display in array.AsSpan())
            {
                var resource = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, transform);
                resource.EnableShadows = false;
                resource.EnableCollider = false;
                display = resource;
            }

            return array;
        }

        void RebuildSize()
        {
            if (Children.Length == 0)
            {
                Children = CreateObjects(Transform);
            }

            var children = Children;
            var (halfSizeX, halfSizeY, halfSizeZ) = size / 2;
            children[0].Transform.localPosition = new Vector3(halfSizeX, halfSizeY, 0);
            children[1].Transform.localPosition = new Vector3(halfSizeX, -halfSizeY, 0);
            children[2].Transform.localPosition = new Vector3(-halfSizeX, -halfSizeY, 0);
            children[3].Transform.localPosition = new Vector3(-halfSizeX, halfSizeY, 0);

            float adjustedColumnWidth = AdjustedColumnWidth;
            foreach (var child in children.AsSpan(..4))
            {
                child.Transform.localScale = new Vector3(adjustedColumnWidth, adjustedColumnWidth, size.z);
            }

            children[4].Transform.localPosition = new Vector3(0, halfSizeY, halfSizeZ);
            children[5].Transform.localPosition = new Vector3(0, -halfSizeY, halfSizeZ);
            children[6].Transform.localPosition = new Vector3(0, -halfSizeY, -halfSizeZ);
            children[7].Transform.localPosition = new Vector3(0, halfSizeY, -halfSizeZ);

            foreach (var child in children.AsSpan(4..8))
            {
                child.Transform.localScale = new Vector3(size.x, adjustedColumnWidth, adjustedColumnWidth);
            }

            children[8].Transform.localPosition = new Vector3(halfSizeX, 0, halfSizeZ);
            children[9].Transform.localPosition = new Vector3(-halfSizeX, 0, halfSizeZ);
            children[10].Transform.localPosition = new Vector3(-halfSizeX, 0, -halfSizeZ);
            children[11].Transform.localPosition = new Vector3(halfSizeX, 0, -halfSizeZ);

            foreach (var child in children.AsSpan(8..12))
            {
                child.Transform.localScale = new Vector3(adjustedColumnWidth, size.y, adjustedColumnWidth);
            }
        }

        void RebuildColumnWidth()
        {
            if (Children.Length == 0)
            {
                Children = CreateObjects(Transform);
            }

            var children = Children;
            float adjustedColumnWidth = AdjustedColumnWidth;
            foreach (var child in children.AsSpan(..4))
            {
                child.Transform.localScale = new Vector3(adjustedColumnWidth, adjustedColumnWidth, size.z);
            }

            foreach (var child in children.AsSpan(4..8))
            {
                child.Transform.localScale = new Vector3(size.x, adjustedColumnWidth, adjustedColumnWidth);
            }

            foreach (var child in children.Skip(8))
            {
                child.Transform.localScale = new Vector3(adjustedColumnWidth, size.y, adjustedColumnWidth);
            }
        }

        void Awake()
        {
            Layer = LayerType.IgnoreRaycast;
            Collider.enabled = false;
            if (Children.Length == 0)
            {
                RebuildSize();
            }

            EnableShadows = false;
        }

        public void SetBounds(in Bounds bounds)
        {
            if (bounds.IsInvalid())
            {
                ThrowHelper.ThrowArgument("Bounds contain invalid values", nameof(bounds));
            }

            Size = bounds.size;
            Transform.localPosition = bounds.center;
        }

        public void SplitForRecycle()
        {
            foreach (var resource in Children)
            {
                resource.ReturnToPool(Resource.Displays.Cube);
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            columnWidth = 0.002f;
            Transform.localScale = Vector3.one;
            lastAdjustedColumnWidth = null;
        }

        public void UpdateColumnWidth()
        {
            lastAdjustedColumnWidth = null;
            RebuildColumnWidth();
        }

        void Update()
        {
            float adjustedColumnWidth = AdjustedColumnWidth;
            if (lastAdjustedColumnWidth is { } width &&
                Mathf.Approximately(adjustedColumnWidth, width)) return;
            RebuildColumnWidth();
        }
    }
}