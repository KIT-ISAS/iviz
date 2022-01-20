#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// A stand-alone display that has no dependencies outside of the GameObject.
    /// More complex displays are usually a composition of marker displays.
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    public abstract class MarkerDisplay : MonoBehaviour, IDisplay, ISupportsLayer
    {
        [SerializeField] Transform? m_Transform;
        [SerializeField] BoxCollider? boxCollider;

        protected Bounds WorldBounds => Collider.bounds;
        public Transform Transform => m_Transform != null ? m_Transform : (m_Transform = transform);
        public BoxCollider Collider => boxCollider != null ? boxCollider : (boxCollider = GetComponent<BoxCollider>());
        public virtual Bounds? Bounds => Collider.GetBounds();

        public bool EnableCollider
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
            set => gameObject.layer = value;
        }

        public virtual void Suspend()
        {
            Visible = true;
        }
    }
}