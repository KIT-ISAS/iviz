using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;

namespace Iviz.App
{
    public class AddDisplayDialogData : DialogData
    {
        static readonly List<Tuple<string, Resource.Module>> Displays = new List<Tuple<string, Resource.Module>>()
        {
            Tuple.Create("<b>Robot</b>\nA robot object", Resource.Module.Robot),
            Tuple.Create("<b>Grid</b>\nA reference plane", Resource.Module.Grid),
            Tuple.Create("<b>DepthProjector</b>\nPoint cloud generator for depth images", Resource.Module.DepthImageProjector),
            Tuple.Create("<b>AR</b>\nManager for augmented reality", Resource.Module.AR),
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
            itemList.Title = "Available Displays";
            itemList.Items = Displays.Select(x => x.Item1);
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;
        }

        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            DisplayListPanel.CreateDisplay(Displays[index].Item2);
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}