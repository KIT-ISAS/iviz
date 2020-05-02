using UnityEngine;
using System.Collections;
using Iviz.RoslibSharp;

namespace Iviz.App
{
    public abstract class DialogData
    {
        protected DisplayListPanel DisplayListPanel { get; private set; }
        protected DialogPanelManager DialogPanelManager => DisplayListPanel.dialogPanelManager;
        public abstract IDialogPanelContents Panel { get; }

        public virtual void Initialize(DisplayListPanel panel)
        {
            DisplayListPanel = panel;
        }

        public abstract void SetupPanel();
        public virtual void CleanupPanel() { }
        public virtual void UpdatePanel() { }

        public virtual void Start()
        {

        }

        public virtual void Cleanup()
        {
            DialogPanelManager.HidePanelFor(this);
            DisplayListPanel = null;
        }

        public void Select()
        {
            DialogPanelManager.TogglePanel(this);
            DisplayListPanel.AllGuiVisible = true;
        }
    }
}