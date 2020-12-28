using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ImagePanelContents"/> 
    /// </summary>
    public sealed class ImageModuleData : ListenerModuleData, IImageDialogListener
    {
        [NotNull] readonly ImageListener listener;
        [NotNull] readonly ImagePanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.Image;
        public override IConfiguration Configuration => listener.Config;

        [NotNull] public ImageListener Image => listener;

        Material IImageDialogListener.Material => listener.Material;

        Vector2 IImageDialogListener.ImageSize => new Vector2(listener.ImageWidth, listener.ImageHeight);

        public ImageModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.GetConfiguration<ImageConfiguration>()?.Topic ?? constructor.Topic,
                constructor.GetConfiguration<ImageConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<ImagePanelContents>(Resource.ModuleType.Image);
            listener = new ImageListener(this);
            if (constructor.Configuration != null)
            {
                listener.Config = (ImageConfiguration)constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
                listener.Config.Type = Type;
            }
            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;
            panel.Description.Label = listener.Description;
            panel.HideButton.State = listener.Visible;

            panel.PreviewWidget.Material = listener.Material;

            panel.Min.Value = listener.MinIntensity;
            panel.Max.Value = listener.MaxIntensity;
            panel.FlipMinMax.Value = listener.FlipMinMax;

            panel.Colormap.Index = (int)listener.Colormap;

            panel.Colormap.Interactable = listener.IsMono;
            panel.Min.Interactable = listener.IsMono;
            panel.Max.Interactable = listener.IsMono;
            panel.FlipMinMax.Interactable = listener.IsMono;

            panel.ShowBillboard.Value = listener.EnableBillboard;
            panel.BillboardSize.Value = listener.BillboardSize;
            panel.BillboardFollowsCamera.Value = listener.BillboardFollowsCamera;
            panel.BillboardOffset.Value = listener.BillboardOffset;

            panel.BillboardSize.Interactable = listener.EnableBillboard;
            panel.BillboardFollowsCamera.Interactable = listener.EnableBillboard;
            panel.BillboardOffset.Interactable = listener.EnableBillboard;

            panel.Colormap.ValueChanged += (i, _) =>
            {
                listener.Colormap = (Resource.ColormapId)i;
            };
            panel.Min.ValueChanged += f =>
            {
                listener.MinIntensity = f;
            };
            panel.Max.ValueChanged += f =>
            {
                listener.MaxIntensity = f;
            };
            panel.FlipMinMax.ValueChanged += f =>
            {
                listener.FlipMinMax = f;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.ShowBillboard.ValueChanged += f =>
            {
                panel.BillboardSize.Interactable = f;
                panel.BillboardFollowsCamera.Interactable = f;
                panel.BillboardOffset.Interactable = f;
                listener.EnableBillboard = f;
            };
            panel.BillboardSize.ValueChanged += f =>
            {
                listener.BillboardSize = f;
            };
            panel.BillboardFollowsCamera.ValueChanged += f =>
            {
                listener.BillboardFollowsCamera = f;
            };
            panel.BillboardOffset.ValueChanged += f =>
            {
                listener.BillboardOffset = f;
            };
            panel.PreviewWidget.Clicked += () =>
            {
                ModuleListPanel.ShowImageDialog(this);
                panel.PreviewWidget.Interactable = false;
            };
            panel.HideButton.Clicked += () =>
            {
                listener.Visible = !listener.Visible;
                panel.HideButton.State = listener.Visible;
                UpdateModuleButton();
            };
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.Colormap.Interactable = listener.IsMono;
            panel.Min.Interactable = listener.IsMono;
            panel.Max.Interactable = listener.IsMono;
            panel.FlipMinMax.Interactable = listener.IsMono;
            panel.Description.Label = listener.Description;
            panel.PreviewWidget.ToggleImageEnabled();
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
                        Logger.Error($"{this}: Unknown field '{field}'");
                        break;                    
                }
            }
            
            ResetPanel();
        }              

        void IImageDialogListener.OnDialogClosed()
        {
            panel.PreviewWidget.Interactable = true;
        }
    }
}
