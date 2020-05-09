using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class InteractiveMarkerDisplayData : DisplayableListenerData
    {
        InteractiveMarkerListener listener;
        InteractiveMarkerPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override TopicListener Listener => listener;
        public override Resource.Module Module => Resource.Module.InteractiveMarker;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            
            GameObject listenerObject = ResourcePool.GetOrCreate(Resource.Listeners.InteractiveMarker);
            listenerObject.name = "InteractiveMarkers";

            listener = listenerObject.GetComponent<InteractiveMarkerListener>();
            listener.Config.topic = topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.InteractiveMarker) as InteractiveMarkerPanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<InteractiveMarkerListener.Configuration>();
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
            ResourcePool.Dispose(Resource.Listeners.InteractiveMarker, listener.gameObject);
            listener = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;
            panel.DisableExpiration.Value = listener.DisableExpiration;

            panel.DisableExpiration.ValueChanged += f =>
            {
                listener.DisableExpiration = f;
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
