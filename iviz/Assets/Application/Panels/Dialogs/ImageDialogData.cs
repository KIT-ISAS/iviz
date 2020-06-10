using System;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public class ImageDialogData : DialogData
    {
        ImageDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public ImageListener Listener { get; set; }

        public override void Initialize(DisplayListPanel panel)
        {
            base.Initialize(panel);
            this.panel = (ImageDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.Connection);
        }

        public override void SetupPanel()
        {
            panel.CloseButton.Clicked += Close;
            panel.Material = Listener.Material;
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }
    }
}
