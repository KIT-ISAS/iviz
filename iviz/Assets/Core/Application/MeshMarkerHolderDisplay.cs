#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    public class MeshMarkerHolderDisplay : MonoBehaviour, IDisplay, ISupportsColor, ISupportsAROcclusion,
        ISupportsTint, ISupportsPbr, ISupportsDynamicBounds, ISupportsShadows
    {
        [SerializeField] protected MeshMarkerDisplay[] children = Array.Empty<MeshMarkerDisplay>();
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] Transform? m_Transform;
        
        public BoxCollider Collider => boxCollider != null
            ? boxCollider
            : boxCollider = gameObject.AssertHasComponent<BoxCollider>(nameof(boxCollider));

        public IReadOnlyList<MeshMarkerDisplay> Children
        {
            get => children;
            set
            {
                children = (value ?? throw new ArgumentNullException(nameof(value))).ToArray();
                Layer = Layer;
            }
        }

        public Transform Transform => this.EnsureHasTransform(ref m_Transform);

        public Bounds? Bounds => children.Length == 0 ? null : Collider.GetLocalBounds();

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

        public bool EnableShadows
        {
            set
            {
                foreach (var child in children)
                {
                    child.EnableShadows = value;
                }
            }
        }
        
        public event Action? BoundsChanged;

        public bool EnableCollider
        {
            set => Collider.enabled = value;
        }

        public void UpdateBounds()
        {
            var markerChildren = children.Select(resource =>
                BoundsUtils.TransformBoundsUntil(resource.Bounds, resource.Transform, Transform));
            Collider.SetLocalBounds(markerChildren.CombineBounds() ?? default);
            BoundsChanged?.Invoke();
        }

        public virtual void Suspend()
        {
            Visible = true;
            BoundsChanged = null;
        }
    }
}