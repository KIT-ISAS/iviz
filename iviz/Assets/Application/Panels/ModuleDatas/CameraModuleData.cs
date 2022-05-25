#nullable enable

using Iviz.Common;
using Iviz.Common.Configurations;
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
    public sealed class CameraModuleData : ModulePanelData
    {
        readonly CameraPanel panel;
        readonly CameraController controller = new();
        public override ModulePanel Panel => panel;
        public CameraConfiguration Configuration => controller.Configuration;

        public CameraModuleData()
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
            panel.Fov.Value = controller.CameraFieldOfView;
            panel.BackgroundColor.Value = controller.BackgroundColor;
            panel.EnableSun.Value = controller.EnableShadows;
            panel.SunDirectionX.Value = controller.SunDirectionX;
            panel.SunDirectionY.Value = controller.SunDirectionY;
            panel.EnableShadows.Value = controller.EnableShadows;
            panel.EquatorIntensity.Value = controller.EquatorIntensity;

            UpdatePose();

            panel.RollPitchYaw.ValueChanged += f => guiInputModule.CameraRpy = f;

            if (virtualCamera != null)
            {
                panel.Position.ValueChanged += f => guiInputModule.CameraPosition = TransformFixed(f);
            }

            panel.Fov.ValueChanged += f => controller.CameraFieldOfView = f;
            panel.EnableSun.ValueChanged += f => controller.EnableSun = f;
            panel.SunDirectionX.ValueChanged += f => controller.SunDirectionX = f;
            panel.SunDirectionY.ValueChanged += f => controller.SunDirectionY = f;
            panel.BackgroundColor.ValueChanged += f => controller.BackgroundColor = f;
            panel.EnableShadows.ValueChanged += f => controller.EnableShadows = f;
            panel.EquatorIntensity.ValueChanged += f => controller.EquatorIntensity = f;
        }

        void OnARViewChanged(bool _) => CheckInteractable();

        void CheckInteractable()
        {
            bool interactable = Settings.VirtualCamera != null && Settings.MainCamera == Settings.VirtualCamera;
            panel.Fov.Interactable = interactable;
            panel.RollPitchYaw.Interactable = interactable;
            panel.Position.Interactable = interactable;

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

            panel.RollPitchYaw.Value = rosCameraRpy;

            Vector3 rosCameraPosition;
            if (Settings.MainCamera == Settings.VirtualCamera)
            {
                rosCameraPosition = InverseTransformFixed(GuiInputModule.Instance.CameraPosition);
            }
            else if (Settings.MainCamera != null)
            {
                rosCameraPosition = InverseTransformFixed(Settings.MainCamera.transform.localPosition);
            }
            else
            {
                rosCameraPosition = Vector3.zero;
            }

            panel.Position.Value = rosCameraPosition;
        }

        public void UpdateConfiguration(CameraConfiguration config)
        {
            controller.Configuration = config;
        }

        public void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonConvert.DeserializeObject<CameraConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(CameraConfiguration.BackgroundColor):
                        controller.BackgroundColor = config.BackgroundColor.ToUnity();
                        break;
                    case nameof(CameraConfiguration.EnableShadows):
                        controller.EnableShadows = config.EnableShadows;
                        break;
                    case nameof(CameraConfiguration.EnableSun):
                        controller.EnableSun = config.EnableSun;
                        break;
                    case nameof(CameraConfiguration.EquatorIntensity):
                        controller.EquatorIntensity = config.EquatorIntensity;
                        break;
                    case nameof(CameraConfiguration.SunDirectionX):
                        controller.SunDirectionX = config.SunDirectionX;
                        break;
                    case nameof(CameraConfiguration.SunDirectionY):
                        controller.SunDirectionY = config.SunDirectionY;
                        break;
                    case nameof(CameraConfiguration.CameraFieldOfView):
                        controller.CameraFieldOfView = config.CameraFieldOfView;
                        break;
                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }
            
            ResetPanel();
        }

        static Vector3 TransformFixed(in Vector3 f) =>
            TfModule.FixedFrame.Transform.TransformPoint(f).Ros2Unity();

        static Vector3 InverseTransformFixed(in Vector3 f) =>
            TfModule.FixedFrame.Transform.InverseTransformPoint(f).Unity2Ros();

        public void Dispose()
        {
            ARController.ARCameraViewChanged -= OnARViewChanged;
        }
    }
}