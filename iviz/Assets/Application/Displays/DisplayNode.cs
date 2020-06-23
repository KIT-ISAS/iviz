using UnityEngine;
using System;
using Iviz.App.Listeners;

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
                SetParent(value, true);
            }
        }

        void SetParent(TFFrame newParent, bool attach)
        {
            if (newParent != parent)
            {
                if (parent != null)
                {
                    parent.RemoveListener(this);
                }
                parent = newParent;
                if (parent != null)
                {
                    //parent.AddListener(this);
                }
                else
                {
                    //Debug.LogWarning("Display: Setting parent of " + name + " to null! (ok if removing)");
                }
            }
            if (attach)
            {
                transform.SetParentLocal(newParent?.transform);
            }
        }

        public virtual void AttachTo(string parentId)
        {
            Parent = (parentId == "") ? TFListener.BaseFrame : TFListener.GetOrCreateFrame(parentId);
        }

        public virtual void AttachTo(string parentId, in Msgs.time timestamp)
        {
            AttachTo(parentId, timestamp.ToTimeSpan());
        }

        public virtual void AttachTo(string parentId, in TimeSpan timestamp)
        {
            transform.SetParentLocal(TFListener.BaseFrame.transform);
            if (parentId == "")
            {
                transform.SetPose(Pose.identity);
            }
            else
            {
                TFFrame frame = TFListener.GetOrCreateFrame(parentId, this);
                SetParent(frame, false);
                transform.SetPose(frame.GetPose(timestamp));
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
                node.Parent = TFListener.BaseFrame;
            }
            return node;
        }
    }
}