using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.App
{
    public sealed class DepthCloudModuleData : ModuleData
    {
        const string NoneStr = "<none>";

        [NotNull] readonly DepthCloudController controller;
        [NotNull] readonly DepthCloudPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override ModuleType ModuleType => ModuleType.DepthCloud;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        readonly List<string> depthImageCandidates = new List<string>();
        readonly List<string> colorImageCandidates = new List<string>();

        public DepthCloudModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<DepthCloudPanelContents>(ModuleType.DepthCloud);

            controller = new DepthCloudController(this);
            if (constructor.Configuration != null)
            {
                controller.Config = (DepthCloudConfiguration) constructor.Configuration;
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

            panel.ColorTopic.Listener = controller.ColorListener;
            panel.DepthTopic.Listener = controller.DepthListener;
            panel.DepthInfoTopic.Listener = controller.DepthInfoListener;

            panel.Color.Value = controller.ColorTopic;
            panel.Depth.Value = controller.DepthTopic;

            panel.ColorPreview.Material = controller.ColorMaterial;
            panel.DepthPreview.Material = controller.DepthMaterial;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;

            string[] topics = GetImageTopics().ToArray();
            panel.Color.Hints = topics;
            panel.Depth.Hints = topics;

            panel.Color.EndEdit += f =>
            {
                controller.ColorTopic = f;
                panel.ColorTopic.Listener = controller.ColorListener;
            };
            panel.Depth.EndEdit += f =>
            {
                controller.DepthTopic = f;
                panel.DepthTopic.Listener = controller.DepthListener;
                panel.DepthInfoTopic.Listener = controller.DepthInfoListener;
            };
            panel.ColorPreview.Clicked += () =>
            {
                ModuleListPanel.ShowImageDialog(new ColorListener(this));
                panel.ColorPreview.Interactable = false;
            };
            panel.DepthPreview.Clicked += () =>
            {
                ModuleListPanel.ShowImageDialog(new DepthListener(this));
                panel.DepthPreview.Interactable = false;
            };
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            string[] topics = GetImageTopics().ToArray();
            panel.Color.Hints = topics;
            panel.Depth.Hints = topics;
            panel.ColorPreview.ToggleImageEnabled();
            panel.DepthPreview.ToggleImageEnabled();
        }

        [NotNull]
        static IEnumerable<string> GetImageTopics() => ConnectionManager.Connection.GetSystemPublishedTopicTypes()
            .Where(topicInfo =>
                topicInfo.Type == Image.RosMessageType || topicInfo.Type == CompressedImage.RosMessageType)
            .Select(topicInfo => topicInfo.Topic);

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
                    case nameof(DepthCloudConfiguration.ColorTopic):
                        controller.ColorTopic = config.ColorTopic;
                        break;
                    case nameof(DepthCloudConfiguration.DepthTopic):
                        controller.DepthTopic = config.DepthTopic;
                        break;
                    default:
                        Logger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.DepthImageProjectors.Add(controller.Config);
        }

        sealed class ColorListener : IImageDialogListener
        {
            readonly DepthCloudController controller;
            readonly ImagePreviewWidget previewWidget;

            public ColorListener([NotNull] DepthCloudModuleData moduleData) =>
                (controller, previewWidget) = (moduleData.controller, moduleData.panel.ColorPreview);

            public Material Material => controller.ColorMaterial;
            public Vector2Int ImageSize => controller.ColorImageSize;
            public void OnDialogClosed() => previewWidget.Interactable = true;
        }
        
        sealed class DepthListener : IImageDialogListener
        {
            readonly DepthCloudController controller;
            readonly ImagePreviewWidget previewWidget;

            public DepthListener([NotNull] DepthCloudModuleData moduleData) =>
                (controller, previewWidget) = (moduleData.controller, moduleData.panel.DepthPreview);

            public Material Material => controller.DepthMaterial;
            public Vector2Int ImageSize => controller.DepthImageSize;
            public void OnDialogClosed() => previewWidget.Interactable = true;
        }        
    }
}