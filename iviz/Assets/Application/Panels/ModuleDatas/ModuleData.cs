using System;
using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public abstract class ModuleData : IModuleData
    {
        [NotNull] protected ModuleListPanel ModuleListPanel { get; }
        protected DataPanelManager DataPanelManager => ModuleListPanel.DataPanelManager;

        string buttonText = "";

        [NotNull]
        public string ButtonText
        {
            get => buttonText;
            protected set
            {
                buttonText = value;
                ModuleListPanel.UpdateModuleButton(this, buttonText);
            }
        }

        [NotNull] public string Topic { get; }
        [NotNull] public string Type { get; }
        public abstract Resource.Module Module { get; }
        [NotNull] public abstract DataPanelContents Panel { get; }
        [NotNull] public abstract IController Controller { get; }
        [NotNull] public abstract IConfiguration Configuration { get; }
        bool Visible => Configuration?.Visible ?? true;
        bool IsSelected => DataPanelManager.SelectedModuleData == this;

        protected ModuleData([NotNull] ModuleListPanel moduleList, [NotNull] string topic, [NotNull] string type)
        {
            ModuleListPanel = moduleList ? moduleList : throw new ArgumentNullException(nameof(moduleList));
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        protected virtual void UpdateModuleButton()
        {
            string text;
            if (!string.IsNullOrEmpty(Topic))
            {
                string topicShort = Resource.Font.Split(Topic, ModuleListPanel.ModuleDataCaptionWidth);

                string type = string.IsNullOrEmpty(Type) ? Module.ToString() : Type;
                int lastSlash = type.LastIndexOf('/');
                string shortType = (lastSlash == -1) ? type : type.Substring(lastSlash + 1);
                string clampedType = Resource.Font.Split(shortType, ModuleListPanel.ModuleDataCaptionWidth);
                text = $"{topicShort}\n<b>{clampedType}</b>";
            }
            else
            {
                text = $"<b>{Module}</b>";
            }

            ButtonText = Visible ? text : $"<color=grey>{text}</color>";
        }

        public abstract void SetupPanel();

        public void ResetPanel()
        {
            if (!IsSelected)
            {
                return;
            }

            Panel.ClearSubscribers();
            SetupPanel();
        }

        public virtual void CleanupPanel()
        {
        }

        public virtual void UpdatePanel()
        {
        }

        public void ToggleShowPanel()
        {
            DataPanelManager.TogglePanel(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        public void ShowPanel()
        {
            DataPanelManager.SelectPanelFor(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        public abstract void UpdateConfiguration(string configAsJson, IEnumerable<string> fields);

        public abstract void AddToState([NotNull] StateConfiguration config);

        public virtual void OnARModeChanged(bool value)
        {
        }

        public virtual void Stop()
        {
            DataPanelManager.HidePanelFor(this);
        }

        public void ResetController()
        {
            Controller.ResetController();
        }

        public override string ToString()
        {
            return $"[{Module} guid={Configuration.Id}]";
        }

        [NotNull]
        public static ModuleData CreateFromResource([NotNull] ModuleDataConstructor c)
        {
            if (c is null)
            {
                throw new ArgumentNullException(nameof(c));
            }

            switch (c.Module)
            {
                case Resource.Module.TF: return new TfModuleData(c);
                case Resource.Module.PointCloud: return new PointCloudModuleData(c);
                case Resource.Module.Grid: return new GridModuleData(c);
                case Resource.Module.Image: return new ImageModuleData(c);
                case Resource.Module.Robot: return new SimpleRobotModuleData(c);
                case Resource.Module.Marker: return new MarkerModuleData(c);
                case Resource.Module.InteractiveMarker: return new InteractiveMarkerModuleData(c);
                case Resource.Module.JointState: return new JointStateModuleData(c);
                case Resource.Module.DepthCloud: return new DepthCloudModuleData(c);
                case Resource.Module.LaserScan: return new LaserScanModuleData(c);
                case Resource.Module.AugmentedReality: return new ARModuleData(c);
                case Resource.Module.Magnitude: return new MagnitudeModuleData(c);
                case Resource.Module.OccupancyGrid: return new OccupancyGridModuleData(c);
                case Resource.Module.Joystick: return new JoystickModuleData(c);
                case Resource.Module.Path: return new PathModuleData(c);
                case Resource.Module.GridMap: return new GridMapModuleData(c);
                default: throw new ArgumentException("Failed to find a module of the given type: " + c.Module);
            }
        }
    }
}