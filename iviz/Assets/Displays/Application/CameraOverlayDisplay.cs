#nullable enable

using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Axis frame shown on the top right of the camera overlay.
    /// </summary>
    [RequireComponent(typeof(AxisFrameDisplay))]
    public class CameraOverlayDisplay : MonoBehaviour
    {
        [SerializeField] AxisFrameDisplay? resource;
        [SerializeField] Camera? parentCamera;
        [SerializeField] Camera? grandparentCamera;

        Vector3 settings;
        
        Transform? mTransform;
        AxisFrameDisplay Display => resource.AssertNotNull(nameof(resource));
        Camera ParentCamera => parentCamera.AssertNotNull(nameof(parentCamera));

        Camera GrandparentCamera => grandparentCamera != null
            ? grandparentCamera
            : grandparentCamera = ParentCamera.transform.parent.GetComponent<Camera>()
                .AssertNotNull(nameof(grandparentCamera));

        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        void Start()
        {
            Display.ColorX = Resources.Resource.Colors.CameraOverlayAxisX;
            Display.ColorY = Resources.Resource.Colors.CameraOverlayAxisY;
            Display.ColorZ = Resources.Resource.Colors.CameraOverlayAxisZ;
            Display.AxisLength = 0.001f;
            Display.Layer = gameObject.layer;
            Display.Emissive = 0.4f;
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
                newSettings.z + Display.AxisLength);
            var widgetWorldPos = ParentCamera.ScreenToWorldPoint(point);
            Transform.position = widgetWorldPos;
        }

        void LateUpdate()
        {
            bool isParentEnabled = GrandparentCamera.enabled;
            if (Display.Visible != isParentEnabled)
            {
                ParentCamera.enabled = isParentEnabled;
                Display.Visible = isParentEnabled;
                return;
            }

            CheckSettings();

            var baseTransform = Quaternions.Rotate90AroundY;
            Transform.rotation = TfModule.OriginFrame.Transform.rotation * baseTransform;
        }
    }
}