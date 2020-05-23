using Iviz.Resources;

namespace Iviz.App
{
    public class LaserScanPanelContents : ListenerPanelContents
    {
        public DataLabelWidget Topic { get; private set; }
        public ToggleWidget UseIntensity { get; private set; }
        public SliderWidget PointSize { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("LaserScan");
            Stats = p.AddSectionTitleWidget("Off | 0 Hz | 0 - 0 ms");
            Topic = p.AddDataLabel("");
            PointSize = p.AddSlider("Point Size").SetMinValue(0.01f).SetMaxValue(0.1f);
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)Resource.ColormapId.hsv);
            UseIntensity = p.AddToggle("Use Intensity If Available");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);

            Topic.label.alignment = UnityEngine.TextAnchor.UpperLeft;
            Topic.label.fontStyle = UnityEngine.FontStyle.Italic;

            Widgets = new Widget[] { UseIntensity, PointSize, Colormap, CloseButton, HideButton };
        }
    }
}