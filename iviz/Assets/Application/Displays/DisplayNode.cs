using UnityEngine;
using System;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public interface IRecyclable
    {
        void Recycle();
    }
}

namespace Iviz.Displays
{
    public interface IDisplay
    {
        string Name { get; }
        Bounds Bounds { get; }
        Bounds WorldBounds { get; }
        bool ColliderEnabled { get; set; }
        void Stop();
        Transform Parent { get; set; }
    }
}

namespace Iviz.App.Displays
{
    public abstract class DisplayNode : MonoBehaviour
    {
        TFFrame parent;
        public virtual TFFrame Parent
        {
            get => parent;
            set
            {
                if (value != parent)
                {
                    if (parent != null)
                    {
                        parent.RemoveListener(this);
                    }
                    parent = value;
                    if (parent != null)
                    {
                        parent.AddListener(this);
                    }
                    else
                    {
                        //Debug.LogWarning("Display: Setting parent of " + name + " to null! (ok if removing)");
                    }
                }
                transform.SetParentLocal(value?.transform);
            }
        }

        public virtual void SetParent(string parentId)
        {
            Parent = (parentId == "") ? TFListener.ListenersFrame : TFListener.GetOrCreateFrame(parentId);
        }

        public event Action Stopped;

        public virtual void Stop()
        {
            Parent = null;
            Stopped?.Invoke();
            Stopped = null;
        }
    }

    public sealed class SimpleDisplayNode : DisplayNode
    {
        public static SimpleDisplayNode Instantiate(string name, Transform transform)
        {
            GameObject obj = new GameObject(name);
            obj.transform.parent = transform;
            return obj.AddComponent<SimpleDisplayNode>();
        }
    }



}