using System.Collections.Generic;
using Iviz.Common;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="PointCloudModuleData"/> 
    /// </summary>
    public sealed class PointCloudModulePanel : ListenerModulePanel
    {
        static readonly List<string> DefaultChannels = new List<string> {"x", "y", "z"};
        public FrameWidget Frame { get; private set; }
        public DataLabelWidget NumPoints { get; private set; }
        public SliderWidgetWithScale PointSize { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public InputFieldWithHintsWidget IntensityChannel { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }

        public ToggleWidget ForceMinMax { get; private set; }
        public SliderWidgetWithScale MinIntensity { get; private set; }
        public SliderWidgetWithScale MaxIntensity { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }
        public DropdownWidget PointCloudType { get; private set; }


        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("PointCloud");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            NumPoints = p.AddDataLabel("Number of Points").SetHasRichText(true).SetCentered();
            PointSize = p.AddSliderWidgetWithScale("Point Size").EnableNegative(false);
            IntensityChannel = p.AddInputFieldWithHints("Intensity Channel")
                .SetOptions(DefaultChannels);

            PointCloudType = p.AddDropdown("Show as").SetOptions(new[] {"Points", "Cubes", "Spheres"});

            Colormap = p.AddDropdown("Colormap")
                .SetOptions(Resource.Colormaps.Names)
                .SetIndex((int) ColormapId.hsv);
            ForceMinMax = p.AddToggle("Colormap Override Min/Max");
            MinIntensity = p.AddSliderWidgetWithScale("Colormap Min");
            MaxIntensity = p.AddSliderWidgetWithScale("Colormap Max");
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
            //p.AddToggle("Override Color");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}