#nullable enable

using Iviz.App;
using Iviz.Common.Configurations;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class CameraController
    {
        readonly CameraConfiguration config = new();
        static GuiInputModule GuiInputModule => GuiInputModule.Instance;

        public CameraConfiguration Configuration
        {
            get => config;
            set
            {
                SunDirectionX = value.SunDirectionX;
                SunDirectionY = value.SunDirectionY;
                EnableShadows = value.EnableShadows;
                CameraFieldOfView = value.CameraFieldOfView;
                BackgroundColor = value.BackgroundColor.ToUnity();
            }
        } 

        public float SunDirectionX
        {
            get => Configuration.SunDirectionX;
            set
            {
                Configuration.SunDirectionX = value;
                GuiInputModule.SunDirectionX = value;
            }
        }

        public float SunDirectionY
        {
            get => Configuration.SunDirectionY;
            set
            {
                Configuration.SunDirectionY = value;
                GuiInputModule.SunDirectionY = value;
            }
        }

        public bool EnableSun
        {
            get => Configuration.EnableSun;
            set
            {
                Configuration.EnableSun = value;
                GuiInputModule.EnableSun = value;
            }
        }
        
        public float EquatorIntensity
        {
            get => Configuration.EquatorIntensity;
            set
            {
                Configuration.EquatorIntensity = value;
                GuiInputModule.EquatorIntensity = value;
            }
        }

        public bool EnableShadows
        {
            get => Configuration.EnableShadows;
            set
            {
                Configuration.EnableShadows = value;
                GuiInputModule.EnableShadows = value;
            }
        }

        public float CameraFieldOfView
        {
            get => Configuration.CameraFieldOfView;
            set
            {
                Configuration.CameraFieldOfView = value;
                GuiInputModule.CameraFieldOfView = value;
            }
        }

        public Color BackgroundColor
        {
            get => Configuration.BackgroundColor.ToUnity();
            set
            {
                Configuration.BackgroundColor = value.ToRos();
                GuiInputModule.BackgroundColor = value;
                ModuleListPanel.Instance.UpdateSettings();
            }
        }

        public CameraController()
        {
            if (!Settings.IsXR)
            {
                Settings.VirtualCamera = Settings.MainCamera;
            }
                
            Configuration = Configuration;
        }
    }
}