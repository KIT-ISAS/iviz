using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Resources;

namespace Iviz.App
{
    public abstract class ModuleData : IModuleData
    {
        protected ModuleListPanel ModuleListPanel { get; }
        protected DataPanelManager DataPanelManager => ModuleListPanel.DataPanelManager;

        string buttonText;

        public string ButtonText
        {
            get => buttonText;
            protected set
            {
                buttonText = value;
                ModuleListPanel.UpdateModuleButton(this, buttonText);
            }
        }

        public string Topic { get; }
        public string Type { get; }
        public abstract Resource.Module Module { get; }
        public abstract DataPanelContents Panel { get; }
        public abstract IController Controller { get; }
        public abstract IConfiguration Configuration { get; }
        bool Visible => Configuration?.Visible ?? true;
        bool IsSelected => DataPanelManager.SelectedModuleData == this;

        protected ModuleData(ModuleListPanel moduleList, string topic, string type)
        {
            ModuleListPanel = moduleList;
            Topic = topic;
            Type = type;
        }

        protected virtual void UpdateModuleButton()
        {
            string text;
            if (!string.IsNullOrEmpty(Topic))
            {
                string topicShort = Resource.Font.Split(Topic, ModuleListPanel.ModuleDataCaptionWidth);
                string typeShort = string.IsNullOrEmpty(Type)
                    ? Module.ToString()
                    : Resource.Font.Split(Type, ModuleListPanel.ModuleDataCaptionWidth);
                text = $"{topicShort}\n<b>{typeShort}</b>";
            }
            else
            {
                text = $"<b>{Module}</b>";
            }

            if (!Visible)
            {
                text = $"<color=grey>{text}</color>";
            }

            ButtonText = text;
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

        public abstract void AddToState(StateConfiguration config);

        public virtual void OnARModeChanged(bool value)
        {
        }

        public virtual void Stop()
        {
            DataPanelManager.HidePanelFor(this);
        }

        public void ResetController()
        {
            Controller.Reset();
        }
        
        public static ModuleData CreateFromResource(ModuleDataConstructor c)
        {
            switch (c.Module)
            {
                case Resource.Module.TF: return new TFModuleData(c);
                case Resource.Module.PointCloud: return new PointCloudModuleData(c);
                case Resource.Module.Grid: return new GridModuleData(c);
                case Resource.Module.Image: return new ImageModuleData(c);
                case Resource.Module.Robot: return new RobotModuleData(c);
                case Resource.Module.SimpleRobot: return new SimpleRobotModuleData(c);
                case Resource.Module.Marker: return new MarkerModuleData(c);
                case Resource.Module.InteractiveMarker: return new InteractiveMarkerModuleData(c);
                case Resource.Module.JointState: return new JointStateModuleData(c);
                case Resource.Module.DepthImageProjector: return new DepthImageProjectorModuleData(c);
                case Resource.Module.LaserScan: return new LaserScanModuleData(c);
                case Resource.Module.AR: return new ARModuleData(c);
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