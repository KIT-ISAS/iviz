﻿using System;
using Iviz.Controllers;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class TfDialogData : DialogData
    {
        [NotNull] readonly TfDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public TfDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<TfDialogContents>(DialogPanelType.Tf);
        }

        public override void SetupPanel()
        {
            panel.Close.Clicked += Close;
            panel.TfLog.Close += Close;
            panel.TfLog.Flush();
            panel.TfLog.UpdateFrameTexts();

            panel.ShowOnlyUsed.Value = !TfListener.Instance.KeepAllFrames;
            panel.ShowOnlyUsed.ValueChanged += f =>
            {
                TfListener.Instance.KeepAllFrames = !f;
                TfListener.Instance.ModuleData.ResetPanel();
            };
        }

        public override void UpdatePanel()
        {
            panel.TfLog.Flush();
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
