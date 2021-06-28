using System;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class ARWidget : MonoBehaviour, IDisplay
    {
        const float PopupDuration = 0.1f;

        [SerializeField, CanBeNull] BoxCollider boxCollider = null;

        protected BoxCollider BoxCollider =>
            boxCollider != null ? boxCollider : (boxCollider = GetComponent<BoxCollider>());

        FrameNode node;
        [NotNull] FrameNode Node => (node != null) ? node : node = FrameNode.Instantiate("Widget Node");
        [NotNull] public TfFrame ParentFrame => Node.Parent.CheckedNull() ?? TfListener.DefaultFrame;

        Transform mTransform;
        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        float? popupStartTime;

        public Bounds? Bounds => new Bounds(BoxCollider.center, BoxCollider.size);

        public virtual Color MainColor { get; set; }
        public virtual Color SecondaryColor { get; set; }

        public int Layer { get; set; }

        float scale = 1;

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                if (popupStartTime != null)
                {
                    transform.localScale = scale * Vector3.one;
                }
            }
        }

        protected virtual void Awake()
        {
            if (Settings.IsHololens)
            {
                var meshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
                foreach (var meshRenderer in meshRenderers)
                {
                    meshRenderer.material = Resource.Materials.Lit.Object;
                }
            }
        }

        public virtual void Initialize()
        {
            popupStartTime = Time.time;
            Update();
        }

        public virtual void Suspend()
        {
            Node.Parent = null;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public string Name { get; set; }

        [NotNull] public string Id { get; internal set; } = "";

        protected virtual void OnDestroy()
        {
            if (node == null)
            {
                return;
            }

            node.DestroySelf();
        }

        public void AttachTo(string parentId)
        {
            if (Transform.parent != Node.Transform)
            {
                Transform.SetParentLocal(Node.Transform);
                Transform.SetLocalPose(Pose.identity);
            }
            
            Node.AttachTo(parentId);
        }

        protected virtual void Update()
        {
            if (popupStartTime == null)
            {
                return;
            }

            float now = Time.time;
            if (now - popupStartTime.Value < PopupDuration)
            {
                float tempScale = (now - popupStartTime.Value) / PopupDuration;
                transform.localScale = (tempScale * 0.5f + 0.5f) * Scale * Vector3.one;
            }
            else
            {
                transform.localScale = Scale * Vector3.one;
                popupStartTime = null;
            }
        }
    }
}