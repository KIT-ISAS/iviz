using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;

namespace Iviz.App
{
    public class AddDisplayDialogData : DialogData
    {
        static readonly List<Tuple<string, Resource.Module>> Modules = new List<Tuple<string, Resource.Module>>()
        {
            Tuple.Create("<b>Robot</b>\nA robot object", Resource.Module.Robot),
            Tuple.Create("<b>Grid</b>\nA reference plane", Resource.Module.Grid),
            Tuple.Create("<b>DepthProjector</b>\nPoint cloud generator for depth images", Resource.Module.DepthImageProjector),
            Tuple.Create("<b>AR</b>\nManager for augmented reality", Resource.Module.AR),
            Tuple.Create("<b>Joystick</b>\nOn-screen joystick", Resource.Module.Joystick),
        };

        DialogItemList itemList;
        public override IDialogPanelContents Panel => itemList;

        public override void Initialize(DisplayListPanel panel)
        {
            base.Initialize(panel);
            itemList = (DialogItemList)DialogPanelManager.GetPanelByType(DialogPanelType.ItemList);
        }

        public override void SetupPanel()
        {
            itemList.Title = "Available Modules";
            itemList.Items = Modules.Select(x => x.Item1);
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;

            bool hasAR = DisplayListPanel.DisplayDatas.Any(x => x.Module == Resource.Module.AR);
            itemList[3].Interactable = !hasAR;
        }

        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            DisplayListPanel.CreateDisplay(Modules[index].Item2);
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}