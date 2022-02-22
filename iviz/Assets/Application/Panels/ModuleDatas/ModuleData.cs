#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;

namespace Iviz.App
{
    /// <summary>
    /// Manager for all stuff related to a module: the <see cref="IController"/>,
    /// the <see cref="IConfiguration"/>, the <see cref="ModulePanel"/>,
    /// and the button in <see cref="ModuleListPanel"/>.
    /// </summary>
    public abstract class ModuleData : ModulePanelData
    {
        string moduleListButtonText = "";

        protected bool IsPanelSelected => ModulePanelManager.SelectedModuleData == this;

        public abstract ModuleType ModuleType { get; }
        public abstract IController Controller { get; }
        public abstract IConfiguration Configuration { get; }

        public string ModuleListButtonText
        {
            get => moduleListButtonText;
            protected set
            {
                moduleListButtonText = value;
                ModuleListPanel.UpdateModuleButtonText(this, moduleListButtonText);
            }
        }
        
        protected virtual void UpdateModuleButton()
        {
            ModuleListButtonText = ModuleListPanel.CreateButtonTextForModule(this);
        }

        public void ToggleVisible()
        {
            Controller.Visible = !Controller.Visible;
            if (Panel.Active && Panel.HideButton != null)
            {
                Panel.HideButton.State = Controller.Visible;
            }

            UpdateModuleButton();
        }

        public virtual void Close()
        {
            HidePanel();
            ModuleListPanel.RemoveModule(this);
        }

        public void ResetPanel()
        {
            if (!IsPanelSelected)
            {
                return;
            }

            Panel.ClearSubscribers();
            SetupPanel();
        }

        public abstract void UpdateConfiguration(string configAsJson, string[] fields);

        public abstract void AddToState(StateConfiguration config);

        public virtual void Dispose()
        {
            HidePanel();
        }

        public void ResetController()
        {
            Controller.ResetController();
        }

        public override string ToString()
        {
            return $"[{GetType().Name} id='{Configuration.Id}']";
        }

        public static ModuleData CreateFromResource(ModuleDataConstructor c)
        {
            return c?.ModuleType switch
            {
                null => throw new ArgumentNullException(nameof(c)),
                ModuleType.TF => new TfModuleData(c),
                ModuleType.PointCloud => new PointCloudModuleData(c),
                ModuleType.Grid => new GridModuleData(c),
                ModuleType.Image => new ImageModuleData(c),
                ModuleType.Robot => new SimpleRobotModuleData(c),
                ModuleType.Marker => new MarkerModuleData(c),
                ModuleType.InteractiveMarker => new InteractiveMarkerModuleData(c),
                ModuleType.JointState => new JointStateModuleData(c),
                ModuleType.DepthCloud => new DepthCloudModuleData(c),
                ModuleType.LaserScan => new LaserScanModuleData(c),
                ModuleType.AR => new ARModuleData(c),
                ModuleType.Magnitude => new MagnitudeModuleData(c),
                ModuleType.OccupancyGrid => new OccupancyGridModuleData(c),
                ModuleType.Joystick => new JoystickModuleData(c),
                ModuleType.Path => new PathModuleData(c),
                ModuleType.GridMap => new GridMapModuleData(c),
                ModuleType.Octomap => new OctomapModuleData(c),
                ModuleType.GuiWidget => new GuiWidgetModuleData(c),
                ModuleType.XR => new XRModuleData(c),
                _ => throw new IndexOutOfRangeException("Failed to find a module of the given type: " + c.ModuleType)
            };
        }
    }
}