using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class InteractiveMarkerDisplayData : ListenerDisplayData
    {
        readonly InteractiveMarkerListener listener;
        readonly InteractiveMarkerPanelContents panel;

        protected override TopicListener Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.InteractiveMarker;

        public override IConfiguration Configuration => listener.Config;

        public InteractiveMarkerDisplayData(DisplayDataConstructor constructor) :
        base(constructor.DisplayList,
            constructor.GetConfiguration<InteractiveMarkerConfiguration>()?.Topic ?? constructor.Topic,
            constructor.Type)
        {

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.InteractiveMarker) as InteractiveMarkerPanelContents;
            listener = Resource.Listeners.Instantiate<InteractiveMarkerListener>();
            listener.name = "InteractiveMarkers";
            listener.DisplayData = this;
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

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;
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

        public override void AddToState(StateConfiguration config)
        {
            throw new System.NotImplementedException();
        }
    }
}
