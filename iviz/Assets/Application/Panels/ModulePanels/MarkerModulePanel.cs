﻿namespace Iviz.App
{
    /// <summary>
    /// <see cref="MarkerModuleData"/> 
    /// </summary>
    public sealed class MarkerModulePanel : ListenerModulePanel
    {
        public SimpleButtonWidget CloseButton { get; private set; }
        public SimpleButtonWidget ResetButton { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public ToggleWidget TriangleListFlipWinding { get; private set; }
        public ToggleWidget ShowDescriptions { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public SliderWidget Metallic { get; private set; }
        public SliderWidget Smoothness { get; private set; }
        public DropdownWidget Mask { get; private set; }
        public MarkerWidget Marker { get; private set; }
        CollapsibleWidget Visuals { get; set; }

        protected override void Initialize()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Markers");
            Listener = p.AddListener();
            //PreferUdp = p.AddToggle("Prefer Unreliable/UDP");

            ShowDescriptions = p.AddToggle("Show Descriptions");
            Mask = p.AddDropdown("Visible Mask");
            
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");
            TriangleListFlipWinding = p.AddToggle("Clockwise Winding in Triangle Lists");
            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Metallic = p.AddSlider("Metallic").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Smoothness = p.AddSlider("Smoothness").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);

            Visuals = p.AddCollapsibleWidget("Visuals")
                .Attach(TriangleListFlipWinding)
                .Attach(OcclusionOnlyMode)
                .Attach(Tint)
                .Attach(Alpha)
                .Attach(Metallic)
                .Attach(Smoothness)
                .FinishAttaching();
            
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ResetButton = p.AddResetButton();

            Marker = p.AddMarker();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}