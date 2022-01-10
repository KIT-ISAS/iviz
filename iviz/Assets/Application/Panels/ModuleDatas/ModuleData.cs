#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// Manager for all stuff related to a module: the <see cref="IController"/>,
    /// the <see cref="IConfiguration"/>, the <see cref="ModulePanel"/>,
    /// and the button in <see cref="ModuleListPanel"/>.
    /// </summary>
    public abstract class ModuleData : ModulePanelData
    {
        string buttonText = "";

        public string ButtonText
        {
            get => buttonText;
            protected set
            {
                buttonText = value;
                ModuleListPanel.UpdateModuleButton(this, buttonText);
            }
        }

        public string Topic { get; } = "";
        protected string Type { get; } = "";
        public abstract ModuleType ModuleType { get; }
        public abstract IController Controller { get; }
        public abstract IConfiguration Configuration { get; }

        protected bool IsSelected => ModulePanelManager.SelectedModuleData == this;

        protected ModuleData()
        {
        }

        protected ModuleData(string topic, string type)
        {
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        protected virtual void UpdateModuleButton()
        {
            string text = Topic.Length != 0 && Type.Length != 0
                ? GetDescriptionForTopic(Topic, Type)
                : $"<b>{ModuleType}</b>";

            ButtonText = Controller.Visible ? text : $"<color=grey>{text}</color>";
        }

        protected static string GetDescriptionForTopic(string topic, string type)
        {
            string topicShort = Resource.Font.Split(topic, ModuleListPanel.ModuleDataCaptionWidth);
            int lastSlash = type.LastIndexOf('/');
            string shortType = (lastSlash == -1) ? type : type[(lastSlash + 1)..];
            string clampedType = Resource.Font.Split(shortType, ModuleListPanel.ModuleDataCaptionWidth);
            return $"{topicShort}\n<b>{clampedType}</b>";
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
            if (!IsSelected)
            {
                return;
            }

            Panel.ClearSubscribers();
            SetupPanel();
        }

        public abstract void UpdateConfiguration(string configAsJson, IEnumerable<string> fields);

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
            return $"[{ModuleType} guid={Configuration.Id}]";
        }

        public static ModuleData CreateFromResource(ModuleDataConstructor c)
        {
            if (c is null)
            {
                throw new ArgumentNullException(nameof(c));
            }

            return c.ModuleType switch
            {
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
                ModuleType.AugmentedReality => new ARModuleData(c),
                ModuleType.Magnitude => new MagnitudeModuleData(c),
                ModuleType.OccupancyGrid => new OccupancyGridModuleData(c),
                ModuleType.Joystick => new JoystickModuleData(c),
                ModuleType.Path => new PathModuleData(c),
                ModuleType.GridMap => new GridMapModuleData(c),
                ModuleType.Octomap => new OctomapModuleData(c),
                ModuleType.GuiDialog => new GuiWidgetModuleData(c),
                ModuleType.XR => new XRModuleData(c),
                _ => throw new ArgumentException("Failed to find a module of the given type: " + c.ModuleType)
            };
        }
    }
}