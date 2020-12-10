namespace Iviz.App
{
    /// <summary>
    /// <see cref="MarkerModuleData"/> 
    /// </summary>
    public sealed class MarkerPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public ToggleWidget TriangleListFlipWinding { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }
        public MarkerWidget Marker { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Markers");
            Listener = p.AddListener();

            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            TriangleListFlipWinding = p.AddToggle("Flip Winding in Triangle Lists");
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            Marker = p.AddMarker();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}