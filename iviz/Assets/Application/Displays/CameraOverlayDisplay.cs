using Iviz.Controllers;
using Iviz.Displays;
using UnityEngine;

namespace Application.Displays
{
    public class CameraOverlayDisplay : MonoBehaviour
    {
        AxisFrameResource resource;
        [SerializeField] Camera parentCamera;
        [SerializeField] Camera grandparentCamera;

        void Start()
        {
            resource = GetComponent<AxisFrameResource>();
            resource.ColorX = new Color(0.9f, 0.4f, 0, 1);
            resource.ColorY = new Color(0, 0.9f, 0.6f, 1);
            resource.ColorZ = new Color(0.6f, 0, 0.9f, 1);

            resource.AxisLength = 0.001f;
            resource.Layer = gameObject.layer;

            parentCamera = transform.parent.GetComponent<Camera>();
            Vector3 point = new Vector3(
                0.9f * parentCamera.pixelWidth,
                0.9f * parentCamera.pixelHeight,
                parentCamera.nearClipPlane + resource.AxisLength);
            Vector3 widgetWorldPos = parentCamera.ScreenToWorldPoint(point);
            transform.position = widgetWorldPos;

            grandparentCamera = parentCamera.transform.parent.GetComponentInParent<Camera>();
        }

        static readonly Quaternion BaseTransform = Quaternion.AngleAxis(90, Vector3.up);

        void LateUpdate()
        {
            var isParentEnabled = grandparentCamera.enabled;
            if (resource.Visible != isParentEnabled)
            {
                parentCamera.enabled = isParentEnabled;
                resource.Visible = isParentEnabled;
                return;
            }

            transform.rotation = TfListener.RootFrame.transform.rotation * BaseTransform;
        }
    }
}