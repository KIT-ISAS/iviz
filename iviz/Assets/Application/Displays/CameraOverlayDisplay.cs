using Iviz.Controllers;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Application.Displays
{
    public class CameraOverlayDisplay : MonoBehaviour
    {
        static readonly Quaternion BaseTransform = Quaternion.AngleAxis(90, Vector3.up);
        
        AxisFrameResource resource;
        [SerializeField] Camera parentCamera;
        [SerializeField] Camera grandparentCamera;
        
        void Start()
        {
            resource = GetComponent<AxisFrameResource>();
            resource.ColorX = Resource.Colors.CameraOverlayAxisX;
            resource.ColorY = Resource.Colors.CameraOverlayAxisY;
            resource.ColorZ = Resource.Colors.CameraOverlayAxisZ;

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

        void LateUpdate()
        {
            bool isParentEnabled = grandparentCamera.enabled;
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