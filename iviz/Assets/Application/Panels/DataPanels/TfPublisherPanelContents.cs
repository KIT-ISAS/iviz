using UnityEngine;

namespace Iviz.App
{
    public sealed class TfPublisherPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public FrameWidget Parent { get; private set; }
        public SliderWidget Roll { get; private set; }
        public SliderWidget Pitch { get; private set; }
        public SliderWidget Yaw { get; private set; }
        public Vector3SliderWidget Position { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("TF Publisher");
            Frame = p.AddFrame();
            Parent = p.AddFrame();
            HideButton = p.AddHideButton();
            CloseButton = p.AddTrashButton();
            Roll = p.AddSlider("Roll").SetMinValue(-180).SetMaxValue(180).SetIntegerOnly(true);
            Pitch = p.AddSlider("Pitch").SetMinValue(-90).SetMaxValue(90).SetIntegerOnly(true);
            Yaw = p.AddSlider("Yaw").SetMinValue(-90).SetMaxValue(90).SetIntegerOnly(true);
            Position = p.AddVector3Slider("Translation");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}