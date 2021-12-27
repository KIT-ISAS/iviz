using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class WidgetsType
    {
        public Info<GameObject> DraggableDisplayButton { get; }
        public Info<GameObject> ItemButton { get; }
        public Info<GameObject> ItemButtonWithDelete { get; }
        public Info<GameObject> ItemListPanel { get; }
        public Info<GameObject> ConnectionPanel { get; }
        public Info<GameObject> TfPanel { get; }
        public Info<GameObject> SaveAsPanel { get; }
        public Info<GameObject> AddTopicPanel { get; }
        public Info<GameObject> MarkerPanel { get; }
        public Info<GameObject> NetworkPanel { get; }
        public Info<GameObject> ConsolePanel { get; }
        public Info<GameObject> SettingsPanel { get; }
        public Info<GameObject> EchoPanel { get; }
        public Info<GameObject> SystemPanel { get; }

        public Info<GameObject> HeadTitle { get; }
        public Info<GameObject> Toggle { get; }
        public Info<GameObject> Slider { get; }
        public Info<GameObject> SliderWithScale { get; }
        public Info<GameObject> Input { get; }
        public Info<GameObject> NumberInput { get; }
        public Info<GameObject> Dropdown { get; }
        public Info<GameObject> ColorPicker { get; }
        public Info<GameObject> ImagePreview { get; }
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
        public Info<GameObject> CollapsibleWidget { get; }

        public Info<GameObject> DataPanel { get; }
        
        public Info<GameObject> ARMarkerPanel { get; }
        public Info<GameObject> ARMarkerWidget { get; }
        
        public Info<GameObject> TfPublisherPanel { get; }
        public Info<GameObject> TfPublisherWidget { get; }
        
        public Info<GameObject> ImageCanvas { get; }

        public WidgetsType()
        {
            var assetHolder = Resource.Extras.WidgetAssetHolder;

            DraggableDisplayButton = new Info<GameObject>(assetHolder.DraggableDisplayButton);
            ItemButton = new Info<GameObject>(assetHolder.ItemButton);
            ItemButtonWithDelete = new Info<GameObject>(assetHolder.ItemButtonWithDelete);
            ItemListPanel = new Info<GameObject>(assetHolder.ItemListPanel);
            ConnectionPanel = new Info<GameObject>(assetHolder.ConnectionPanel);
            TfPanel = new Info<GameObject>(assetHolder.TfTreePanel);
            SaveAsPanel = new Info<GameObject>(assetHolder.SaveAsPanel);
            AddTopicPanel = new Info<GameObject>(assetHolder.AddTopicPanel);
            MarkerPanel = new Info<GameObject>(assetHolder.MarkersPanel);
            NetworkPanel = new Info<GameObject>(assetHolder.NetworkPanel);
            ConsolePanel = new Info<GameObject>(assetHolder.ConsoleLog);
            SettingsPanel = new Info<GameObject>(assetHolder.SettingsPanel);
            EchoPanel = new Info<GameObject>(assetHolder.EchoPanel);
            SystemPanel = new Info<GameObject>(assetHolder.SystemInfo);
            
            HeadTitle = new Info<GameObject>(assetHolder.HeadTitle);
            Toggle = new Info<GameObject>(assetHolder.Toggle);
            Slider = new Info<GameObject>(assetHolder.Slider);
            SliderWithScale = new Info<GameObject>(assetHolder.SliderWithScale);
            Input = new Info<GameObject>(assetHolder.InputField);
            NumberInput = new Info<GameObject>(assetHolder.NumberInputField);
            ColorPicker = new Info<GameObject>(assetHolder.ColorPicker);
            ImagePreview = new Info<GameObject>(assetHolder.ImagePreview);
            Dropdown = new Info<GameObject>(assetHolder.Dropdown);
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
            
            DataPanel = new Info<GameObject>(assetHolder.DataPanel, nameof(assetHolder.DataPanel));
            ARMarkerPanel = new Info<GameObject>(assetHolder.ARMarkerPanel, nameof(assetHolder.ARMarkerPanel));
            ARMarkerWidget = new Info<GameObject>(assetHolder.ARMarkers, nameof(assetHolder.ARMarkers));

            TfPublisherPanel = new Info<GameObject>(assetHolder.TfPublisherPanel, nameof(assetHolder.TfPublisherPanel));
            TfPublisherWidget = new Info<GameObject>(assetHolder.TfPublisherWidget, nameof(assetHolder.TfPublisherWidget));

            CollapsibleWidget = new Info<GameObject>(assetHolder.Collapsible, nameof(assetHolder.Collapsible));
            ImageCanvas =  new Info<GameObject>(assetHolder.ImageCanvas, nameof(assetHolder.ImageCanvas));
        }
    }
}