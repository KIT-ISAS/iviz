using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public abstract class DialogData
    {
        static readonly Vector2 DefaultAnchoredPosition = new Vector2(0, -40);
        static readonly Vector2 DefaultSize = new Vector2(0, -90);
        public static readonly Vector2 MinSize = new Vector2(-120, -450);

        [NotNull] protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;

        [NotNull] protected static DialogPanelManager DialogPanelManager => ModuleListPanel.DialogPanelManager;
        [NotNull] public abstract IDialogPanelContents Panel { get; }

        bool detached;

        public bool Detached
        {
            get => detached;
            set
            {
                if (detached == value)
                {
                    return;
                }

                detached = value;
                if (detached)
                {
                    DialogPanelManager.DetachSelectedPanel();
                }

                if (Panel is DetachablePanelContents detachablePanel)
                {
                    detachablePanel.Detached = value;
                }
            }
        }

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
            var gameObject = ((MonoBehaviour)Panel).gameObject;
            var transform = (RectTransform)gameObject.transform;
            transform.anchoredPosition = DefaultAnchoredPosition;
            transform.sizeDelta = DefaultSize;
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