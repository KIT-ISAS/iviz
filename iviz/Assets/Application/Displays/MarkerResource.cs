using UnityEngine;

namespace Iviz.App
{
    public abstract class MarkerResource : MonoBehaviour, IDisplay
    {
        protected Collider Collider;

        public Bounds Bounds { get; protected set; }
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

        protected virtual void Awake()
        {
            Collider = GetComponent<Collider>();
            Bounds = Collider.bounds;
        }

        public virtual void Stop()
        {
        }
    }
}