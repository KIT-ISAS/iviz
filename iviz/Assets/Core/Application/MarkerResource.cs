#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Iviz.Displays
{
    public abstract class MarkerResource : MonoBehaviour, IDisplay
    {
        Transform? mTransform;
        [SerializeField] BoxCollider? boxCollider;
        bool colliderEnabled = true;

        protected bool HasBoxCollider => boxCollider != null || TryGetBoxCollider(out boxCollider);

        bool TryGetBoxCollider([NotNullWhen(true)] out BoxCollider? bc) => (bc = GetComponent<BoxCollider>()) != null;

        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        [NotNull]
        protected BoxCollider BoxCollider
        {
            get => boxCollider != null || TryGetBoxCollider(out boxCollider)
                ? boxCollider
                : throw new NullReferenceException("This asset has no box collider!");
            set => boxCollider = value != null
                ? value
                : throw new ArgumentNullException(nameof(value), "Cannot set a null box collider!");
        }

        protected Bounds WorldBounds => BoxCollider.bounds;

        [NotNull]
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Transform Parent
        {
            get => Transform.parent;
            set => Transform.parent = value;
        }

        protected virtual void Awake()
        {
        }

        public Bounds? Bounds => HasBoxCollider ? new Bounds(BoxCollider.center, BoxCollider.size) : null;

        public bool ColliderEnabled
        {
            get => colliderEnabled;
            set
            {
                colliderEnabled = value;
                if (HasBoxCollider)
                {
                    BoxCollider.enabled = value;
                }
            }
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