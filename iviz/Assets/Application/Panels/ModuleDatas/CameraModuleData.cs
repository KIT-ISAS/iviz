#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="CameraPanelContents"/> 
    /// </summary>
    public sealed class CameraModuleData : ModuleData
    {
        readonly CameraConfiguration config = new();
        readonly CameraPanelContents panel;

        public override ModuleType ModuleType => ModuleType.Camera;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => config;
        public override IController? Controller => null;

        public CameraModuleData()
        {
            panel = DataPanelManager.GetPanelByResourceType<CameraPanelContents>(ModuleType);
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
                return;
            }

            var guiInputModule = GuiInputModule.Instance;
            var virtualCamera = Settings.VirtualCamera;
            
            panel.Frame.Owner = guiInputModule;
            panel.Fov.Value = guiInputModule.CameraFieldOfView;
            panel.Roll.Value = guiInputModule.CameraRoll;
            panel.Pitch.Value = guiInputModule.CameraPitch;
            panel.Yaw.Value = guiInputModule.CameraYaw;
            panel.Position.Value = virtualCamera.transform.position;

            panel.Roll.ValueChanged += f => guiInputModule.CameraRoll = f;
            panel.Pitch.ValueChanged += f => guiInputModule.CameraPitch = f;
            panel.Yaw.ValueChanged += f => guiInputModule.CameraYaw = f;
            panel.Fov.ValueChanged += f => guiInputModule.CameraFieldOfView = f;
            panel.Position.ValueChanged += f => virtualCamera.transform.position = f;
            panel.CloseButton.Clicked += () => DataPanelManager.HidePanelFor(this);
        }

        public override void UpdatePanelFast()
        {
            if (Settings.VirtualCamera is not { } virtualCamera)
            {
                return;
            }

            var guiInputModule = GuiInputModule.Instance;
            panel.Roll.Value = guiInputModule.CameraRoll;
            panel.Pitch.Value = guiInputModule.CameraPitch;
            panel.Yaw.Value = guiInputModule.CameraYaw;

            var position = virtualCamera.transform.position;
            if (panel.Position.Value != position)
            {
                panel.Position.Value = virtualCamera.transform.position;
            }
        }

        public override void UpdateConfiguration(string _, IEnumerable<string> __)
        {
            ResetPanel();
        }

        public override void AddToState(StateConfiguration _)
        {
        }
    }

    internal class CameraConfiguration : IConfiguration
    {
        public string Id { get; set; } = "";
        public ModuleType ModuleType => ModuleType.Camera;
        public bool Visible => true;
    }
}