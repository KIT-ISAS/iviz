using System.Collections.Generic;
using Iviz.Resources;
using UnityEngine.UI;

namespace Iviz.App
{
    public class PointCloudPanelContents : ListenerPanelContents
    {
        static readonly List<string> DefaultChannels = new List<string> { "x", "y", "z" };
        public DataLabelWidget NumPoints { get; private set; }
        public DataLabelWidget MinMax { get; private set; }
        public SliderWidget PointSize { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public DropdownWidget IntensityChannel { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public ToggleWidget ForceMinMax { get; private set; }
        public NumberInputFieldWidget MinIntensity { get; private set; }
        public NumberInputFieldWidget MaxIntensity { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }


        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("PointCloud");
            Listener = p.AddListener();
            NumPoints = p.AddDataLabel("Number of Points");
            MinMax = p.AddDataLabel("Min/Max");
            PointSize = p.AddSlider("Point Size").SetMinValue(0.01f).SetMaxValue(0.1f);
            //p.AddToggle("Calculate Min/Max");
            IntensityChannel = p.AddDropdown("Intensity Channel")
                        .SetOptions(DefaultChannels);
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)Resource.ColormapId.hsv);

            ForceMinMax = p.AddToggle("Force Min/Max");
            MinIntensity = p.AddNumberInputField("Min");
            MaxIntensity = p.AddNumberInputField("Max");
            FlipMinMax = p.AddToggle("Flip Min/Max");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            //p.AddToggle("Override Color");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}