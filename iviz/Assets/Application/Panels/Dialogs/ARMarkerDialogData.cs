using System;
using System.Text;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class ARMarkerDialogData : DialogData
    {
        [NotNull] readonly ARMarkerDialogContents panel;
        readonly StringBuilder description = new StringBuilder(65536);
        public override IDialogPanelContents Panel => panel;

        public ARMarkerDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<ARMarkerDialogContents>(DialogPanelType.ARMarkers);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            panel.Close.Clicked += Close;
            UpdatePanel();
        }

        public override void UpdatePanel()
        {
        }
    }
}
