using Iviz.App.Listeners;
using UnityEngine;

namespace Iviz.App.Displays
{
    public class Billboard : MonoBehaviour
    {
        public Camera mainCamera;
        public Transform parent;
        public Vector3 offset;

        void Start()
        {
            if (parent == null)
            {
                parent = transform.parent;
            }
            if (mainCamera == null)
            {
                mainCamera = TFListener.MainCamera;
            }
        }

        void LateUpdate()
        {
            //transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
            //    camera.transform.rotation * Vector3.up);
            transform.LookAt(2 * transform.position - mainCamera.transform.position, Vector3.up);
            transform.position = parent.position + offset;
        }
    }
}