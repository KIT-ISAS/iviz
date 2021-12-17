#nullable enable

using System;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public sealed class ImageDialogData : DialogData
    {
        readonly ImageDialogContents panel;
        readonly GameObject canvas;
        readonly ImageDialogListener listener;

        public override IDialogPanelContents Panel => panel;
        public event Action? Closed;

        public ImageDialogData(ImageDialogListener listener, Transform holder)
        {
            this.listener = listener;
            canvas = ResourcePool.Rent(Resource.Widgets.ImageCanvas, holder);
            if (Settings.IsXR)
            {
                canvas.ProcessCanvasForXR();
            }

            panel = canvas.GetComponentInChildren<ImageDialogContents>();
            panel.Closed += () => Closed?.Invoke();
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