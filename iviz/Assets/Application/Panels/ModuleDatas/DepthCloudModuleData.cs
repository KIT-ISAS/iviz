﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Tools;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    public sealed class DepthCloudModuleData : ModuleData
    {
        const string ModuleTypeStr = "DepthCloud";
        const string NoneStr = "<none>";

        readonly DepthCloudController controller;
        readonly DepthCloudModulePanel panel;

        ImageDialogData? colorDialogData;
        ImageDialogData? depthDialogData;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.DepthCloud;
        public override IConfiguration Configuration => controller.Config;
        public override Controller Controller => controller;

        public DepthCloudModuleData(ModuleDataConstructor constructor)
        {
            panel = ModulePanelManager.GetPanelByResourceType<DepthCloudModulePanel>(ModuleType.DepthCloud);
            controller = new DepthCloudController((DepthCloudConfiguration?)constructor.Configuration);
            UpdateModuleButton();
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                controller.Dispose();
                colorDialogData?.Dispose();
                depthDialogData?.Dispose();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to dispose controller", e);
            }
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = controller;
            panel.HideButton.State = controller.Visible;

            panel.ColorTopic.Listener = controller.ColorListener;
            panel.DepthTopic.Listener = controller.DepthListener;

            panel.ColorPreview.Material = controller.ColorMaterial;
            panel.DepthPreview.Material = controller.DepthMaterial;

            panel.Description.Text = controller.Description;

            panel.ForceMinMax.Value = controller.OverrideMinMax;
            panel.Min.Value = controller.MinIntensity;
            panel.Max.Value = controller.MaxIntensity;
            panel.FlipMinMax.Value = controller.FlipMinMax;

            panel.Min.Interactable = controller.OverrideMinMax;
            panel.Max.Interactable = controller.OverrideMinMax;

            panel.Colormap.Index = (int)controller.Colormap;

            panel.Color.Value = controller.ColorTopic;
            panel.Depth.Value = controller.DepthTopic;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;

            var topics = GetImageTopics();
            panel.Color.Hints = topics;
            panel.Depth.Hints = topics;

            panel.Color.Submit += f =>
            {
                controller.ColorTopic = f.Length == 0 || f[0] == NoneStr[0] ? "" : f;
                panel.ColorPreview.UpdateMaterial();
                panel.ColorTopic.Listener = controller.ColorListener;

                if (colorDialogData != null)
                {
                    colorDialogData.Title = SanitizeTitle(controller.ColorTopic);
                }
            };
            panel.Depth.Submit += f =>
            {
                controller.DepthTopic = f.Length == 0 || f[0] == NoneStr[0] ? "" : f;
                UpdateModuleButton();

                panel.DepthTopic.Listener = controller.DepthListener;

                if (depthDialogData != null)
                {
                    depthDialogData.Title = SanitizeTitle(controller.DepthTopic);
                }

                if (controller.DepthTopic.Length == 0)
                {
                    controller.Colormap = controller.Colormap;
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
            panel.Colormap.ValueChanged += (i, _) => controller.Colormap = (ColormapId)i;
            panel.Min.ValueChanged += f => controller.MinIntensity = f;
            panel.Max.ValueChanged += f => controller.MaxIntensity = f;
            panel.FlipMinMax.ValueChanged += f => controller.FlipMinMax = f;
            panel.ForceMinMax.ValueChanged += f =>
            {
                controller.OverrideMinMax = f;
                if (controller.MeasuredIntensityBounds is var (min, max))
                {
                    panel.Min.Value = min;
                    panel.Max.Value = max;
                }

                panel.Min.Interactable = f;
                panel.Max.Interactable = f;
            };
        }

        static string SanitizeTitle(string? topic) => topic.IsNullOrEmpty() ? "[Empty]" : topic;

        public override void UpdatePanel()
        {
            var topics = GetImageTopics();
            panel.Color.Hints = topics;
            panel.Depth.Hints = topics;

            panel.Description.Text = controller.Description;
        }

        public override void UpdatePanelFast()
        {
            colorDialogData?.ResetImageEnabled();
            depthDialogData?.ResetImageEnabled();

            panel.ColorPreview.UpdateMaterial();
            panel.DepthPreview.UpdateMaterial();
        }

        static List<string> GetImageTopics()
        {
            var topics = new List<string> { NoneStr };
            var imageTopics = RosManager.Connection.GetSystemPublishedTopicTypes()
                .Where(topicInfo => topicInfo.Type is Image.MessageType or CompressedImage.MessageType)
                .Select(topicInfo => topicInfo.Topic);
            topics.AddRange(imageTopics);
            return topics;
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonUtils.DeserializeObject<DepthCloudConfiguration>(configAsJson);

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
                    case nameof(DepthCloudConfiguration.Colormap):
                        controller.Colormap = config.Colormap;
                        break;
                    case nameof(DepthCloudConfiguration.OverrideMinMax):
                        controller.OverrideMinMax = config.OverrideMinMax;
                        break;
                    case nameof(DepthCloudConfiguration.MinIntensity):
                        controller.MinIntensity = config.MinIntensity;
                        break;
                    case nameof(DepthCloudConfiguration.MaxIntensity):
                        controller.MaxIntensity = config.MaxIntensity;
                        break;
                    case nameof(DepthCloudConfiguration.FlipMinMax):
                        controller.FlipMinMax = config.FlipMinMax;
                        break;
                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
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

        protected override void UpdateModuleButton()
        {
            ModuleListButtonText =
                ModuleListPanel.CreateButtonTextForListenerModule(this, controller.DepthTopic, ModuleTypeStr);
        }

        sealed class ColorImageDialogListener : ImageDialogListener
        {
            readonly DepthCloudModuleData moduleData;
            public ColorImageDialogListener(DepthCloudModuleData moduleData) => this.moduleData = moduleData;
            public override Material Material => moduleData.controller.ColorMaterial;
            public override Vector2Int ImageSize => moduleData.controller.ColorImageSize;

            public override bool TrySampleColor(in Vector2 rawUV, out Vector2Int uv, out TextureFormat format,
                out Vector4 color) =>
                moduleData.controller.TrySampleColor(rawUV, out uv, out format, out color);

            protected override void Dispose() => moduleData.OnDialogClosed(DialogData);
        }

        sealed class DepthImageDialogListener : ImageDialogListener
        {
            readonly DepthCloudModuleData moduleData;
            public DepthImageDialogListener(DepthCloudModuleData moduleData) => this.moduleData = moduleData;
            public override Material Material => moduleData.controller.DepthMaterial;
            public override Vector2Int ImageSize => moduleData.controller.DepthImageSize;

            public override bool TrySampleColor(in Vector2 rawUV, out Vector2Int uv, out TextureFormat format,
                out Vector4 color) =>
                moduleData.controller.TrySampleDepth(rawUV, out uv, out format, out color);

            protected override void Dispose() => moduleData.OnDialogClosed(DialogData);
        }
    }
}