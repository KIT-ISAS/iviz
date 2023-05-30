namespace Iviz.App
{
    /// <summary>
    /// <see cref="InteractiveMarkerModuleData"/> 
    /// </summary>    
    public sealed class InteractiveMarkerModulePanel : ListenerModulePanel
    {
        public ListenerWidget FullListener { get; private set; }
        public SimpleButtonWidget CloseButton { get; private set; }
        public ToggleWidget DescriptionsVisible { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public ToggleWidget TriangleListFlipWinding { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public SliderWidget Metallic { get; private set; }
        public SliderWidget Smoothness { get; private set; }
        public SenderWidget Sender { get; private set; }
        public MarkerWidget Marker { get; private set; }
        CollapsibleWidget Visuals { get; set; }

        protected override void Initialize()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Interactive\nMarker");
            Listener = p.AddListener();
            FullListener = p.AddListener();
            DescriptionsVisible = p.AddToggle("Show Descriptions");
            TriangleListFlipWinding = p.AddToggle("Clockwise Winding in Triangle Lists");
            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Metallic = p.AddSlider("Metallic").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Smoothness = p.AddSlider("Smoothness").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            
            Visuals = p.AddCollapsibleWidget("Visuals")
                .Attach(TriangleListFlipWinding)
                .Attach(Tint)
                .Attach(Alpha)
                .Attach(Metallic)
                .Attach(Smoothness)
                .FinishAttaching();

            Sender = p.AddSender();
            Marker = p.AddMarker();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}