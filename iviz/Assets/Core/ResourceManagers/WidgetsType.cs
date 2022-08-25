#nullable enable

using Iviz.Displays;
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
        public ResourceKey<GameObject> RobotPanel { get; }

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
        public ResourceKey<GameObject> Sender { get; }
        public ResourceKey<GameObject> Listener { get; }
        public ResourceKey<GameObject> Frame { get; }
        public ResourceKey<GameObject> Vector3Slider { get; }
        public ResourceKey<GameObject> Vector3 { get; }
        public ResourceKey<GameObject> Vector3Multi { get; }
        public ResourceKey<GameObject> InputWithHints { get; }
        public ResourceKey<GameObject> MarkerWidget { get; }
        public ResourceKey<GameObject> RobotWidget { get; }
        public ResourceKey<GameObject> CollapsibleWidget { get; }

        public ResourceKey<GameObject> DataPanel { get; }

        public ResourceKey<GameObject> ARMarkerPanel { get; }
        public ResourceKey<GameObject> ARMarkerWidget { get; }

        public ResourceKey<GameObject> TfPublisherWidget { get; }
        public ResourceKey<GameObject> MagnitudeWidget { get; }

        public ResourceKey<GameObject> ImageCanvas { get; }

        public WidgetsType()
        {
            var assetHolder = ResourcePool.WidgetAssetHolder;

            DraggableDisplayButton =
                Create(assetHolder.DraggableDisplayButton, nameof(assetHolder.DraggableDisplayButton));
            ItemButton = Create(assetHolder.ItemButton, nameof(assetHolder.ItemButton));
            ItemButtonWithDelete = Create(assetHolder.ItemButtonWithDelete, nameof(assetHolder.ItemButtonWithDelete));
            ItemListPanel = Create(assetHolder.ItemListPanel, nameof(assetHolder.ItemListPanel));
            ConnectionPanel = Create(assetHolder.ConnectionPanel, nameof(assetHolder.ConnectionPanel));
            TfPanel = Create(assetHolder.TfTreePanel, nameof(assetHolder.TfTreePanel));
            SaveAsPanel = Create(assetHolder.SaveAsPanel, nameof(assetHolder.SaveAsPanel));
            AddTopicPanel = Create(assetHolder.AddTopicPanel, nameof(assetHolder.AddTopicPanel));
            MarkerPanel = Create(assetHolder.MarkersPanel, nameof(assetHolder.MarkersPanel));
            NetworkPanel = Create(assetHolder.NetworkPanel, nameof(assetHolder.NetworkPanel));
            ConsolePanel = Create(assetHolder.ConsoleLog, nameof(assetHolder.ConsoleLog));
            SettingsPanel = Create(assetHolder.SettingsPanel, nameof(assetHolder.SettingsPanel));
            EchoPanel = Create(assetHolder.EchoPanel, nameof(assetHolder.EchoPanel));
            SystemPanel = Create(assetHolder.SystemInfo, nameof(assetHolder.SystemInfo));
            RobotPanel = Create(assetHolder.RobotPanel, nameof(assetHolder.RobotPanel));

            HeadTitle = Create(assetHolder.HeadTitle, nameof(assetHolder.HeadTitle));
            Toggle = Create(assetHolder.Toggle, nameof(assetHolder.Toggle));
            Slider = Create(assetHolder.Slider, nameof(assetHolder.Slider));
            SliderWithScale = Create(assetHolder.SliderWithScale, nameof(assetHolder.SliderWithScale));
            Input = Create(assetHolder.InputField, nameof(assetHolder.InputField));
            NumberInput = Create(assetHolder.NumberInputField, nameof(assetHolder.NumberInputField));
            ColorPicker = Create(assetHolder.ColorPicker, nameof(assetHolder.ColorPicker));
            ImagePreview = Create(assetHolder.ImagePreview, nameof(assetHolder.ImagePreview));
            Dropdown = Create(assetHolder.Dropdown, nameof(assetHolder.Dropdown));
            TrashButton = Create(assetHolder.TrashButton, nameof(assetHolder.TrashButton));
            CloseButton = Create(assetHolder.CloseButton, nameof(assetHolder.CloseButton));
            DataLabel = Create(assetHolder.DataLabel, nameof(assetHolder.DataLabel));
            ToggleButton = Create(assetHolder.ToggleButton, nameof(assetHolder.ToggleButton));
            ResetButton = Create(assetHolder.ResetButton, nameof(assetHolder.ResetButton));
            Sender = Create(assetHolder.Sender, nameof(assetHolder.Sender));
            Listener = Create(assetHolder.Listener, nameof(assetHolder.Listener));
            Frame = Create(assetHolder.Frame, nameof(assetHolder.Frame));
            Vector3 = Create(assetHolder.Vector3, nameof(assetHolder.Vector3));
            Vector3Slider = Create(assetHolder.Vector3Slider, nameof(assetHolder.Vector3Slider));
            Vector3Multi = Create(assetHolder.Vector3Multi, nameof(assetHolder.Vector3Multi));
            InputWithHints = Create(assetHolder.InputFieldWithHints, nameof(assetHolder.InputFieldWithHints));
            MarkerWidget = Create(assetHolder.Markers, nameof(assetHolder.Markers));
            RobotWidget = Create(assetHolder.RobotWidget, nameof(assetHolder.RobotWidget));

            DataPanel = Create(assetHolder.DataPanel, nameof(assetHolder.DataPanel));
            ARMarkerPanel = Create(assetHolder.ARMarkerPanel, nameof(assetHolder.ARMarkerPanel));
            ARMarkerWidget = Create(assetHolder.ARMarkers, nameof(assetHolder.ARMarkers));

            TfPublisherWidget = Create(assetHolder.TfPublisherWidget, nameof(assetHolder.TfPublisherWidget));
            MagnitudeWidget = Create(assetHolder.MagnitudeWidget, nameof(assetHolder.MagnitudeWidget));

            CollapsibleWidget = Create(assetHolder.Collapsible, nameof(assetHolder.Collapsible));
            ImageCanvas = Create(assetHolder.ImageCanvas, nameof(assetHolder.ImageCanvas));

            static ResourceKey<GameObject> Create(GameObject obj, string? msg = null) => new(obj, msg);
        }
    }
}