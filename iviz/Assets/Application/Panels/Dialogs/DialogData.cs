using System;
using JetBrains.Annotations;

namespace Iviz.App
{
    public abstract class DialogData
    {
        [NotNull] protected ModuleListPanel ModuleListPanel { get; }
        protected DialogPanelManager DialogPanelManager => ModuleListPanel.DialogPanelManager;
        [NotNull] public abstract IDialogPanelContents Panel { get; }

        protected DialogData([NotNull] ModuleListPanel moduleListPanel)
        {
            ModuleListPanel = moduleListPanel != null ? moduleListPanel : throw new ArgumentNullException(nameof(moduleListPanel));
        }

        public abstract void SetupPanel();
        public virtual void CleanupPanel() { }
        public virtual void UpdatePanel() { }

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