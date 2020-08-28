using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;

namespace Iviz.App
{
    public sealed class AddModuleDialogData : DialogData
    {
        static readonly List<(string caption, Resource.Module module)> Modules = new List<(string, Resource.Module)>
        {
            ("<b>Robot</b>\nRobot from the parameter server", Resource.Module.Robot),
            ("<b>Grid</b>\nA reference plane", Resource.Module.Grid),
            ("<b>DepthCloud</b>\nPoint cloud generator for depth images", Resource.Module.DepthCloud),
            ("<b>AR</b>\nManager for augmented reality", Resource.Module.AR),
            ("<b>Joystick</b>\nOn-screen joystick", Resource.Module.Joystick),
            // ("<b>Robot (Template)</b>\nRobot from a template", Resource.Module.Robot),
        };

        const int ARIndex = 3;
        const int JoystickIndex = 4;

        ItemListDialogContents itemList;
        public override IDialogPanelContents Panel => itemList;

        public override void Initialize(ModuleListPanel panel)
        {
            base.Initialize(panel);
            itemList = (ItemListDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.ItemList);
        }

        public override void SetupPanel()
        {
            itemList.Title = "Available Modules";
            itemList.Items = Modules.Select(x => x.caption);
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;

            bool hasAR = ModuleListPanel.ModuleDatas.Any(x => x.Module == Resource.Module.AR);
            itemList[ARIndex].Interactable = !hasAR;

            bool hasJoystick = ModuleListPanel.ModuleDatas.Any(x => x.Module == Resource.Module.Joystick);
            itemList[JoystickIndex].Interactable = !hasJoystick;
        }

        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            var moduleData = ModuleListPanel.CreateModule(Modules[index].module);
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
            itemList[ARIndex].Interactable = true;
            itemList[JoystickIndex].Interactable = true;
        }

    }
}