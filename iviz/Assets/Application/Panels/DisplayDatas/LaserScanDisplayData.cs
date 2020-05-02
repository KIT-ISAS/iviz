using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class LaserScanDisplayData : DisplayableListenerData
    {
        LaserScanListener display;
        LaserScanPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override DisplayableListener Display => display;
        public override Resource.Module Module => Resource.Module.LaserScan;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Displays.LaserScan);
            displayObject.name = "LaserScan:" + Topic;

            display = displayObject.GetComponent<LaserScanListener>();
            display.Parent = TFListener.DisplaysFrame;
            display.Config.topic = Topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.LaserScan) as LaserScanPanelContents;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<LaserScanListener.Configuration>();
            Topic = display.Config.topic;
            return this;
        }

        public override void Start()
        {
            base.Start();
            display.StartListening();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            display.Stop();
            ResourcePool.Dispose(Resource.Displays.PointCloud, display.gameObject);
            display = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;

            panel.Colormap.Index = (int)display.Colormap;
            panel.PointSize.Value = display.PointSize;
            panel.IgnoreIntensity.Value = display.IgnoreIntensity;

            panel.IgnoreIntensity.ValueChanged += f =>
            {
                display.IgnoreIntensity = f;
            };
            panel.PointSize.ValueChanged += f =>
            {
                display.PointSize = f;
            };
            panel.Colormap.ValueChanged += (i, _) =>
            {
                display.Colormap = (Resource.ColormapId)i;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
    }
}
