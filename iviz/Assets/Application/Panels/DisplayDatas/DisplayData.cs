using Newtonsoft.Json.Linq;

namespace Iviz.App
{
    public abstract class DisplayData
    {
        public static DisplayData CreateFromResource(Resource.Module resource)
        {
            switch (resource)
            {
                case Resource.Module.TF: return new TFDisplayData();
                case Resource.Module.PointCloud: return new PointCloudDisplayData();
                case Resource.Module.Grid: return new GridDisplayData();
                case Resource.Module.Image: return new ImageDisplayData();
                case Resource.Module.Robot: return new RobotDisplayData();
                case Resource.Module.Marker: return new MarkerDisplayData();
                case Resource.Module.InteractiveMarker: return new InteractiveMarkerDisplayData();
                case Resource.Module.JointState: return new JointStateDisplayData();
                case Resource.Module.DepthImageProjector: return new DepthImageProjectorDisplayData();
                case Resource.Module.LaserScan: return new LaserScanDisplayData();
                default: return null;
            }
        }

        public string Topic { get; protected set; }
        public string Type { get; protected set; }
        protected DisplayListPanel DisplayListPanel { get; private set; }
        protected DataPanelManager DataPanelManager => DisplayListPanel.dataPanelManager;
        public abstract Resource.Module Module { get; }
        public abstract DataPanelContents Panel { get; }

        public abstract void SetupPanel();
        public virtual void CleanupPanel() { }
        public virtual void UpdatePanel() { }

        string buttonText;
        protected string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value;
                DisplayListPanel.SetButtonText(this, buttonText);
            }
        }

        public void Select()
        {
            DataPanelManager.SelectPanelFor(this);
            DisplayListPanel.AllGuiVisible = true;
        }

        public virtual DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            DisplayListPanel = displayList;
            Topic = topic;
            Type = type;
            return this;
        }

        public abstract DisplayData Deserialize(JToken j);

        public virtual void Start()
        {
            if (string.IsNullOrEmpty(ButtonText))
            {
                const int maxLength = 20;
                if (Topic.Length > maxLength)
                {
                    string topicShort = Topic.Substring(0, maxLength);
                    ButtonText = $"{topicShort}...\n<b>{Module}</b>";
                }
                else if (Topic != "")
                {
                    ButtonText = $"{Topic}\n<b>{Module}</b>";
                }
                else
                {
                    ButtonText = $"<b>{Module}</b>";
                }
            }
        }

        public abstract JToken Serialize();
        
        public virtual void Cleanup()
        {
            DataPanelManager.HidePanelFor(this);
            DisplayListPanel = null;
        }
    }
}
