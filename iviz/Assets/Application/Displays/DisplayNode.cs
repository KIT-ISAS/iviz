using UnityEngine;
using System;

namespace Iviz.Controllers
{
    public abstract class DisplayNode : MonoBehaviour
    {
        TFFrame parent;

        public virtual TFFrame Parent
        {
            get => parent;
            set => SetParent(value, true);
        }

        void SetParent(TFFrame newParent, bool attach)
        {
            if (newParent == parent)
            {
                return;
            }

            parent?.RemoveListener(this);
            parent = newParent;
            parent?.AddListener(this);
            if (attach)
            {
                transform.SetParentLocal(newParent is null ? 
                    TFListener.RootFrame.transform : 
                    newParent.transform);
            }
        }

        public void AttachTo(string parentId)
        {
            if (Parent is null || parentId != Parent.Id)
            {
                Parent = string.IsNullOrEmpty(parentId) ? 
                    TFListener.MapFrame : 
                    TFListener.GetOrCreateFrame(parentId);
            }
        }

        public void AttachTo(string parentId, in Msgs.time timestamp)
        {
            AttachTo(parentId, timestamp.ToTimeSpan());
        }

        public void AttachTo(string parentId, in TimeSpan timestamp)
        {
            transform.SetParentLocal(TFListener.MapFrame.transform);
            if (parentId.Length == 0)
            {
                transform.SetPose(Pose.identity);
            }
            else
            {
                if (Parent is null || parentId != Parent.Id)
                {
                    TFFrame frame = TFListener.GetOrCreateFrame(parentId, this);
                    SetParent(frame, false);
                }

                transform.SetPose(Parent.GetPose(timestamp));
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
            if (!(transform is null))
            {
                obj.transform.parent = transform;
            }
            else
            {
                node.Parent = TFListener.MapFrame;
            }

            return node;
        }
    }
}