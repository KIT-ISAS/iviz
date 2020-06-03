
namespace Iviz.App
{
    public class OdometryPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public SliderWidget Scale { get; private set; }
        public ToggleWidget TrailEnabled { get; private set; }
        public SliderWidget TrailTime { get; private set; }
        public ColorPickerWidget Color { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Odometry");
            Listener = p.AddListener();
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Scale = p.AddSlider("Axis Scale").SetMinValue(0.1f).SetMaxValue(5.0f);
            TrailEnabled = p.AddToggle("Enable Trail");
            TrailTime = p.AddSlider("Trail Time").SetMinValue(0.5f).SetMaxValue(5.0f);
            Color = p.AddColorPicker("Color");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}