using Iviz.Resources;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class DepthCloudModuleData : ModuleData
    {
        [NotNull] readonly DepthCloudController controller;
        [NotNull] readonly DepthCloudPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.DepthCloud;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        readonly List<string> depthImageCandidates = new List<string>();
        readonly List<string> colorImageCandidates = new List<string>();

        public DepthCloudModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<DepthCloudPanelContents>(Resource.ModuleType.DepthCloud);

            controller = new DepthCloudController(this);
            if (constructor.Configuration != null)
            {
                controller.Config = (DepthCloudConfiguration) constructor.Configuration;
                controller.ColorImage = GetImageWithName(controller.ColorName);
                controller.DepthImage = GetImageWithName(controller.DepthName);
            }

            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            controller.StopController();
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = controller;
            panel.HideButton.State = controller.Visible;

            depthImageCandidates.Clear();
            depthImageCandidates.Add("<none>");
            depthImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas.Where(x => x.ModuleType == Resource.ModuleType.Image).Select(x => x.Topic)
            );
            panel.Depth.Options = depthImageCandidates;
            panel.Depth.Value = controller.DepthName;

            colorImageCandidates.Clear();
            colorImageCandidates.Add("<none>");
            colorImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas.Where(x => x.ModuleType == Resource.ModuleType.Image).Select(x => x.Topic)
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
            return ModuleListPanel.ModuleDatas.OfType<ImageModuleData>().FirstOrDefault(x => x.Topic == name)?.Image;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            depthImageCandidates.Clear();
            depthImageCandidates.Add("<none>");
            depthImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas.Where(x => x.ModuleType == Resource.ModuleType.Image).Select(x => x.Topic)
            );
            panel.Depth.Options = depthImageCandidates;

            colorImageCandidates.Clear();
            colorImageCandidates.Add("<none>");
            colorImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas.Where(x => x.ModuleType == Resource.ModuleType.Image).Select(x => x.Topic)
            );
            panel.Color.Options = colorImageCandidates;
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<DepthCloudConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(DepthCloudConfiguration.Visible):
                        controller.Visible = config.Visible;
                        break;
                    case nameof(DepthCloudConfiguration.ColorName):
                        controller.ColorName = config.ColorName;
                        break;
                    case nameof(DepthCloudConfiguration.DepthName):
                        controller.DepthName = config.DepthName;
                        break;
                    case nameof(DepthCloudConfiguration.PointSize):
                        controller.PointSize = config.PointSize;
                        break;
                    case nameof(DepthCloudConfiguration.FovAngle):
                        controller.FovAngle = config.FovAngle;
                        break;

                    default:
                        Logger.External(LogLevel.Warn, $"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }   
        
        public override void AddToState(StateConfiguration config)
        {
            config.DepthImageProjectors.Add(controller.Config);
        }
    }
}