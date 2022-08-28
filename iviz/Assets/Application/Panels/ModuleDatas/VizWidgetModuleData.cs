#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Core.Configurations;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class VizWidgetModuleData : ListenerModuleData, IInteractableModuleData
    {
        readonly VizWidgetListener listener;
        readonly VizWidgetModulePanel panel;

        protected override ListenerController Listener => listener;
        public override ModuleType ModuleType => ModuleType.VizWidget;
        public override ModulePanel Panel => panel;
        public override IConfiguration Configuration => listener.Config;

        public VizWidgetModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<VizWidgetModulePanel>(ModuleType.VizWidget);
            listener = new VizWidgetListener((VizWidgetConfiguration?)constructor.Configuration, Topic, constructor.Type);
            Interactable = ModuleListPanel.SceneInteractable;
            UpdateModuleButton();
        }

        public bool Interactable
        {
            set => listener.Interactable = value;
        }

        public override void SetupPanel()
        {
            panel.HideButton.State = listener.Visible;
            panel.Listener.Listener = listener.Listener;
            panel.FeedbackSender.Set(listener.FeedbackSender);
            panel.Marker.MarkerListener = listener;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.ResetButton.Clicked += listener.ResetController;
            
            HighlightAll();
        }
        
        void HighlightAll()
        {
            //const int maxHighlights = 50;
            // NYI
        }        

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            //var config = JsonConvert.DeserializeObject<GuiWidgetConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(VizWidgetConfiguration.Visible):
                        break;
                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Dialogs.Add(listener.Config);
        }
    }
}