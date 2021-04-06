using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="LaserScanModuleData"/> 
    /// </summary>

    public sealed class LaserScanPanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }

        public ToggleWidget UseIntensity { get; private set; }
        public DataLabelWidget NumPoints { get; private set; }
        public SliderWidget PointSize { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

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
            NumPoints = p.AddDataLabel("Number of Points").SetHasRichText(true).SetAlignment(TextAnchor.MiddleCenter);
            PointSize = p.AddSlider("Point Size").SetMinValue(0.01f).SetMaxValue(0.1f);
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)Resource.ColormapId.hsv);
            UseIntensity = p.AddToggle("Use Intensity instead of Range");

            ForceMinMax = p.AddToggle("Colormap Override Min/Max");
            MinIntensity = p.AddNumberInputField("Colormap Min");
            MaxIntensity = p.AddNumberInputField("Colormap Max");
            FlipMinMax = p.AddToggle("Flip Min/Max");

            UseLines = p.AddToggle("Use Lines");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}