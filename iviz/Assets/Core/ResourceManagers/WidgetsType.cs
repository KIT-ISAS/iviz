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
        public Info<GameObject> SystemPanel { get; }

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
        public Info<GameObject> ResetButton { get; }
        public Info<GameObject> Vector3 { get; }
        public Info<GameObject> Sender { get; }
        public Info<GameObject> Listener { get; }
        public Info<GameObject> Frame { get; }
        public Info<GameObject> Vector3Slider { get; }
        public Info<GameObject> InputWithHints { get; }
        public Info<GameObject> MarkerWidget { get; }
        public Info<GameObject> Mover { get; }

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
            SystemPanel = new Info<GameObject>(assetHolder.SystemInfo);
            
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
            ResetButton = new Info<GameObject>(assetHolder.ResetButton);
            Vector3 = new Info<GameObject>(assetHolder.Vector3);
            Sender = new Info<GameObject>(assetHolder.Sender);
            Listener = new Info<GameObject>(assetHolder.Listener);
            Frame = new Info<GameObject>(assetHolder.Frame);
            Vector3Slider = new Info<GameObject>(assetHolder.Vector3Slider);
            InputWithHints = new Info<GameObject>(assetHolder.InputFieldWithHints);
            MarkerWidget = new Info<GameObject>(assetHolder.Markers);            
            
            DataPanel = new Info<GameObject>(assetHolder.DataPanel);
        }
    }
}