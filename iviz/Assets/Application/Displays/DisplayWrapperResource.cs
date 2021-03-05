using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public abstract class DisplayWrapperResource : MonoBehaviour, IDisplay, IRecyclable
    {
        const string ErrorNoDisplayMessage = "Display has not been set! Make sure to set a display beforehand";

        [CanBeNull] protected abstract IDisplay Display { get; }

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public Bounds? Bounds => Display?.Bounds;
        
        public int Layer
        {
            get => Display?.Layer ?? throw new NullReferenceException(ErrorNoDisplayMessage);
            set
            {
                if (Display == null)
                {
                    throw new NullReferenceException(ErrorNoDisplayMessage);
                }

                Display.Layer = value;
            }
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }
        
        public virtual void Suspend()
        {
            Display?.Suspend();
        }

        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void SplitForRecycle()
        {
            if (Display != null)
            {
                ResourcePool.TryReturnDisplay(Display);
            }
        }
    }
}