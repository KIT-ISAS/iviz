using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public sealed class ARGuiModuleData : ListenerModuleData
    {
        readonly ARGuiListener listener;
        readonly ARGuiPanelContents panel;

        protected override ListenerController Listener => listener;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.ARGuiSystem;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => listener.Config;


        public ARGuiModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<ARGuiPanelContents>(Resource.ModuleType.ARGuiSystem);
            listener = new ARGuiListener(this);
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (ARGuiConfiguration) constructor.Configuration;
            }

            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = listener;
            panel.Listener.Listener = listener.Listener;
            panel.FeedbackSender.Set(listener.FeedbackSender);
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
        }

        public override void AddToState(StateConfiguration config)
        {
        }
    }
}