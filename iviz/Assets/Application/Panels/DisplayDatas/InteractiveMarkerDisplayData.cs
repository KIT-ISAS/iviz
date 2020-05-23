using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class InteractiveMarkerDisplayData : DisplayableListenerData
    {
        readonly InteractiveMarkerListener listener;
        readonly InteractiveMarkerPanelContents panel;

        protected override TopicListener Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.InteractiveMarker;

        public override IConfiguration Configuration => listener.Config;

        public InteractiveMarkerDisplayData(DisplayDataConstructor constructor) :
        base(constructor.DisplayList, ((InteractiveMarkerConfiguration)constructor.Configuration)?.Topic ?? constructor.Topic, constructor.Type)
        {
            GameObject listenerObject = Resource.Listeners.InteractiveMarker.Instantiate();
            listenerObject.name = "InteractiveMarkers";

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.InteractiveMarker) as InteractiveMarkerPanelContents;
            listener = listenerObject.GetComponent<InteractiveMarkerListener>();
            if (constructor.Configuration != null)
            {
                listener.Config = (InteractiveMarkerConfiguration)constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        /*
        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            
            GameObject listenerObject = ResourcePool.GetOrCreate(Resource.Listeners.InteractiveMarker);
            listenerObject.name = "InteractiveMarkers";

            listener = listenerObject.GetComponent<InteractiveMarkerListener>();
            listener.Config.Topic = topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.InteractiveMarker) as InteractiveMarkerPanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<InteractiveMarkerConfiguration>();
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

        /*
        public override JToken Serialize()
        {
            return JToken.FromObject(listener.Config);
        }
        */

        public override void AddToState(StateConfiguration config)
        {
            throw new System.NotImplementedException();
        }
    }
}
