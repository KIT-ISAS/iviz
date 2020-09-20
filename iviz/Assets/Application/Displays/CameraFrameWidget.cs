using System;
using Iviz.Controllers;
using Iviz.Displays;
using UnityEngine;

namespace Application.Displays
{
    public class CameraFrameWidget : MonoBehaviour
    {
        Camera parentCamera;
        AxisFrameResource resource;

        void Start()
        {
            resource = GetComponent<AxisFrameResource>();
            resource.ColorX = new Color(0.9f, 0.4f, 0, 1);
            resource.ColorY = new Color(0, 0.9f, 0.6f, 1);
            resource.ColorZ = new Color(0.6f, 0, 0.9f, 1);

            resource.AxisLength = 0.001f;
            resource.Layer = gameObject.layer;

            SetCamera(transform.parent.GetComponent<Camera>());
        }

        void SetCamera(Camera newCamera)
        {
            parentCamera = newCamera;
            Vector3 point = new Vector3(
                0.9f * parentCamera.pixelWidth,
                0.9f * parentCamera.pixelHeight,
                parentCamera.nearClipPlane + resource.AxisLength);
            Vector3 widgetWorldPos = parentCamera.ScreenToWorldPoint(point);

            transform.SetParent(newCamera.transform, false);
            transform.position = widgetWorldPos;
        }


        static readonly Quaternion BaseTransform = Quaternion.AngleAxis(90, Vector3.up);

        void LateUpdate()
        {
            transform.rotation = TFListener.RootFrame.transform.rotation * BaseTransform;

            if (TFListener.MainCamera is null || parentCamera == TFListener.MainCamera)
            {
                return;
            }

            SetCamera(TFListener.MainCamera);
        }
    }
}