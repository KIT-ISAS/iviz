using JetBrains.Annotations;

namespace Iviz.App
{
    public abstract class DialogData
    {
        [NotNull]
        protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;

        protected static DialogPanelManager DialogPanelManager => ModuleListPanel.DialogPanelManager;
        [NotNull] public abstract IDialogPanelContents Panel { get; }

        /// <summary>
        /// Called once when the dialog is selected.
        /// </summary>
        public abstract void SetupPanel();

        /// <summary>
        /// Called when the dialog is closed or replaced.
        /// </summary>
        public virtual void CleanupPanel()
        {
        }

        /// <summary>
        /// Called every second.
        /// </summary>
        public virtual void UpdatePanel()
        {
        }

        public virtual void FinalizePanel()
        {
        }

        public void Show()
        {
            DialogPanelManager.TogglePanel(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        protected void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }
    }
}