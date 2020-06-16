using System;
using Iviz.App.Listeners;
using Iviz.Resources;

namespace Iviz.App
{
    public abstract class ModuleData
    {
        protected const int MaxTextRowLength = 200;
        protected DisplayListPanel ModuleListPanel { get; }
        protected DataPanelManager DataPanelManager => ModuleListPanel.DataPanelManager;

        string buttonText_;
        public string ButtonText
        {
            get => buttonText_;
            protected set
            {
                buttonText_ = value;
                ModuleListPanel.SetDisplayDataCaption(this, buttonText_);
            }
        }

        public string Topic { get; }
        public string Type { get; }
        public abstract Resource.Module Module { get; }
        public abstract DataPanelContents Panel { get; }
        public abstract IController Controller { get; }
        public abstract IConfiguration Configuration { get; }
        protected virtual bool Visible => Configuration?.Visible ?? true;

        protected ModuleData(DisplayListPanel displayList, string topic, string type)
        {
            ModuleListPanel = displayList;
            Topic = topic;
            Type = type;
        }

        protected virtual void UpdateButtonText()
        {
            string text;
            if (!string.IsNullOrEmpty(Topic))
            {
                string topicShort = Resource.Font.Split(Topic, DisplayListPanel.DisplayDataCaptionWidth);
                text = $"{topicShort}\n<b>{Module}</b>";
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

        public virtual void CleanupPanel() { }

        public virtual void UpdatePanel() { }

        public void ToggleSelect()
        {
            DataPanelManager.TogglePanel(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        public void Select()
        {
            DataPanelManager.SelectPanelFor(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        public abstract void AddToState(StateConfiguration config);

        public virtual void Stop()
        {
            DataPanelManager.HidePanelFor(this);
        }
        
        public static ModuleData CreateFromResource(ModuleDataConstructor c)
        {
            switch (c.Module)
            {
                case Resource.Module.TF: return new TFModuleData(c);
                case Resource.Module.PointCloud: return new PointCloudDisplayData(c);
                case Resource.Module.Grid: return new GridModuleData(c);
                case Resource.Module.Image: return new ImageModuleData(c);
                case Resource.Module.Robot: return new RobotModuleData(c);
                case Resource.Module.Marker: return new MarkerModuleData(c);
                case Resource.Module.InteractiveMarker: return new InteractiveMarkerModuleData(c);
                case Resource.Module.JointState: return new JointStateModuleData(c);
                case Resource.Module.DepthImageProjector: return new DepthImageProjectorModuleData(c);
                case Resource.Module.LaserScan: return new LaserScanModuleData(c);
                case Resource.Module.AR: return new ARModuleData(c);
                case Resource.Module.Magnitude: return new MagnitudeModuleData(c);
                case Resource.Module.OccupancyGrid: return new OccupancyGridModuleData(c);
                case Resource.Module.Joystick: return new JoystickModuleData(c);
                default: throw new ArgumentException(nameof(c));
            }
        }
    }
}
