using System;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public sealed class ImageDialogData : DialogData
    {
        [NotNull] readonly ImageDialogContents panel;
        [NotNull] readonly GameObject canvas;
        [NotNull] readonly ImageDialogListener listener;
        
        public override IDialogPanelContents Panel => panel;
        public event Action Closed; 

        public ImageDialogData([NotNull] ImageDialogListener listener, Transform holder)
        {
            this.listener = listener;
            canvas = ResourcePool.Rent(Resource.Widgets.ImageCanvas, holder);
            panel = canvas.GetComponentInChildren<ImageDialogContents>();
            panel.CloseButton.Clicked += () => Closed?.Invoke();
        }

        public string Title
        {
            set => panel.Title = value;
        }

        public override void SetupPanel()
        {
            panel.Material = listener.Material;
            panel.ImageSize = listener.ImageSize;
        }

        public void ToggleImageEnabled()
        {
            panel.ToggleImageEnabled();
            panel.ImageSize = listener.ImageSize;
        }

        public void Stop()
        {
            Closed = null;
            panel.ClearSubscribers();
            ModuleListPanel.Instance.DisposeImageDialog(this);
            ResourcePool.Return(Resource.Widgets.ImageCanvas, canvas);
        }
    }
}