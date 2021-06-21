using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iviz.Msgs.IvizCommonMsgs;
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
    public sealed class InteractiveMarkerModuleData : ListenerModuleData, IInteractableModuleData
    {
        [NotNull] readonly InteractiveMarkerListener listener;
        [NotNull] readonly InteractiveMarkerPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override ModuleType ModuleType => ModuleType.InteractiveMarker;

        public override IConfiguration Configuration => listener.Config;

        public InteractiveMarkerModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.GetConfiguration<InteractiveMarkerConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<InteractiveMarkerPanelContents>(
                ModuleType.InteractiveMarker);
            listener = new InteractiveMarkerListener(this);
            if (constructor.Configuration != null)
            {
                listener.Config = (InteractiveMarkerConfiguration) constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
            }

            Interactable = ModuleListPanel.SceneInteractable;
            listener.StartListening();
            UpdateModuleButton();
        }

        public bool Interactable
        {
            get => listener.Interactable;
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
                        Logger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }
    }
}