using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class PointCloudDisplayData : DisplayableListenerData
    {
        PointCloudListener listener;
        PointCloudPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override TopicListener Listener => listener;
        public override Resource.Module Module => Resource.Module.PointCloud;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            GameObject listenerObject = ResourcePool.GetOrCreate(Resource.Listeners.PointCloud);
            listenerObject.name = "PointCloud:" + Topic;

            listener = listenerObject.GetComponent<PointCloudListener>();
            listener.Config.topic = Topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.PointCloud) as PointCloudPanelContents;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<PointCloudListener.Configuration>();
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
            panel.IntensityChannel.Options = listener.FieldNames;
            panel.IntensityChannel.Value = listener.IntensityChannel;

            panel.PointSize.ValueChanged += f =>
            {
                listener.PointSize = f;
            };
            panel.IntensityChannel.ValueChanged += (_, s) =>
            {
                listener.IntensityChannel = s;
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

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.IntensityChannel.Options = listener.FieldNames;
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(listener.Config);
        }
    }
}
