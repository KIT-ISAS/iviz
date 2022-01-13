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
    public class FrameNode
    {
        bool disposed;
        TfFrame? parent;

        readonly GameObject gameObject;
        public Transform Transform { get; }

        public virtual TfFrame? Parent
        {
            get => parent;
            set
            {
                //SetParent(value, true);
                if (parent == value || !IsAlive)
                {
                    return;
                }

                parent?.RemoveListener(this);
                parent = value;
                parent?.AddListener(this);

                Transform.SetParentLocal(value == null ? TfListener.OriginFrame.Transform : value.Transform);
            }
        }

        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public string? ParentId => parent?.Id;

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        protected FrameNode()
        {
            gameObject = new GameObject();
            Transform = gameObject.transform;
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
            if (!frame.IsAlive)
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

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            Stop();
            UnityEngine.Object.Destroy(gameObject);
            disposed = true;
        }

        sealed class SimpleFrameNode : FrameNode
        {
        }

        public bool IsAlive => !disposed;

        public static FrameNode Instantiate(string name)
        {
            return new SimpleFrameNode
            {
                Name = name ?? throw new ArgumentNullException(nameof(name)),
                Parent = TfListener.DefaultFrame
            };
        }
    }
}