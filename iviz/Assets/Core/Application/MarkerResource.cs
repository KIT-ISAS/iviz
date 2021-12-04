﻿#nullable enable

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

        protected Bounds WorldBounds => Collider.bounds;
        public Transform Transform => m_Transform != null ? m_Transform : (m_Transform = transform);
        public BoxCollider Collider => boxCollider != null ? boxCollider : (boxCollider = GetComponent<BoxCollider>());
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
            set => gameObject.layer = value;
        }

        public virtual void Suspend()
        {
            Visible = true;
        }
    }
}