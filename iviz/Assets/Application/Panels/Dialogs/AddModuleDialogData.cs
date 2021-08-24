using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class AddModuleDialogData : DialogData
    {
        static readonly List<(string Caption, ModuleType Module)> Modules = new List<(string, ModuleType)>
        {
            ("<b>AugmentedReality</b>\nManager for augmented reality", ModuleType.AugmentedReality),
            ("<b>Robot</b>\nRobot from the parameter server", ModuleType.Robot),
            ("<b>DepthCloud</b>\nPoint cloud generator for depth images", ModuleType.DepthCloud),
            ("<b>Joystick</b>\nOn-screen joystick", ModuleType.Joystick),
            ("<b>Grid</b>\nA reference plane", ModuleType.Grid),
        };

        static readonly ModuleType[] UniqueModules =
        {
            ModuleType.AugmentedReality,
            ModuleType.Joystick
        };

        [NotNull] readonly ItemListDialogContents itemList;
        public override IDialogPanelContents Panel => itemList;

        public AddModuleDialogData()
        {
            itemList = DialogPanelManager.GetPanelByType<ItemListDialogContents>(DialogPanelType.AddModule);
        }

        public override void SetupPanel()
        {
            itemList.Title = "Add New Module";
            itemList.Items = Modules.Select(tuple => tuple.Caption);
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += Close;

            foreach (ModuleType module in UniqueModules)
            {
                bool hasModule = ModuleListPanel.ModuleDatas.Any(moduleData => moduleData.ModuleType == module);
                if (hasModule)
                {
                    var entry = Modules.Zip(itemList).FirstOrDefault(tuple => tuple.First.Module == module).Second;
                    entry.Interactable = false;
                }
            }
        }

        void OnItemClicked(int index, int _)
        {
            var moduleData = ModuleListPanel.CreateModule(Modules[index].Module);
            Close();
            
            if (moduleData is ARModuleData)
            {
                ModuleListPanel.AllGuiVisible = false;
            }
            else
            {
                moduleData.ShowPanel();
            }
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