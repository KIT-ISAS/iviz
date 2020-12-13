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
        public override Resource.ModuleType ModuleType => Resource.ModuleType.InteractiveMarker;

        public override IConfiguration Configuration => listener.Config;

        public InteractiveMarkerModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.GetConfiguration<InteractiveMarkerConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<InteractiveMarkerPanelContents>(Resource.ModuleType
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
            panel.DescriptionsVisible.Value = listener.DescriptionsVisible;
            panel.Sender.Set(listener.Publisher);
            panel.Marker.MarkerListener = listener;
            panel.HideButton.State = listener.Visible;

            panel.DescriptionsVisible.ValueChanged += f =>
            {
                listener.DescriptionsVisible = f;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.HideButton.Clicked += () =>
            {
                listener.Visible = !listener.Visible;
                panel.HideButton.State = listener.Visible;
                UpdateModuleButton();
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
                        listener.Visible = config.Visible;
                        break;
                    case nameof(InteractiveMarkerConfiguration.DescriptionsVisible):
                        listener.DescriptionsVisible = config.DescriptionsVisible;
                        break;
                    default:
                        Logger.External($"{this}: Unknown field '{field}'", LogLevel.Warn);
                        break;
                }
            }
            
            ResetPanel();
        }
    }
}