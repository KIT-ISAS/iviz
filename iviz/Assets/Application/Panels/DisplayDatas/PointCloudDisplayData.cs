using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class PointCloudDisplayData : DisplayableListenerData
    {
        readonly PointCloudListener listener;
        readonly PointCloudPanelContents panel;

        protected override TopicListener Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.PointCloud;
        public override IConfiguration Configuration => listener.Config;


        /*
        public override IConfiguration Configuration
        {
            get => listener.Config;
            set
            {
                listener.Config = (PointCloudConfiguration)value;
                //Topic = listener.Config.Topic;
            }
        }
        */

        public PointCloudDisplayData(DisplayDataConstructor constructor) :
        base(constructor.DisplayList, ((PointCloudConfiguration)constructor.Configuration)?.Topic ?? constructor.Topic, constructor.Type)
        {
            GameObject listenerObject = Resource.Listeners.PointCloud.Instantiate();
            listenerObject.name = "PointCloud:" + Topic;

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.PointCloud) as PointCloudPanelContents;
            listener = listenerObject.GetComponent<PointCloudListener>();
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (PointCloudConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        /*
        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type) :
        {
            base.Initialize(displayList, topic, type);
            GameObject listenerObject = ResourcePool.GetOrCreate(Resource.Listeners.PointCloud);
            listenerObject.name = "PointCloud:" + Topic;

            listener = listenerObject.GetComponent<PointCloudListener>();
            listener.Config.Topic = Topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.PointCloud) as PointCloudPanelContents;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<PointCloudConfiguration>();
            Topic = listener.Config.Topic;
            return this;
        }

        public override void Start()
        {
            base.Start();
            listener.StartListening();
        }
        */

        public override void SetupPanel()
        {
            panel.Topic.Label = SanitizedTopicText();

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

        /*
        public override JToken Serialize()
        {
            return JToken.FromObject(listener.Config);
        }
        */

        public override void AddToState(StateConfiguration config)
        {
            config.PointClouds.Add(listener.Config);
        }
    }
}
