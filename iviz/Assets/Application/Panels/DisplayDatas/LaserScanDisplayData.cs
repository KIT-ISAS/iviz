using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class LaserScanDisplayData : DisplayableListenerData
    {
        LaserScanListener listener;
        LaserScanPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override TopicListener Listener => listener;
        public override Resource.Module Module => Resource.Module.LaserScan;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            GameObject listenerObject = ResourcePool.GetOrCreate(Resource.Listeners.LaserScan);
            listenerObject.name = "LaserScan:" + Topic;

            listener = listenerObject.GetComponent<LaserScanListener>();
            listener.Config.topic = Topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.LaserScan) as LaserScanPanelContents;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<LaserScanListener.Configuration>();
            Topic = listener.Config.topic;
            return this;
        }

        public override void Start()
        {
            base.Start();
            listener.StartListening();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            listener.Stop();
            ResourcePool.Dispose(Resource.Listeners.PointCloud, listener.gameObject);
            listener = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;

            panel.Colormap.Index = (int)listener.Colormap;
            panel.PointSize.Value = listener.PointSize;
            panel.IgnoreIntensity.Value = listener.IgnoreIntensity;

            panel.IgnoreIntensity.ValueChanged += f =>
            {
                listener.IgnoreIntensity = f;
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
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(listener.Config);
        }
    }
}
