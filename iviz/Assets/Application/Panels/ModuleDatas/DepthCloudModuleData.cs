using Iviz.Resources;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;

namespace Iviz.App
{
    public sealed class DepthCloudModuleData : ModuleData
    {
        readonly DepthCloudController controller;
        readonly DepthCloudPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.DepthCloud;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        readonly List<string> depthImageCandidates = new List<string>();
        readonly List<string> colorImageCandidates = new List<string>();

        public DepthCloudModuleData(ModuleDataConstructor constructor) :
        base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.DepthCloud) as DepthCloudPanelContents;

            controller = new DepthCloudController(this);
            if (constructor.Configuration != null)
            {
                controller.Config = (DepthCloudConfiguration)constructor.Configuration;
                controller.ColorImage = GetImageWithName(controller.ColorName);
                controller.DepthImage = GetImageWithName(controller.DepthName);
            }
            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            controller.Stop();
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = controller;
            panel.HideButton.State = controller.Visible;

            depthImageCandidates.Clear();
            depthImageCandidates.Add("<none>");
            depthImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas.
                Where(x => x.Module == Resource.Module.Image).
                Select(x => x.Topic)
            );
            panel.Depth.Options = depthImageCandidates;
            panel.Depth.Value = controller.DepthName;

            colorImageCandidates.Clear();
            colorImageCandidates.Add("<none>");
            colorImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas.
                Where(x => x.Module == Resource.Module.Image).
                Select(x => x.Topic)
            );
            panel.Color.Options = colorImageCandidates;
            panel.Color.Value = controller.ColorName;

            panel.PointSize.Value = controller.PointSize;
            panel.FOV.Value = controller.FovAngle;

            panel.PointSize.ValueChanged += f =>
            {
                controller.PointSize = f;
            };
            panel.Depth.ValueChanged += (i, s) =>
            {
                controller.DepthImage = (i == 0) ? null : GetImageWithName(s);
                controller.DepthName = s;
            };
            panel.Color.ValueChanged += (i, s) =>
            {
                controller.ColorImage = (i == 0) ? null : GetImageWithName(s);
                controller.ColorName = s;
            };
            panel.FOV.ValueChanged += f =>
            {
                controller.FovAngle = f;
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
                UpdateModuleButton();
            };
        }

        ImageListener GetImageWithName(string name)
        {
            return ModuleListPanel.ModuleDatas.
                OfType<ImageModuleData>().
                FirstOrDefault(x => x.Topic == name)?.Image;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            depthImageCandidates.Clear();
            depthImageCandidates.Add("<none>");
            depthImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas.
                Where(x => x.Module == Resource.Module.Image).
                Select(x => x.Topic)
            );
            panel.Depth.Options = depthImageCandidates;

            colorImageCandidates.Clear();
            colorImageCandidates.Add("<none>");
            colorImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas.
                Where(x => x.Module == Resource.Module.Image).
                Select(x => x.Topic)
            );
            panel.Color.Options = colorImageCandidates;
        }

        public override void AddToState(StateConfiguration config)
        {
            config.DepthImageProjectors.Add(controller.Config);
        }
    }
}
