using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public abstract class MarkerResource : MonoBehaviour, IDisplay
    {
        [SerializeField] [CanBeNull] BoxCollider boxCollider;

        bool colliderEnabled = true;

        protected bool HasBoxCollider => boxCollider != null;

        [NotNull]
        protected BoxCollider BoxCollider
        {
            get => boxCollider != null
                ? boxCollider
                : throw new NullReferenceException("This asset has no box collider!");
            set => boxCollider =
                value != null
                    ? value
                    : throw new ArgumentNullException(nameof(value), "Cannot set a null box collider!");
        }

        protected Bounds WorldBounds => BoxCollider.bounds;

        public string Name
        {
            get => gameObject.name;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));

                gameObject.name = value;
            }
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        protected virtual void Awake()
        {
            if (boxCollider == null) boxCollider = GetComponent<BoxCollider>();

            ColliderEnabled = ColliderEnabled;
        }

        public Bounds? Bounds => HasBoxCollider ? new Bounds(BoxCollider.center, BoxCollider.size) : (Bounds?) null;

        public bool ColliderEnabled
        {
            get => colliderEnabled;
            set
            {
                colliderEnabled = value;
                if (HasBoxCollider) BoxCollider.enabled = value;
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
            Layer = 0;
        }

        protected void DisableBoxCollider()
        {
            boxCollider = null;
        }
    }
}