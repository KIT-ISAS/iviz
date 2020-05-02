using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class InteractiveMarkerDisplayData : DisplayableListenerData
    {
        InteractiveMarkerListener display;
        InteractiveMarkerPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override DisplayableListener Display => display;
        public override Resource.Module Module => Resource.Module.InteractiveMarker;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            
            Resource.DisplaysType.Initialize();
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.DisplaysType.InteractiveMarker);
            displayObject.name = "InteractiveMarkers";

            display = displayObject.GetComponent<InteractiveMarkerListener>();
            display.Parent = TFListener.DisplaysFrame;
            display.Config.topic = topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.InteractiveMarker) as InteractiveMarkerPanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<InteractiveMarkerListener.Configuration>();
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
            ResourcePool.Dispose(Resource.DisplaysType.InteractiveMarker, display.gameObject);
            display = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;
            panel.DisableExpiration.Value = display.DisableExpiration;

            panel.DisableExpiration.ValueChanged += f =>
            {
                display.DisableExpiration = f;
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
