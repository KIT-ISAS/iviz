#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class MarkerResource : MonoBehaviour, IDisplay
    {
        [SerializeField] Transform? m_Transform;
        [SerializeField] BoxCollider? boxCollider;

        public Transform Transform => m_Transform != null ? m_Transform : (m_Transform = transform);

        protected BoxCollider Collider =>
            boxCollider != null ? boxCollider : (boxCollider = GetComponent<BoxCollider>());

        protected Bounds WorldBounds => Collider.bounds;

        public virtual Bounds? Bounds => new(Collider.center, Collider.size);

        public bool ColliderEnabled
        {
            set => Collider.enabled = value;
        }

        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public virtual int Layer
        {
            get => gameObject.layer;
            set => gameObject.layer = value;
        }

        public virtual void Suspend()
        {
            Visible = true;
        }
    }
}