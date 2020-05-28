using Iviz.App.Listeners;
using UnityEngine;

namespace Iviz.App.Displays
{
    public class Billboard : MonoBehaviour
    {
        public Transform parent;
        public Vector3 offset;

        void Start()
        {
            if (parent == null)
            {
                parent = transform.parent;
            }
        }

        void LateUpdate()
        {
            GameObject mainCamera = TFListener.MainCamera.gameObject;
            transform.LookAt(2 * transform.position - mainCamera.transform.position, Vector3.up);
            transform.position = parent.position + offset;
        }
    }
}