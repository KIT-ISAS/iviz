using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class ARWidget : MonoBehaviour, IDisplay
    {
        [SerializeField, CanBeNull] BoxCollider boxCollider = null;
        BoxCollider BoxCollider => boxCollider != null ? boxCollider : (boxCollider = GetComponent<BoxCollider>());

        FrameNode node;

        Transform mTransform;
        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        public Bounds? Bounds => new Bounds(BoxCollider.center, BoxCollider.size);

        public int Layer { get; set; }

        protected virtual void Awake()
        {
            node = FrameNode.Instantiate("Widget Node");
            Transform.parent = node.Transform;
        }

        public void Initialize()
        {
            Transform.parent = node.Transform;
            Transform.SetLocalPose(Pose.identity);
        }
        
        public virtual void Suspend()
        {
            node.Parent = null;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public string Name { get; set; }

        [CanBeNull] public string Id { get; internal set; }

        protected virtual void OnDestroy()
        {
            Destroy(node);
        }
        
        public void AttachTo(string parentId)
        {
            node.AttachTo(parentId);
        }        
        
    }
}