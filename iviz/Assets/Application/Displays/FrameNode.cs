using UnityEngine;
using System;
using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    /// <summary>
    /// Class for displays that want to attach themselves to a TF frame.
    /// This increases the reference count of the frame, and prevents the TFListener from
    /// removing it.
    /// Also used by controllers to have a GameObject they can attach their displays to.
    /// </summary>
    public abstract class FrameNode : MonoBehaviour
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

            if (parent != null)
            {
                parent.RemoveListener(this);
            }

            parent = newParent;
            if (parent != null)
            {
                parent.AddListener(this);
            }

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

        void AttachTo([NotNull] string parentId, in TimeSpan timestamp)
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

                if (Parent != null)
                {
                    transform.SetLocalPose(TfListener.RelativePoseToRoot(Parent.LookupPose(timestamp)));
                }
            }
        }

        public virtual void Stop()
        {
            Parent = null;
        }
        
        sealed class SimpleFrameNode : FrameNode
        {
        }
        
        public static FrameNode Instantiate([NotNull] string name, [CanBeNull] Transform transform = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            GameObject obj = new GameObject(name);
            SimpleFrameNode node = obj.AddComponent<SimpleFrameNode>();
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