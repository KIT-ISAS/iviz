using System;
using JetBrains.Annotations;

namespace Iviz.App
{
    public abstract class DialogData
    {
        [NotNull]
        protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;

        protected static DialogPanelManager DialogPanelManager => ModuleListPanel.DialogPanelManager;
        [NotNull] public abstract IDialogPanelContents Panel { get; }

        public abstract void SetupPanel();

        public virtual void CleanupPanel()
        {
        }

        public virtual void UpdatePanel()
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