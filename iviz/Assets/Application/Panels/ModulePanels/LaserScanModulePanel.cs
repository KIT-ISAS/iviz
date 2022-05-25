using Iviz.Common;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="LaserScanModuleData"/> 
    /// </summary>

    public sealed class LaserScanModulePanel : ListenerModulePanel
    {
        public FrameWidget Frame { get; private set; }

        public ToggleWidget UseIntensity { get; private set; }
        public DataLabelWidget NumPoints { get; private set; }
        public SliderWidgetWithScale PointSize { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public SimpleButtonWidget CloseButton { get; private set; }
        public ToggleWidget ForceMinMax { get; private set; }
        public NumberInputFieldWidget MinIntensity { get; private set; }
        public NumberInputFieldWidget MaxIntensity { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }
        public ToggleWidget UseLines { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("LaserScan");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            NumPoints = p.AddDataLabel("Number of Points").SetHasRichText(true).SetCentered();
            PointSize = p.AddSliderWidgetWithScale("Point Size").EnableNegative(false);
            UseIntensity = p.AddToggle("Use Intensity instead of Range");
            UseLines = p.AddToggle("Use Lines");

            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)ColormapId.hsv);
            ForceMinMax = p.AddToggle("Colormap Override Min/Max");
            MinIntensity = p.AddNumberInputField("Colormap Min");
            MaxIntensity = p.AddNumberInputField("Colormap Max");
            FlipMinMax = p.AddToggle("Flip Min/Max");

            p.AddCollapsibleWidget("Colormap")
                .Attach(Colormap)
                .Attach(ForceMinMax)
                .Attach(MinIntensity)
                .Attach(MaxIntensity)
                .Attach(FlipMinMax)
                .FinishAttaching();
            
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}