using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class AggregatedMeshMarkerResource : MonoBehaviour, ISupportsTintAndAROcclusion
    {
        [SerializeField] BoxCollider markerCollider;

        public Bounds Bounds => new Bounds(markerCollider.center, markerCollider.size);
        public Bounds WorldBounds => markerCollider.bounds;

        public Vector3 WorldScale => transform.lossyScale;
        public Pose WorldPose => transform.AsPose();

        [SerializeField] MeshMarkerResource[] children = Array.Empty<MeshMarkerResource>();
        public IReadOnlyCollection<MeshMarkerResource> Children
        {
            get => children;
            set => children = value.ToArray();
        } 
        
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

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

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public bool ColliderEnabled
        {
            get => markerCollider.enabled;
            set
            {
                markerCollider.enabled = value;

                foreach (var child in children)
                {
                    if (child != null)
                    {
                        child.ColliderEnabled = value;
                    }
                }
            }
        }

        [SerializeField] bool occlusionOnly;
        public bool OcclusionOnly
        {
            get => occlusionOnly;
            set
            {
                occlusionOnly = value;
                foreach (MeshMarkerResource resource in children)
                {
                    if (resource != null) // do not use 'is' here
                    {
                        resource.OcclusionOnly = value;
                    }
                }
            }
        }

        [SerializeField] Color tint = Color.white;
        public Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                foreach (MeshMarkerResource resource in children)
                {
                    if (resource != null) // do not use 'is' here
                    {
                        resource.Tint = value;
                    }
                }
            }
        }

        void Awake()
        {
            markerCollider = GetComponent<BoxCollider>();
        }

        public void Suspend()
        {
        }
    }
}
