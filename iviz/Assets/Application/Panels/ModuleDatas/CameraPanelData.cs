#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="CameraPanel"/> 
    /// </summary>
    public sealed class CameraPanelData : ModulePanelData
    {
        readonly CameraPanel panel;
        public override ModulePanel Panel => panel;

        public CameraPanelData()
        {
            panel = ModulePanelManager.GetPanelByResourceType<CameraPanel>(ModuleType.Camera);
            ARController.ARCameraViewChanged += OnARViewChanged;
        }

        public override void SetupPanel()
        {
            panel.CloseButton.Clicked += HidePanel;

            CheckInteractable();

            var guiInputModule = GuiInputModule.Instance;
            var virtualCamera = Settings.VirtualCamera;

            panel.Frame.Owner = guiInputModule;
            panel.Fov.Value = Settings.MainCamera.GetHorizontalFov();

            UpdatePose();

            panel.Roll.ValueChanged += f => guiInputModule.CameraRoll = f;
            panel.Pitch.ValueChanged += f => guiInputModule.CameraPitch = f;
            panel.Yaw.ValueChanged += f => guiInputModule.CameraYaw = f;

            if (virtualCamera != null)
            {
                panel.Position.ValueChanged += f => guiInputModule.CameraPosition = TransformFixed(f);
                panel.InputPosition.ValueChanged += f => guiInputModule.CameraPosition = TransformFixed(f);
            }

            panel.Fov.ValueChanged += f => guiInputModule.CameraFieldOfView = f;
        }

        void OnARViewChanged(bool _) => CheckInteractable();

        void CheckInteractable()
        {
            bool interactable = Settings.VirtualCamera != null && Settings.MainCamera == Settings.VirtualCamera;
            panel.Fov.Interactable = interactable;
            panel.Roll.Interactable = interactable;
            panel.Pitch.Interactable = interactable;
            panel.Yaw.Interactable = interactable;
            panel.Position.Interactable = interactable;
            panel.InputPosition.Interactable = interactable;

            panel.Fov.Value = Settings.MainCamera.GetHorizontalFov();
        }

        public override void UpdatePanelFast()
        {
            UpdatePose();
        }

        void UpdatePose()
        {
            Vector3 rosCameraRpy;
            if (Settings.MainCamera == Settings.VirtualCamera)
            {
                rosCameraRpy = GuiInputModule.Instance.CameraRpy;
            }
            else if (Settings.MainCamera != null)
            {
                var (unityX, unityY, unityZ) = Settings.MainCamera.transform.rotation.eulerAngles;
                var rosRpy = new Vector3(-unityZ, unityX, -unityY);
                rosCameraRpy = UnityUtils.RegularizeRpy(rosRpy);
            }
            else
            {
                rosCameraRpy = Vector3.zero;
            }

            (panel.Roll.Value, panel.Pitch.Value, panel.Yaw.Value) = rosCameraRpy;

            Vector3 rosCameraPosition;
            if (Settings.MainCamera == Settings.VirtualCamera)
            {
                rosCameraPosition = InverseTransformFixed(GuiInputModule.Instance.CameraPosition);
            } else if (Settings.MainCamera != null)
            {
                rosCameraPosition = InverseTransformFixed(Settings.MainCamera.transform.localPosition);
            }
            else
            {
                rosCameraPosition = Vector3.zero;
            }

            panel.Position.Value = rosCameraPosition;
            panel.InputPosition.Value = rosCameraPosition;
        }

        static Vector3 TransformFixed(in Vector3 f) =>
            TfListener.FixedFrame.Transform.TransformPoint(f).Ros2Unity();

        static Vector3 InverseTransformFixed(in Vector3 f) =>
            TfListener.FixedFrame.Transform.InverseTransformPoint(f).Unity2Ros();

        public void Dispose()
        {
            ARController.ARCameraViewChanged -= OnARViewChanged;
        }
    }
}