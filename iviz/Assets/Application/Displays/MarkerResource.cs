using UnityEngine;

namespace Iviz.App
{
    public abstract class MarkerResource : MonoBehaviour
    {
        Color color = Color.white;
        public Color Color {
            get => color;
            set {
                color = value;
                SetColor(value);
            }
        }

        public Collider Collider { get; private set; }

        public Bounds Bounds { get; protected set; }
        public Bounds WorldBounds => Collider.bounds;

        public virtual float Width
        {
            get => 0;
            set { }
        }

        public abstract void SetColor(Color color);

        protected virtual void Awake()
        {
            Collider = GetComponent<Collider>();
            Bounds = Collider.bounds;
        }
    }
}