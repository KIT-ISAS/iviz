using System;
using Iviz.App.Listeners;
using UnityEngine;

namespace Iviz.App
{
    public interface IImageDialogListener
    {
        Material Material { get; }
        Vector2 ImageSize { get; }
        void OnDialogClosed();
    }

    public sealed class ImageDialogData : DialogData
    {
        ImageDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public IImageDialogListener Listener { get; set; }

        public override void Initialize(DisplayListPanel panel)
        {
            base.Initialize(panel);
            this.panel = (ImageDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.Image);
        }

        public override void SetupPanel()
        {
            panel.CloseButton.Clicked += Close;
            panel.Material = Listener.Material;
            panel.ImageSize = Listener.ImageSize;
        }

        void Close()
        {
            Listener.OnDialogClosed();
            DialogPanelManager.HidePanelFor(this);
        }
    }
}
