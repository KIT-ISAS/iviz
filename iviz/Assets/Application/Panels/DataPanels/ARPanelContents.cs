
namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModuleData"/> 
    /// </summary>
    public sealed class ARPanelContents : DataPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public Vector3SliderWidget WorldOffset { get; private set; }
        public SliderWidget WorldAngle { get; private set; }
        public SliderWidget WorldScale { get; private set; }
        public ToggleWidget SearchMarker { get; private set; }
        //public NumberInputFieldWidget MarkerSize { get; private set; }
        public ToggleWidget MarkerHorizontal { get; private set; }
        public SliderWidget MarkerAngle { get; private set; }
        public InputFieldWithHintsWidget MarkerFrame { get; private set; }
        public Vector3SliderWidget MarkerOffset { get; private set; }

        //public ToggleWidget PublishHead { get; private set; }
        //public ToggleWidget PublishPlanes { get; private set; }

        //public SenderWidget HeadSender { get; private set; }
        //public SenderWidget MarkersSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("AR");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Frame = p.AddFrame();
            WorldScale = p.AddSlider("World Scale").SetMinValue(0.01f).SetMaxValue(1f);
            WorldOffset = p.AddVector3Slider("Move World By").SetRange(0.5f);
            WorldAngle = p.AddSlider("Rotate World By").SetMinValue(-180).SetMaxValue(180).SetNumberOfSteps(72);
            SearchMarker = p.AddToggle("Enable Marker Detection");
            MarkerHorizontal = p.AddToggle("Is Marker Horizontal");
            MarkerAngle = p.AddSlider("Marker Angle").SetMinValue(0).SetMaxValue(7*45).SetNumberOfSteps(7);
            MarkerFrame = p.AddInputFieldWithHints("Marker Follows TF Frame").SetPlaceholder("(none)");
            MarkerOffset = p.AddVector3Slider("Offset From TF Frame");
            //MarkerSize = p.AddNumberInputField("Size (m)");
            //PublishHead = p.AddToggle("Publish Camera as PoseStamped");
            //PublishPlanes = p.AddToggle("Publish Planes as Markers");
            //HeadSender = p.AddSender();
            //MarkersSender = p.AddSender();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}