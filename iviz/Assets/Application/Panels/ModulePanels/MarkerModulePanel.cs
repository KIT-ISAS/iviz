namespace Iviz.App
{
    /// <summary>
    /// <see cref="MarkerModuleData"/> 
    /// </summary>
    public sealed class MarkerModulePanel : ListenerModulePanel
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public TrashButtonWidget ResetButton { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public ToggleWidget TriangleListFlipWinding { get; private set; }
        public ToggleWidget ShowDescriptions { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public SliderWidget Metallic { get; private set; }
        public SliderWidget Smoothness { get; private set; }
        public DropdownWidget Mask { get; private set; }
        public MarkerWidget Marker { get; private set; }
        CollapsibleWidget Material { get; set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Markers");
            Listener = p.AddListener();
            //PreferUdp = p.AddToggle("Prefer UDP");

            TriangleListFlipWinding = p.AddToggle("Clockwise Winding in Triangle Lists");
            ShowDescriptions = p.AddToggle("Show Descriptions");
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");
            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Metallic = p.AddSlider("Metallic").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Smoothness = p.AddSlider("Smoothness").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);

            Material = p.AddCollapsibleWidget("Visuals")
                .Attach(OcclusionOnlyMode)
                .Attach(Tint)
                .Attach(Alpha)
                .Attach(Metallic)
                .Attach(Smoothness)
                .FinishAttaching();
            
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ResetButton = p.AddResetButton();

            Mask = p.AddDropdown("Visible Mask");
            Marker = p.AddMarker();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}