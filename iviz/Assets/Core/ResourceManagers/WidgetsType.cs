#nullable enable

using UnityEngine;

namespace Iviz.Resources
{
    public sealed class WidgetsType
    {
        public ResourceKey<GameObject> DraggableDisplayButton { get; }
        public ResourceKey<GameObject> ItemButton { get; }
        public ResourceKey<GameObject> ItemButtonWithDelete { get; }
        public ResourceKey<GameObject> ItemListPanel { get; }
        public ResourceKey<GameObject> ConnectionPanel { get; }
        public ResourceKey<GameObject> TfPanel { get; }
        public ResourceKey<GameObject> SaveAsPanel { get; }
        public ResourceKey<GameObject> AddTopicPanel { get; }
        public ResourceKey<GameObject> MarkerPanel { get; }
        public ResourceKey<GameObject> NetworkPanel { get; }
        public ResourceKey<GameObject> ConsolePanel { get; }
        public ResourceKey<GameObject> SettingsPanel { get; }
        public ResourceKey<GameObject> EchoPanel { get; }
        public ResourceKey<GameObject> SystemPanel { get; }

        public ResourceKey<GameObject> HeadTitle { get; }
        public ResourceKey<GameObject> Toggle { get; }
        public ResourceKey<GameObject> Slider { get; }
        public ResourceKey<GameObject> SliderWithScale { get; }
        public ResourceKey<GameObject> Input { get; }
        public ResourceKey<GameObject> NumberInput { get; }
        public ResourceKey<GameObject> Dropdown { get; }
        public ResourceKey<GameObject> ColorPicker { get; }
        public ResourceKey<GameObject> ImagePreview { get; }
        public ResourceKey<GameObject> TrashButton { get; }
        public ResourceKey<GameObject> CloseButton { get; }
        public ResourceKey<GameObject> DataLabel { get; }
        public ResourceKey<GameObject> ToggleButton { get; }
        public ResourceKey<GameObject> ResetButton { get; }
        public ResourceKey<GameObject> Vector3 { get; }
        public ResourceKey<GameObject> Sender { get; }
        public ResourceKey<GameObject> Listener { get; }
        public ResourceKey<GameObject> Frame { get; }
        public ResourceKey<GameObject> Vector3Slider { get; }
        public ResourceKey<GameObject> InputWithHints { get; }
        public ResourceKey<GameObject> MarkerWidget { get; }
        public ResourceKey<GameObject> CollapsibleWidget { get; }

        public ResourceKey<GameObject> DataPanel { get; }
        
        public ResourceKey<GameObject> ARMarkerPanel { get; }
        public ResourceKey<GameObject> ARMarkerWidget { get; }
        
        public ResourceKey<GameObject> TfPublisherWidget { get; }
        public ResourceKey<GameObject> MagnitudeWidget { get; }
        
        public ResourceKey<GameObject> ImageCanvas { get; }

        public WidgetsType()
        {
            var assetHolder = Resource.Extras.WidgetAssetHolder;

            DraggableDisplayButton = Create(assetHolder.DraggableDisplayButton);
            ItemButton = Create(assetHolder.ItemButton);
            ItemButtonWithDelete = Create(assetHolder.ItemButtonWithDelete);
            ItemListPanel = Create(assetHolder.ItemListPanel);
            ConnectionPanel = Create(assetHolder.ConnectionPanel);
            TfPanel = Create(assetHolder.TfTreePanel);
            SaveAsPanel = Create(assetHolder.SaveAsPanel);
            AddTopicPanel = Create(assetHolder.AddTopicPanel);
            MarkerPanel = Create(assetHolder.MarkersPanel);
            NetworkPanel = Create(assetHolder.NetworkPanel);
            ConsolePanel = Create(assetHolder.ConsoleLog);
            SettingsPanel = Create(assetHolder.SettingsPanel);
            EchoPanel = Create(assetHolder.EchoPanel);
            SystemPanel = Create(assetHolder.SystemInfo);
            
            HeadTitle = Create(assetHolder.HeadTitle);
            Toggle = Create(assetHolder.Toggle);
            Slider = Create(assetHolder.Slider);
            SliderWithScale = Create(assetHolder.SliderWithScale);
            Input = Create(assetHolder.InputField);
            NumberInput = Create(assetHolder.NumberInputField);
            ColorPicker = Create(assetHolder.ColorPicker);
            ImagePreview = Create(assetHolder.ImagePreview);
            Dropdown = Create(assetHolder.Dropdown);
            TrashButton = Create(assetHolder.TrashButton);
            CloseButton = Create(assetHolder.CloseButton);
            DataLabel = Create(assetHolder.DataLabel);
            ToggleButton = Create(assetHolder.ToggleButton);
            ResetButton = Create(assetHolder.ResetButton);
            Vector3 = Create(assetHolder.Vector3);
            Sender = Create(assetHolder.Sender);
            Listener = Create(assetHolder.Listener);
            Frame = Create(assetHolder.Frame);
            Vector3Slider = Create(assetHolder.Vector3Slider);
            InputWithHints = Create(assetHolder.InputFieldWithHints);
            MarkerWidget = Create(assetHolder.Markers);            
            
            DataPanel = Create(assetHolder.DataPanel, nameof(assetHolder.DataPanel));
            ARMarkerPanel = Create(assetHolder.ARMarkerPanel, nameof(assetHolder.ARMarkerPanel));
            ARMarkerWidget = Create(assetHolder.ARMarkers, nameof(assetHolder.ARMarkers));

            TfPublisherWidget = Create(assetHolder.TfPublisherWidget, nameof(assetHolder.TfPublisherWidget));
            MagnitudeWidget = Create(assetHolder.MagnitudeWidget, nameof(assetHolder.MagnitudeWidget));

            CollapsibleWidget = Create(assetHolder.Collapsible, nameof(assetHolder.Collapsible));
            ImageCanvas =  Create(assetHolder.ImageCanvas, nameof(assetHolder.ImageCanvas));
            
            static ResourceKey<GameObject> Create(GameObject obj, string? msg = null) => new(obj, msg);
        }
    }
}