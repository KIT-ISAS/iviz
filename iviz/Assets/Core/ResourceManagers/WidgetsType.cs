using UnityEngine;

namespace Iviz.Resources
{
    public sealed class WidgetsType
    {
        public Info<GameObject> DisplayButton { get; }
        public Info<GameObject> TopicsButton { get; }
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

        public WidgetsType()
        {
            DisplayButton = new Info<GameObject>("Widgets/Display Button");
            TopicsButton = new Info<GameObject>("Widgets/Topics Button");
            ItemListPanel = new Info<GameObject>("Widgets/Item List Panel");
            ConnectionPanel = new Info<GameObject>("Widgets/Connection Panel");
            ImagePanel = new Info<GameObject>("Widgets/Image Panel");
            TfPanel = new Info<GameObject>("Widgets/TF Tree Panel");
            SaveAsPanel = new Info<GameObject>("Widgets/Save As Panel");
            AddTopicPanel = new Info<GameObject>("Widgets/Add Topic Panel");
            MarkerPanel = new Info<GameObject>("Widgets/Markers Panel");
            NetworkPanel = new Info<GameObject>("Widgets/Network Panel");
            ConsolePanel = new Info<GameObject>("Widgets/Console Panel");

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
        }
    }
}