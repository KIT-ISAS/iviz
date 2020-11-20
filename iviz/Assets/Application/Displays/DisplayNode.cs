using UnityEngine;
using System;
using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    public abstract class DisplayNode : MonoBehaviour
    {
        TfFrame parent;

        [CanBeNull] public virtual TfFrame Parent
        {
            get => parent;
            set => SetParent(value, true);
        }

        void SetParent(TfFrame newParent, bool attach)
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
                transform.SetParentLocal(newParent == null ? 
                    TfListener.RootFrame.transform : 
                    newParent.transform);
            }
        }

        public void AttachTo([NotNull] string parentId)
        {
            if (parentId == null)
            {
                throw new ArgumentNullException(nameof(parentId));
            }

            if (Parent == null || parentId != Parent.Id)
            {
                Parent = string.IsNullOrEmpty(parentId) ? 
                    TfListener.MapFrame : 
                    TfListener.GetOrCreateFrame(parentId);
            }
        }
        
        public void AttachTo([NotNull] string parentId, in Msgs.time timestamp)
        {
            if (parentId == null)
            {
                throw new ArgumentNullException(nameof(parentId));
            }

            AttachTo(parentId, timestamp.ToTimeSpan());
        }

        public void AttachTo([NotNull] string parentId, in TimeSpan timestamp)
        {
            if (parentId == null)
            {
                throw new ArgumentNullException(nameof(parentId));
            }

            transform.SetParentLocal(TfListener.MapFrame.transform);
            if (parentId.Length == 0)
            {
                transform.SetLocalPose(Pose.identity);
            }
            else
            {
                if (Parent == null || parentId != Parent.Id)
                {
                    TfFrame frame = TfListener.GetOrCreateFrame(parentId, this);
                    SetParent(frame, false);
                }

                if (!(Parent is null))
                {
                    transform.SetLocalPose(Parent.LookupPose(timestamp));
                }
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
        public static SimpleDisplayNode Instantiate([NotNull] string name, [CanBeNull] Transform transform = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            GameObject obj = new GameObject(name);
            SimpleDisplayNode node = obj.AddComponent<SimpleDisplayNode>();
            if (transform != null)
            {
                obj.transform.parent = transform;
            }
            else
            {
                node.Parent = TfListener.MapFrame;
            }

            return node;
        }
    }
}