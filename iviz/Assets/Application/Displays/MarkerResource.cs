using UnityEngine;

namespace Iviz.Displays
{
    public abstract class MarkerResource : MonoBehaviour, IDisplay
    {
        protected BoxCollider Collider;

        public Bounds Bounds => new Bounds(Collider.center, Collider.size);
        public Bounds WorldBounds => Collider.bounds;

        public abstract string Name { get; }

        public bool ColliderEnabled
        {
            get => Collider.enabled;
            set => Collider.enabled = value;
        }
        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }
        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(this);
        }
        protected virtual void Awake()
        {
            Collider = GetComponent<BoxCollider>();
        }
        public virtual void Stop()
        {
        }
    }
}