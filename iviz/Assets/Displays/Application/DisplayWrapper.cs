#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public abstract class DisplayWrapper : MonoBehaviour, IDisplay, ISupportsLayer
    {
        Transform? mTransform;

        protected abstract IDisplay Display { get; }
        public Transform Transform => this.EnsureHasTransform(ref mTransform);
        public Bounds? Bounds => Display.Bounds;

        public int Layer
        {
            set
            {
                gameObject.layer = value;
                if (Display is ISupportsLayer supportsLayer)
                {
                    supportsLayer.Layer = value;
                }
            }
        }

        public virtual void Suspend()
        {
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
    }
}