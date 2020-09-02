using UnityEngine;

namespace Iviz.Displays
{
    public abstract class MarkerResource : MonoBehaviour, IDisplay
    {
        protected BoxCollider Collider;

        public Bounds Bounds => Collider is null ? new Bounds() : new Bounds(Collider.center, Collider.size);
        public Bounds WorldBounds => Collider?.bounds ?? new Bounds();

        public virtual string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public bool ColliderEnabled
        {
            get => Collider?.enabled ?? false;
            set
            {
                if (!(Collider is null))
                {
                    Collider.enabled = value;
                }
            }
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public Vector3 WorldScale => transform.lossyScale;

        public Pose WorldPose => transform.AsPose();

        public virtual int Layer
        {
            get => gameObject.layer;
            set => gameObject.layer = value;
        }

        protected virtual void Awake()
        {
            Collider = GetComponent<BoxCollider>();
        }

        public virtual void Suspend()
        {
            Layer = 0;
        }
    }
}