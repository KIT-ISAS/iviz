#nullable enable

using System;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;

namespace Iviz.App
{
    public sealed class TfDialogData : DialogData
    {
        readonly TfDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public TfDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<TfDialogContents>(DialogPanelType.Tf);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            panel.FrameName.Value = "";
            panel.Close.Clicked += Close;
            panel.TfLog.Close += Close;
            panel.TfLog.Flush();
            panel.TfLog.UpdateFrameButtons();

            panel.CreateFrameClicked += () =>
            {
                string frameName = panel.FrameName.Value;
                if (frameName is "")
                {
                    RosLogger.Error($"{this}: Cannot create frame with empty name.");
                    return;
                }

                var tfPublisher = ModuleListPanel.TfPublisher;
                if (tfPublisher.IsPublishing(frameName))
                {
                    RosLogger.Info($"{this}: A frame with name '{frameName}' already exists.");
                    return;
                }
                
                var tfFrame = tfPublisher.Add(frameName);
                panel.TfLog.SelectedFrame = tfFrame;
                panel.TfLog.Flush();
            };

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

        public override void UpdatePanelFast()
        {
            panel.TfLog.UpdateFrameText();
        }

        public void Show(TfFrame frame)
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
