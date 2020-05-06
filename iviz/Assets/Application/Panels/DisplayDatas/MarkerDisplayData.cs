using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class MarkerDisplayData : DisplayableListenerData
    {
        MarkerListener listener;
        MarkerPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override TopicListener Listener => listener;
        public override Resource.Module Module => Resource.Module.Marker;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            
            GameObject listenerObject = ResourcePool.GetOrCreate(Resource.Listeners.Marker);
            listenerObject.name = "Marker";

            listener = listenerObject.GetComponent<MarkerListener>();
            listener.Config.topic = topic;
            listener.Config.type = type;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Marker) as MarkerPanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<MarkerListener.Configuration>();
            Topic = listener.Config.topic;
            Type = listener.Config.type;
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
            ResourcePool.Dispose(Resource.Listeners.Marker, listener.gameObject);
            listener = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;
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
