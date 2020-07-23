using Iviz.Controllers;

namespace Iviz.App
{
    public sealed class TFDialogData : DialogData
    {
        TFDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public override void Initialize(ModuleListPanel newPanel)
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
                TFListener.Instance.ModuleData.ResetPanel();
            };
            panel.ResetAll.Clicked += () =>
            {
                ModuleListPanel.ResetAllModules();
            };
        }

        public override void UpdatePanel()
        {
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
