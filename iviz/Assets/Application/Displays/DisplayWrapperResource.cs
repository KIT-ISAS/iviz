#nullable enable

using System;
using UnityEngine;

namespace Iviz.Displays
{
    public abstract class DisplayWrapperResource : MonoBehaviour, IDisplay
    {
        Transform? mTransform;

        protected abstract IDisplay Display { get; }
        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        public string Name
        {
            set => gameObject.name = value;
        }

        public Bounds? Bounds => Display.Bounds;

        public int Layer
        {
            set
            {
                gameObject.layer = value;
                Display.Layer = value;
            }
        }

        public virtual void Suspend()
        {
        }

        public bool Visible
        {
            set => gameObject.SetActive(value);
        }
    }
}