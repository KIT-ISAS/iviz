using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;

namespace Iviz.App
{
    public sealed class AddModuleDialogData : DialogData
    {
        static readonly List<Tuple<string, Resource.Module>> Modules = new List<Tuple<string, Resource.Module>>()
        {
            Tuple.Create("<b>Robot</b>\nRobot from the parameter server", Resource.Module.SimpleRobot),
            Tuple.Create("<b>Robot (Template)</b>\nRobot from a template", Resource.Module.Robot),
            Tuple.Create("<b>Grid</b>\nA reference plane", Resource.Module.Grid),
            Tuple.Create("<b>DepthProjector</b>\nPoint cloud generator for depth images", Resource.Module.DepthImageProjector),
            Tuple.Create("<b>AR</b>\nManager for augmented reality", Resource.Module.AR),
            Tuple.Create("<b>Joystick</b>\nOn-screen joystick", Resource.Module.Joystick),
        };

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
            itemList.Items = Modules.Select(x => x.Item1);
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;

            bool hasAR = ModuleListPanel.ModuleDatas.Any(x => x.Module == Resource.Module.AR);
            itemList[4].Interactable = !hasAR;

            bool hasJoystick = ModuleListPanel.ModuleDatas.Any(x => x.Module == Resource.Module.Joystick);
            itemList[5].Interactable = !hasJoystick;
        }

        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            var moduleData = ModuleListPanel.CreateModule(Modules[index].Item2);
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
            itemList[4].Interactable = true;
            itemList[5].Interactable = true;
        }

    }
}