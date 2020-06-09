
namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARDisplayData"/> 
    /// </summary>
    public class ARPanelContents : DataPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public Vector3Widget Origin { get; private set; }
        public SliderWidget WorldScale { get; private set; }
        public ToggleWidget SearchMarker { get; private set; }
        public NumberInputFieldWidget MarkerSize { get; private set; }
        public ToggleWidget MarkerHorizontal { get; private set; }
        public SliderWidget MarkerAngle { get; private set; }
        public InputFieldWidget MarkerFrame { get; private set; }
        public Vector3Widget MarkerOffset { get; private set; }

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
            SearchMarker = p.AddToggle("Use Origin Marker");
            Origin = p.AddVector3("Session Start Position");
            MarkerHorizontal = p.AddToggle("Is Marker Horizontal");
            MarkerAngle = p.AddSlider("Marker Angle").SetMinValue(0).SetMaxValue(7*45).SetNumberOfSteps(7);
            MarkerFrame = p.AddInputField("Marker Follows TF Frame").SetPlaceholder("(none)");
            MarkerOffset = p.AddVector3("Offset From TF Frame");
            MarkerSize = p.AddNumberInputField("Size (m)");
            HeadSender = p.AddSender();
            MarkersSender = p.AddSender();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}