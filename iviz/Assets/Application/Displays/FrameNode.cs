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
        TfFrame parent;

        Transform mTransform;
        public Transform Transform => mTransform.SafeNull() ?? (mTransform = transform);

        IFrameNodeOwner owner;
        
        [CanBeNull]
        public virtual TfFrame Parent
        {
            get => parent;
            set => SetParent(value, true);
        }

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
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
                Transform.SetParentLocal(newParent == null ? TfListener.OriginFrame.Transform : newParent.Transform);
            }
        }

        public void AttachTo([CanBeNull] string parentId, Msgs.time _ = default)
        {
            if (parentId == null)
            {
                return;
            }

            if (Parent == null || parentId != Parent.Id)
            {
                Parent = string.IsNullOrEmpty(parentId) ? TfListener.MapFrame : TfListener.GetOrCreateFrame(parentId);
            }
        }

        public virtual void Stop()
        {
            Parent = null;
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

            GameObject obj = new GameObject(name);
            SimpleFrameNode node = obj.AddComponent<SimpleFrameNode>();
            if (TfListener.Instance != null)
            {
                node.Parent = TfListener.MapFrame;
            }
            else
            {
                // TF not initialized yet
                node.transform.parent = TfListener.UnityFrame.transform;
            }

            node.owner = owner;
            return node;
        }
    }
}