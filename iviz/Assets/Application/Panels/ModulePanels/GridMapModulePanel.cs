using Iviz.Common;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridMapModuleData"/> 
    /// </summary>

    public sealed class GridMapModulePanel : ListenerModulePanel
    {
        public FrameWidget Frame { get; private set; }
        public DataLabelWidget Description { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public InputFieldWithHintsWidget IntensityChannel { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }

        public ToggleWidget ForceMinMax { get; private set; }
        public NumberInputFieldWidget MinIntensity { get; private set; }
        public NumberInputFieldWidget MaxIntensity { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public SliderWidget Smoothness { get; private set; }
        public SliderWidget Metallic { get; private set; }
        CollapsibleWidget Material { get; set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("GridMap");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            Description = p.AddDataLabel("Min/Max").SetHasRichText(true).SetCentered();
            IntensityChannel = p.AddInputFieldWithHints("Intensity Channel");
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)ColormapId.hsv);

            ForceMinMax = p.AddToggle("Colormap Force Min/Max");
            MinIntensity = p.AddNumberInputField("Colormap Min");
            MaxIntensity = p.AddNumberInputField("Colormap Max");
            FlipMinMax = p.AddToggle("Flip Min/Max");

            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Metallic = p.AddSlider("Metallic").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Smoothness = p.AddSlider("Smoothness").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);

            Material = p.AddCollapsibleWidget("Visuals")
                .Attach(Tint)
                .Attach(Alpha)
                .Attach(Metallic)
                .Attach(Smoothness)
                .FinishAttaching();            
            
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}