using UnityEngine;

namespace Iviz.Displays
{
    public abstract class MarkerResource : MonoBehaviour, IDisplay
    {
        protected BoxCollider boxCollider;

        public Bounds Bounds => boxCollider == null ? new Bounds() : new Bounds(boxCollider.center, boxCollider.size);
        public Bounds WorldBounds => boxCollider == null ? new Bounds() : boxCollider.bounds;

        public virtual string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        bool colliderEnabled = true;
        public bool ColliderEnabled
        {
            get => colliderEnabled;
            set
            {
                colliderEnabled = value;
                if (boxCollider != null)
                {
                    boxCollider.enabled = value;
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

        public virtual Vector3 WorldScale => transform.lossyScale;

        public virtual Pose WorldPose => transform.AsPose();

        public virtual int Layer
        {
            get => gameObject.layer;
            set => gameObject.layer = value;
        }

        protected virtual void Awake()
        {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }

            ColliderEnabled = ColliderEnabled;
        }

        public virtual void Suspend()
        {
            Layer = 0;
        }
    }
}