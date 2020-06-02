using Iviz.Resources;

namespace Iviz.App
{
    public class ImagePanelContents : ListenerPanelContents
    {
        public ToggleWidget ShowBillboard { get; private set; }
        public SliderWidget BillboardSize { get; private set; }
        public ImagePreviewWidget PreviewWidget { get; private set; }
        public DataLabelWidget Description { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public SliderWidget Min { get; private set; }
        public SliderWidget Max { get; private set; }
        //public DropdownWidget Anchor { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Image");
            Listener = p.AddListener();
            Colormap = p.AddDropdown("Colormap")
                .SetOptions(Resource.Colormaps.Names)
                .SetIndex((int)Resource.ColormapId.gray);
            Min = p.AddSlider("Intensity Min").SetMinValue(0).SetMaxValue(1);
            Max = p.AddSlider("Intensity Max").SetMinValue(0).SetMaxValue(1);
            /*
            Anchor = p.AddDropdown("Anchor")
                        .SetOptions(AnchorCanvas.AnchorNames)
                        .SetIndex((int)AnchorCanvas.AnchorType.None);
            */
            ShowBillboard = p.AddToggle("Show Billboard");
            BillboardSize = p.AddSlider("Billboard Size").SetMinValue(0.1f).SetMaxValue(10);

            PreviewWidget = p.AddImagePreviewWidget("Preview");
            Description = p.AddDataLabel("");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}