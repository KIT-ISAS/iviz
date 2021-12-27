#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ImagePanelContents"/> 
    /// </summary>
    public sealed class ImageModuleData : ListenerModuleData
    {
        readonly ImageListener listener;
        readonly ImagePanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override ModuleType ModuleType => ModuleType.Image;
        public override IConfiguration Configuration => listener.Config;

        ImageDialogData? imageDialogData;

        public ImageModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic,
                constructor.TryGetConfigurationType() ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<ImagePanelContents>(ModuleType.Image);
            listener = new ImageListener(this, (ImageConfiguration?)constructor.Configuration, Topic, Type);
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;
            panel.Description.Text = $"<b>{listener.Description}</b>";
            panel.HideButton.State = listener.Visible;

            panel.PreviewWidget.Material = listener.Material;

            panel.ForceMinMax.Value = listener.OverrideMinMax;
            panel.Min.Value = listener.MinIntensity;
            panel.Max.Value = listener.MaxIntensity;
            panel.FlipMinMax.Value = listener.FlipMinMax;
            panel.UseIntrinsicScale.Value = listener.UseIntrinsicScale;

            panel.Colormap.Index = (int)listener.Colormap;

            panel.ForceMinMax.Interactable = listener.IsMono;
            panel.Colormap.Interactable = listener.IsMono;
            panel.Min.Interactable = listener.IsMono && listener.OverrideMinMax;
            panel.Max.Interactable = listener.IsMono && listener.OverrideMinMax;
            panel.FlipMinMax.Interactable = listener.IsMono;

            panel.ShowBillboard.Value = listener.EnableBillboard;
            panel.BillboardSize.Value = listener.BillboardSize;
            panel.BillboardFollowsCamera.Value = listener.BillboardFollowsCamera;
            panel.BillboardOffset.Value = listener.BillboardOffset;

            panel.BillboardSize.Interactable = listener.EnableBillboard;
            panel.BillboardFollowsCamera.Interactable = listener.EnableBillboard;
            panel.BillboardOffset.Interactable = listener.EnableBillboard;
            panel.UseIntrinsicScale.Interactable = listener.EnableBillboard;

            panel.UseIntrinsicScale.ValueChanged += f => listener.UseIntrinsicScale = f;
            panel.Colormap.ValueChanged += (i, _) => listener.Colormap = (ColormapId)i;
            panel.Min.ValueChanged += f => listener.MinIntensity = f;
            panel.Max.ValueChanged += f => listener.MaxIntensity = f;
            panel.FlipMinMax.ValueChanged += f => listener.FlipMinMax = f;
            panel.CloseButton.Clicked += Close;
            panel.ForceMinMax.ValueChanged += f =>
            {
                listener.OverrideMinMax = f;
                panel.Min.Interactable = listener.IsMono && f;
                panel.Max.Interactable = listener.IsMono && f;
            };
            panel.ShowBillboard.ValueChanged += f =>
            {
                panel.BillboardSize.Interactable = f;
                panel.BillboardFollowsCamera.Interactable = f;
                panel.BillboardOffset.Interactable = f;
                listener.EnableBillboard = f;
                panel.UseIntrinsicScale.Interactable = f;
            };
            panel.BillboardSize.ValueChanged += f => listener.BillboardSize = f;
            panel.BillboardFollowsCamera.ValueChanged += f => listener.BillboardFollowsCamera = f;
            panel.BillboardOffset.ValueChanged += f => listener.BillboardOffset = f;
            panel.PreviewWidget.Clicked += () =>
            {
                if (imageDialogData == null)
                {
                    imageDialogData = new ColorImageListener(this).DialogData;
                    imageDialogData.SetupPanel();
                }

                imageDialogData.Title = listener.Topic;
            };
            panel.HideButton.Clicked += ToggleVisible;
        }

        public override void UpdatePanel()
        {
            panel.Colormap.Interactable = listener.IsMono;
            panel.ForceMinMax.Interactable = listener.IsMono;
            panel.Min.Interactable = listener.IsMono && listener.OverrideMinMax;
            panel.Max.Interactable = listener.IsMono && listener.OverrideMinMax;
            panel.FlipMinMax.Interactable = listener.IsMono;
            panel.Description.Text = $"<b>{listener.Description}</b>";
        }
        
        public override void UpdatePanelFast()
        {
            panel.PreviewWidget.UpdateMaterial();
            imageDialogData?.ToggleImageEnabled();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Images.Add(listener.Config);
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<ImageConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(ImageConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(ImageConfiguration.Colormap):
                        listener.Colormap = config.Colormap;
                        break;
                    case nameof(ImageConfiguration.MinIntensity):
                        listener.MinIntensity = config.MinIntensity;
                        break;
                    case nameof(ImageConfiguration.MaxIntensity):
                        listener.MaxIntensity = config.MaxIntensity;
                        break;
                    case nameof(ImageConfiguration.FlipMinMax):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(ImageConfiguration.EnableBillboard):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(ImageConfiguration.BillboardSize):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(ImageConfiguration.BillboardFollowCamera):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(ImageConfiguration.BillboardOffset):
                        listener.Visible = config.Visible;
                        break;
                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }

        void OnDialogClosed()
        {
            imageDialogData = null;
        }

        public override void Dispose()
        {
            base.Dispose();
            imageDialogData?.Stop();
        }

        sealed class ColorImageListener : ImageDialogListener
        {
            readonly ImageModuleData moduleData;
            public ColorImageListener(ImageModuleData moduleData) => this.moduleData = moduleData;
            public override Material Material => moduleData.listener.Material;
            public override Vector2Int ImageSize => moduleData.listener.ImageSize;

            public override bool TrySampleColor(in Vector2 rawUV, out Vector2Int uv, out TextureFormat format,
                out Vector4 color) =>
                moduleData.listener.TrySampleColor(rawUV, out uv, out format, out color);

            protected override void Stop() => moduleData.OnDialogClosed();
        }
    }
}