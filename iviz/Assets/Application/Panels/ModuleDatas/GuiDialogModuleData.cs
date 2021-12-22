#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class GuiDialogModuleData : ListenerModuleData, IInteractableModuleData
    {
        readonly GuiWidgetListener dialogListener;
        readonly GuiDialogPanelContents dialogPanel;

        protected override ListenerController Listener => dialogListener;
        public override ModuleType ModuleType => ModuleType.GuiDialog;
        public override DataPanelContents Panel => dialogPanel;
        public override IConfiguration Configuration => dialogListener.Config;

        public GuiDialogModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic, constructor.Type)
        {
            dialogPanel = DataPanelManager.GetPanelByResourceType<GuiDialogPanelContents>(ModuleType.GuiDialog);
            dialogListener = new GuiWidgetListener(this, (GuiDialogConfiguration?)constructor.Configuration, Topic);
            Interactable = ModuleListPanel.SceneInteractable;
            UpdateModuleButton();
        }

        public bool Interactable
        {
            set => dialogListener.Interactable = value;
        }

        public override void SetupPanel()
        {
            dialogPanel.Frame.Owner = dialogListener;
            dialogPanel.Listener.Listener = dialogListener.Listener;
            dialogPanel.FeedbackSender.Set(dialogListener.FeedbackSender);
            dialogPanel.CloseButton.Clicked += Close;
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<TfConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(GuiDialogConfiguration.Visible):
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
            config.Dialogs.Add(dialogListener.Config);
        }
    }
}