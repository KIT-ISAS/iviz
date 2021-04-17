using System;
using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public abstract class ModuleData : IModuleData
    {
        [NotNull] protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;
        protected static DataPanelManager DataPanelManager => ModuleListPanel.DataPanelManager;

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

        [NotNull]
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(Topic))
                {
                    return $"<b>{ModuleType}</b>";
                }

                string type = string.IsNullOrEmpty(Type) ? ModuleType.ToString() : Type;
                int lastSlash = type.LastIndexOf('/');
                string shortType = (lastSlash == -1) ? type : type.Substring(lastSlash + 1);
                return $"{Topic}\n<b>{shortType}</b>";
            }
        }

        [NotNull] public string Topic { get; }
        [NotNull] public string Type { get; }
        public abstract Resource.ModuleType ModuleType { get; }
        [NotNull] public abstract DataPanelContents Panel { get; }
        [NotNull] public abstract IController Controller { get; }
        [NotNull] public abstract IConfiguration Configuration { get; }
        bool Visible => Configuration.Visible;
        protected bool IsSelected => DataPanelManager.SelectedModuleData == this;

        protected ModuleData([NotNull] string topic, [NotNull] string type)
        {
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        protected virtual void UpdateModuleButton()
        {
            string text;
            if (!string.IsNullOrEmpty(Topic))
            {
                string topicShort = Resource.Font.Split(Topic, ModuleListPanel.ModuleDataCaptionWidth);

                string type = string.IsNullOrEmpty(Type) ? ModuleType.ToString() : Type;
                int lastSlash = type.LastIndexOf('/');
                string shortType = (lastSlash == -1) ? type : type.Substring(lastSlash + 1);
                string clampedType = Resource.Font.Split(shortType, ModuleListPanel.ModuleDataCaptionWidth);
                text = $"{topicShort}\n<b>{clampedType}</b>";
            }
            else
            {
                text = $"<b>{ModuleType}</b>";
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
            return $"[{ModuleType} guid={Configuration.Id}]";
        }

        [NotNull]
        public static ModuleData CreateFromResource([NotNull] ModuleDataConstructor c)
        {
            if (c is null)
            {
                throw new ArgumentNullException(nameof(c));
            }

            switch (c.ModuleType)
            {
                case Resource.ModuleType.TF: return new TfModuleData(c);
                case Resource.ModuleType.PointCloud: return new PointCloudModuleData(c);
                case Resource.ModuleType.Grid: return new GridModuleData(c);
                case Resource.ModuleType.Image: return new ImageModuleData(c);
                case Resource.ModuleType.Robot: return new SimpleRobotModuleData(c);
                case Resource.ModuleType.Marker: return new MarkerModuleData(c);
                case Resource.ModuleType.InteractiveMarker: return new InteractiveMarkerModuleData(c);
                case Resource.ModuleType.JointState: return new JointStateModuleData(c);
                case Resource.ModuleType.DepthCloud: return new DepthCloudModuleData(c);
                case Resource.ModuleType.LaserScan: return new LaserScanModuleData(c);
                case Resource.ModuleType.AugmentedReality: return new ARModuleData(c);
                case Resource.ModuleType.Magnitude: return new MagnitudeModuleData(c);
                case Resource.ModuleType.OccupancyGrid: return new OccupancyGridModuleData(c);
                case Resource.ModuleType.Joystick: return new JoystickModuleData(c);
                case Resource.ModuleType.Path: return new PathModuleData(c);
                case Resource.ModuleType.GridMap: return new GridMapModuleData(c);
                case Resource.ModuleType.Octomap: return new OctomapModuleData(c);
                case Resource.ModuleType.ARGuiSystem: return new ARGuiModuleData(c);
                default: throw new ArgumentException("Failed to find a module of the given type: " + c.ModuleType);
            }
        }
    }
}