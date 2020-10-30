using JetBrains.Annotations;
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
        [NotNull] readonly ImageDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public IImageDialogListener Listener { get; set; }

        public ImageDialogData([NotNull] ModuleListPanel panel) : base(panel)
        {
            this.panel = DialogPanelManager.GetPanelByType<ImageDialogContents>(DialogPanelType.Image);
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
