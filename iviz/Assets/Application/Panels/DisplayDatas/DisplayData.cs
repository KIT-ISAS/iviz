using System;
using Iviz.Resources;
using Newtonsoft.Json.Linq;

namespace Iviz.App
{
    public class DisplayDataConstructor
    {
        public Resource.Module Module { get; set; }
        public DisplayListPanel DisplayList { get; set; }
        public string Topic { get; set; }
        public string Type { get; set; }
        public IConfiguration Configuration { get; set; }

        public T GetConfiguration<T>() where T : class, IConfiguration => Configuration as T;
    }

    public abstract class DisplayData
    {
        protected const int MaxTextRowLength = 35;
        protected DisplayListPanel DisplayListPanel { get; }
        protected DataPanelManager DataPanelManager => DisplayListPanel.DataPanelManager;

        string buttonText;
        public string ButtonText
        {
            get => buttonText;
            protected set
            {
                buttonText = value;
                DisplayListPanel.SetButtonText(this, buttonText);
            }
        }

        public string Topic { get; }
        public string Type { get; }
        public abstract Resource.Module Module { get; }
        public abstract DataPanelContents Panel { get; }
        public abstract IController Controller { get; }
        public abstract IConfiguration Configuration { get; }
        protected virtual bool Visible => Configuration?.Visible ?? true;

        protected DisplayData(DisplayListPanel displayList, string topic, string type)
        {
            DisplayListPanel = displayList;
            Topic = topic;
            Type = type;
        }


        protected virtual void UpdateButtonText()
        {
            string text;
            if (!string.IsNullOrEmpty(Topic))
            {
                string topicShort = RosUtils.SanitizedText(Topic, MaxTextRowLength);
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

        public void Select()
        {
            DataPanelManager.TogglePanel(this);
            DisplayListPanel.AllGuiVisible = true;
        }

        public abstract void AddToState(StateConfiguration config);

        public virtual void Stop()
        {
            DataPanelManager.HidePanelFor(this);
        }
        
        public static DisplayData CreateFromResource(DisplayDataConstructor c)
        {
            switch (c.Module)
            {
                case Resource.Module.TF: return new TFDisplayData(c);
                case Resource.Module.PointCloud: return new PointCloudDisplayData(c);
                case Resource.Module.Grid: return new GridDisplayData(c);
                case Resource.Module.Image: return new ImageDisplayData(c);
                case Resource.Module.Robot: return new RobotDisplayData(c);
                case Resource.Module.Marker: return new MarkerDisplayData(c);
                case Resource.Module.InteractiveMarker: return new InteractiveMarkerDisplayData(c);
                case Resource.Module.JointState: return new JointStateDisplayData(c);
                case Resource.Module.DepthImageProjector: return new DepthImageProjectorDisplayData(c);
                case Resource.Module.LaserScan: return new LaserScanDisplayData(c);
                case Resource.Module.AR: return new ARDisplayData(c);
                default: throw new ArgumentException(nameof(c));
            }
        }
    }
}
