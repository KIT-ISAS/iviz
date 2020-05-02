using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class PointCloudDisplayData : DisplayableListenerData
    {
        PointCloudListener display;
        PointCloudPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override DisplayableListener Display => display;
        public override Resource.Module Module => Resource.Module.PointCloud;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            Resource.DisplaysType.Initialize();
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.DisplaysType.PointCloud);
            displayObject.name = "PointCloud:" + Topic;

            display = displayObject.GetComponent<PointCloudListener>();
            display.Parent = TFListener.DisplaysFrame;
            display.Config.topic = Topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.PointCloud) as PointCloudPanelContents;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<PointCloudListener.Configuration>();
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
            ResourcePool.Dispose(Resource.DisplaysType.PointCloud, display.gameObject);
            display = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;

            panel.Colormap.Index = (int)display.Colormap;
            panel.PointSize.Value = display.PointSize;
            panel.IntensityChannel.Options = display.FieldNames;
            panel.IntensityChannel.Value = display.IntensityChannel;

            panel.PointSize.ValueChanged += f =>
            {
                display.PointSize = f;
            };
            panel.IntensityChannel.ValueChanged += (_, s) =>
            {
                display.IntensityChannel = s;
            };
            panel.Colormap.ValueChanged += (i, _) =>
            {
                display.Colormap = (Resource.Colormaps.Id)i;
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
            panel.IntensityChannel.Options = display.FieldNames;
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
    }
}
