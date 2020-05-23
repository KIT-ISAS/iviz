using System.Collections.Generic;
using Iviz.Resources;
using UnityEngine.UI;

namespace Iviz.App
{
    public class PointCloudPanelContents : ListenerPanelContents
    {
        static readonly List<string> DefaultChannels = new List<string> { "x", "y", "z" };
        public DataLabelWidget Topic { get; private set; }
        public SliderWidget PointSize { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public DropdownWidget IntensityChannel { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public ToggleWidget ForceMinMax { get; private set; }
        public InputFieldWidget MinIntensity { get; private set; }
        public InputFieldWidget MaxIntensity { get; private set; }


        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("PointCloud");
            Stats = p.AddSectionTitleWidget("Off | 0 Hz | 0 - 0 ms");
            Topic = p.AddDataLabel("");
            PointSize = p.AddSlider("Point Size").SetMinValue(0.01f).SetMaxValue(0.1f);
            //p.AddToggle("Calculate Min/Max");
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)Resource.ColormapId.hsv);
            IntensityChannel = p.AddDropdown("Intensity Channel")
                        .SetOptions(DefaultChannels);

            ForceMinMax = p.AddToggle("Force Min/Max");
            MinIntensity = p.AddShortInputField("Min").SetContentType(InputField.ContentType.DecimalNumber);
            MaxIntensity = p.AddShortInputField("Max").SetContentType(InputField.ContentType.DecimalNumber);

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            //p.AddToggle("Override Color");
            p.UpdateSize();
            gameObject.SetActive(false);

            Topic.label.alignment = UnityEngine.TextAnchor.UpperLeft;
            Topic.label.fontStyle = UnityEngine.FontStyle.Italic;

            Widgets = new Widget[] {
                PointSize, Colormap, IntensityChannel, CloseButton,
                HideButton, ForceMinMax, MinIntensity, MaxIntensity };
        }
    }
}