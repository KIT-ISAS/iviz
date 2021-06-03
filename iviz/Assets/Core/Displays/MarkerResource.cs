using System;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Sdf;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public abstract class MarkerResource : MonoBehaviour, IDisplay
    {
        [SerializeField, CanBeNull] BoxCollider boxCollider;
        bool colliderEnabled = true;
        Transform mTransform;
        
        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        protected bool HasBoxCollider => boxCollider != null || TryGetBoxCollider(out boxCollider);

        [ContractAnnotation("=> false, bc:null; => true, bc:notnull")]
        bool TryGetBoxCollider(out BoxCollider bc) => (bc = GetComponent<BoxCollider>()) != null;

        [NotNull]
        protected BoxCollider BoxCollider
        {
            get => boxCollider != null || TryGetBoxCollider(out boxCollider)
                ? boxCollider
                : throw new NullReferenceException("This asset has no box collider!");
            set => boxCollider = value.CheckedNull()
                                 ?? throw new ArgumentNullException(nameof(value), "Cannot set a null box collider!");
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

        public Bounds? Bounds => HasBoxCollider ? new Bounds(BoxCollider.center, BoxCollider.size) : (Bounds?) null;

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

        public bool TryRaycast(in Vector2 cameraPoint, out Vector3 hit)
        {
            if (!HasBoxCollider)
            {
                hit = default;
                return false;
            }

            var ray = Settings.MainCamera.ScreenPointToRay(cameraPoint);
            if (BoxCollider.Raycast(ray, out var hitInfo, 1000f))
            {
                hit = hitInfo.point;
                return true;
            }

            hit = default;
            return false;
        }
    }
}