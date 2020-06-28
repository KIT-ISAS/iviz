
namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModuleData"/> 
    /// </summary>
    public class ARPanelContents : DataPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public Vector3SliderWidget Origin { get; private set; }
        public SliderWidget WorldScale { get; private set; }
        public ToggleWidget SearchMarker { get; private set; }
        //public NumberInputFieldWidget MarkerSize { get; private set; }
        public ToggleWidget MarkerHorizontal { get; private set; }
        public SliderWidget MarkerAngle { get; private set; }
        public InputFieldWidget MarkerFrame { get; private set; }
        public Vector3SliderWidget MarkerOffset { get; private set; }

        public ToggleWidget PublishHead { get; private set; }
        public ToggleWidget PublishPlanes { get; private set; }

        public SenderWidget HeadSender { get; private set; }
        public SenderWidget MarkersSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("AR");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Frame = p.AddFrame();
            WorldScale = p.AddSlider("World Scale").SetMinValue(0.01f).SetMaxValue(1f);
            Origin = p.AddVector3Slider("Move World By").SetRange(1.5f);
            SearchMarker = p.AddToggle("Enable Marker Detection");
            MarkerHorizontal = p.AddToggle("Is Marker Horizontal");
            MarkerAngle = p.AddSlider("Marker Angle").SetMinValue(0).SetMaxValue(7*45).SetNumberOfSteps(7);
            MarkerFrame = p.AddInputField("Marker Follows TF Frame").SetPlaceholder("(none)");
            MarkerOffset = p.AddVector3Slider("Offset From TF Frame");
            //MarkerSize = p.AddNumberInputField("Size (m)");
            PublishHead = p.AddToggle("Publish Camera as PoseStamped");
            PublishPlanes = p.AddToggle("Publish Planes as Markers");
            HeadSender = p.AddSender();
            MarkersSender = p.AddSender();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}