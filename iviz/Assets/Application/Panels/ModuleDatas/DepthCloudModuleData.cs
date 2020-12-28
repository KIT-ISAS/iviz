using System;
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
        const string NoneStr = "<none>";

        [NotNull] readonly DepthCloudController controller;
        [NotNull] readonly DepthCloudPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.DepthCloud;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        readonly List<string> depthImageCandidates = new List<string>();
        readonly List<string> colorImageCandidates = new List<string>();

        public DepthCloudModuleData([NotNull] ModuleDataConstructor constructor) : 
            base(constructor.Topic, constructor.Type)
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
            depthImageCandidates.Add(NoneStr);
            depthImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas
                    .Where(data => data.ModuleType == Resource.ModuleType.Image)
                    .Select(data => data.Topic)
            );
            panel.Depth.Options = depthImageCandidates;
            try
            {
                panel.Depth.Value = controller.DepthName.Length != 0 ? controller.DepthName : NoneStr;
            }
            catch (InvalidOperationException)
            {
                panel.Depth.Index = 0;
            }

            colorImageCandidates.Clear();
            colorImageCandidates.Add(NoneStr);
            colorImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas
                    .Where(data => data.ModuleType == Resource.ModuleType.Image)
                    .Select(data => data.Topic)
            );
            panel.Color.Options = colorImageCandidates;
            try
            {
                panel.Color.Value = controller.ColorName.Length != 0 ? controller.ColorName : NoneStr;
            }
            catch (InvalidOperationException)
            {
                panel.Color.Index = 0;
            }

            panel.PointSize.Value = controller.PointSize;
            panel.FOV.Value = controller.FovAngle;

            panel.PointSize.ValueChanged += f => { controller.PointSize = f; };
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
            panel.FOV.ValueChanged += f => { controller.FovAngle = f; };
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
            return ModuleListPanel.ModuleDatas
                .OfType<ImageModuleData>()
                .FirstOrDefault(data => data.Topic == name)
                ?.Image;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            depthImageCandidates.Clear();
            depthImageCandidates.Add(NoneStr);
            depthImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas
                    .Where(data => data.ModuleType == Resource.ModuleType.Image)
                    .Select(data => data.Topic)
            );
            panel.Depth.Options = depthImageCandidates;

            colorImageCandidates.Clear();
            colorImageCandidates.Add(NoneStr);
            colorImageCandidates.AddRange(
                ModuleListPanel.ModuleDatas
                    .Where(data => data.ModuleType == Resource.ModuleType.Image)
                    .Select(data => data.Topic)
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
                        ImageListener color = GetImageWithId(config.ColorName);
                        controller.ColorImage = color;
                        controller.ColorName = color.Config.Topic;
                        break;
                    case nameof(DepthCloudConfiguration.DepthName):
                        ImageListener depth = GetImageWithId(config.DepthName);
                        controller.DepthImage = depth;
                        controller.DepthName = depth.Config.Topic;
                        break;
                    case nameof(DepthCloudConfiguration.PointSize):
                        controller.PointSize = config.PointSize;
                        break;
                    case nameof(DepthCloudConfiguration.FovAngle):
                        controller.FovAngle = config.FovAngle;
                        break;
                    default:
                        Logger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }

        [NotNull]
        static ImageListener GetImageWithId(string imageId)
        {
            ModuleData imageData = ModuleListPanel.Instance.ModuleDatas.FirstOrDefault(
                data => data.Configuration.Id == imageId);
            if (imageData == null)
            {
                throw new InvalidOperationException($"No image with id '{imageId}' found");
            }

            if (imageData.ModuleType != Resource.ModuleType.Image)
            {
                throw new InvalidOperationException($"Module with id '{imageId}' is not an image");
            }

            return ((ImageModuleData) imageData).Image;
        }

        public override void AddToState(StateConfiguration config)
        {
            config.DepthImageProjectors.Add(controller.Config);
        }
    }
}