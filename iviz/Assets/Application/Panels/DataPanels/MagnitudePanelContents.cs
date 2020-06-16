
namespace Iviz.App
{
    /// <summary>
    /// <see cref="MagnitudeModuleData"/> 
    /// </summary>
    /// 
    public class MagnitudePanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public SliderWidget Scale { get; private set; }
        public ToggleWidget ShowAxis { get; private set; }
        public ToggleWidget ShowTrail { get; private set; }
        public ToggleWidget ShowVector { get; private set; }
        public SliderWidget VectorScale { get; private set; }
        public SliderWidget TrailTime { get; private set; }
        public ColorPickerWidget Color { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Magnitude");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Scale = p.AddSlider("Axis Scale").SetMinValue(0.1f).SetMaxValue(10.0f);
            ShowAxis = p.AddToggle("Show Frame");
            ShowTrail = p.AddToggle("Enable Trail");
            TrailTime = p.AddSlider("Trail Time").SetMinValue(0.5f).SetMaxValue(5.0f);
            Color = p.AddColorPicker("Color");
            ShowVector = p.AddToggle("Enable Vector");
            VectorScale = p.AddSlider("Vector Scale").SetMinValue(0.1f).SetMaxValue(10.0f);
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}