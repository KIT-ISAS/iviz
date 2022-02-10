
namespace Iviz.App
{
    /// <summary>
    /// <see cref="MagnitudeModuleData"/> 
    /// </summary>
    /// 
    public sealed class MagnitudeModulePanel : ListenerModulePanel
    {
        public FrameWidget Frame { get; private set; }
        //public ToggleWidget PreferUdp { get; private set; }
        public MagnitudeWidget Magnitude { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public SliderWidgetWithScale Scale { get; private set; }
        public ToggleWidget ShowAxis { get; private set; }
        public ToggleWidget ShowTrail { get; private set; }
        public ToggleWidget ShowVector { get; private set; }
        public ToggleWidget ShowAngle { get; private set; }
        public SliderWidgetWithScale VectorScale { get; private set; }
        public ColorPickerWidget VectorColor { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Magnitude");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            Magnitude = p.AddMagnitudeWidget();
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ShowAxis = p.AddToggle("Show Frame");
            ShowAngle = p.AddToggle("Show Angle");
            ShowVector = p.AddToggle("Show Vector");
            ShowTrail = p.AddToggle("Show Trail");

            Scale = p.AddSliderWidgetWithScale("Scale (All)");
            
            VectorColor = p.AddColorPicker("Vector Color");
            VectorScale = p.AddSliderWidgetWithScale("Additional Scale (Vector)");
            p.AddCollapsibleWidget("Vector...")
                .Attach(VectorColor)
                .Attach(VectorScale)
                .FinishAttaching();
            
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}