using UnityEngine;

namespace Iviz.App
{
    public sealed class CameraPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public SliderWidget Fov { get; private set; }
        public SliderWidget Roll { get; private set; }
        public SliderWidget Pitch { get; private set; }
        public SliderWidget Yaw { get; private set; }
        public Vector3SliderWidget Position { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Camera");
            Frame = p.AddFrame();
            HideButton = p.AddHideButton();
            CloseButton = p.AddCloseButton();
            Fov = p.AddSlider("Field of View").SetMinValue(30).SetMaxValue(170).SetIntegerOnly(true);
            Roll = p.AddSlider("Roll").SetMinValue(-180).SetMaxValue(180).SetIntegerOnly(true);
            Pitch = p.AddSlider("Pitch").SetMinValue(-89).SetMaxValue(89).SetIntegerOnly(true);
            Yaw = p.AddSlider("Yaw").SetMinValue(-90).SetMaxValue(90).SetIntegerOnly(true);
            Position = p.AddVector3Slider("Translation From Fixed");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}