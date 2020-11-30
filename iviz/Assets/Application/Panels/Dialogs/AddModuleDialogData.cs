using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class AddModuleDialogData : DialogData
    {
        static readonly List<(string Caption, Resource.ModuleType Module)> Modules = new List<(string, Resource.ModuleType)>
        {
            ("<b>AugmentedReality</b>\nManager for augmented reality", Resource.ModuleType.AugmentedReality),
            ("<b>Robot</b>\nRobot from the parameter server", Resource.ModuleType.Robot),
            ("<b>DepthCloud</b>\nPoint cloud generator for depth images", Resource.ModuleType.DepthCloud),
            ("<b>Joystick</b>\nOn-screen joystick", Resource.ModuleType.Joystick),
            ("<b>Grid</b>\nA reference plane", Resource.ModuleType.Grid),
        };

        static readonly Resource.ModuleType[] UniqueModules =
        {
            Resource.ModuleType.AugmentedReality,
            Resource.ModuleType.Joystick
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
            itemList.Items = Modules.Select(tuple => tuple.Caption);
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += Close;

            foreach (Resource.ModuleType module in UniqueModules)
            {
                bool hasModule = ModuleListPanel.ModuleDatas.Any(moduleData => moduleData.ModuleType == module);
                if (hasModule)
                {
                    //int moduleEntry = Modules.FindIndex(entry => entry.Module == module);
                    //itemList[moduleEntry].Interactable = false;
                    var entry = Modules.Zip(itemList).FirstOrDefault(tuple => tuple.First.Module == module).Second;
                    entry.Interactable = false;
                }
            }
        }

        void OnItemClicked(int index, string _)
        {
            var moduleData = ModuleListPanel.CreateModule(Modules[index].Module);
            Close();
            moduleData.ShowPanel();
        }

        public override void CleanupPanel()
        {
            foreach (var entry in itemList)
            {
                entry.Interactable = true;
            }
        }

    }
}