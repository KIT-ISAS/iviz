using System;
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

        [CanBeNull] IImageDialogListener Listener { get; set; }

        public ImageDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<ImageDialogContents>(DialogPanelType.Image);
        }

        public override void SetupPanel()
        {
            if (Listener == null)
            {
                throw new InvalidOperationException("Cannot setup panel without a listener!");
            }
            
            panel.CloseButton.Clicked += OnCloseClicked;
            panel.Material = Listener.Material;
            panel.ImageSize = Listener.ImageSize;
        }
        
        public void Show([NotNull] IImageDialogListener listener)
        {
            Listener = listener ?? throw new ArgumentNullException(nameof(listener));
            Show();
        }

        void OnCloseClicked()
        {
            Listener?.OnDialogClosed();
            Close();
        }
    }
}
