using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Displays;
using UnityEngine;

namespace Application.Displays
{
    public class CameraFrameWidget : MonoBehaviour
    {
        AxisFrameResource resource;

        void Start()
        {
            resource = GetComponent<AxisFrameResource>();
            resource.ColorX = new Color(0.9f, 0.4f, 0, 1);
            resource.ColorY = new Color(0, 0.9f, 0.6f, 1);
            resource.ColorZ = new Color(0.6f, 0, 0.9f, 1);

            resource.AxisLength = 0.001f;
            resource.Layer = gameObject.layer;

            Camera ownCamera = transform.parent.GetComponent<Camera>();
            Vector3 point = new Vector3(
                0.9f * ownCamera.pixelWidth,
                0.9f * ownCamera.pixelHeight,
                ownCamera.nearClipPlane + resource.AxisLength);
            Vector3 widgetWorldPos = ownCamera.ScreenToWorldPoint(point);
            transform.position = widgetWorldPos;
        }

        static readonly Quaternion BaseTransform = Quaternion.AngleAxis(90, Vector3.up);

        void LateUpdate()
        {
            transform.rotation = TFListener.RootFrame.transform.rotation * BaseTransform;
        }
    }
}