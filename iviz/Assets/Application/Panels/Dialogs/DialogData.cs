#nullable enable

using UnityEngine;

namespace Iviz.App
{
    public abstract class DialogData
    {
        static Vector2 DefaultAnchoredPosition => new(0, -40);
        static Vector2 DefaultSize => new(0, -90);
        public static Vector2 MinSize => new(-120, -450);

        protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;
        protected static DialogPanelManager DialogPanelManager => ModuleListPanel.DialogPanelManager;
        public bool IsPanelActive => DialogPanelManager.IsActive(this);

        bool detached;
        
        public bool Detached
        {
            get => detached;
            set
            {
                if (Panel is DetachableDialogPanel detachablePanel)
                {
                    detached = value;
                    detachablePanel.Detached = value;
                }
            }
        }
        
        public abstract IDialogPanel Panel { get; }

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

        public virtual void Dispose()
        {
        }
        
        public void ResetPanel()
        {
            if (!IsPanelActive)
            {
                return;
            }

            Panel.ClearSubscribers();
            SetupPanel();
        }

        protected void ResetPanelPosition()
        {
            var gameObject = ((MonoBehaviour) Panel).gameObject;
            var transform = (RectTransform) gameObject.transform;
            transform.anchoredPosition = DefaultAnchoredPosition;
            transform.sizeDelta = DefaultSize;
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

        public override string ToString() => $"[{GetType().Name}]";
    }
}