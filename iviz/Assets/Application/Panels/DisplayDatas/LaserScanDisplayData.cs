using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class LaserScanDisplayData : ListenerDisplayData
    {
        readonly LaserScanListener listener;
        readonly LaserScanPanelContents panel;

        protected override TopicListener Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.LaserScan;
        public override IConfiguration Configuration => listener.Config;


        public LaserScanDisplayData(DisplayDataConstructor constructor) :
        base(constructor.DisplayList,
            constructor.GetConfiguration<LaserScanConfiguration>()?.Topic ?? constructor.Topic,
            constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.LaserScan) as LaserScanPanelContents;
            listener = Resource.Listeners.Instantiate<LaserScanListener>();
            listener.name = "LaserScan:" + Topic;
            listener.DisplayData = this;
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (LaserScanConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;

            panel.Colormap.Index = (int)listener.Colormap;
            panel.PointSize.Value = listener.PointSize;
            panel.UseIntensity.Value = listener.UseIntensity;
            panel.HideButton.State = listener.Visible;

            panel.UseIntensity.ValueChanged += f =>
            {
                listener.UseIntensity = f;
            };
            panel.PointSize.ValueChanged += f =>
            {
                listener.PointSize = f;
            };
            panel.Colormap.ValueChanged += (i, _) =>
            {
                listener.Colormap = (Resource.ColormapId)i;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
            panel.HideButton.Clicked += () =>
            {
                listener.Visible = !listener.Visible;
                panel.HideButton.State = listener.Visible;
                UpdateButtonText();
            };
        }

        public override void AddToState(StateConfiguration config)
        {
            config.LaserScans.Add(listener.Config);
        }
    }
}
