#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    public class MeshMarkerHolderResource : MonoBehaviour, IDisplay, ISupportsColor, ISupportsAROcclusion,
        ISupportsTint, ISupportsPbr
    {
        [SerializeField] protected MeshMarkerResource[] children = Array.Empty<MeshMarkerResource>();

        BoxCollider? boxCollider;
        Transform? mTransform;

        protected BoxCollider Collider => boxCollider != null
            ? boxCollider
            : boxCollider = GetComponent<BoxCollider>().AssertNotNull(nameof(boxCollider));

        public IReadOnlyList<MeshMarkerResource> Children
        {
            get => children;
            set
            {
                children = (value ?? throw new ArgumentNullException(nameof(value))).ToArray();
                Layer = Layer;
            }
        }

        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        public Bounds Bounds => new(Collider.center, Collider.size);

        Bounds? IDisplay.Bounds => Bounds;

        public int Layer
        {
            get => gameObject.layer;
            set
            {
                gameObject.layer = value;

                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.Layer = value;
                    }
                }
            }
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public bool OcclusionOnly
        {
            set
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.OcclusionOnly = value;
                    }
                }
            }
        }

        public Color Tint
        {
            set
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.Tint = value;
                    }
                }
            }
        }


        public float Smoothness
        {
            set
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.Smoothness = value;
                    }
                }
            }
        }

        public float Metallic
        {
            set
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.Metallic = value;
                    }
                }
            }
        }

        public Color Color
        {
            set
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.Color = value;
                    }
                }
            }
        }

        public Color EmissiveColor
        {
            set
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.EmissiveColor = value;
                    }
                }
            }
        }

        public bool ShadowsEnabled
        {
            set
            {
                foreach (var resource in children)
                {
                    resource.ShadowsEnabled = value;
                }
            }
        }

        public void UpdateBounds()
        {
            var markerChildren = children.Select(resource =>
                (Bounds?)BoundsUtils.TransformBoundsUntil(resource.Bounds, resource.Transform, Transform));
            var nullableRootBounds = markerChildren.CombineBounds();

            if (nullableRootBounds is not { } rootBounds)
            {
                return;
            }

            Collider.center = rootBounds.center;
            Collider.size = rootBounds.size;
        }

        public virtual void Suspend()
        {
        }
    }
}