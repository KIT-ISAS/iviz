using Iviz.Controllers;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="InteractiveMarkerPanelContents"/> 
    /// </summary>
    public sealed class InteractiveMarkerModuleData : ListenerModuleData
    {
        readonly InteractiveMarkerListener listener;
        readonly InteractiveMarkerPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.InteractiveMarker;

        public override IConfiguration Configuration => listener.Config;

        public InteractiveMarkerModuleData(ModuleDataConstructor constructor) :
        base(constructor.ModuleList,
            constructor.GetConfiguration<InteractiveMarkerConfiguration>()?.Topic ?? constructor.Topic,
            constructor.Type)
        {

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.InteractiveMarker) as InteractiveMarkerPanelContents;
            listener = new InteractiveMarkerListener(this);
            if (constructor.Configuration != null)
            {
                listener.Config = (InteractiveMarkerConfiguration)constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
            }
            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;
            panel.DisableExpiration.Value = listener.DisableExpiration;
            panel.Sender.Set(listener.Publisher);
            
            panel.DisableExpiration.ValueChanged += f =>
            {
                listener.DisableExpiration = f;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
        }

        public override void AddToState(StateConfiguration config)
        {
            config.InteractiveMarkers.Add(listener.Config);
        }
    }
}
