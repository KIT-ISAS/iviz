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

            display = Resource.Listeners.AR.Instantiate().GetComponent<ARController>();
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

            panel.SearchMarker.Value = display.SearchMarker;
            panel.MarkerSize.Value = display.MarkerSize;
            panel.MarkerHorizontal.Value = display.MarkerHorizontal;
            panel.MarkerAngle.Value = display.MarkerAngle;
            panel.MarkerFrame.Value = display.MarkerFrame;
            panel.MarkerOffset.Value = display.MarkerOffset;

            panel.WorldScale.ValueChanged += f =>
            {
                display.WorldScale = f;
            };
            panel.Origin.ValueChanged += f =>
            {
                display.Origin = f;
            };
            panel.SearchMarker.ValueChanged += f =>
            {
                display.SearchMarker = f;
            };
            panel.MarkerSize.ValueChanged += f =>
            {
                display.MarkerSize = f;
            };
            panel.MarkerHorizontal.ValueChanged += f =>
            {
                display.MarkerHorizontal = f;
            };
            panel.MarkerAngle.ValueChanged += f =>
            {
                display.MarkerAngle = (int)f;
            };
            panel.MarkerFrame.ValueChanged += f =>
            {
                display.MarkerFrame = f;
            };
            panel.MarkerOffset.ValueChanged += f =>
            {
                display.MarkerOffset = f;
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
