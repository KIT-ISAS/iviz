
namespace Iviz.App
{
    public sealed class CameraPanel : ModulePanel
    {
        public SimpleButtonWidget CloseButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public SliderWidget Fov { get; private set; }
        public Vector3MultiWidget RollPitchYaw { get; private set; }
        public Vector3MultiWidget Position { get; private set; }
        public ColorPickerWidget BackgroundColor { get; private set; }
        public ToggleWidget EnableSun { get; private set; }
        public SliderWidget SunDirectionX { get; private set; }
        public SliderWidget SunDirectionY { get; private set; }
        public SliderWidget EquatorIntensity { get; private set; }
        public ToggleWidget EnableShadows { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Camera");
            Frame = p.AddFrame();
            HideButton = p.AddHideButton();
            CloseButton = p.AddCloseButton();
            Fov = p.AddSlider("Field of View").SetMinValue(30).SetMaxValue(170).SetIntegerOnly(true);
            RollPitchYaw = p.AddVector3Multi("Roll, Pitch, Yaw").SetRange(10);
            Position = p.AddVector3Multi("Position From Fixed");

            BackgroundColor = p.AddColorPicker("Background Color");
            EnableSun = p.AddToggle("Enable Sun");
            SunDirectionX = p.AddSlider("Sun Direction X").SetMinValue(-80).SetMaxValue(80).SetIntegerOnly(true);
            SunDirectionY = p.AddSlider("Sun Direction Y").SetMinValue(-180).SetMaxValue(179).SetIntegerOnly(true);
            EquatorIntensity = p.AddSlider("Equator Intensity").SetMinValue(0).SetMaxValue(1);
            EnableShadows = p.AddToggle("Enable Shadows");
            
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}