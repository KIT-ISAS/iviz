using UnityEngine;
using System.Collections;
using Iviz.App;
using System;
using System.Net;
using System.Text;
using System.IO;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public class TFDialogData : DialogData
    {
        TFDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public override void Initialize(DisplayListPanel newPanel)
        {
            base.Initialize(newPanel);
            panel = (TFDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.TF);
        }

        public override void SetupPanel()
        {
            panel.Active = true;
            panel.Close.Clicked += Close;
            panel.TFLog.Close += Close;
            panel.TFLog.Flush();
            panel.TFLog.UpdateFrameTexts();

            panel.ShowOnlyUsed.Value = !TFListener.Instance.ShowAllFrames;
            panel.ShowOnlyUsed.ValueChanged += f =>
            {
                TFListener.Instance.ShowAllFrames = !f;
                TFListener.Instance.ModuleData.ForceUpdatePanel();
            };
            panel.ResetAll.Clicked += () =>
            {
                TFListener.Instance.ForceClearFrames();
            };
        }

        public override void UpdatePanel()
        {
            //Debug.Log("update panel");
            base.UpdatePanel();
            panel.TFLog.Flush();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }
        
        public void Show(TFFrame frame)
        {
            Show();
            panel.TFLog.SelectedFrame = frame;
        }        
    }
}
