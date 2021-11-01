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

        [CanBeNull] ImageDialogData colorDialogData;
        [CanBeNull] ImageDialogData depthDialogData;

        public override DataPanelContents Panel => panel;
        public override ModuleType ModuleType => ModuleType.DepthCloud;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public DepthCloudModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<DepthCloudPanelContents>(ModuleType.DepthCloud);

            controller = new DepthCloudController(this);
            if (constructor.Configuration != null)
            {
                controller.Config = (DepthCloudConfiguration)constructor.Configuration;
            }

            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            controller.StopController();
            colorDialogData?.Stop();
            depthDialogData?.Stop();
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = controller;
            panel.HideButton.State = controller.Visible;

            panel.ColorTopic.Listener = controller.ColorListener;
            panel.DepthTopic.Listener = controller.DepthListener;

            panel.Color.Value = controller.ColorTopic;
            panel.Depth.Value = controller.DepthTopic;

            panel.ColorPreview.Material = controller.ColorMaterial;
            panel.DepthPreview.Material = controller.DepthMaterial;

            panel.Description.Label = controller.Description;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;

            var topics = GetImageTopics();
            panel.Color.Hints = topics;
            panel.Depth.Hints = topics;

            panel.Color.EndEdit += f =>
            {
                controller.ColorTopic = f.Length == 0 || f[0] == NoneStr[0] ? "" : f;
                panel.ColorTopic.Listener = controller.ColorListener;

                if (colorDialogData != null)
                {
                    colorDialogData.Title = SanitizeTitle(controller.ColorTopic);
                }
            };
            panel.Depth.EndEdit += f =>
            {
                controller.DepthTopic = f.Length == 0 || f[0] == NoneStr[0] ? "" : f;
                panel.DepthTopic.Listener = controller.DepthListener;

                if (depthDialogData != null)
                {
                    depthDialogData.Title = SanitizeTitle(controller.DepthTopic);
                }
            };
            panel.ColorPreview.Clicked += () =>
            {
                if (colorDialogData == null)
                {
                    colorDialogData = new ColorImageDialogListener(this).DialogData;
                    colorDialogData.SetupPanel();
                }

                colorDialogData.Title = SanitizeTitle(controller.ColorTopic);
            };
            panel.DepthPreview.Clicked += () =>
            {
                if (depthDialogData == null)
                {
                    depthDialogData = new DepthImageDialogListener(this).DialogData;
                    depthDialogData.SetupPanel();
                }

                depthDialogData.Title = SanitizeTitle(controller.DepthTopic);
            };
        }

        [NotNull]
        static string SanitizeTitle([CanBeNull] string topic) => string.IsNullOrEmpty(topic) ? "[Empty]" : topic;

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            var topics = GetImageTopics();
            panel.Color.Hints = topics;
            panel.Depth.Hints = topics;
            panel.ColorPreview.ToggleImageEnabled();
            panel.DepthPreview.ToggleImageEnabled();
            colorDialogData?.ToggleImageEnabled();
            depthDialogData?.ToggleImageEnabled();
            panel.Description.Label = controller.Description;
        }

        [NotNull]
        static List<string> GetImageTopics()
        {
            var topics = new List<string> { NoneStr };
            topics.AddRange(ConnectionManager.Connection.GetSystemPublishedTopicTypes()
                .Where(topicInfo => topicInfo.Type == Image.RosMessageType ||
                                    topicInfo.Type == CompressedImage.RosMessageType)
                .Select(topicInfo => topicInfo.Topic)
            );
            return topics;
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

        void OnDialogClosed(ImageDialogData listener)
        {
            if (listener == colorDialogData)
            {
                colorDialogData = null;
            }
            else if (listener == depthDialogData)
            {
                depthDialogData = null;
            }
        }

        sealed class ColorImageDialogListener : ImageDialogListener
        {
            [NotNull] readonly DepthCloudModuleData moduleData;
            public ColorImageDialogListener([NotNull] DepthCloudModuleData moduleData) => this.moduleData = moduleData;
            public override Material Material => moduleData.controller.ColorMaterial;
            public override Vector2Int ImageSize => moduleData.controller.ColorImageSize;
            protected override void Stop() => moduleData.OnDialogClosed(DialogData);
        }

        sealed class DepthImageDialogListener : ImageDialogListener
        {
            [NotNull] readonly DepthCloudModuleData moduleData;
            public DepthImageDialogListener([NotNull] DepthCloudModuleData moduleData) => this.moduleData = moduleData;
            public override Material Material => moduleData.controller.DepthMaterial;
            public override Vector2Int ImageSize => moduleData.controller.DepthImageSize;
            protected override void Stop() => moduleData.OnDialogClosed(DialogData);
        }
    }
}