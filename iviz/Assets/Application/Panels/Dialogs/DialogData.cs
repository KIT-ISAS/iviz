using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public abstract class DialogData
    {
        [NotNull]
        protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;

        [NotNull] protected static DialogPanelManager DialogPanelManager => ModuleListPanel.DialogPanelManager;
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

        public virtual void UpdatePanelFast()
        {
        }

        public virtual void FinalizePanel()
        {
        }

        protected void ResetPanelPosition()
        {
            var gameObject = ((MonoBehaviour) Panel).gameObject;
            var transform = (RectTransform) gameObject.transform;
            transform.anchoredPosition = new Vector2(0, 5);
        }

        public void Show()
        {
            DialogPanelManager.TogglePanel(this);
            ModuleListPanel.AllGuiVisible = true;
        }
        
        [NotNull]
        public override string ToString()
        {
            return $"[{GetType().Name}]";
        }        

        protected void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }
    }
}