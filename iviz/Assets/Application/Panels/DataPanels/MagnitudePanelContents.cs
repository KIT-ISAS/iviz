
namespace Iviz.App
{
    /// <summary>
    /// <see cref="MagnitudeModuleData"/> 
    /// </summary>
    /// 
    public sealed class MagnitudePanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public SliderWidget Scale { get; private set; }
        public ToggleWidget ShowAxis { get; private set; }
        public ToggleWidget ShowTrail { get; private set; }
        public ToggleWidget ShowVector { get; private set; }
        public ToggleWidget ShowAngle { get; private set; }
        public SliderWidget VectorScale { get; private set; }
        public SliderWidget TrailTime { get; private set; }
        public ColorPickerWidget Color { get; private set; }
        public SliderWidget ScaleMultiplier { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Magnitude");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ShowAxis = p.AddToggle("Show Frame");
            ShowAngle = p.AddToggle("Show Angle");
            ShowTrail = p.AddToggle("Enable Trail");
            TrailTime = p.AddSlider("Trail Time").SetMinValue(0.5f).SetMaxValue(5.0f).SetNumberOfSteps(45);
            Color = p.AddColorPicker("Color");
            ShowVector = p.AddToggle("Enable Vector");
            Scale = p.AddSlider("Scale").SetMinValue(0.1f).SetMaxValue(10.0f).SetNumberOfSteps(99);
            VectorScale = p.AddSlider("Vector Scale").SetMinValue(0.1f).SetMaxValue(10.0f).SetNumberOfSteps(99);
            ScaleMultiplier = p.AddSlider("Scale by Power of 10").SetMinValue(-4).SetMaxValue(4).SetIntegerOnly(true);
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}