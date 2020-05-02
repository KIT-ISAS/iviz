namespace Iviz.App
{
    public class DepthImageProjectorPanelContents : DataPanelContents
    {
        public DropdownWidget Depth { get; private set; }
        public DropdownWidget Color { get; private set; }
        public SliderWidget FOV { get; private set; }
        public SliderWidget PointSize { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }

        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("DepthProjector");
            Depth = p.AddDropdown("Depth Image");
            Color = p.AddDropdown("Color Image");
            FOV = p.AddSlider("FOV Angle").SetMinValue(0).SetMaxValue(89);
            PointSize = p.AddSlider("Point Size").SetMinValue(0.1f).SetMaxValue(5f);
            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);

            Widgets = new Widget[] { Depth, Color, FOV, PointSize, CloseButton };
        }
    }
}