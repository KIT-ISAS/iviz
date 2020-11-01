using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class Billboard : MonoBehaviour
    {
        public Transform parent;
        public Vector3 offset;

        public bool UseAbsoluteOffset { get; set; } = true;

        void Start()
        {
            if (parent == null)
            {
                parent = transform.parent;
            }
        }

        void LateUpdate()
        {
            GameObject mainCamera = Settings.MainCamera.gameObject;
            transform.LookAt(2 * transform.position - mainCamera.transform.position, Vector3.up);
            if (UseAbsoluteOffset && parent != null)
            {
                transform.position = parent.position + offset;
            } 
        }
    }
}