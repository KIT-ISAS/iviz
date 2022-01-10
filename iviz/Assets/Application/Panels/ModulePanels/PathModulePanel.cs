namespace Iviz.App
{
    /// <summary>
    /// <see cref="PathModuleData"/> 
    /// </summary>

    public sealed class PathModulePanel : ListenerModulePanel
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public SliderWidget LineWidth { get; private set; }
        public ToggleWidget ShowAxes { get; private set; }
        public SliderWidget AxesLength { get; private set; }
        public ToggleWidget ShowLines { get; private set; }
        public ColorPickerWidget LineColor { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Path");
            Listener = p.AddListener();
            Frame = p.AddFrame();

            ShowAxes = p.AddToggle("Show Frames");
            ShowLines = p.AddToggle("Connect Frames");

            AxesLength = p.AddSlider("Frame Size").SetMinValue(0.01f).SetMaxValue(0.5f).SetNumberOfSteps(49);
            LineWidth = p.AddSlider("Line Width").SetMinValue(0.005f).SetMaxValue(0.1f).SetNumberOfSteps(94);
            LineColor = p.AddColorPicker("Line Color");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}