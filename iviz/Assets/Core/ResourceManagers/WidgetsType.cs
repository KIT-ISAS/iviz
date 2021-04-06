using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class WidgetsType
    {
        public Info<GameObject> DisplayButton { get; }
        public Info<GameObject> ItemButton { get; }
        public Info<GameObject> ItemButtonWithDelete { get; }
        public Info<GameObject> ItemListPanel { get; }
        public Info<GameObject> ConnectionPanel { get; }
        public Info<GameObject> ImagePanel { get; }
        public Info<GameObject> TfPanel { get; }
        public Info<GameObject> SaveAsPanel { get; }
        public Info<GameObject> AddTopicPanel { get; }
        public Info<GameObject> MarkerPanel { get; }
        public Info<GameObject> NetworkPanel { get; }
        public Info<GameObject> ConsolePanel { get; }
        public Info<GameObject> MenuPanel { get; }
        public Info<GameObject> SettingsPanel { get; }
        public Info<GameObject> EchoPanel { get; }

        public Info<GameObject> HeadTitle { get; }
        public Info<GameObject> SectionTitle { get; }
        public Info<GameObject> Toggle { get; }
        public Info<GameObject> Slider { get; }
        public Info<GameObject> Input { get; }
        public Info<GameObject> ShortInput { get; }
        public Info<GameObject> NumberInput { get; }
        public Info<GameObject> Dropdown { get; }
        public Info<GameObject> ColorPicker { get; }
        public Info<GameObject> ImagePreview { get; }
        public Info<GameObject> CloseButton { get; }
        public Info<GameObject> TrashButton { get; }
        public Info<GameObject> DataLabel { get; }
        public Info<GameObject> ToggleButton { get; }
        public Info<GameObject> Vector3 { get; }
        public Info<GameObject> Sender { get; }
        public Info<GameObject> Listener { get; }
        public Info<GameObject> Frame { get; }
        public Info<GameObject> Vector3Slider { get; }
        public Info<GameObject> InputWithHints { get; }
        public Info<GameObject> MarkerWidget { get; }

        public Info<GameObject> DataPanel { get; }

        public WidgetsType()
        {
            var assetHolder = UnityEngine.Resources.Load<GameObject>("Widget Asset Holder")
                .GetComponent<WidgetAssetHolder>();

            DisplayButton = new Info<GameObject>(assetHolder.DisplayButton);
            ItemButton = new Info<GameObject>(assetHolder.ItemButton);
            ItemButtonWithDelete = new Info<GameObject>(assetHolder.ItemButtonWithDelete);
            ItemListPanel = new Info<GameObject>(assetHolder.ItemListPanel);
            ConnectionPanel = new Info<GameObject>(assetHolder.ConnectionPanel);
            ImagePanel = new Info<GameObject>(assetHolder.ImagePanel);
            TfPanel = new Info<GameObject>(assetHolder.TfTreePanel);
            SaveAsPanel = new Info<GameObject>(assetHolder.SaveAsPanel);
            AddTopicPanel = new Info<GameObject>(assetHolder.AddTopicPanel);
            MarkerPanel = new Info<GameObject>(assetHolder.MarkersPanel);
            NetworkPanel = new Info<GameObject>(assetHolder.NetworkPanel);
            ConsolePanel = new Info<GameObject>(assetHolder.ConsoleLog);
            SettingsPanel = new Info<GameObject>(assetHolder.SettingsPanel);
            EchoPanel = new Info<GameObject>(assetHolder.EchoPanel);
            
            MenuPanel = new Info<GameObject>(assetHolder.MenuPanel);
            
            HeadTitle = new Info<GameObject>(assetHolder.HeadTitle);
            SectionTitle = new Info<GameObject>(assetHolder.SectionTitle);
            Toggle = new Info<GameObject>(assetHolder.Toggle);
            Slider = new Info<GameObject>(assetHolder.Slider);
            Input = new Info<GameObject>(assetHolder.InputField);
            ShortInput = new Info<GameObject>(assetHolder.ShortInputField);
            NumberInput = new Info<GameObject>(assetHolder.NumberInputField);
            ColorPicker = new Info<GameObject>(assetHolder.ColorPicker);
            ImagePreview = new Info<GameObject>(assetHolder.ImagePreview);
            Dropdown = new Info<GameObject>(assetHolder.Dropdown);
            CloseButton = new Info<GameObject>(assetHolder.CloseButton);
            TrashButton = new Info<GameObject>(assetHolder.TrashButton);
            DataLabel = new Info<GameObject>(assetHolder.DataLabel);
            ToggleButton = new Info<GameObject>(assetHolder.ToggleButton);
            Vector3 = new Info<GameObject>(assetHolder.Vector3);
            Sender = new Info<GameObject>(assetHolder.Sender);
            Listener = new Info<GameObject>(assetHolder.Listener);
            Frame = new Info<GameObject>(assetHolder.Frame);
            Vector3Slider = new Info<GameObject>(assetHolder.Vector3Slider);
            InputWithHints = new Info<GameObject>(assetHolder.InputFieldWithHints);
            MarkerWidget = new Info<GameObject>(assetHolder.Markers);            
            
            DataPanel = new Info<GameObject>(assetHolder.DataPanel);            

            /*
            DisplayButton = new Info<GameObject>("Widgets/Display Button");
            ItemButton = new Info<GameObject>("Widgets/Item Button");
            ItemListPanel = new Info<GameObject>("Widgets/Item List Panel");
            ConnectionPanel = new Info<GameObject>("Widgets/Connection Panel");
            ImagePanel = new Info<GameObject>("Widgets/Image Panel");
            TfPanel = new Info<GameObject>("Widgets/TF Tree Panel");
            SaveAsPanel = new Info<GameObject>("Widgets/Save As Panel");
            AddTopicPanel = new Info<GameObject>("Widgets/Add Topic Panel");
            MarkerPanel = new Info<GameObject>("Widgets/Markers Panel");
            NetworkPanel = new Info<GameObject>("Widgets/Network Panel");
            ConsolePanel = new Info<GameObject>("Widgets/Console Panel");
            SettingsPanel = new Info<GameObject>("Widgets/Settings Panel");
            EchoPanel = new Info<GameObject>("Widgets/Echo Panel");

            MenuPanel = new Info<GameObject>("Widgets/Menu Panel");

            HeadTitle = new Info<GameObject>("Widgets/Head Title");
            SectionTitle = new Info<GameObject>("Widgets/Section Title");
            Toggle = new Info<GameObject>("Widgets/Toggle");
            Slider = new Info<GameObject>("Widgets/Slider");
            Input = new Info<GameObject>("Widgets/Input Field");
            ShortInput = new Info<GameObject>("Widgets/Short Input Field");
            NumberInput = new Info<GameObject>("Widgets/Number Input Field");
            ColorPicker = new Info<GameObject>("Widgets/ColorPicker");
            ImagePreview = new Info<GameObject>("Widgets/Image Preview");
            Dropdown = new Info<GameObject>("Widgets/Dropdown");
            CloseButton = new Info<GameObject>("Widgets/Close Button");
            TrashButton = new Info<GameObject>("Widgets/Trash Button");
            DataLabel = new Info<GameObject>("Widgets/Data Label");
            ToggleButton = new Info<GameObject>("Widgets/Toggle Button");
            Vector3 = new Info<GameObject>("Widgets/Vector3");
            Sender = new Info<GameObject>("Widgets/Sender");
            Listener = new Info<GameObject>("Widgets/Listener");
            Frame = new Info<GameObject>("Widgets/Frame");
            Vector3Slider = new Info<GameObject>("Widgets/Vector3 Slider");
            InputWithHints = new Info<GameObject>("Widgets/Input Field With Hints");
            MarkerWidget = new Info<GameObject>("Widgets/Markers");
            */
        }
    }
}