#nullable enable

using UnityEngine;
using System;
using Iviz.Core;

namespace Iviz.Controllers.TF
{
    /// <summary>
    /// A wrapper around a GameObject that can be attached to a <see cref="TfFrame"/>.
    /// TfFrames should not have displays as direct children. Instead, controllers should create a FrameNode,
    /// attach the node to the TFFrame, and then the displays to this node.
    /// FrameNodes allow <see cref="TfModule"/> to keep track of which frames are being used, and to forget those that are empty if necessary. 
    /// </summary>
    public class FrameNode // not sealed! has children
    {
        readonly GameObject gameObject;
        TfFrame? parent;
        bool disposed;
        string name;

        public Transform Transform { get; }

        public string? ParentId => parent?.Id;

        public virtual TfFrame? Parent
        {
            get => parent;
            set => SetParent(value);
        }

        void SetParent(TfFrame? value)
        {
            if (parent == value || !IsAlive || value is { IsAlive: false })
            {
                return;
            }

            parent?.RemoveListener(this);
            parent = value;
            parent?.AddListener(this);

            Transform.SetParentLocal(value?.Transform);
        }

        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                gameObject.name = $"[{value}]";
            }
        }

        protected FrameNode()
        {
            name = "";
            gameObject = new GameObject();
            Transform = gameObject.transform;
        }

        public FrameNode(string name, TfFrame? parent = null) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            SetParent(parent is { IsAlive: true } ? parent : TfModule.DefaultFrame);
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
                Parent = parentId.Length == 0
                    ? TfModule.DefaultFrame
                    : TfModule.GetOrCreateFrame(parentId);
            }
        }

        protected virtual void Stop()
        {
            Parent = null;
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

        public bool IsAlive => !disposed;

        public override string ToString() => "{" + Name + "}";

        public static FrameNode Instantiate(string name)
        {
            return new FrameNode(name);
        }
    }
}