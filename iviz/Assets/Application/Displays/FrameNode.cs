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
        bool disposed;
        [CanBeNull] TfFrame parent;

        Transform mTransform;
        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        [CanBeNull]
        public virtual TfFrame Parent
        {
            get => parent;
            set => SetParent(value, true);
        }

        [NotNull] public string ParentId => parent != null ? parent.Id : ""; 

        [NotNull]
        protected string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        void SetParent([CanBeNull] TfFrame newParent, bool attach)
        {
            if (!IsAlive)
            {
                return; // destroying!
            }
            
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
                Transform.SetParentLocal(newParent == null ? TfListener.OriginFrame.Transform : newParent.Transform);
            }
        }

        public void AttachTo(in Msgs.StdMsgs.Header header)
        {
            AttachTo(header.FrameId, header.Stamp);
        }

        public void AttachTo([CanBeNull] string parentId)
        {
            AttachTo(parentId, default);
        }

        void AttachTo([CanBeNull] string parentId, in Msgs.time _)
        {
            if (parentId == null)
            {
                return;
            }

            if (Parent == null || parentId != Parent.Id)
            {
                Parent = string.IsNullOrEmpty(parentId) ? TfListener.DefaultFrame : TfListener.GetOrCreateFrame(parentId);
            }
        }

        protected virtual void Stop()
        {
            Parent = null;
            disposed = true;
        }

        public void DestroySelf()
        {
            Stop();
            Destroy(gameObject);
            disposed = true;
        }

        sealed class SimpleFrameNode : FrameNode
        {
        }

        public bool IsAlive => !disposed && this != null;

        [NotNull]
        public static FrameNode Instantiate([NotNull] string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var node = new GameObject(name).AddComponent<SimpleFrameNode>();
            if (TfListener.Instance != null && TfListener.DefaultFrame != null)
            {
                node.Parent = TfListener.DefaultFrame;
            }

            return node;
        }
    }
}