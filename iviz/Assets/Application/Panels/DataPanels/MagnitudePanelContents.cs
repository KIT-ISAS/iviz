
namespace Iviz.App
{
    /// <summary>
    /// <see cref="MagnitudeModuleData"/> 
    /// </summary>
    /// 
    public sealed class MagnitudePanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }
        //public ToggleWidget PreferUdp { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public SliderWidget Scale { get; private set; }
        public ToggleWidget ShowAxis { get; private set; }
        public ToggleWidget ShowTrail { get; private set; }
        public ToggleWidget ShowVector { get; private set; }
        public ToggleWidget ShowAngle { get; private set; }
        public SliderWidget VectorScale { get; private set; }
        //public SliderWidget TrailTime { get; private set; }
        public ColorPickerWidget VectorColor { get; private set; }
        public SliderWidget ScaleMultiplier { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Magnitude");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            //PreferUdp = p.AddToggle("Prefer UDP");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ShowAxis = p.AddToggle("Show Frame");
            ShowAngle = p.AddToggle("Show Angle");
            ShowVector = p.AddToggle("Show Vector");
            ShowTrail = p.AddToggle("Show Trail");

            Scale = p.AddSlider("Scale (All)").SetMinValue(0.1f).SetMaxValue(10.0f).SetNumberOfSteps(99);
            
            VectorColor = p.AddColorPicker("Vector Color");
            VectorScale = p.AddSlider("Additional Scale (Vector)").SetMinValue(0.1f).SetMaxValue(10.0f).SetNumberOfSteps(99);
            ScaleMultiplier = p.AddSlider("Multiply Scale (Vector) by Power of 10").SetMinValue(-4).SetMaxValue(4).SetIntegerOnly(true);
            p.AddCollapsibleWidget("Vector...")
                .Attach(VectorColor)
                .Attach(VectorScale)
                .Attach(ScaleMultiplier)
                .UpdateSize();
            
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}