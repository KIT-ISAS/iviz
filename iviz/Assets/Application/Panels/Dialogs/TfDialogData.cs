using System;
using Iviz.Controllers;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class TfDialogData : DialogData
    {
        [NotNull] readonly TFDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public TfDialogData([NotNull] ModuleListPanel newPanel) : base(newPanel)
        {
            panel = DialogPanelManager.GetPanelByType<TFDialogContents>(DialogPanelType.Tf);
        }

        public override void SetupPanel()
        {
            panel.Close.Clicked += Close;
            panel.TfLog.Close += Close;
            panel.TfLog.Flush();
            panel.TfLog.UpdateFrameTexts();

            panel.ShowOnlyUsed.Value = TfListener.Instance.KeepOnlyUsedFrames;
            panel.ShowOnlyUsed.ValueChanged += f =>
            {
                TfListener.Instance.KeepOnlyUsedFrames = f;
                TfListener.Instance.ModuleData.ResetPanel();
            };
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.TfLog.Flush();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }
        
        public void Show([NotNull] TfFrame frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            Show();
            panel.TfLog.SelectedFrame = frame;
        }        
    }
}
