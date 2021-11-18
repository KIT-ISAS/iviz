#nullable enable

using UnityEngine;
using System;
using Iviz.Core;

namespace Iviz.Controllers.TF
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
        TfFrame? parent;

        Transform? mTransform;
        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        public virtual TfFrame? Parent
        {
            get => parent;
            set => SetParent(value, true);
        }

        public string? ParentId => parent != null ? parent.Id : null;

        protected string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        void SetParent(TfFrame? newParent, bool attach)
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
            AttachTo(header.FrameId);
        }

        public void AttachTo(string? parentId)
        {
            if (parentId == null)
            {
                return;
            }

            if (Parent == null || parentId != Parent.Id)
            {
                Parent = string.IsNullOrEmpty(parentId)
                    ? TfListener.DefaultFrame
                    : TfListener.GetOrCreateFrame(parentId);
            }
        }

        public void AttachTo(TfFrame frame)
        {
            if (frame == null)
            {
                Parent = TfListener.DefaultFrame;
            }
            else if (Parent == null || frame.Id != Parent.Id)
            {
                Parent = frame;
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

        public static FrameNode Instantiate(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var node = new GameObject(name).AddComponent<SimpleFrameNode>();
            if (TfListener.HasInstance)
            {
                node.Parent = TfListener.DefaultFrame;
            }

            return node;
        }
    }
}