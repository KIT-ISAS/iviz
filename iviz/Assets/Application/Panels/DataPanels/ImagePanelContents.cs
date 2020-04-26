using System.Collections.Generic;

namespace Iviz.App
{
    public class ImagePanelContents : ListenerPanelContents
    {
        public ImagePreviewWidget PreviewWidget { get; private set; }
        public DataLabelWidget Topic { get; private set; }
        public DataLabelWidget Description { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public SliderWidget Min { get; private set; }
        public SliderWidget Max { get; private set; }
        public DropdownWidget Anchor { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }

        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Image");
            Stats = p.AddSectionTitleWidget("Off | 0 Hz | 0 - 0 ms");
            Topic = p.AddDataLabel("");
            Colormap = p.AddDropdown("Colormap")
                .SetOptions(Resource.Colormaps.Names)
                .SetIndex((int)Resource.Colormaps.Id.gray);
            Min = p.AddSlider("Min Value").SetMinValue(0).SetMaxValue(1);
            Max = p.AddSlider("Max Value").SetMinValue(0).SetMaxValue(1);
            Anchor = p.AddDropdown("Anchor")
                        .SetOptions(AnchorCanvas.AnchorNames)
                        .SetIndex((int)AnchorCanvas.AnchorType.None);
            PreviewWidget = p.AddImagePreviewWidget("Preview");
            Description = p.AddDataLabel("");
            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);

            Topic.label.alignment = UnityEngine.TextAnchor.UpperLeft;

            Widgets = new Widget[] { PreviewWidget, Colormap, Anchor, Min, Max, CloseButton };

        }
    }
}