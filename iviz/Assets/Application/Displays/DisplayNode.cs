using UnityEngine;
using System;
using Iviz.App.Listeners;

namespace Iviz.Displays
{
    public interface IRecyclable
    {
        void Recycle();
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
                /*
                if (value != null)
                {
                    transform.SetPose(value.transform.AsPose());
                }
                */
            }
        }

        public virtual void AttachTo(string parentId)
        {
            Parent = (parentId == "") ? TFListener.ListenersFrame : TFListener.GetOrCreateFrame(parentId);
        }

        public virtual void AttachTo(string parentId, DateTime timestamp)
        {
            Parent = TFListener.ListenersFrame;
            if (parentId == "")
            {
                transform.SetPose(Pose.identity);
            }
            else
            {
                transform.SetPose(TFListener.GetOrCreateFrame(parentId).GetPose(timestamp));
            }
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
        public static SimpleDisplayNode Instantiate(string name, Transform transform = null)
        {
            GameObject obj = new GameObject(name);
            SimpleDisplayNode node = obj.AddComponent<SimpleDisplayNode>();
            if (transform != null)
            {
                obj.transform.parent = transform;
            }
            else
            {
                node.Parent = TFListener.ListenersFrame;
            }
            return node;
        }
    }
}