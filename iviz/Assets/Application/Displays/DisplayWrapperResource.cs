using UnityEngine;

namespace Iviz.Displays
{
    public abstract class DisplayWrapperResource : MonoBehaviour, IDisplay
    {
        protected abstract IDisplay Display { get; }

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public Bounds Bounds => Display.Bounds;
        public Bounds WorldBounds => Display.WorldBounds;
        public Pose WorldPose => Display.WorldPose;
        public Vector3 WorldScale => Display.WorldScale;
        
        public int Layer
        {
            get => Display.Layer;
            set => Display.Layer = value;
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        public bool ColliderEnabled
        {
            get => Display.ColliderEnabled;
            set => Display.ColliderEnabled = value;
        }        
        
        public virtual void Suspend()
        {
            Display.Suspend();
        }

        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
    }
}