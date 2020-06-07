
namespace Iviz.App
{
    public class ARPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public Vector3Widget Origin { get; private set; }
        public NumberInputFieldWidget WorldScale { get; private set; }
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
            Origin = p.AddVector3("Origin");
            WorldScale = p.AddNumberInputField("World Scale");
            SearchMarker = p.AddToggle("Use Origin Marker");
            MarkerSize = p.AddNumberInputField("Origin Marker Size");
            MarkerHorizontal = p.AddToggle("Is Marker Horizontal");
            MarkerAngle = p.AddSlider("Marker Angle").SetMinValue(0).SetMaxValue(7).SetIntegerOnly(true);
            MarkerFrame = p.AddInputField("Marker Frame");
            MarkerOffset = p.AddVector3("Marker Offset");
            HeadSender = p.AddSender();
            MarkersSender = p.AddSender();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}