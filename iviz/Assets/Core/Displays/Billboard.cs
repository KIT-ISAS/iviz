using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class Billboard : MonoBehaviour
    {
        Transform mTransform;
        [SerializeField] Transform parent;
        [SerializeField] Vector3 offset;

        public Transform Parent { get; set; }
        public Vector3 Offset { get; set; }

        public bool UseAbsoluteOffset { get; set; } = true;

        void Start()
        {
            mTransform = transform;
            if (parent == null)
            {
                parent = mTransform.parent;
            }
        }

        void LateUpdate()
        {
            mTransform.LookAt(2 * mTransform.position - Settings.MainCameraTransform.position, Vector3.up);
            if (UseAbsoluteOffset && parent != null)
            {
                mTransform.position = parent.position + offset;
            } 
        }
    }
}