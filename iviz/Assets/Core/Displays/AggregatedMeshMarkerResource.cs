using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class AggregatedMeshMarkerResource : MonoBehaviour, ISupportsTintAndAROcclusion
    {
        [SerializeField] MeshTrianglesResource[] children = Array.Empty<MeshTrianglesResource>();
        [SerializeField] bool occlusionOnly;
        [SerializeField] Color tint = Color.white;

        BoxCollider boxCollider;
        [NotNull] BoxCollider Collider => boxCollider != null ? boxCollider : boxCollider = GetComponent<BoxCollider>();

        [NotNull]
        public IReadOnlyCollection<MeshTrianglesResource> Children
        {
            get => children;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                children = value.ToArray();
                Layer = Layer;
            }
        }

        [NotNull] public Bounds? Bounds => new Bounds(Collider.center, Collider.size);

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
            get => occlusionOnly;
            set
            {
                occlusionOnly = value;
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
            get => tint;
            set
            {
                tint = value;
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.Tint = value;
                    }
                }
            }
        }

        public void Suspend()
        {
        }

        void Awake()
        {
            Layer = LayerType.Unclickable;
        }
    }
}