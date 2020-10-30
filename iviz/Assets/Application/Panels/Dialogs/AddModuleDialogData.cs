using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class AddModuleDialogData : DialogData
    {
        static readonly List<(string Caption, Resource.Module Module)> Modules = new List<(string, Resource.Module)>
        {
            ("<b>AugmentedReality</b>\nManager for augmented reality", Resource.Module.AugmentedReality),
            ("<b>Robot</b>\nRobot from the parameter server", Resource.Module.Robot),
            ("<b>DepthCloud</b>\nPoint cloud generator for depth images", Resource.Module.DepthCloud),
            ("<b>Joystick</b>\nOn-screen joystick", Resource.Module.Joystick),
            ("<b>Grid</b>\nA reference plane", Resource.Module.Grid),
        };

        static readonly Resource.Module[] UniqueModules =
        {
            Resource.Module.AugmentedReality,
            Resource.Module.Joystick
        };

        [NotNull] readonly ItemListDialogContents itemList;
        public override IDialogPanelContents Panel => itemList;

        public AddModuleDialogData([NotNull] ModuleListPanel panel) : base(panel)
        {
            itemList = DialogPanelManager.GetPanelByType<ItemListDialogContents>(DialogPanelType.ItemList);
        }

        public override void SetupPanel()
        {
            itemList.Title = "Available Modules";
            itemList.Items = Modules.Select(entry => entry.Caption);
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;

            foreach (Resource.Module module in UniqueModules)
            {
                bool hasModule = ModuleListPanel.ModuleDatas.Any(moduleData => moduleData.Module == module);
                if (hasModule)
                {
                    int moduleEntry = Modules.FindIndex(entry => entry.Module == module);
                    itemList[moduleEntry].Interactable = false;
                }
            }
        }

        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            var moduleData = ModuleListPanel.CreateModule(Modules[index].Module);
            Close();
            moduleData.ShowPanel();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

        public override void CleanupPanel()
        {
            base.CleanupPanel();

            foreach (var entry in itemList.ItemEntries)
            {
                entry.Interactable = true;
            }
        }

    }
}