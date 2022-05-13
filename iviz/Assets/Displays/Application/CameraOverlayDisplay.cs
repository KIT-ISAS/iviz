#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Axis frame shown on the top right of the camera overlay.
    /// </summary>
    [RequireComponent(typeof(AxisFrameDisplay))]
    public sealed class CameraOverlayDisplay : MonoBehaviour
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
            : grandparentCamera = ParentCamera.transform.parent.AssertHasComponent<Camera>(nameof(grandparentCamera));

        Transform Transform => this.EnsureHasTransform(ref mTransform);

        void Start()
        {
            Display.ColorX = Resources.Resource.Colors.CameraOverlayAxisX;
            Display.ColorY = Resources.Resource.Colors.CameraOverlayAxisY;
            Display.ColorZ = Resources.Resource.Colors.CameraOverlayAxisZ;
            Display.AxisLength = 0.001f;
            Display.Layer = gameObject.layer;
            Display.Emissive = 0.4f;
            CheckSettings();

            GameThread.EveryFrame += DoUpdate;
        }

        void OnDestroy()
        {
            GameThread.EveryFrame -= DoUpdate;
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

        void DoUpdate()
        {
            bool isParentEnabled = GrandparentCamera.isActiveAndEnabled;
            if (Display.Visible != isParentEnabled)
            {
                ParentCamera.enabled = isParentEnabled;
                Display.Visible = isParentEnabled;
            }

            if (!isParentEnabled)
            {
                return;
            }
            
            CheckSettings();

            var baseTransform = Quaternions.Rotate90AroundY;
            Transform.rotation = TfModule.OriginTransform.rotation * baseTransform;
        }
    }
}