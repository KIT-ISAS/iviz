#nullable enable

using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(AxisFrameResource))]
    public class CameraOverlayDisplay : MonoBehaviour
    {
        static readonly Quaternion BaseTransform = Quaternion.AngleAxis(90, Vector3.up);

        [SerializeField] AxisFrameResource? resource;
        [SerializeField] Camera? parentCamera;
        [SerializeField] Camera? grandparentCamera;

        Vector3 settings;
        
        Transform? mTransform;
        AxisFrameResource Resource => resource.AssertNotNull(nameof(resource));
        Camera ParentCamera => parentCamera.AssertNotNull(nameof(parentCamera));

        Camera GrandparentCamera => grandparentCamera != null
            ? grandparentCamera
            : grandparentCamera = ParentCamera.transform.parent.GetComponent<Camera>()
                .AssertNotNull(nameof(grandparentCamera));

        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        void Start()
        {
            Resource.ColorX = Resources.Resource.Colors.CameraOverlayAxisX;
            Resource.ColorY = Resources.Resource.Colors.CameraOverlayAxisY;
            Resource.ColorZ = Resources.Resource.Colors.CameraOverlayAxisZ;
            Resource.AxisLength = 0.001f;
            Resource.Layer = gameObject.layer;
            CheckSettings();
        }

        void CheckSettings()
        {
            var newSettings = new Vector3(
                ParentCamera.pixelWidth,
                ParentCamera.pixelHeight,
                ParentCamera.nearClipPlane
            );

            if (newSettings == settings)
            {
                return;
            }
            
            settings = newSettings;
            var point = new Vector3(
                0.9f * newSettings.x,
                0.9f * newSettings.y,
                newSettings.z + Resource.AxisLength);
            var widgetWorldPos = ParentCamera.ScreenToWorldPoint(point);
            Transform.position = widgetWorldPos;
        }

        void LateUpdate()
        {
            bool isParentEnabled = GrandparentCamera.enabled;
            if (Resource.Visible != isParentEnabled)
            {
                ParentCamera.enabled = isParentEnabled;
                Resource.Visible = isParentEnabled;
                return;
            }

            CheckSettings();
            Transform.rotation = TfListener.OriginFrame.Transform.rotation * BaseTransform;
        }
    }
}