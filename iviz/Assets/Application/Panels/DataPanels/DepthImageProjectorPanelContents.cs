namespace Iviz.App
{
    public sealed class DepthImageProjectorPanelContents : DataPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public DropdownWidget Depth { get; private set; }
        public DropdownWidget Color { get; private set; }
        public SliderWidget FOV { get; private set; }
        public SliderWidget PointSize { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("DepthProjector");
            Frame = p.AddFrame();
            Depth = p.AddDropdown("Depth Image");
            Color = p.AddDropdown("Color Image");
            FOV = p.AddSlider("FOV Angle").SetMinValue(0).SetMaxValue(89).SetNumberOfSteps(89);
            PointSize = p.AddSlider("Point Size").SetMinValue(0.1f).SetMaxValue(5f).SetNumberOfSteps(49);
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}