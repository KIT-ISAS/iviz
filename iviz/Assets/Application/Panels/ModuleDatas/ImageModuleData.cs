using Iviz.Controllers;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ImagePanelContents"/> 
    /// </summary>
    public sealed class ImageModuleData : ListenerModuleData, IImageDialogListener
    {
        readonly ImageListener listener;
        readonly ImagePanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Image;
        public override IConfiguration Configuration => listener.Config;

        public ImageListener Image => listener;

        Material IImageDialogListener.Material => listener.Material;

        Vector2 IImageDialogListener.ImageSize => new Vector2(listener.ImageWidth, listener.ImageHeight);

        public ImageModuleData(ModuleDataConstructor constructor) :
            base(constructor.ModuleList,
                constructor.GetConfiguration<ImageConfiguration>()?.Topic ?? constructor.Topic,
                constructor.GetConfiguration<ImageConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Image) as ImagePanelContents;
            listener = new ImageListener(this);
            //listener = Instantiate<ImageListener>();
            //listener.name = "Image";
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
            panel.Listener.RosListener = listener.Listener;
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

        void IImageDialogListener.OnDialogClosed()
        {
            panel.PreviewWidget.Interactable = true;
        }
    }
}
