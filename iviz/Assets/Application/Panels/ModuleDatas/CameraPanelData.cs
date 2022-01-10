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
        }

        public override void SetupPanel()
        {
            if (Settings.VirtualCamera == null)
            {
                panel.Frame.Owner = GuiInputModule.Instance;
                panel.Fov.Interactable = false;
                panel.Roll.Interactable = false;
                panel.Pitch.Interactable = false;
                panel.Yaw.Interactable = false;
                panel.Position.Interactable = false;
                panel.InputPosition.Interactable = false;
                return;
            }

            var guiInputModule = GuiInputModule.Instance;
            var virtualCamera = Settings.VirtualCamera;

            panel.Frame.Owner = guiInputModule;
            panel.Fov.Value = guiInputModule.CameraFieldOfView;

            UpdatePose();

            panel.Roll.ValueChanged += f => guiInputModule.CameraRoll = f;
            panel.Pitch.ValueChanged += f => guiInputModule.CameraPitch = f;
            panel.Yaw.ValueChanged += f => guiInputModule.CameraYaw = f;

            panel.Position.ValueChanged += f =>
                virtualCamera.transform.localPosition = TfListener.FixedFramePose.Multiply(f);
            panel.InputPosition.ValueChanged += f =>
                virtualCamera.transform.localPosition = TfListener.FixedFramePose.Multiply(f);
            ;

            panel.Fov.ValueChanged += f => guiInputModule.CameraFieldOfView = f;
            panel.CloseButton.Clicked += HidePanel;
        }

        public override void UpdatePanelFast()
        {
            UpdatePose();
        }

        void UpdatePose()
        {
            if (Settings.VirtualCamera is not { } virtualCamera)
            {
                return;
            }

            var guiInputModule = GuiInputModule.Instance;
            (panel.Roll.Value, panel.Pitch.Value, panel.Yaw.Value) = guiInputModule.CameraRpy;

            var cameraPosition = TfListener.RelativeToFixedFrame(virtualCamera.transform.AsPose()).position;
            panel.Position.Value = cameraPosition;
            panel.InputPosition.Value = cameraPosition;
        }
    }
}