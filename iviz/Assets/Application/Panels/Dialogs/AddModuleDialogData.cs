#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Core;
using Iviz.Tools;

namespace Iviz.App
{
    public sealed class AddModuleDialogData : DialogData
    {
        static readonly (string Caption, ModuleType Module)[] Modules =
        {
            ("<b>AugmentedReality</b>\nManager for augmented reality", ModuleType.AR),
            ("<b>Robot</b>\nRobot from the parameter server", ModuleType.Robot),
            ("<b>DepthCloud</b>\nPoint cloud generator for depth images", ModuleType.DepthCloud),
            ("<b>Joystick</b>\nOn-screen joystick", ModuleType.Joystick),
            ("<b>Grid</b>\nA reference plane", ModuleType.Grid),
        };

        static readonly ModuleType[] UniqueModules =
        {
            ModuleType.AR,
            ModuleType.Joystick
        };

        readonly ItemListDialogPanel itemList;
        public override IDialogPanel Panel => itemList;

        public AddModuleDialogData()
        {
            itemList = DialogPanelManager.GetPanelByType<ItemListDialogPanel>(DialogPanelType.AddModule);
        }

        public override void SetupPanel()
        {
            itemList.Title = "Add New Module";
            itemList.SetItems(Modules.Select(tuple => tuple.Caption));
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += Close;

            foreach (var module in UniqueModules)
            {
                if (ModuleListPanel.ModuleDatas.Any(moduleData => moduleData.ModuleType == module)
                    && Modules.Zip(itemList).TryGetFirst(tuple => tuple.First.Module == module, out var entry))
                {
                    entry.Second.Interactable = false;
                }
            }
        }

        void OnItemClicked(int index, int _)
        {
            var moduleType = Modules[index].Module;

            ModuleData moduleData;
            try
            {
                moduleData = ModuleListPanel.CreateModule(moduleType);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to create module for type '{moduleType}'", e);
                return;
            }

            Close();

            if (moduleData is not ARModuleData)
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