using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using Iviz.Roslib;
using Iviz.XmlRpc;
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

            foreach (Resource.ModuleType module in UniqueModules)
            {
                bool hasModule = ModuleListPanel.ModuleDatas.Any(moduleData => moduleData.ModuleType == module);
                if (hasModule)
                {
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