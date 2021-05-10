using System.Collections.Generic;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public sealed class GuiDialogModuleData : ListenerModuleData
    {
        readonly GuiDialogListener dialogListener;
        readonly GuiDialogPanelContents dialogPanel;

        protected override ListenerController Listener => dialogListener;
        public override ModuleType ModuleType => ModuleType.GuiDialog;
        public override DataPanelContents Panel => dialogPanel;
        public override IConfiguration Configuration => dialogListener.Config;


        public GuiDialogModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.GetConfiguration<GuiDialogConfiguration>()?.Topic ?? constructor.Topic,
                constructor.GetConfiguration<GuiDialogConfiguration>()?.Type ?? constructor.Type)
        {
            dialogPanel = DataPanelManager.GetPanelByResourceType<GuiDialogPanelContents>(ModuleType.GuiDialog);
            dialogListener = new GuiDialogListener(this);
            if (constructor.Configuration == null)
            {
                dialogListener.Config.Topic = Topic;
                dialogListener.Config.Type = Type;
            }
            else
            {
                dialogListener.Config = (GuiDialogConfiguration) constructor.Configuration;
            }

            dialogListener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            dialogPanel.Frame.Owner = dialogListener;
            dialogPanel.Listener.Listener = dialogListener.Listener;
            dialogPanel.FeedbackSender.Set(dialogListener.FeedbackSender);
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Dialogs.Add(dialogListener.Config);
        }
    }
}