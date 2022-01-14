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
                gameObject.name = value;
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
            SetParent(parent is { IsAlive: true } ? parent : TfListener.DefaultFrame);
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
                    ? TfListener.DefaultFrame
                    : TfListener.GetOrCreateFrame(parentId);
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
        /*
        public bool IsAlive
        {
            get
            {
                if (disposed)
                {
                    return false;
                }

                
#if UNITY_EDITOR
                if (gameObject == null || Transform == null)
                {
                    Debug.LogError($"{this}: FrameNode has been deleted before being disposed!");
                    return false;
                }
#endif

                return true;
            }
        }
        */

        public override string ToString() => "{" + Name + "}";

        public static FrameNode Instantiate(string name)
        {
            return new FrameNode(name);
        }
    }
}