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
        FrameNode Node => (node != null) ? node : node = FrameNode.Instantiate("Widget Node");

        Transform mTransform;
        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        public Bounds? Bounds => new Bounds(BoxCollider.center, BoxCollider.size);

        public int Layer { get; set; }

        public void Initialize()
        {
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

        [CanBeNull] public string Id { get; internal set; }

        protected virtual void OnDestroy()
        {
            Destroy(Node);
        }

        public void AttachTo(string parentId)
        {
            if (Transform.parent != Node.Transform)
            {
                Transform.parent = Node.Transform;
                Transform.SetLocalPose(Pose.identity);
            }

            Node.AttachTo(parentId);
        }
    }
}