using UnityEngine;
using System;

namespace Iviz.App
{
    public interface IRecyclable
    {
        void Recycle();
    }

    public interface IDisplay
    {
        string Name { get; }
        Bounds Bounds { get; }
        Bounds WorldBounds { get; }
        bool ColliderEnabled { get; set; }
        void Stop();
        Transform Parent { get; set; }
    }


    public abstract class DisplayNode : MonoBehaviour
    {
        public static Color EnabledFontColor { get; } = new Color(0.196f, 0.196f, 0.196f);
        public static Color DisabledFontColor { get; } = EnabledFontColor * 3;

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
        public static SimpleDisplayNode Instantiate(Transform transform)
        {
            GameObject obj = new GameObject("DisplayNode");
            obj.transform.parent = transform;
            return obj.AddComponent<SimpleDisplayNode>();
        }
    }



}