namespace Iviz.App
{
    public abstract class DialogData
    {
        protected DisplayListPanel ModuleListPanel { get; private set; }
        protected DialogPanelManager DialogPanelManager => ModuleListPanel.DialogPanelManager;
        public abstract IDialogPanelContents Panel { get; }

        public virtual void Initialize(DisplayListPanel panel)
        {
            ModuleListPanel = panel;
        }

        public abstract void SetupPanel();
        public virtual void CleanupPanel() { }
        public virtual void UpdatePanel() { }

        public virtual void Cleanup()
        {
            DialogPanelManager.HidePanelFor(this);
            ModuleListPanel = null;
        }

        public void Show()
        {
            DialogPanelManager.TogglePanel(this);
            ModuleListPanel.AllGuiVisible = true;
        }
    }
}