using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class MarkerDisplayData : DisplayableListenerData
    {
        readonly MarkerListener listener;
        readonly MarkerPanelContents panel;

        public override DataPanelContents Panel => panel;
        protected override TopicListener Listener => listener;
        public override Resource.Module Module => Resource.Module.Marker;

        public override IConfiguration Configuration => listener.Config;

        public MarkerDisplayData(DisplayDataConstructor constructor) :
            base(constructor.DisplayList,
                ((MarkerConfiguration)constructor.Configuration)?.Topic ?? constructor.Topic,
                ((MarkerConfiguration)constructor.Configuration)?.Type ?? constructor.Type)
        {
            GameObject listenerObject = Resource.Listeners.Marker.Instantiate();
            listenerObject.name = "Marker:" + Topic;

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Marker) as MarkerPanelContents;
            listener = listenerObject.GetComponent<MarkerListener>();
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
                listener.Config.Type = Type;
            }
            else
            {
                listener.Config = (MarkerConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        /*
        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);

            GameObject listenerObject = ResourcePool.GetOrCreate(Resource.Listeners.Marker);
            listenerObject.name = "Marker";

            listener = listenerObject.GetComponent<MarkerListener>();
            listener.Config.Topic = topic;
            listener.Config.Type = type;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Marker) as MarkerPanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<MarkerConfiguration>();
            Topic = listener.Config.Topic;
            Type = listener.Config.Type;
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
            panel.Topic.Label = Topic;
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
        }

        /*
        public override JToken Serialize()
        {
            return JToken.FromObject(listener.Config);
        }
        */

        public override void AddToState(StateConfiguration config)
        {
            config.Markers.Add(listener.Config);
        }
    }
}
