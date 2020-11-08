using System.Collections;
using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="InteractiveMarkerPanelContents"/> 
    /// </summary>
    public sealed class InteractiveMarkerModuleData : ListenerModuleData
    {
        [NotNull] readonly InteractiveMarkerListener listener;
        [NotNull] readonly InteractiveMarkerPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.InteractiveMarker;

        public override IConfiguration Configuration => listener.Config;

        public InteractiveMarkerModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.ModuleList,
                constructor.GetConfiguration<InteractiveMarkerConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<InteractiveMarkerPanelContents>(Resource.Module
                .InteractiveMarker);
            listener = new InteractiveMarkerListener(this);
            if (constructor.Configuration != null)
            {
                listener.Config = (InteractiveMarkerConfiguration) constructor.Configuration;
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
            panel.Listener.Listener = listener.Listener;
            panel.FullListener.Listener = listener.FullListener;
            panel.DisableExpiration.Value = listener.EnableAutoExpiration;
            panel.Sender.Set(listener.Publisher);
            panel.Marker.MarkerListener = listener;

            panel.DisableExpiration.ValueChanged += f =>
            {
                listener.EnableAutoExpiration = f;
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

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<InteractiveMarkerConfiguration>(configAsJson);
            
            foreach (string field in fields)
            {
                switch (field) 
                {
                    case nameof(InteractiveMarkerConfiguration.Visible):
                        // TODO!
                        //listener.Visible = config.Visible;
                        break;
                    case nameof(InteractiveMarkerConfiguration.EnableAutoExpiration):
                        listener.EnableAutoExpiration = config.EnableAutoExpiration;
                        break;
                    default:
                        Logger.External(LogLevel.Warn, $"{this}: Unknown field '{field}'");
                        break;
                }
            }
            
            ResetPanel();
        }
    }
}