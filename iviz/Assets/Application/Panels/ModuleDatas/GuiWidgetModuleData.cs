#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class GuiWidgetModuleData : ListenerModuleData, IInteractableModuleData
    {
        readonly GuiWidgetListener listener;
        readonly GuiWidgetModulePanel panel;

        protected override ListenerController Listener => listener;
        public override ModuleType ModuleType => ModuleType.GuiWidget;
        public override ModulePanel Panel => panel;
        public override IConfiguration Configuration => listener.Config;

        public GuiWidgetModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<GuiWidgetModulePanel>(ModuleType.GuiWidget);
            listener = new GuiWidgetListener((GuiWidgetConfiguration?)constructor.Configuration, Topic);
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
            panel.Frame.Owner = listener;
            panel.Listener.Listener = listener.Listener;
            panel.FeedbackSender.Set(listener.FeedbackSender);
            panel.CloseButton.Clicked += Close;
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<TfConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(GuiWidgetConfiguration.Visible):
                        break;
                    default:
                        Core.RosLogger.Error($"{this}: Unknown field '{field}'");
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