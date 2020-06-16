using Iviz.App.Listeners;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARPanelContents"/> 
    /// </summary>
    public class ARModuleData : ModuleData
    {
        readonly ARController controller;
        readonly ARPanelContents panel;

        public override Resource.Module Module => Resource.Module.AR;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public ARModuleData(ModuleDataConstructor constructor) :
            base(constructor.DisplayList, constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.AR) as ARPanelContents;

            controller = Resource.Controllers.AR.Instantiate().GetComponent<ARController>();
            controller.ModuleData = this;
            if (constructor.Configuration != null)
            {
                controller.Config = (ARConfiguration)constructor.Configuration;
            }

            UpdateButtonText();
        }

        public override void Stop()
        {
            base.Stop();

            controller.Stop();
            Object.Destroy(controller.gameObject);
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = controller;
            panel.WorldScale.Value = controller.WorldScale;
            panel.Origin.Value = controller.Origin;

            panel.SearchMarker.Value = controller.SearchMarker;
            panel.MarkerSize.Value = controller.MarkerSize;
            panel.MarkerHorizontal.Value = controller.MarkerHorizontal;
            panel.MarkerAngle.Value = controller.MarkerAngle;
            panel.MarkerFrame.Value = controller.MarkerFrame;
            panel.MarkerOffset.Value = controller.MarkerOffset;

            panel.HeadSender.Set(controller.RosSenderHead);
            panel.MarkersSender.Set(controller.RosSenderMarkers);

            panel.PublishHead.Value = controller.PublishPose;
            panel.PublishPlanes.Value = controller.PublishPlanesAsMarkers;

            CheckInteractable();

            panel.WorldScale.ValueChanged += f =>
            {
                controller.WorldScale = f;
            };
            panel.Origin.ValueChanged += f =>
            {
                controller.Origin = f;
            };
            panel.SearchMarker.ValueChanged += f =>
            {
                controller.SearchMarker = f;
                CheckInteractable();
            };
            panel.MarkerSize.ValueChanged += f =>
            {
                controller.MarkerSize = f;
            };
            panel.MarkerHorizontal.ValueChanged += f =>
            {
                controller.MarkerHorizontal = f;
            };
            panel.MarkerAngle.ValueChanged += f =>
            {
                controller.MarkerAngle = (int)f;
            };
            panel.MarkerFrame.EndEdit += f =>
            {
                controller.MarkerFrame = f;
                CheckInteractable();
            };
            panel.MarkerOffset.ValueChanged += f =>
            {
                controller.MarkerOffset = f;
            };
            panel.PublishHead.ValueChanged += f =>
            {
                controller.PublishPose = f;
                panel.HeadSender.Set(controller.RosSenderHead);
            };
            panel.PublishPlanes.ValueChanged += f =>
            {
                controller.PublishPlanesAsMarkers = f;
                panel.MarkersSender.Set(controller.RosSenderMarkers);
            };

            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.HideButton.Clicked += () =>
            {
                controller.Visible = !controller.Visible;
                panel.HideButton.State = controller.Visible;
                UpdateButtonText();
            };
        }

        void CheckInteractable()
        {
            panel.Origin.Interactable = !controller.SearchMarker;
            panel.MarkerHorizontal.Interactable = controller.SearchMarker;
            panel.MarkerAngle.Interactable = controller.SearchMarker;
            panel.MarkerFrame.Interactable = controller.SearchMarker;
            panel.MarkerOffset.Interactable = controller.SearchMarker && controller.MarkerFrame.Length != 0;
            panel.MarkerSize.Interactable = controller.SearchMarker;
        }

        public override void AddToState(StateConfiguration config)
        {
            config.AR = controller.Config;
        }
    }
}
