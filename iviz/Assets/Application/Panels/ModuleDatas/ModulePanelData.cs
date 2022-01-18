#nullable enable

namespace Iviz.App
{
    /// <summary>
    /// Interface for classes that manage a <see cref="ModulePanel"/>.
    /// </summary>
    public interface IManagesPanel
    {
        ModulePanel Panel { get; }
        void SetupPanel();
        void UpdatePanel();
        void UpdatePanelFast();
    }

    /// <summary>
    /// Helper functions for classes that manage a <see cref="ModulePanel"/>.
    /// Usually a <see cref="ModuleData"/>, but some modules may have multiple or custom ModulePanels.
    /// </summary>
    public abstract class ModulePanelData : IManagesPanel
    {
        protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;
        protected static ModulePanelManager ModulePanelManager => ModuleListPanel.ModulePanelManager;

        public abstract ModulePanel Panel { get; }

        public void ToggleShowPanel()
        {
            ModulePanelManager.TogglePanel(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        public void ShowPanel()
        {
            ModulePanelManager.SelectPanelFor(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        public void HidePanel()
        {
            ModulePanelManager.HidePanelFor(this);
        }

        public virtual void SetupPanel()
        {
        }

        public virtual void UpdatePanel()
        {
        }

        public virtual void UpdatePanelFast()
        {
        }
    }
}