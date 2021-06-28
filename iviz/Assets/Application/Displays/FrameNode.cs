using UnityEngine;
using System;
using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    public interface IFrameNodeOwner
    {
        string Description { get; }
    }

    /// <summary>
    /// Class for displays that want to attach themselves to a TF frame.
    /// This increases the reference count of the frame, and prevents the TFListener from
    /// removing it.
    /// Also used by controllers to have a GameObject they can attach their displays to.
    /// </summary>
    public abstract class FrameNode : MonoBehaviour
    {
        [CanBeNull] TfFrame parent;

        Transform mTransform;
        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        IFrameNodeOwner owner;
        
        [CanBeNull]
        public virtual TfFrame Parent
        {
            get => parent;
            set => SetParent(value, true);
        }

        [NotNull]
        protected string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        void SetParent([CanBeNull] TfFrame newParent, bool attach)
        {
            if (gameObject == null)
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
        }

        public void DestroySelf()
        {
            Stop();
            Destroy(gameObject);
        }

        sealed class SimpleFrameNode : FrameNode
        {
        }

        [NotNull]
        public static FrameNode Instantiate([NotNull] string name, [CanBeNull] IFrameNodeOwner owner = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            SimpleFrameNode node = new GameObject(name).AddComponent<SimpleFrameNode>();
            if (TfListener.Instance != null && TfListener.DefaultFrame != null)
            {
                node.Parent = TfListener.DefaultFrame;
            }

            node.owner = owner;
            return node;
        }
    }
}