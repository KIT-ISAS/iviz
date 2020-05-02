using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class MarkerDisplayData : DisplayableListenerData
    {
        MarkerListener display;
        MarkerPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override DisplayableListener Display => display;
        public override Resource.Module Module => Resource.Module.Marker;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Displays.Marker);
            displayObject.name = "Marker";

            display = displayObject.GetComponent<MarkerListener>();
            display.Parent = TFListener.DisplaysFrame;
            display.Config.topic = topic;
            display.Config.type = type;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Marker) as MarkerPanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<MarkerListener.Configuration>();
            Topic = display.Config.topic;
            Type = display.Config.type;
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
            ResourcePool.Dispose(Resource.Displays.Marker, display.gameObject);
            display = null;
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
            return JToken.FromObject(display.Config);
        }
    }
}
