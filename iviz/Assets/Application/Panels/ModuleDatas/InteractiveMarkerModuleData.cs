#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="InteractiveMarkerModulePanel"/> 
    /// </summary>
    public sealed class InteractiveMarkerModuleData : ListenerModuleData, IInteractableModuleData
    {
        readonly InteractiveMarkerListener listener;
        readonly InteractiveMarkerModulePanel panel;

        protected override ListenerController Listener => listener;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.InteractiveMarker;

        public override IConfiguration Configuration => listener.Config;

        public InteractiveMarkerModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic, constructor.Type)
        {
            panel = ModulePanelManager.GetPanelByResourceType<InteractiveMarkerModulePanel>(
                ModuleType.InteractiveMarker);
            listener = new InteractiveMarkerListener((InteractiveMarkerConfiguration?)constructor.Configuration, Topic);
            Interactable = ModuleListPanel.SceneInteractable;
            UpdateModuleButton();
        }

        public bool Interactable
        {
            set => listener.Interactable = value;
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.FullListener.Listener = listener.FullListener;
            panel.DescriptionsVisible.Value = listener.DescriptionsVisible;
            panel.Sender.Set(listener.Publisher);
            panel.Marker.MarkerListener = listener;
            panel.HideButton.State = listener.Visible;

            panel.DescriptionsVisible.ValueChanged += f => { listener.DescriptionsVisible = f; };
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
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
                        RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }
    }
}