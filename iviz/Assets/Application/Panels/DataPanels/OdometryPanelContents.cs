
namespace Iviz.App
{
    public class OdometryPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public SliderWidget AxisScale { get; private set; }
        public ToggleWidget TrailEnabled { get; private set; }
        public ColorPickerWidget TrailColor { get; private set; }
        public SliderWidget TrailTime { get; private set; }
        public ToggleWidget UseColorAxis { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Odometry");
            Listener = p.AddListener();
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}