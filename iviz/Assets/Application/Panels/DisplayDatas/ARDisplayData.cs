using Iviz.App.Listeners;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARPanelContents"/> 
    /// </summary>
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
            panel.Frame.Owner = display;
            panel.WorldScale.Value = display.WorldScale;
            panel.Origin.Value = display.Origin;

            panel.SearchMarker.Value = display.SearchMarker;
            panel.MarkerSize.Value = display.MarkerSize;
            panel.MarkerHorizontal.Value = display.MarkerHorizontal;
            panel.MarkerAngle.Value = display.MarkerAngle;
            panel.MarkerFrame.Value = display.MarkerFrame;
            panel.MarkerOffset.Value = display.MarkerOffset;

            panel.HeadSender.Set(display.RosSenderHead);
            panel.MarkersSender.Set(display.RosSenderMarkers);

            panel.PublishHead.Value = display.PublishPose;
            panel.PublishPlanes.Value = display.PublishPlanesAsMarkers;

            CheckInteractable();

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
                CheckInteractable();
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
            panel.MarkerFrame.EndEdit += f =>
            {
                display.MarkerFrame = f;
                CheckInteractable();
            };
            panel.MarkerOffset.ValueChanged += f =>
            {
                display.MarkerOffset = f;
            };
            panel.PublishHead.ValueChanged += f =>
            {
                display.PublishPose = f;
                panel.HeadSender.Set(display.RosSenderHead);
            };
            panel.PublishPlanes.ValueChanged += f =>
            {
                display.PublishPlanesAsMarkers = f;
                panel.MarkersSender.Set(display.RosSenderMarkers);
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

        void CheckInteractable()
        {
            panel.Origin.Interactable = !display.SearchMarker;
            panel.MarkerHorizontal.Interactable = display.SearchMarker;
            panel.MarkerAngle.Interactable = display.SearchMarker;
            panel.MarkerFrame.Interactable = display.SearchMarker;
            panel.MarkerOffset.Interactable = display.SearchMarker && display.MarkerFrame.Length != 0;
            panel.MarkerSize.Interactable = display.SearchMarker;
        }

        public override void AddToState(StateConfiguration config)
        {
            config.AR = display.Config;
        }
    }
}
