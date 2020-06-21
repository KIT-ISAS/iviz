using System;
using System.Collections.Generic;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class AggregatedMeshMarker : MonoBehaviour, IDisplay, ISupportsAROcclusion, ISupportsTint
    {
        BoxCollider Collider;

        public Bounds Bounds => new Bounds(Collider.center, Collider.size);
        public Bounds WorldBounds => Collider.bounds;

        public Vector3 WorldScale => transform.lossyScale;
        public Pose WorldPose => transform.AsPose();

        public IReadOnlyList<MeshMarkerResource> Children { get; set; } = Array.Empty<MeshMarkerResource>();

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public int Layer
        {
            get => gameObject.layer;
            set => gameObject.layer = value;
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
            get => Collider.enabled;
            set => Collider.enabled = value;
        }

        [SerializeField] bool occlusionOnly_;
        public bool OcclusionOnly
        {
            get => occlusionOnly_;
            set
            {
                occlusionOnly_ = value;
                Children.ForEach(x => x.OcclusionOnly = value);
            }
        }

        [SerializeField] Color tint_ = Color.white;
        public Color Tint
        {
            get => tint_;
            set
            {
                tint_ = value;
                Children.ForEach(x => x.Tint = value);
            }
        }

        void Awake()
        {
            Name = "AggregatedMeshMarker";
            Collider = GetComponent<BoxCollider>();
        }

        public void Stop()
        {
        }
    }
}
