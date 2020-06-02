using Iviz.App.Listeners;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public class ARDisplayData : DisplayData
    {
        readonly ARController display;
        readonly ARPanelContents panel;

        public override Resource.Module Module => Resource.Module.AR;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => display.Config;
        public override IController Controller => display;

        public ARDisplayData(DisplayDataConstructor constructor) :
            base(constructor.DisplayList, constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.AR) as ARPanelContents;

            display = Resource.Listeners.Instantiate<ARController>();
            display.name = "AR";
            display.DisplayData = this;
            if (constructor.Configuration != null)
            {
                display.Config = (ARConfiguration)constructor.Configuration;
            }

            UpdateButtonText();
        }

        public override void Stop()
        {
            base.Stop();

            display.Stop();
            Object.Destroy(display.gameObject);
        }

        public override void SetupPanel()
        {
            panel.WorldScale.Value = display.WorldScale;
            panel.Origin.Value = display.Origin;

            panel.WorldScale.ValueChanged += f =>
            {
                display.WorldScale = f;
            };
            panel.Origin.ValueChanged += f =>
            {
                display.Origin = f;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
            panel.HideButton.Clicked += () =>
            {
                display.Visible = !display.Visible;
                panel.HideButton.State = display.Visible;
                UpdateButtonText();
            };
        }

        public override void AddToState(StateConfiguration config)
        {
            config.AR = display.Config;
        }
    }
}
