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
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public Bounds? Bounds => Display.Bounds;

        public int Layer
        {
            get => Display.Layer;
            set
            {
                gameObject.layer = value;
                Display.Layer = value;
            }
        }

        public virtual void Suspend()
        {
        }

        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
    }
}