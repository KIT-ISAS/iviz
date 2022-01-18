using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class PublishedFramePanel : ModulePanel
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public FrameWidget Parent { get; private set; }
        public InputFieldWithHintsWidget ParentId { get; private set; }
        public SliderWidget Roll { get; private set; }
        public SliderWidget Pitch { get; private set; }
        public SliderWidget Yaw { get; private set; }
        public Vector3SliderWidget Position { get; private set; }

        public Vector3Widget InputRpy { get; private set; }
        public Vector3Widget InputPosition { get; private set; }
        
        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Published Frame");
            Frame = p.AddFrame();
            Parent = p.AddFrame();
            ParentId = p.AddInputFieldWithHints("Parent Frame").SetPlaceholder("<empty>");
            HideButton = p.AddHideButton();
            CloseButton = p.AddCloseButton();
            Roll = p.AddSlider("Roll").SetMinValue(-180).SetMaxValue(180).SetIntegerOnly(true);
            Pitch = p.AddSlider("Pitch").SetMinValue(-90).SetMaxValue(90).SetIntegerOnly(true);
            Yaw = p.AddSlider("Yaw").SetMinValue(-180).SetMaxValue(180).SetIntegerOnly(true);
            Position = p.AddVector3Slider("Translation");


            InputRpy = p.AddVector3("Roll, Pitch, Yaw");
            InputPosition = p.AddVector3("Position");
            p.AddCollapsibleWidget("Explicit Pose")
                .Attach(InputRpy)
                .Attach(InputPosition)
                .FinishAttaching();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}