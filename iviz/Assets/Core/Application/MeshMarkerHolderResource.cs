#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    public class MeshMarkerHolderResource : MonoBehaviour, IDisplay, ISupportsColor, ISupportsAROcclusion,
        ISupportsTint, ISupportsPbr
    {
        [SerializeField] protected MeshMarkerResource[] children = Array.Empty<MeshMarkerResource>();
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] Transform? m_Transform;

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

        public Transform Transform => m_Transform != null ? m_Transform : (m_Transform = transform);

        public Bounds? Bounds => children.Length == 0 ? null : new Bounds(Collider.center, Collider.size);

        public int Layer
        {
            protected get => gameObject.layer;
            set
            {
                gameObject.layer = value;

                foreach (var child in children)
                {
                    child.Layer = value;
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
                    child.OcclusionOnly = value;
                }
            }
        }

        public Color Tint
        {
            set
            {
                foreach (var child in children)
                {
                    child.Tint = value;
                }
            }
        }


        public float Smoothness
        {
            set
            {
                foreach (var child in children)
                {
                    child.Smoothness = value;
                }
            }
        }

        public float Metallic
        {
            set
            {
                foreach (var child in children)
                {
                    child.Metallic = value;
                }
            }
        }

        public Color Color
        {
            set
            {
                foreach (var child in children)
                {
                    child.Color = value;
                }
            }
        }

        public Color EmissiveColor
        {
            set
            {
                foreach (var child in children)
                {
                    child.EmissiveColor = value;
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

        public bool ColliderEnabled
        {
            set => Collider.enabled = value;
        }

        public void UpdateBounds()
        {
            var markerChildren = children.Select(resource =>
                BoundsUtils.TransformBoundsUntil(resource.Bounds, resource.Transform, Transform));
            var nullableRootBounds = markerChildren.CombineBounds();

            if (nullableRootBounds is not { } rootBounds)
            {
                Collider.center = Vector3.zero;
                Collider.size = Vector3.zero;
                return;
            }

            Collider.center = rootBounds.center;
            Collider.size = rootBounds.size;
        }

        public virtual void Suspend()
        {
            Visible = true;
        }
    }
}