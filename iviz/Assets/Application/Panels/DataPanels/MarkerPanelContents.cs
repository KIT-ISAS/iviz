namespace Iviz.App
{
    /// <summary>
    /// <see cref="MarkerModuleData"/> 
    /// </summary>
    public sealed class MarkerPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public ToggleWidget PreferUdp { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public ToggleWidget TriangleListFlipWinding { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }
        public DropdownWidget Mask { get; private set; }
        public MarkerWidget Marker { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Markers");
            Listener = p.AddListener();
            PreferUdp = p.AddToggle("Prefer UDP");

            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            TriangleListFlipWinding = p.AddToggle("Flip Winding in Triangle Lists");
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            Mask = p.AddDropdown("Visible Mask");
            Marker = p.AddMarker();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}